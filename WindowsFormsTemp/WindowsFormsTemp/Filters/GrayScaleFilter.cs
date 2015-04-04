using System.Threading.Tasks;
using WindowsFormsTemp.ImagePrimitives;
using WindowsFormsTemp.NavigationPrimitives;

namespace WindowsFormsTemp.Filters
{
    public class GrayScaleFilter : IFilter
    {
        public static readonly GrayScaleFilter EqualWeights = new GrayScaleFilter(1.0 / 3.0, 1.0 / 3.0, 1.0 / 3.0);
        public static readonly GrayScaleFilter Ccir6011 = new GrayScaleFilter(0.299, 0.587, 0.114);

        private GrayScaleFilter(double rWeight, double gWeight, double bWeight)
        {
            this.rWeight = rWeight;
            this.gWeight = gWeight;
            this.bWeight = bWeight;
        }

        public IBitmap Apply(IBitmap image, IFilterData filterData)
        {
            IBitmap result = new PlainBitmap(image.Width, image.Height);

            Parallel.For((long)0, image.Width, column => Parallel.For((long)0, image.Height, row =>
            {
                var currentPosition = new Position((int)row, (int)column);
                RgbColor pixel = image.GetPixel(currentPosition);
                var value = GetGrayValue(pixel);
                pixel = new RgbColor
                {
                    R = value,
                    G = value,
                    B = value
                };
                result.SetPixel(currentPosition, pixel);
            }));

            return result;
        }

        private byte GetGrayValue(RgbColor pixel)
        {
            return (byte)(bWeight*pixel.B + gWeight*pixel.G + rWeight*pixel.R);
        }

        private readonly double rWeight;
        private readonly double gWeight;
        private readonly double bWeight;
    }
}