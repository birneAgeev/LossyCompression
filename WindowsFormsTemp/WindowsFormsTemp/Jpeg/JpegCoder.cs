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

        public byte[] Encode(IBitmap bitmap)
        {
            JpegThinnerResult thinnerResult = JpegThinner.Instance.ThinOut(bitmap.ToYCrCbBitmap(), ThinningMode.None);

            var result = new JpegData
            {
                Y = EncodeComponent(thinnerResult.ImageData.Y),
                Cr = EncodeComponent(thinnerResult.ImageData.Cr),
                Cb = EncodeComponent(thinnerResult.ImageData.Cb),
                Width = bitmap.Width,
                Height = bitmap.Height,
                ThinningMode = ThinningMode.None
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
                            new MaxValuesThresholderSettings {MaxCount = 4});
                    result.AddRange(ZigZag(transformedBlock));
                }
            }

            return result.ToArray();
        }

        private double[,] DecodeComponent(short[] data, int width, int height)
        {
            
        }

        private double[,] ZigZag(short[] data, int n)
        {
            var result = new double[n, n];
            var ptr = 0;
            for (int i = 0; i < n; ++i)
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
            for (int i = 1; i < n; ++i)
            {
                int x = i;
                int y = n - 1;
                while (x < n)
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
                    yield return (short)Math.Round(matrix[y, x]);
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
                    yield return (short)Math.Round(matrix[y, x]);
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
            public ThinningMode ThinningMode { get; set; }
        }
    }
}