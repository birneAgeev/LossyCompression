using System;
using System.Drawing;
using WindowsFormsTemp.ImagePrimitives;
using WindowsFormsTemp.Jpeg;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class JpegTests
    {
        private const string DefaultImagePath = "ImageData/image_Lena256gb.bmp";

        [Test]
        public void SevenZipTest()
        {
            var bytes = new byte[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 8, 7, 6, 5, 4, 3, 2, 1};

            var encoded = SevenZipCoder.Instance.Encode(bytes);
            foreach (var x in encoded)
            {
                Console.Write(x);
                Console.Write(" ");
            }

            Assert.AreEqual(bytes, SevenZipCoder.Instance.Decode(SevenZipCoder.Instance.Encode(bytes)));
        }

        [Test]
        public void Test()
        {
            IBitmap bitmap = new Bitmap(DefaultImagePath).ToPlainBitmap();

            byte[] bytes = JpegCoder.Instance.Encode(bitmap);

            Console.WriteLine(bytes.Length);
        }
    }
}