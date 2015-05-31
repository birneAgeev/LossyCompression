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
            int n = matrix.GetLength(0);
            for (int i = 0; i < n; ++i)
            {
                int x = 0;
                int y = i;
                while (y >= 0)
                {
                    yield return matrix[y, x];
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
                {4, 8, 12},
            };

            foreach (var x in ZigZag(matrix))
            {
                Console.Write(x);
                Console.Write(" ");
            }
        }
    }
}