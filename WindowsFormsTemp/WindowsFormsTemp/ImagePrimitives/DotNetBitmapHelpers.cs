using System.Drawing;
using System.Drawing.Imaging;
using System.Threading.Tasks;
using WindowsFormsTemp.NavigationPrimitives;

namespace WindowsFormsTemp.ImagePrimitives
{
    public static class DotNetBitmapHelpers
    {
        public static IBitmap ToPlainBitmap(this Bitmap bitmap)
        {
            var result = new PlainBitmap(bitmap.Width, bitmap.Height);
            for (int row = 0; row < bitmap.Height; ++row)
            {
                for (int column = 0; column < bitmap.Width; ++column)
                {
                    Color pixel = bitmap.GetPixel(column, row);
                    result.SetPixel(new Position
                    {
                        Row = row,
                        Column = column
                    }, new RgbColor
                    {
                        R = pixel.R,
                        G = pixel.G,
                        B = pixel.B
                    });
                }
            }
            return result;
        }

        public static unsafe IBitmap ZzzToPlainBitmap(this Bitmap bitmap)
        {
            var result = new PlainBitmap(bitmap.Width, bitmap.Height);

            BitmapData bitmapData =
                bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                    ImageLockMode.ReadOnly, bitmap.PixelFormat);

            int bytesPerPixel = Image.GetPixelFormatSize(bitmap.PixelFormat)/8;
            int heightInPixels = bitmapData.Height;
            int widthInBytes = bitmapData.Width*bytesPerPixel;
            var ptrFirstPixel = (byte*) bitmapData.Scan0;

            Parallel.For((long) 0, heightInPixels, y =>
            {
                byte* currentLine = ptrFirstPixel + ((int) y*bitmapData.Stride);
                for (int x = 0; x < widthInBytes; x = x + bytesPerPixel)
                {
                    result.SetPixel((int) y, x/bytesPerPixel, new RgbColor
                    {
                        R = currentLine[x + 2],
                        G = currentLine[x + 1],
                        B = currentLine[x]
                    });
                }
            });
            bitmap.UnlockBits(bitmapData);

            return result;
        }
    }
}