using System.Drawing;
using WindowsFormsTemp.NavigationPrimitives;

namespace WindowsFormsTemp.ImagePrimitives
{
    public static class DotNetBitmapHelpers
    {
        public static IBitmap ToPlainBitmap(this Bitmap bitmap)
        {
            var result = new PlainBitmap<RgbPixel>(bitmap.Width, bitmap.Height);
            for (var row = 0; row < bitmap.Height; ++row)
            {
                for (var column = 0; column < bitmap.Width; ++column)
                {
                    var pixel = bitmap.GetPixel(column, row);
                    result.SetPixel(new Position
                    {
                        Row = row,
                        Column = column
                    }, new RgbPixel
                    {
                        R = pixel.R,
                        G = pixel.G,
                        B = pixel.B
                    });
                }
            }
            return result;
        }
    }
}