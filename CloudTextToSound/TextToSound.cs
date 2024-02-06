﻿using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CloudTextToSound
{
    /// <summary>
    /// 语音合成类
    /// </summary>
    public class TextToSound
    {
        private int AppId;
        private bool PlayAudioList = false;
        private bool FirstAudioPlayed = false;
        private string SecretId;
        private string SecretKey;
        private readonly List<TTSDialog> dialogs = new List<TTSDialog>();
        /// <summary>
        /// 播放音频列表结束事件
        /// </summary>
        public event EventHandler AudioListPlayEnd;
        /// <summary>
        /// 初始化实时转写
        /// </summary>
        /// <param name="AppId">应用的APPID</param>
        /// <param name="SecretId">应用的SecretID</param>
        /// <param name="SecretKey">应用的SecertKey</param>
        /// <returns></returns>
        public void InitializTTS(int AppId, string SecretId, string SecretKey)
        {
            this.AppId = AppId;
            this.SecretId = SecretId;
            this.SecretKey = SecretKey;
        }
        /// <summary>
        /// 获取当前的音频输入设备列表
        /// </summary>
        /// <returns>音频设备列表</returns>
        public static List<VoiceDevice> GetVoiceOutputDevice()
        {
            var inputDeviceCount = WaveOut.DeviceCount;
            var DeviceList = new List<VoiceDevice>();
            for (int i = 0; i < inputDeviceCount; i++)
            {
                var capabilities = WaveOut.GetCapabilities(i);
                DeviceList.Add(new VoiceDevice(i, capabilities.ProductName));
            }
            return DeviceList;
        }
        /// <summary>
        /// 创建一个实时TTS的实例
        /// </summary>
        /// <param name="voiceTimbreType">音色类型</param>
        /// <returns>TTS对话</returns>
        public TTSDialog CreateDialog(VoiceTimbreType voiceTimbreType)
        {
            return new TTSDialog(AppId, SecretId, SecretKey, 600, 0, CodecType.pcm, voiceTimbreType);
        }
        /// <summary>
        /// 创建一个实时TTS的实例
        /// </summary>
        /// <param name="voiceDevice">音频设备</param>
        /// <param name="voiceTimbreType">音色类型</param>
        /// <returns>TTS对话</returns>
        public TTSDialog CreateDialog(VoiceDevice voiceDevice, VoiceTimbreType voiceTimbreType)
        {
            return new TTSDialog(AppId, SecretId, SecretKey, 600, voiceDevice.DeviceIndex, CodecType.pcm, voiceTimbreType);
        }
        /// <summary>
        /// 创建一个实时TTS的实例
        /// </summary>
        /// <param name="maxTime">最大时间</param>
        /// <param name="voiceTimbreType">音色类型</param>
        /// <returns>TTS对话</returns>
        public TTSDialog CreateDialog(int maxTime, VoiceTimbreType voiceTimbreType)
        {
            return new TTSDialog(AppId, SecretId, SecretKey, maxTime, 0, CodecType.pcm, voiceTimbreType);
        }
        /// <summary>
        /// 创建一个实时TTS的实例
        /// </summary>
        /// <param name="maxTime">最大时间</param>
        /// <param name="voiceDevice">音频设备</param>
        /// <param name="codecType">编码类型</param>
        /// <param name="voiceTimbreType">音色类型</param>
        /// <returns></returns>
        public TTSDialog CreateDialog(int maxTime, VoiceDevice voiceDevice, CodecType codecType, VoiceTimbreType voiceTimbreType)
        {
            return new TTSDialog(AppId, SecretId, SecretKey, maxTime, voiceDevice.DeviceIndex, codecType, voiceTimbreType);
        }
        /// <summary>
        /// 播放音频列表
        /// </summary>
        public void StartPlayAudioList()
        {
            if (!PlayAudioList)
            {
                PlayAudioList = true;
                Task.Run(() =>
                {
                    while (PlayAudioList)
                    {
                        if (dialogs.Count > 0)
                        {
                            if (!FirstAudioPlayed)
                            {
                                FirstAudioPlayed = true;
                            }
                            dialogs[0].StartTTS().Wait();
                            if (dialogs.Count > 0)
                            {
                                dialogs.RemoveAt(0);
                            }
                        }
                        else if (FirstAudioPlayed)
                        {
                            AudioListPlayEnd?.Invoke(this, new EventArgs());
                            FirstAudioPlayed = false;
                        }
                    }
                });
            }
        }
        /// <summary>
        /// 添加音频到播放列表
        /// </summary>
        /// <param name="dialog">TTS对话</param>
        public void AddAudioToPlayList(TTSDialog dialog)
        {
            dialogs.Add(dialog);
        }
        /// <summary>
        /// 移除指定的音频
        /// </summary>
        /// <param name="dialog"></param>
        public void RemoveAudioFromPlayList(TTSDialog dialog)
        {
            dialogs.Remove(dialog);
        }
        /// <summary>
        /// 移除指定位置的音频
        /// </summary>
        /// <param name="index"></param>
        public void RemoveAudioFromPlayList(int index)
        {
            dialogs.RemoveAt(index);
        }
        /// <summary>
        /// 移除最后一个音频
        /// </summary>
        public void RemoveLastAudioFromPlayList()
        {
            dialogs.RemoveAt(dialogs.Count - 1);
        }
        /// <summary>
        /// 移除第一个音频
        /// </summary>
        public void RemoveFirstAudioFromPlayList()
        {
            dialogs.RemoveAt(0);
        }
        /// <summary>
        /// 清空音频播放列表
        /// </summary>
        public void ClearAudioPlayList()
        {
            dialogs.Clear();
        }
        /// <summary>
        /// 结束播放音频列表
        /// </summary>
        public void EndPlayAudioList()
        {
            PlayAudioList = false;
        }
    }
}
