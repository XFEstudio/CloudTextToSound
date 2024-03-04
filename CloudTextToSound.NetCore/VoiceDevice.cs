namespace CloudTextToSound
{
    /// <summary>
    /// 音频设备类
    /// </summary>
    /// <remarks>
    /// 创建一个音频设备类
    /// </remarks>
    /// <param name="DeviceIndex">音频设备的索引</param>
    /// <param name="DeviceName">音频设备的名称和描述</param>
    public class VoiceDevice(int DeviceIndex, string DeviceName)
    {
        /// <summary>
        /// 获取字符串名称
        /// </summary>
        /// <returns>字符串名称</returns>
        public override string ToString()
        {
            return GetIndexAndName();
        }
        /// <summary>
        /// 当前设备的索引
        /// </summary>
        public int DeviceIndex { get; private protected set; } = DeviceIndex;
        /// <summary>
        /// 当前设备的名称
        /// </summary>
        public string DeviceName { get; private protected set; } = DeviceName;

        /// <summary>
        /// 获取用于展示的索引+名称描述文本
        /// </summary>
        /// <returns>索引和名称的string</returns>
        public string GetIndexAndName()
        {
            return $"设备编号：[{DeviceIndex}]\n设备名称：{DeviceName}";
        }
    }
}
