using System;
using System.Threading.Tasks;
using WindowsFormsTemp.ImagePrimitives;
using WindowsFormsTemp.NavigationPrimitives;

namespace WindowsFormsTemp.Filters
{
    public class GrayScaleFilter : IFilter
    {
        public static readonly GrayScaleFilter EqualWeights = new GrayScaleFilter(1.0/3.0, 1.0/3.0, 1.0/3.0);
        public static readonly GrayScaleFilter Ccir6011 = new GrayScaleFilter(0.299, 0.587, 0.114);
        private readonly double bWeight;
        private readonly double gWeight;
        private readonly double rWeight;

        private GrayScaleFilter(double rWeight, double gWeight, double bWeight)
        {
            this.rWeight = rWeight;
            this.gWeight = gWeight;
            this.bWeight = bWeight;
        }

        public IBitmap Apply(IBitmap image, IFilterData filterData)
        {
            if (!(image is IBitmap<RgbPixel>))
                throw new ArgumentException("Image is not RGB.");
            var img = (IBitmap<RgbPixel>) image;

            var result = new PlainBitmap<RgbPixel>(img.Width, img.Height);

            Parallel.For((long) 0, img.Width, column => Parallel.For((long) 0, img.Height, row =>
            {
                var currentPosition = new Position((int) row, (int) column);
                var pixel = img.GetPixel(currentPosition);
                var value = GetGrayValue(pixel);
                pixel = new RgbPixel
                {
                    R = value,
                    G = value,
                    B = value
                };
                result.SetPixel(currentPosition, pixel);
            }));

            return result;
        }

        private byte GetGrayValue(RgbPixel pixel)
        {
            return (byte) (bWeight*pixel.B + gWeight*pixel.G + rWeight*pixel.R);
        }
    }
}