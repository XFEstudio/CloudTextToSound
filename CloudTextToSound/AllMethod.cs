using System;
using System.Security.Cryptography;
using System.Text;

namespace CloudTextToSound
{
    /// <summary>
    /// 一些方法
    /// </summary>
    public static class AllMethod
    {
        /// <summary>
        /// 获取加密签名
        /// </summary>
        /// <param name="sKey"></param>
        /// <param name="sSrc"></param>
        /// <returns></returns>
        public static string GetEncodeSignature(string sKey, string sSrc)
        {
            HMACSHA1 hMACSHA1 = new HMACSHA1
            {
                Key = Encoding.UTF8.GetBytes(sKey)
            };
            string Sign = Convert.ToBase64String(hMACSHA1.ComputeHash(Encoding.UTF8.GetBytes(sSrc)));
            return Sign;
        }
    }
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
