using System.Drawing;
using WindowsFormsTemp.NavigationPrimitives;

namespace WindowsFormsTemp.ImagePrimitives
{
    public interface IBitmap
    {
        int Width { get; }
        int Height { get; }
        IBitmap<RgbPixel> ToRgbBitmap();
    }

    public interface IBitmap<TPixel> : IBitmap where TPixel : IPixel
    {
        TPixel GetPixel(IPosition position);
        TPixel GetPixel(int row, int column);
        void SetPixel(IPosition position, TPixel color);
        void SetPixel(int row, int column, TPixel color);
    }

    public static class BitmapHelpers
    {
        public static Bitmap ToDotNetBitmap(this IBitmap bitmap)
        {
            var rgbBitmap = bitmap.ToRgbBitmap();
            var result = new Bitmap(rgbBitmap.Width, rgbBitmap.Height);
            for (var row = 0; row < rgbBitmap.Height; ++row)
            {
                for (var column = 0; column < rgbBitmap.Width; ++column)
                {
                    var pixel = rgbBitmap.GetPixel(new Position
                    {
                        Column = column,
                        Row = row
                    });
                    result.SetPixel(column, row, Color.FromArgb(pixel.R, pixel.G, pixel.B));
                }
            }
            return result;
        }
    }
}