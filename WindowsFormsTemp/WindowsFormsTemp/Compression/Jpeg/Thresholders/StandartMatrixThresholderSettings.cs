using System;

namespace WindowsFormsTemp.Compression.Jpeg.Thresholders
{
    [Serializable]
    public class StandartMatrixThresholderSettings : IJpegThresholderSettings
    {
        public StandartMatrixType StandartMatrixType { get; set; }
        public short Divisor { get; set; }
    }

    public enum StandartMatrixType
    {
        Y,
        CrCb
    }
}