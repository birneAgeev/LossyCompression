using System;

namespace WindowsFormsTemp.Compression.Jpeg.Thresholders
{
    [Serializable]
    public class CustomMatrixThresholderSettings : IJpegThresholderSettings
    {
        public short Alpha { get; set; }
        public short Gamma { get; set; }
    }
}