using System;

namespace CloudTextToSound
{
    /// <summary>
    /// TTS对话事件参数
    /// </summary>
    public class TTSDialogEventArgs : EventArgs
    {
        /// <summary>
        /// TTS返回消息
        /// </summary>
        public TTSBackMessage BackMessage { get; private set; }
        /// <summary>
        /// 是否有返回消息
        /// </summary>
        public bool HasBackMessage { get { return BackMessage != null; } }
        /// <summary>
        /// 是否有音频数据 
        /// </summary>
        public bool HasBuffer { get { return Buffer != null; } }
        /// <summary>
        /// TTS返回音频数据
        /// </summary>
        public byte[] Buffer { get; private set; }
        /// <summary>
        /// 异常
        /// </summary>
        public Exception Exception { get; private set; }
        internal TTSDialogEventArgs(TTSBackMessage backMessage, byte[] buffer)
        {
            this.BackMessage = backMessage;
            this.Buffer = buffer;
        }
        internal TTSDialogEventArgs(TTSBackMessage backMessage, byte[] buffer, Exception exception)
        {
            this.BackMessage = backMessage;
            this.Buffer = buffer;
            this.Exception = exception;
        }
    }
}
