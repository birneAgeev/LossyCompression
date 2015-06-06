using System;

namespace WindowsFormsTemp.Compression.Jpeg.Thresholders
{
    [Serializable]
    public class GeneralizedThresholderSettings : IJpegThresholderSettings
    {
        public ThresholderType ThresholderType { get; set; }
        public MaxValuesThresholderSettings MaxValuesThresholderSettings { get; set; }
        public CustomMatrixThresholderSettings CustomMatrixThresholderSettings { get; set; }
        public StandartMatrixThresholderSettings StandartMatrixThresholderSettings { get; set; }
    }

    public enum ThresholderType
    {
        MaxValuesThresholder,
        CustomMatrixThresholder,
        StandartMatrixThresholder,
        EmptyThresholder
    }
}