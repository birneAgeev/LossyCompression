using System;
using WindowsFormsTemp.Jpeg.Thresholders;

namespace WindowsFormsTemp.Jpeg
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