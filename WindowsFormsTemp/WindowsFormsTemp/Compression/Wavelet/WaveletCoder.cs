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
                Y = EncodeComponent(thinned.Y, settings.Depth, settings.Order, settings.Threshold),
                Cb = EncodeComponent(thinned.Cb, settings.Depth, settings.Order, settings.Threshold),
                Cr = EncodeComponent(thinned.Cr, settings.Depth, settings.Order, settings.Threshold),
                Settings = settings
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
            var settings = waveletData.Settings;

            var thinnerResult = new ThinnerResult
            {
                ImageData = new SeparatedYCrCb
                {
                    Y = DecodeComponent(waveletData.Y, settings.Depth, settings.Order),
                    Cr = DecodeComponent(waveletData.Cr, settings.Depth, settings.Order),
                    Cb = DecodeComponent(waveletData.Cb, settings.Depth, settings.Order)
                },
                ThinningMode = settings.ThinningMode
            };

            var result = Thinner.Instance.Decompress(thinnerResult);

            return result;
        }

        private static double[,] DecodeComponent(short[,] component, int depth, int order)
        {
            var height = component.GetLength(0);
            var width = component.GetLength(1);
            var matrix = new double[height, width];

            for (var i = 0; i < height; ++i)
            {
                for (var j = 0; j < width; ++j)
                {
                    matrix[i, j] = component[i, j];
                }
            }

            for (var level = 0; level < depth; ++level)
            {
                WaveletTransformation.Instance.Inverse(matrix, width >> (depth - level - 1), height >> (depth - level - 1), order);
            }
            return matrix;
        }

        private static short[,] EncodeComponent(double[,] matrix, int depth, int order, double threshold)
        {
            var height = matrix.GetLength(0);
            var width = matrix.GetLength(1);
            for (var level = 0; level < depth; ++level)
            {
                WaveletTransformation.Instance.Transform(matrix, width >> level, height >> level, order);
            }

            var result = new short[height, width];
            for (var i = 0; i < height; ++i)
            {
                for (var j = 0; j < width; ++j)
                {
                    result[i, j] = (short) Math.Round(Math.Abs(matrix[i, j]) > threshold ? matrix[i, j] : 0.0);
                }
            }

            return result;
        }

        [Serializable]
        private class WaveletData
        {
            public short[,] Y { get; set; }
            public short[,] Cr { get; set; }
            public short[,] Cb { get; set; }
            public WaveletCoderSettings Settings { get; set; }
        }
    }
}