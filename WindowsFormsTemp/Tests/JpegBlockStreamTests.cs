using WindowsFormsTemp.Compression.Jpeg;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class JpegBlockStreamTests
    {
        [Test]
        public void Test()
        {
            var source = new double[,]
            {
                {1, 2, 3, 4, 5, 6},
                {6, 7, 8, 9, 10, 11},
                {11, 12, 13, 14, 15, 16},
                {16, 17, 18, 19, 20, 21},
                {21, 22, 23, 24, 25, 26}
            };

            var stream = new JpegBlockStream(source, 4);
            
            Assert.AreEqual(new double[,]
            {
                {25, 26, 26, 26},
                {25, 26, 26, 26},
                {25, 26, 26, 26},
                {25, 26, 26, 26}
            }, stream.GetBlock(1, 1));

            Assert.AreEqual(new double[,]
            {
                {21, 22, 23, 24},
                {21, 22, 23, 24},
                {21, 22, 23, 24},
                {21, 22, 23, 24}
            }, stream.GetBlock(1, 0));

            Assert.AreEqual(new double[,]
            {
                {5, 6, 6, 6},
                {10, 11, 11, 11},
                {15, 16, 16, 16},
                {20, 21, 21, 21}
            }, stream.GetBlock(0, 1));
        }
    }
}