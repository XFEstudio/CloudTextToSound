using System;

namespace CloudTextToSound
{
    /// <summary>
    /// 云端文本转语音异常
    /// </summary>
    public class CloudTextToSoundException : Exception
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
