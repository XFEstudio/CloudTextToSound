using Newtonsoft.Json;
using XFEExtension.NetCore.ImplExtension;

namespace CloudTextToSound.NetCore
{
    /// <summary>
    /// TTS返回消息
    /// </summary>
    /// <remarks>
    /// TTS返回消息
    /// </remarks>
    /// <param name="code"></param>
    /// <param name="message"></param>
    /// <param name="sessionId"></param>
    /// <param name="requestId"></param>
    /// <param name="messageId"></param>
    /// <param name="result"></param>
    /// <param name="final"></param>
    [CreateImpl]
    [method: JsonConstructor]
    public class TTSBackMessage(int code, string message, string sessionId, string requestId, string messageId, TTSResult result, int final)
    {
        /// <summary>
        /// 返回值，0为成功
        /// </summary>
        public int Code { get; private set; } = code;
        /// <summary>
        /// 返回消息，success为成功
        /// </summary>
        public string Message { get; private set; } = message;
        /// <summary>
        /// 该次请求的session_id
        /// </summary>
        public string SessionId { get; private set; } = sessionId;
        /// <summary>
        /// 该次请求的request_id
        /// </summary>
        public string RequestId { get; private set; } = requestId;
        /// <summary>
        /// 该次请求的message_id
        /// </summary>
        public string MessageId { get; private set; } = messageId;
        /// <summary>
        /// 请求的返回结果
        /// </summary>
        public TTSResult Result { get; private set; } = result;
        /// <summary>
        /// 是否为最后一条消息，1为最后一条
        /// </summary>
        public int Final { get; private set; } = final;
    }
}
