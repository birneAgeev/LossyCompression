using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using WindowsFormsTemp.Compression.CompressionCommons;
using WindowsFormsTemp.Compression.Jpeg;
using WindowsFormsTemp.ImagePrimitives;

namespace WindowsFormsTemp.Compression.Wavelet
{
    class WaveletCoder : IWaveletCoder
    {
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

        private static short[,] EncodeComponent(double[,] matrix, int depth)
        {
            var height = matrix.GetLength(0);
            var width = matrix.GetLength(1);
            for (var level = 0; level < depth; ++level)
            {
                WaveletTransformation.Instance.Transform(matrix, width >> level, height >> level);
            }

            var result = new short[height, width];
            for (var i = 0; i < height; ++i)
            {
                for (var j = 0; j < width; ++j)
                {
                    result[i, j] = (short)Math.Round(matrix[i, j]);
                }
            }

            return result;
        }

        public IBitmap Decode(byte[] data)
        {
            
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