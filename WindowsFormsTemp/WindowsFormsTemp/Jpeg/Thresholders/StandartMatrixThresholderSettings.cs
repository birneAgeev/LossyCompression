using System;

namespace WindowsFormsTemp.Jpeg.Thresholders
{
    [Serializable]
    public class StandartMatrixThresholderSettings : IJpegThresholderSettings
    {
         public StandartMatrixType StandartMatrixType { get; set; }
    }

    public enum StandartMatrixType
    {
        Y,
        CrCb
    }
}