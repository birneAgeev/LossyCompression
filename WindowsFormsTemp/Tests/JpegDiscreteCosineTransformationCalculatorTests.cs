using System;
using WindowsFormsTemp.Jpeg;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class JpegDiscreteCosineTransformationCalculatorTests
    {
        [Test]
        public void Test()
        {
            var matrix = new double[,]
            {
                {2, 4, 2, 4, 2, 4, 2, 4},
                {4, 2, 4, 2, 4, 2, 4, 2},
                {2, 4, 2, 4, 2, 4, 2, 4},
                {4, 2, 4, 2, 4, 2, 4, 2},
                {2, 4, 2, 4, 2, 4, 2, 4},
                {4, 2, 4, 2, 4, 2, 4, 2},
                {2, 4, 2, 4, 2, 4, 2, 4},
                {4, 2, 4, 2, 4, 2, 4, 2}
            };

            var result = JpegDiscreteCosineTransformationCalculator.Instance.ForwardTransform(matrix);

            for (var i = 0; i < 8; ++i)
            {
                for (var j = 0; j < 8; ++j)
                {
                    Console.Write(@"{0:0.00}", result[i, j]);
                    Console.Write(@" ");
                }
                Console.WriteLine();
            }
        }
    }
}