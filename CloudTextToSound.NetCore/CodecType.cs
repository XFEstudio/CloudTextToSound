namespace CloudTextToSound.NetCore
{
    /// <summary>
    /// 语音返回的音频格式
    /// </summary>
    public enum CodecType
    {
        /// <summary>
        /// 返回多段含 opus 压缩分片音频（默认）
        /// </summary>
        opus,
        /// <summary>
        /// 返回二进制 pcm 音频
        /// </summary>
        pcm,
        /// <summary>
        /// 返回二进制 mp3 音频
        /// </summary>
        mp3
    }
}
