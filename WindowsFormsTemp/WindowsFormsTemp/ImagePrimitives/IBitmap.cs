using System.Drawing;
using WindowsFormsTemp.NavigationPrimitives;

namespace WindowsFormsTemp.ImagePrimitives
{
    public interface IBitmap
    {
        int Width { get; }
        int Height { get; }
        RgbColor GetPixel(IPosition position);
        RgbColor GetPixel(int row, int column);
        void SetPixel(IPosition position, RgbColor color);
        void SetPixel(int row, int column, RgbColor color);
        Bitmap ToDotNetBitmap();
    }
}