using System;

namespace WindowsFormsTemp.Compression.Jpeg.Thresholders
{
    [Serializable]
    public class MaxValuesThresholderSettings : IJpegThresholderSettings
    {
        public int MaxCount { get; set; }
    }
}