using System.Drawing;
using WindowsFormsTemp.NavigationPrimitives;

namespace WindowsFormsTemp.ImagePrimitives
{
    public class PlainBitmap : IBitmap
    {
        private readonly RgbColor[,] data;

        public PlainBitmap(int width, int height)
        {
            Width = width;
            Height = height;
            data = new RgbColor[Height, Width];
        }

        public int Width { get; private set; }
        public int Height { get; private set; }

        public RgbColor GetPixel(IPosition position)
        {
            return data[position.Row, position.Column];
        }

        public RgbColor GetPixel(int row, int column)
        {
            return data[row, column];
        }

        public void SetPixel(IPosition position, RgbColor color)
        {
            data[position.Row, position.Column] = color;
        }

        public void SetPixel(int row, int column, RgbColor color)
        {
            data[row, column] = color;
        }

        public Bitmap ToDotNetBitmap()
        {
            var result = new Bitmap(Width, Height);
            for (int row = 0; row < Height; ++row)
            {
                for (int column = 0; column < Width; ++column)
                {
                    RgbColor pixel = GetPixel(new Position
                    {
                        Column = column,
                        Row = row
                    });
                    result.SetPixel(column, row, Color.FromArgb(pixel.R, pixel.G, pixel.B));
                }
            }
            return result;
        }

        //public unsafe Bitmap ToDotNetBitmap()
        //{
        //    var result = new Bitmap(Width, Height);

        //    BitmapData bitmapData =
        //        result.LockBits(new Rectangle(0, 0, Width, Height),
        //            ImageLockMode.WriteOnly, result.PixelFormat);

        //    int bytesPerPixel = Image.GetPixelFormatSize(result.PixelFormat)/8;
        //    int heightInPixels = bitmapData.Height;
        //    int widthInBytes = bitmapData.Width*bytesPerPixel;
        //    var ptrFirstPixel = (byte*) bitmapData.Scan0;

        //    //Parallel.For((long) 0, heightInPixels, y =>
        //    for (int y = 0; y < heightInPixels; ++y)
        //    {
        //        byte* currentLine = ptrFirstPixel + (int) y*bitmapData.Stride;
        //        for (int x = 0; x < widthInBytes; x = x + bytesPerPixel)
        //        {
        //            RgbColor pixel = GetPixel((int) y, x/bytesPerPixel);

        //            currentLine[x] = pixel.B;
        //            currentLine[x + 1] = pixel.G;
        //            currentLine[x + 2] = pixel.R;
        //        }
        //    }
        //    //});
        //    result.UnlockBits(bitmapData);

        //    return result;
        //}
    }
}