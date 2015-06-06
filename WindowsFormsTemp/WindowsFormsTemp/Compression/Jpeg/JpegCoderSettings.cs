using System;
using WindowsFormsTemp.Compression.CompressionCommons;
using WindowsFormsTemp.Compression.Jpeg.Thresholders;

namespace WindowsFormsTemp.Compression.Jpeg
{
    [Serializable]
    public class JpegCoderSettings
    {
        public ThinningMode ThinningMode { get; set; }
        public GeneralizedThresholderSettings YThresholderSettings { get; set; }
        public GeneralizedThresholderSettings CrThresholderSettings { get; set; }
        public GeneralizedThresholderSettings CbThresholderSettings { get; set; }

        public int BlocSize
        {
            get { return 8; }
        }
    }
}