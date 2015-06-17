using System;
using System.Collections.Generic;

namespace WindowsFormsTemp.Compression.Wavelet
{
    public class WaveletTransformation
    {
        public static readonly WaveletTransformation Instance = new WaveletTransformation();

        private static readonly Dictionary<int, double[]> Transformations = new Dictionary<int, double[]>
        {
            {2, new[] {1.0, 1.0}},
            {4, new[] {0.6830127, 1.1830127, 0.3169873, -0.1830127}}
        };

        private WaveletTransformation()
        {
        }

        public void Transform(double[,] matrix, int width, int height, int order)
        {
            for (var i = 0; i < height; ++i)
            {
                matrix.SetRow(Convolute(matrix.GetRow(i, width, height), Transformations[order]), i);
            }
            for (var i = 0; i < width; ++i)
            {
                matrix.SetColumn(Convolute(matrix.GetColumn(i, width, height), Transformations[order]), i);
            }
        }

        private static double[] Convolute(double[] vector, double[] transform, int delta = 0)
        {
            var result = new double[vector.Length];
            var lowFilter = transform;
            var highFilter = GetHighFilter(transform);

            for (var i = 0; i < vector.Length; i += 2)
            {
                var sumLow = 0.0;
                var sumHigh = 0.0;
                for (var j = 0; j < transform.Length; ++j)
                {
                    var index = (i + j - delta + vector.Length)%vector.Length; //out of range
                    sumLow += vector[index]*lowFilter[j];
                    sumHigh += vector[index]*highFilter[j];
                }
                result[i >> 1] = sumLow/2.0;
                result[(vector.Length >> 1) + (i >> 1)] = sumHigh/2.0;
            }

            return result;
        }

        private static double[] GetHighFilter(double[] transform)
        {
            var result = new double[transform.Length];
            var pow = 1;
            for (var i = 0; i < transform.Length; ++i)
            {
                result[i] = pow*transform[transform.Length - i - 1];
                pow *= -1;
            }

            return result;
        }
    }

    public static class MatrixHelper
    {
        public static double[] GetRow(this double[,] matrix, int rowIndex, int width, int height)
        {
            var result = new double[width];
            for (var i = 0; i < width; ++i)
            {
                result[i] = matrix[rowIndex, i];
            }
            return result;
        }

        public static double[] GetColumn(this double[,] matrix, int columnIndex, int width, int height)
        {
            var result = new double[height];
            for (var i = 0; i < height; ++i)
            {
                result[i] = matrix[i, columnIndex];
            }
            return result;
        }

        public static void SetRow(this double[,] matrix, double[] row, int rowIndex)
        {
            for (var i = 0; i < row.Length; ++i)
            {
                matrix[rowIndex, i] = row[i];
            }
        }

        public static void SetColumn(this double[,] matrix, double[] column, int columnIndex)
        {
            for (var i = 0; i < column.Length; ++i)
            {
                matrix[i, columnIndex] = column[i];
            }
        }
    }
}