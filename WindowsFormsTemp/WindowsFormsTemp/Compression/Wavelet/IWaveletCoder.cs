using WindowsFormsTemp.ImagePrimitives;

namespace WindowsFormsTemp.Compression.Wavelet
{
    public interface IWaveletCoder
    {
        byte[] Encode(IBitmap bitmap, WaveletCoderSettings settings);
        IBitmap Decode(byte[] data);
    }
}