using System;

namespace WindowsFormsTemp.Jpeg.Thresholders
{
    [Serializable]
    public class MaxValuesThresholderSettings : IJpegThresholderSettings
    {
        public int MaxCount { get; set; }
    }
}