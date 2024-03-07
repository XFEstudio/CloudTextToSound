using NAudio.Wave;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using XFEExtension.NetCore.ImplExtension;

namespace CloudTextToSound.NetCore
{
    /// <summary>
    /// TTS对话类
    /// </summary>
    [CreateImpl]
    public abstract class TTSDialog
    {
        private protected int maxTime = 600;
        private protected int deviceNumber = 0;
        private protected int appId;
        private protected string uUID = Guid.NewGuid().ToString();
        private protected string secretId;
        private protected string secretKey;
        private protected BufferedWaveProvider bufferedWaveProvider;
        private protected WaveOutEvent waveOut;
        private protected  CodecType codecType;
        private protected VoiceTimbreType voiceTimbreType;
        private protected const string ParamAction = "TextToStreamAudioWS";
        /// <summary>
        /// TTS的文字
        /// </summary>
        public string Text { get; set; } = string.Empty;
        /// <summary>
        /// 说话速度
        /// </summary>
        public float Speed { get; set; } = 0;
        /// <summary>
        /// 音量
        /// </summary>
        public float Volume { get; set; } = 0;
        /// <summary>
        /// TTS接收到文本以及音频数据的事件
        /// </summary>
        public event EventHandler<TTSDialogEventArgs> OnTTSDialogReceived;
        /// <summary>
        /// 音频播放完成事件
        /// </summary>
        public event EventHandler AudioPlayFinished;
        /// <summary>
        /// 音频接收完成事件
        /// </summary>
        public event EventHandler AudioReceiveCompleted;
        /// <summary>
        /// 开始TTS
        /// </summary>
        /// <param name="tTSText">待转音频的文字</param>
        /// <param name="useInnerAudio">是否使用本dll内置的音频</param>
        /// <param name="autoPlayAudio">是否自动播放音频</param>
        /// <returns></returns>
        public async Task StartTTS(string tTSText = "", bool useInnerAudio = true, bool autoPlayAudio = true)
        {
            if (tTSText == string.Empty)
            {
                tTSText = Text;
            }
            TimeSpan tp = DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0);//设置当前的UNIX时间戳
            string originParamString = $"Action={ParamAction}&AppId={appId}&Codec={codecType}&EnableSubtitle=True&Expired={Convert.ToInt64(tp.TotalSeconds) + maxTime}&ModelType=1&SampleRate=16000&SecretId={secretId}&SessionId={uUID}&Speed={Speed}&Text={tTSText}&Timestamp={Convert.ToInt64(tp.TotalSeconds)}&VoiceType={voiceTimbreType.GetID()}&Volume={Volume}";
            string originSignatureString = "GETtts.cloud.tencent.com/stream_ws?" + originParamString;
            string signatureString = HttpUtility.UrlEncode(AllMethod.GetEncodeSignature(secretKey, originSignatureString));
            string finalParamString = originParamString.Replace(tTSText, HttpUtility.UrlEncode(tTSText)) + $"&Signature={signatureString}";
            using var client = new ClientWebSocket();
            await client.ConnectAsync(new Uri($"wss://tts.cloud.tencent.com/stream_ws?{finalParamString}"), CancellationToken.None);
            if (client.State == WebSocketState.Open)
            {
                var waveFormat = new WaveFormat(16000, 16, 1); // 16k PCM, 16 bits per sample, mono
                waveOut = new WaveOutEvent();
                bufferedWaveProvider = new BufferedWaveProvider(waveFormat)
                {
                    DiscardOnBufferOverflow = true
                };
                waveOut.DeviceNumber = deviceNumber;
                waveOut.Init(bufferedWaveProvider);
                if (autoPlayAudio)
                {
                    waveOut.Play();
                }

                while (client.State == WebSocketState.Open)
                {
                    var buffer = new byte[1024];
                    WebSocketReceiveResult result = await client.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                    if (result.MessageType == WebSocketMessageType.Binary)
                    {
                        // 此处为16k的pcm音频数据
                        OnTTSDialogReceived?.Invoke(this, new TTSDialogEventArgs(null, buffer));
                        if (useInnerAudio)
                        {
                            var bufferList = new List<byte>();
                            bufferList.AddRange(buffer.Take(result.Count));
                            while (!result.EndOfMessage)
                            {
                                result = await client.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                                bufferList.AddRange(buffer.Take(result.Count));
                            }
                            var finalResult = bufferList.ToArray();
                            bufferedWaveProvider.BufferLength += finalResult.Length * finalResult.Length;
                            bufferedWaveProvider.AddSamples(finalResult, 0, finalResult.Length);
                        }
                    }
                    else if (result.MessageType == WebSocketMessageType.Text)
                    {
                        // 此处为json格式的字幕数据
                        var subtitle = Encoding.UTF8.GetString(buffer, 0, result.Count);
                        // 循环读取直至消息结束
                        while (!result.EndOfMessage)
                        {
                            result = await client.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                            subtitle += Encoding.UTF8.GetString(buffer, 0, result.Count);
                        }
                        try
                        {
                            var backMessage = JsonConvert.DeserializeObject<TTSBackMessage>(subtitle);
                            OnTTSDialogReceived?.Invoke(this, new TTSDialogEventArgs(backMessage, null));
                            if (backMessage.Final == 1)
                            {
                                await client.CloseAsync(WebSocketCloseStatus.NormalClosure, "Close", CancellationToken.None);
                                AudioReceiveCompleted?.Invoke(this, new EventArgs());
                                while (bufferedWaveProvider.BufferedDuration != TimeSpan.Zero) { }
                                AudioPlayFinished?.Invoke(this, new EventArgs());
                                waveOut.Stop();
                                waveOut.Dispose();
                                OnTTSDialogReceived = null;
                                AudioPlayFinished = null;
                                AudioReceiveCompleted = null;
                                break;
                            }
                        }
                        catch (JsonException ex)
                        {
                            Console.WriteLine(subtitle);
                            OnTTSDialogReceived?.Invoke(this, new TTSDialogEventArgs(null, null, ex));
                            AudioReceiveCompleted?.Invoke(this, new EventArgs());
                            await client.CloseAsync(WebSocketCloseStatus.NormalClosure, "Close", CancellationToken.None);
                            while (bufferedWaveProvider.BufferedDuration != TimeSpan.Zero) { }
                            AudioPlayFinished?.Invoke(this, new EventArgs());
                            waveOut.Stop();
                            waveOut.Dispose();
                            OnTTSDialogReceived = null;
                            AudioPlayFinished = null;
                            AudioReceiveCompleted = null;
                            break;
                        }
                    }
                }

            }
            else
            {
                Console.WriteLine("握手失败！状态为：" + client.State);
            }
        }
        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            OnTTSDialogReceived = null;
            AudioPlayFinished = null;
            AudioReceiveCompleted = null;
            waveOut.Dispose();
            bufferedWaveProvider.ClearBuffer();
        }
        /// <summary>
        /// 播放音频
        /// </summary>
        public void Play()
        {
            while (waveOut == null) { }
            while (true)
            {
                try
                {
                    waveOut.Play();
                    break;
                }
                catch (Exception) { }
            }
        }
        /// <summary>
        /// 立即播放音频
        /// </summary>
        public void InstantPlay()
        {
            waveOut.Play();
        }
        /// <summary>
        /// 暂停音频
        /// </summary>
        public void Pause()
        {
            waveOut.Pause();
        }
        /// <summary>
        /// 停止音频
        /// </summary>
        public void Stop()
        {
            waveOut.Stop();
        }
        internal TTSDialog(int appId, string secretId, string secretKey, int maxTime, int deviceNumber, CodecType codecType, VoiceTimbreType voiceTimbreType)
        {
            this.appId = appId;
            this.secretId = secretId;
            this.secretKey = secretKey;
            this.maxTime = maxTime;
            this.deviceNumber = deviceNumber;
            this.codecType = codecType;
            this.voiceTimbreType = voiceTimbreType;
        }
    }
}
