using System.Drawing;
using WindowsFormsTemp.ImagePrimitives;
using WindowsFormsTemp.Jpeg;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class JpegThinnerTest
    {
        [Test]
        public void Test()
        {
            var bitmap = new Bitmap(DefaultImagePath).ToPlainBitmap().ToYCrCbBitmap();
            var result = (IBitmap<YCrCbPixel>)JpegThinner.Instance.Decompress(JpegThinner.Instance.ThinOut(bitmap, ThinningMode.None));

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

        private const string DefaultImagePath = "ImageData/image_Lena256gb.bmp";
    }
}