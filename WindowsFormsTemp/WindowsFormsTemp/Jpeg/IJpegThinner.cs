using WindowsFormsTemp.ImagePrimitives;

namespace WindowsFormsTemp.Jpeg
{
    public interface IJpegThinner
    {
        JpegThinnerResult ThinOut(IBitmap bitmap, ThinningMode thinningMode);
        IBitmap Decompress(JpegThinnerResult compressedData);
    }
}