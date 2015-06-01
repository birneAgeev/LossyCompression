using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using WindowsFormsTemp.ImagePrimitives;

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
            JpegThinnerResult thinnerResult = JpegThinner.Instance.ThinOut(bitmap.ToYCrCbBitmap(), settings.ThinningMode);

            var result = new JpegData
            {
                Y = EncodeComponent(thinnerResult.ImageData.Y),
                Cr = EncodeComponent(thinnerResult.ImageData.Cr),
                Cb = EncodeComponent(thinnerResult.ImageData.Cb),
                Width = bitmap.Width,
                Height = bitmap.Height,
                Settings = settings
            };

            var stream = new MemoryStream();
            var formatter = new BinaryFormatter();
            formatter.Serialize(stream, result);

            byte[] bytes = stream.ToArray();

            return SevenZipCoder.Instance.Encode(bytes);
        }

        public IBitmap Decode(byte[] data)
        {
            byte[] bytes = SevenZipCoder.Instance.Decode(data);

            var formatter = new BinaryFormatter();
            var stream = new MemoryStream(bytes);

            var jpegData = (JpegData) formatter.Deserialize(stream);

            int widthDivisor = JpegThinner.ModeToDividers[jpegData.Settings.ThinningMode].Item1;
            int heightDivisor = JpegThinner.ModeToDividers[jpegData.Settings.ThinningMode].Item2;

            var thinnerData = new JpegThinnerResult
            {
                ImageData = new SeparatedYCrCb
                {
                    Y = DecodeComponent(jpegData.Y,
                        jpegData.Width,
                        jpegData.Height,
                        jpegData.Settings.BlocSize),
                    Cr = DecodeComponent(jpegData.Cr,
                        jpegData.Width/widthDivisor,
                        jpegData.Height/heightDivisor,
                        jpegData.Settings.BlocSize),
                    Cb = DecodeComponent(jpegData.Cb,
                        jpegData.Width/widthDivisor,
                        jpegData.Height/heightDivisor,
                        jpegData.Settings.BlocSize)
                },
                ThinningMode = jpegData.Settings.ThinningMode
            };

            return JpegThinner.Instance.Decompress(thinnerData);
        }

        private short[] EncodeComponent(double[,] matrix)
        {
            var result = new List<short>();

            var blockStream = new JpegBlockStream(matrix);

            for (int i = 0; i < blockStream.HeightInBlocks; ++i)
            {
                for (int j = 0; j < blockStream.WidthInBlocks; ++j)
                {
                    double[,] transformedBlock = JpegDiscreteCosineTransformationCalculator.Instance
                        .ForwardTransform(blockStream.GetBlock(i, j))
                        .Apply(MaxValuesThresholder.Instatnce,
                            new MaxValuesThresholderSettings {MaxCount = 64});
                    result.AddRange(ZigZag(transformedBlock));
                }
            }

            return result.ToArray();
        }

        private double[,] DecodeComponent(short[] data, int width, int height, int blockSize)
        {
            var result = new double[height, width];
            int ptr = 0;

            for (int i = 0; i < height; i += blockSize)
            {
                for (int j = 0; j < width; j += blockSize)
                {
                    double[,] block = ZigZag(data, ptr, blockSize);
                    block = JpegDiscreteCosineTransformationCalculator.Instance.InverseTransform(block);
                    ptr += blockSize*blockSize;
                    for (int y = 0; y < blockSize; ++y)
                    {
                        for (int x = 0; x < blockSize; ++x)
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
            int ptr = offset;
            for (int i = 0; i < blockSize; ++i)
            {
                int x = 0;
                int y = i;
                while (y >= 0)
                {
                    result[y, x] = data[ptr++];
                    ++x;
                    --y;
                }
            }
            for (int i = 1; i < blockSize; ++i)
            {
                int x = i;
                int y = blockSize - 1;
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
            int n = matrix.GetLength(0);
            for (int i = 0; i < n; ++i)
            {
                int x = 0;
                int y = i;
                while (y >= 0)
                {
                    yield return (short) Math.Round(matrix[y, x]);
                    ++x;
                    --y;
                }
            }
            for (int i = 1; i < n; ++i)
            {
                int x = i;
                int y = n - 1;
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