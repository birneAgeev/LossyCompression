using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using WindowsFormsTemp.Compression.CompressionCommons;
using WindowsFormsTemp.Compression.Jpeg;
using WindowsFormsTemp.ImagePrimitives;

namespace WindowsFormsTemp.Compression.Wavelet
{
    public class WaveletCoder : IWaveletCoder
    {
        public static WaveletCoder Instance = new WaveletCoder();

        private WaveletCoder()
        {
        }

        public byte[] Encode(IBitmap bitmap, WaveletCoderSettings settings)
        {
            var thinned = Thinner.Instance.ThinOut(bitmap.ToYCrCbBitmap(), settings.ThinningMode).ImageData;

            var result = new WaveletData
            {
                Y = EncodeComponent(thinned.Y, settings.Depth),
                Cb = EncodeComponent(thinned.Cb, settings.Depth),
                Cr = EncodeComponent(thinned.Cr, settings.Depth)
            };

            var stream = new MemoryStream();
            var formatter = new BinaryFormatter();
            formatter.Serialize(stream, result);

            var bytes = stream.ToArray();

            return SevenZipCoder.Instance.Encode(bytes);
        }

        public IBitmap Decode(byte[] data)
        {
            var bytes = SevenZipCoder.Instance.Decode(data);

            var formatter = new BinaryFormatter();
            var stream = new MemoryStream(bytes);

            var waveletData = (WaveletData) formatter.Deserialize(stream);

            var height = waveletData.Y.GetLength(0);
            var width = waveletData.Y.GetLength(1);
            var result = new PlainBitmap<YCrCbPixel>(width, height);

            for (var i = 0; i < height; ++i)
            {
                for (var j = 0; j < height; ++j)
                {
                    result.SetPixel(i, j, new YCrCbPixel
                    {
                        Y = ToByte(waveletData.Y[i, j]),
                        Cr = ToByte(waveletData.Cr[i, j]),
                        Cb = ToByte(waveletData.Cb[i, j])
                    });
                }
            }

            return result;
        }

        private static short[,] EncodeComponent(double[,] matrix, int depth)
        {
            var height = matrix.GetLength(0);
            var width = matrix.GetLength(1);
            for (var level = 0; level < depth; ++level)
            {
                WaveletTransformation.Instance.Transform(matrix, width >> level, height >> level, 4);
            }

            var result = new short[height, width];
            for (var i = 0; i < height; ++i)
            {
                for (var j = 0; j < width; ++j)
                {
                    result[i, j] = (short) Math.Round(matrix[i, j]);
                }
            }

            return result;
        }

        private static byte ToByte(double a)
        {
            var integer = (int) Math.Round(a);
            if (integer > 255)
                return 255;
            if (integer < 0)
                return 0;
            return (byte) integer;
        }

        [Serializable]
        private class WaveletData
        {
            public short[,] Y { get; set; }
            public short[,] Cr { get; set; }
            public short[,] Cb { get; set; }
        }
    }
}