using System;

namespace WindowsFormsTemp.ImagePrimitives
{
    public class YCrCbPixel : IPixel
    {
        public byte Y { get; set; }
        public byte Cr { get; set; }
        public byte Cb { get; set; }

        public RgbPixel ToRgb()
        {
            return new RgbPixel
            {
                R = ToByte(Y + 1.402*(Cr - 128.0)),
                G = ToByte(Y - 0.34414*(Cb - 128.0) - 0.71414*(Cr - 128.0)),
                B = ToByte(Y + 1.772*(Cb - 128.0))
            };
        }

        private static byte ToByte(double a)
        {
            var integer = (int) Math.Round(a);
            if (integer > 255)
                return 255;
            if (integer < 0)
                return 0;
            return (byte) integer;
        }
    }
}