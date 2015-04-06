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
            IBitmap result = new PlainBitmap(image.Width, image.Height);

            Parallel.For((long) 0, image.Width, column => Parallel.For((long) 0, image.Height, row =>
            {
                RgbColor pixel = image.GetPixel((int) row, (int) column);
                result.SetPixel((int) row, (int) column, new RgbColor
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