using WindowsFormsTemp.ImagePrimitives;

namespace WindowsFormsTemp.Compression.Jpeg
{
    public interface IJpegCoder
    {
        byte[] Encode(IBitmap bitmap, JpegCoderSettings settings);
        IBitmap Decode(byte[] data);
    }
}