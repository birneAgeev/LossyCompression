using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class TempTests
    {
        private IEnumerable<int> ZigZag(int[,] matrix)
        {
            var n = matrix.GetLength(0);
            for (var i = 0; i < n; ++i)
            {
                var x = 0;
                var y = i;
                while (y >= 0)
                {
                    yield return matrix[y, x];
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
                    yield return matrix[y, x];
                    ++x;
                    --y;
                }
            }
        }

        [Test]
        public void Test()
        {
            var matrix = new[,]
            {
                {1, 3, 6},
                {2, 5, 9},
                {4, 8, 12}
            };

            foreach (var x in ZigZag(matrix))
            {
                Console.Write(x);
                Console.Write(" ");
            }
        }
    }
}