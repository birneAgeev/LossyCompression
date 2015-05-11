using System;
using System.Threading.Tasks;
using WindowsFormsTemp.ImagePrimitives;
using WindowsFormsTemp.NavigationPrimitives;

namespace WindowsFormsTemp.Filters
{
    public class YuvFilter : IFilter
    {
        public static readonly YuvFilter Instance = new YuvFilter();

        private YuvFilter()
        {
        }

        public IBitmap Apply(IBitmap image, IFilterData filterData)
        {
            if (!(image is IBitmap<RgbPixel>))
                throw new ArgumentException("Image is not RGB.");
            var img = (IBitmap<RgbPixel>) image;

            if (!(filterData is YuvData))
                throw new ArgumentException("Filter data is not YuvData!");

            var yuvData = filterData as YuvData;

            var result = new PlainBitmap<RgbPixel>(img.Width, img.Height);

            Parallel.For(0, img.Width, column => Parallel.For(0, img.Height, row =>
            {
                var currentPosition = new Position
                {
                    Column = column,
                    Row = row
                };
                var pixel = img.GetPixel(currentPosition);
                pixel = Transform(pixel, yuvData);
                result.SetPixel(currentPosition, pixel);
            }));

            return result;
        }

        private RgbPixel Transform(RgbPixel pixel, YuvData yuvData)
        {
            var y = (int) Math.Round(0.299*pixel.R + 0.587*pixel.G + 0.114*pixel.B);
            var cr = (int) Math.Round(-0.1687*pixel.R - 0.3313*pixel.G + 0.5*pixel.B + 128.0);
            var cb = (int) Math.Round(0.5*pixel.R - 0.4187*pixel.G - 0.0813*pixel.B + 128.0);

            y = QuantizationEffect(y, yuvData.YQuantizationDegree, 8);
            cr = QuantizationEffect(cr, yuvData.UQuantizationDegree, 8);
            cb = QuantizationEffect(cb, yuvData.VQuantizationDegree, 8);

            return new RgbPixel
            {
                R = ToByte(y + 1.402*(cb - 128.0)),
                G = ToByte(y - 0.34414*(cr - 128.0) - 0.71414*(cb - 128.0)),
                B = ToByte(y + 1.772*(cr - 128.0))
            };
        }

        private int QuantizationEffect(int value, byte quantizationDegree, int maxDegree)
        {
            var shift = maxDegree - quantizationDegree;
            return (value >> shift << shift) + (shift != 0 ? (1 << (shift - 1)) : 0);
        }

        private byte ToByte(double a)
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