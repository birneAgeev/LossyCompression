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
    }
}