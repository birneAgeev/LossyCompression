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
            if (!(filterData is YuvData))
                throw new ArgumentException("Filter data is not YuvData!");

            var yuvData = filterData as YuvData;

            IBitmap result = new PlainBitmap(image.Width, image.Height);

            Parallel.For(0, image.Width, column => Parallel.For(0, image.Height, row =>
            {
                var currentPosition = new Position
                {
                    Column = column,
                    Row = row
                };
                RgbColor pixel = image.GetPixel(currentPosition);
                pixel = Transform(pixel, yuvData);
                result.SetPixel(currentPosition, pixel);
            }));

            return result;
        }

        private RgbColor Transform(RgbColor pixel, YuvData yuvData)
        {
            var y = (int) Math.Round(0.299*pixel.R + 0.587*pixel.G + 0.114*pixel.B);
            var u = (int) Math.Round(-0.14713*pixel.R - 0.28886*pixel.G + 0.436*pixel.B + 111.18);
            var v = (int) Math.Round(0.615*pixel.R - 0.51499*pixel.G - 0.10001*pixel.B + 156.825);

            y = QuantizationEffect(y, yuvData.YQuantizationDegree, 8);
            u = QuantizationEffect(u, yuvData.UQuantizationDegree, 8);
            v = QuantizationEffect(v, yuvData.VQuantizationDegree, 8);

            return new RgbColor
            {
                R = ToByte(y + 1.13983*(v - 156.825)),
                G = ToByte(y - 0.39465*(u - 111.18) - 0.58060*(v - 156.825)),
                B = ToByte(y + 2.03211*(u - 111.18))
            };
        }

        private int QuantizationEffect(int value, byte quantizationDegree, int maxDegree)
        {
            int shift = maxDegree - quantizationDegree;
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