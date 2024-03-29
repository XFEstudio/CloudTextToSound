﻿using Newtonsoft.Json;

namespace CloudTextToSound
{
    /// <summary>
    /// TTS结果
    /// </summary>
    public class TTSResult
    {
        /// <summary>
        /// TTS结构体
        /// </summary>
        public struct TTSResultStruct
        {
            /// <summary>
            /// 该字的内容
            /// </summary>
            public string Text { get; set; }
            /// <summary>
            /// 该字在整个音频流中的起始时间
            /// </summary>
            public int BeginTime { get; set; }
            /// <summary>
            /// 该字在整个音频流中的结束时间
            /// </summary>
            public int EndTime { get; set; }
            /// <summary>
            /// 该字在整个文本中的开始位置，从0开始
            /// </summary>
            public int BeginIndex { get; set; }
            /// <summary>
            /// 该字在整个文本中的结束位置，从0开始
            /// </summary>
            public int EndIndex { get; set; }
            /// <summary>
            /// 该字的音素（注意：此字段可能返回 null，表示取不到有效值）
            /// </summary>
            public string Phoneme { get; set; }
        }
        /// <summary>
        /// TTS结构
        /// </summary>
        public TTSResultStruct[] Subtitles { get; private set; }
        /// <summary>
        /// TTS结果
        /// </summary>
        /// <param name="subtitles"></param>
        [JsonConstructor]
        public TTSResult(TTSResultStruct[] subtitles)
        {
            this.Subtitles = subtitles;
        }
    }
}
