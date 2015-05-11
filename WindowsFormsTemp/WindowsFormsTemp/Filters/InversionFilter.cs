using System;
using System.Threading.Tasks;
using WindowsFormsTemp.ImagePrimitives;

namespace WindowsFormsTemp.Filters
{
    public class InversionFilter : IFilter
    {
        public static InversionFilter Instance = new InversionFilter();

        private InversionFilter()
        {
        }

        public IBitmap Apply(IBitmap image, IFilterData filterData = null)
        {
            if (!(image is IBitmap<RgbPixel>))
                throw new ArgumentException("Image is not RGB.");
            var img = (IBitmap<RgbPixel>) image;

            var result = new PlainBitmap<RgbPixel>(img.Width, img.Height);

            Parallel.For((long) 0, img.Width, column => Parallel.For((long) 0, img.Height, row =>
            {
                RgbPixel pixel = img.GetPixel((int) row, (int) column);
                result.SetPixel((int) row, (int) column, new RgbPixel
                {
                    R = (byte) (255 - pixel.R),
                    G = (byte) (255 - pixel.G),
                    B = (byte) (255 - pixel.B)
                });
            }));

            return result;
        }
    }
}