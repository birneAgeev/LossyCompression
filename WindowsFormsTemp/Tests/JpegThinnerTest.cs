using System;
using System.Drawing;
using WindowsFormsTemp.Compression.CompressionCommons;
using WindowsFormsTemp.Compression.Jpeg;
using WindowsFormsTemp.ImagePrimitives;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class JpegThinnerTest
    {
        private const string DefaultImagePath = "ImageData/image_Lena256gb.bmp";

        [Test]
        public void Test()
        {
            var bitmap = new Bitmap(DefaultImagePath).ToPlainBitmap().ToYCrCbBitmap();
            var result =
                (IBitmap<YCrCbPixel>)
                    Thinner.Instance.Decompress(Thinner.Instance.ThinOut(bitmap, ThinningMode.None));

            for (var i = 0; i < bitmap.Height; ++i)
            {
                for (var j = 0; j < bitmap.Width; ++j)
                {
                    Assert.AreEqual(bitmap.GetPixel(i, j).Y + 0.0, result.GetPixel(i, j).Y + 0.0);
                    Assert.AreEqual(bitmap.GetPixel(i, j).Cr + 0.0, result.GetPixel(i, j).Cr + 0.0);
                    Assert.AreEqual(bitmap.GetPixel(i, j).Cb + 0.0, result.GetPixel(i, j).Cb + 0.0);
                }
            }
        }

        [Test]
        public void ThinningTest()
        {
            var bitmap = new PlainBitmap<YCrCbPixel>(5, 5);

            for (var i = 0; i < 5; ++i)
            {
                for (var j = 0; j < 5; ++j)
                {
                    bitmap.SetPixel(i, j, new YCrCbPixel
                    {
                        Y = (byte) (i + j),
                        Cr = (byte) (i + j),
                        Cb = (byte) (i + j)
                    });
                }
            }

            var thinned = Thinner.Instance.ThinOut(bitmap, ThinningMode._1H2V).ImageData;

            for (var i = 0; i < 5; ++i)
            {
                for (var j = 0; j < 5; ++j)
                {
                    Console.Write(thinned.Y[i, j]);
                    Console.Write(@" ");
                }
                Console.WriteLine();
            }
            Console.WriteLine(@"***************");
            for (var i = 0; i < 3; ++i)
            {
                for (var j = 0; j < 5; ++j)
                {
                    Console.Write(thinned.Cr[i, j]);
                    Console.Write(@" ");
                }
                Console.WriteLine();
            }
            Console.WriteLine(@"***************");
            for (var i = 0; i < 3; ++i)
            {
                for (var j = 0; j < 5; ++j)
                {
                    Console.Write(thinned.Cb[i, j]);
                    Console.Write(@" ");
                }
                Console.WriteLine();
            }
            Console.WriteLine(@"---------------------");

            var decompressed =
                Thinner.Instance.Decompress(Thinner.Instance.ThinOut(bitmap, ThinningMode._1H2V))
                    .ToYCrCbBitmap();

            for (var i = 0; i < 5; ++i)
            {
                for (var j = 0; j < 5; ++j)
                {
                    Console.Write(decompressed.GetPixel(i, j).Y);
                    Console.Write(@" ");
                }
                Console.WriteLine();
            }
            Console.WriteLine(@"***************");
            for (var i = 0; i < 5; ++i)
            {
                for (var j = 0; j < 5; ++j)
                {
                    Console.Write(decompressed.GetPixel(i, j).Cr);
                    Console.Write(@" ");
                }
                Console.WriteLine();
            }
            Console.WriteLine(@"***************");
            for (var i = 0; i < 5; ++i)
            {
                for (var j = 0; j < 5; ++j)
                {
                    Console.Write(decompressed.GetPixel(i, j).Cb);
                    Console.Write(@" ");
                }
                Console.WriteLine();
            }
            Console.WriteLine(@"***************");
        }

        [Test]
        public void ThinningTest2()
        {
            var bitmap = new PlainBitmap<YCrCbPixel>(5, 5);

            for (var i = 0; i < 5; ++i)
            {
                for (var j = 0; j < 5; ++j)
                {
                    bitmap.SetPixel(i, j, new YCrCbPixel
                    {
                        Y = (byte) (i + j),
                        Cr = (byte) (i + j),
                        Cb = (byte) (i + j)
                    });
                }
            }

            var thinned = Thinner.Instance.ThinOut(bitmap, ThinningMode._2H1V).ImageData;

            for (var i = 0; i < 5; ++i)
            {
                for (var j = 0; j < 5; ++j)
                {
                    Console.Write(thinned.Y[i, j]);
                    Console.Write(@" ");
                }
                Console.WriteLine();
            }
            Console.WriteLine(@"***************");
            for (var i = 0; i < 5; ++i)
            {
                for (var j = 0; j < 3; ++j)
                {
                    Console.Write(thinned.Cr[i, j]);
                    Console.Write(@" ");
                }
                Console.WriteLine();
            }
            Console.WriteLine(@"***************");
            for (var i = 0; i < 5; ++i)
            {
                for (var j = 0; j < 3; ++j)
                {
                    Console.Write(thinned.Cb[i, j]);
                    Console.Write(@" ");
                }
                Console.WriteLine();
            }
            Console.WriteLine(@"---------------------");

            var decompressed =
                Thinner.Instance.Decompress(Thinner.Instance.ThinOut(bitmap, ThinningMode._2H1V))
                    .ToYCrCbBitmap();

            for (var i = 0; i < 5; ++i)
            {
                for (var j = 0; j < 5; ++j)
                {
                    Console.Write(decompressed.GetPixel(i, j).Y);
                    Console.Write(@" ");
                }
                Console.WriteLine();
            }
            Console.WriteLine(@"***************");
            for (var i = 0; i < 5; ++i)
            {
                for (var j = 0; j < 5; ++j)
                {
                    Console.Write(decompressed.GetPixel(i, j).Cr);
                    Console.Write(@" ");
                }
                Console.WriteLine();
            }
            Console.WriteLine(@"***************");
            for (var i = 0; i < 5; ++i)
            {
                for (var j = 0; j < 5; ++j)
                {
                    Console.Write(decompressed.GetPixel(i, j).Cb);
                    Console.Write(@" ");
                }
                Console.WriteLine();
            }
            Console.WriteLine(@"***************");
        }

        [Test]
        public void ThinningTest3()
        {
            var bitmap = new PlainBitmap<YCrCbPixel>(4, 4);

            for (var i = 0; i < 4; ++i)
            {
                for (var j = 0; j < 4; ++j)
                {
                    bitmap.SetPixel(i, j, new YCrCbPixel
                    {
                        Y = (byte) (i + j),
                        Cr = (byte) (i + j),
                        Cb = (byte) (i + j)
                    });
                }
            }

            var thinned = Thinner.Instance.ThinOut(bitmap, ThinningMode._2H1V).ImageData;

            for (var i = 0; i < 4; ++i)
            {
                for (var j = 0; j < 4; ++j)
                {
                    Console.Write(thinned.Y[i, j]);
                    Console.Write(@" ");
                }
                Console.WriteLine();
            }
            Console.WriteLine(@"***************");
            for (var i = 0; i < 4; ++i)
            {
                for (var j = 0; j < 2; ++j)
                {
                    Console.Write(thinned.Cr[i, j]);
                    Console.Write(@" ");
                }
                Console.WriteLine();
            }
            Console.WriteLine(@"***************");
            for (var i = 0; i < 4; ++i)
            {
                for (var j = 0; j < 2; ++j)
                {
                    Console.Write(thinned.Cb[i, j]);
                    Console.Write(@" ");
                }
                Console.WriteLine();
            }
            Console.WriteLine(@"---------------------");

            var decompressed =
                Thinner.Instance.Decompress(Thinner.Instance.ThinOut(bitmap, ThinningMode._2H1V))
                    .ToYCrCbBitmap();

            for (var i = 0; i < 4; ++i)
            {
                for (var j = 0; j < 4; ++j)
                {
                    Console.Write(decompressed.GetPixel(i, j).Y);
                    Console.Write(@" ");
                }
                Console.WriteLine();
            }
            Console.WriteLine(@"***************");
            for (var i = 0; i < 4; ++i)
            {
                for (var j = 0; j < 4; ++j)
                {
                    Console.Write(decompressed.GetPixel(i, j).Cr);
                    Console.Write(@" ");
                }
                Console.WriteLine();
            }
            Console.WriteLine(@"***************");
            for (var i = 0; i < 4; ++i)
            {
                for (var j = 0; j < 4; ++j)
                {
                    Console.Write(decompressed.GetPixel(i, j).Cb);
                    Console.Write(@" ");
                }
                Console.WriteLine();
            }
            Console.WriteLine(@"***************");
        }
    }
}