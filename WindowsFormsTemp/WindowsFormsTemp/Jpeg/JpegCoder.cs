using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using WindowsFormsTemp.ImagePrimitives;
using WindowsFormsTemp.Jpeg.Thresholders;

namespace WindowsFormsTemp.Jpeg
{
    public class JpegCoder : IJpegCoder
    {
        public static JpegCoder Instance = new JpegCoder();

        private JpegCoder()
        {
        }

        public byte[] Encode(IBitmap bitmap, JpegCoderSettings settings)
        {
            var thinnerResult = JpegThinner.Instance.ThinOut(bitmap.ToYCrCbBitmap(), settings.ThinningMode);

            var result = new JpegData
            {
                Y = EncodeComponent(thinnerResult.ImageData.Y, settings.YThresholderSettings),
                Cr = EncodeComponent(thinnerResult.ImageData.Cr, settings.CrThresholderSettings),
                Cb = EncodeComponent(thinnerResult.ImageData.Cb, settings.CbThresholderSettings),
                Width = bitmap.Width,
                Height = bitmap.Height,
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

            var jpegData = (JpegData) formatter.Deserialize(stream);

            var widthDivisor = JpegThinner.ModeToDividers[jpegData.Settings.ThinningMode].Item2;
            var heightDivisor = JpegThinner.ModeToDividers[jpegData.Settings.ThinningMode].Item1;

            var thinnerData = new JpegThinnerResult
            {
                ImageData = new SeparatedYCrCb
                {
                    Y = DecodeComponent(jpegData.Y,
                        jpegData.Width,
                        jpegData.Height,
                        jpegData.Settings.BlocSize,
                        jpegData.Settings.YThresholderSettings),
                    Cr = DecodeComponent(jpegData.Cr,
                        jpegData.Width/widthDivisor,
                        jpegData.Height/heightDivisor,
                        jpegData.Settings.BlocSize,
                        jpegData.Settings.CrThresholderSettings),
                    Cb = DecodeComponent(jpegData.Cb,
                        jpegData.Width/widthDivisor,
                        jpegData.Height/heightDivisor,
                        jpegData.Settings.BlocSize,
                        jpegData.Settings.CbThresholderSettings)
                },
                ThinningMode = jpegData.Settings.ThinningMode
            };

            return JpegThinner.Instance.Decompress(thinnerData);
        }

        private short[] EncodeComponent(double[,] matrix, GeneralizedThresholderSettings settings)
        {
            var result = new List<short>();

            var blockStream = new JpegBlockStream(matrix);

            for (var i = 0; i < blockStream.HeightInBlocks; ++i)
            {
                for (var j = 0; j < blockStream.WidthInBlocks; ++j)
                {
                    var transformedBlock = JpegDiscreteCosineTransformationCalculator.Instance
                        .ForwardTransform(blockStream.GetBlock(i, j))
                        .Threshold(GeneralizedThresholder.Instance,
                            settings);
                    result.AddRange(ZigZag(transformedBlock));
                }
            }

            return result.ToArray();
        }

        private double[,] DecodeComponent(short[] data, int width, int height, int blockSize,
            GeneralizedThresholderSettings settings)
        {
            var result = new double[height, width];
            var ptr = 0;

            for (var i = 0; i < height; i += blockSize)
            {
                for (var j = 0; j < width; j += blockSize)
                {
                    var block = ZigZag(data, ptr, blockSize)
                        .Restore(GeneralizedThresholder.Instance, settings);
                    block = JpegDiscreteCosineTransformationCalculator.Instance.InverseTransform(block);
                    ptr += blockSize*blockSize;
                    for (var y = 0; y < blockSize; ++y)
                    {
                        for (var x = 0; x < blockSize; ++x)
                        {
                            result[i + y, j + x] = block[y, x];
                        }
                    }
                }
            }

            return result;
        }

        private double[,] ZigZag(short[] data, int offset, int blockSize)
        {
            var result = new double[blockSize, blockSize];
            var ptr = offset;
            for (var i = 0; i < blockSize; ++i)
            {
                var x = 0;
                var y = i;
                while (y >= 0)
                {
                    result[y, x] = data[ptr++];
                    ++x;
                    --y;
                }
            }
            for (var i = 1; i < blockSize; ++i)
            {
                var x = i;
                var y = blockSize - 1;
                while (x < blockSize)
                {
                    result[y, x] = data[ptr++];
                    ++x;
                    --y;
                }
            }

            return result;
        }

        private IEnumerable<short> ZigZag(double[,] matrix)
        {
            var n = matrix.GetLength(0);
            for (var i = 0; i < n; ++i)
            {
                var x = 0;
                var y = i;
                while (y >= 0)
                {
                    yield return (short) Math.Round(matrix[y, x]);
                    ++x;
                    --y;
                }
            }
            for (var i = 1; i < n; ++i)
            {
                var x = i;
                var y = n - 1;
                while (x < n)
                {
                    yield return (short) Math.Round(matrix[y, x]);
                    ++x;
                    --y;
                }
            }
        }

        [Serializable]
        private class JpegData
        {
            public short[] Y { get; set; }
            public short[] Cr { get; set; }
            public short[] Cb { get; set; }
            public int Width { get; set; }
            public int Height { get; set; }
            public JpegCoderSettings Settings { get; set; }
        }
    }
}