using System;

namespace WindowsFormsTemp.ImagePrimitives
{
    public class RgbPixel : IPixel
    {
        public byte R { get; set; }
        public byte G { get; set; }
        public byte B { get; set; }

        public RgbPixel ToRgb()
        {
            return this;
        }

        public YCrCbPixel ToYCrCb()
        {
            return new YCrCbPixel
            {
                Y = (byte) Math.Round(0.299*R + 0.587*G + 0.114*B),
                Cb = (byte) Math.Round(-0.1687*R - 0.3313*G + 0.5*B + 128.0),
                Cr = (byte) Math.Round(0.5*R - 0.4187*G - 0.0813*B + 128.0)
            };
        }
    }
}