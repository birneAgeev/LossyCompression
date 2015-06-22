using System;
using WindowsFormsTemp.Compression.CompressionCommons;

namespace WindowsFormsTemp.Compression.Wavelet
{
    [Serializable]
    public class WaveletCoderSettings
    {
        public ThinningMode ThinningMode { get; set; }
        public int Depth { get; set; }
        public int Order { get; set; }
        public double Threshold { get; set; }
    }
}