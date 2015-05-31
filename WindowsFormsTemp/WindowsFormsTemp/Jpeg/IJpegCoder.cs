using WindowsFormsTemp.ImagePrimitives;

namespace WindowsFormsTemp.Jpeg
{
    public interface IJpegCoder
    {
        byte[] Encode(IBitmap bitmap);
        IBitmap Decode(byte[] data);
    }
}