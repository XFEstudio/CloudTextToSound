using System;
using XFE各类拓展.NetCore.ImplExtension;

namespace CloudTextToSound.NetCore
{
    /// <summary>
    /// 云端文本转语音异常
    /// </summary>
    [CreateImpl]
    public abstract class CloudTextToSoundException : Exception
    {
        /// <summary>
        /// 云端文本转语音异常
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public CloudTextToSoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
        /// <summary>
        /// 云端文本转语音异常
        /// </summary>
        /// <param name="message"></param>
        public CloudTextToSoundException(string message) : base(message)
        {
        }
    }
}
