using Newtonsoft.Json;

namespace CloudTextToSound
{
    /// <summary>
    /// TTS返回消息
    /// </summary>
    public class TTSBackMessage
    {
        /// <summary>
        /// 返回值，0为成功
        /// </summary>
        public int Code { get; private set; }
        /// <summary>
        /// 返回消息，success为成功
        /// </summary>
        public string Message { get; private set; }
        /// <summary>
        /// 该次请求的session_id
        /// </summary>
        public string SessionId { get; private set; }
        /// <summary>
        /// 该次请求的request_id
        /// </summary>
        public string RequestId { get; private set; }
        /// <summary>
        /// 该次请求的message_id
        /// </summary>
        public string MessageId { get; private set; }
        /// <summary>
        /// 请求的返回结果
        /// </summary>
        public TTSResult Result { get; private set; }
        /// <summary>
        /// 是否为最后一条消息，1为最后一条
        /// </summary>
        public int Final { get; private set; }
        /// <summary>
        /// TTS返回消息
        /// </summary>
        /// <param name="code"></param>
        /// <param name="message"></param>
        /// <param name="sessionId"></param>
        /// <param name="requestId"></param>
        /// <param name="messageId"></param>
        /// <param name="result"></param>
        /// <param name="final"></param>
        [JsonConstructor]
        public TTSBackMessage(int code, string message, string sessionId, string requestId, string messageId, TTSResult result, int final)
        {
            this.Code = code;
            this.Message = message;
            this.SessionId = sessionId;
            this.RequestId = requestId;
            this.MessageId = messageId;
            this.Result = result;
            this.Final = final;
        }
    }
}
