using System;
using System.Collections.Generic;
using System.Linq;

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
            for (int i = 0; i < height; ++i)
            {
                matrix.SetRow(Convolute(matrix.GetRow(i), Transformations[order]), i);
            }
            for (int i = 0; i < width; ++i)
            {
                matrix.SetColumn(Convolute(matrix.GetColumn(i), Transformations[order]), i);
            }
        }

        private static double[] Convolute(double[] vector, double[] transform, int delta = 0)
        {
            var result = new double[vector.Length];
            double[] lowFilter = transform;
            double[] highFilter = GetHighFilter(transform);

            for (int i = 0; i < vector.Length; i += 2)
            {
                double sumLow = 0.0;
                double sumHigh = 0.0;
                for (int j = 0; j < transform.Length; ++j)
                {
                    int index = (i + j - delta + transform.Length)%transform.Length; //out of range
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
            int pow = 1;
            for (int i = 0; i < transform.Length; ++i)
            {
                result[i] = pow*transform[transform.Length - i - 1];
                pow *= -1;
            }

            return result;
        }
    }

    public static class MatrixHelper //перепилить на локальные индексы
    {
        public static double[] GetRow(this double[,] matrix, int rowIndex)
        {
            int width = matrix.GetLength(1);
            return matrix.Cast<double>().Skip(width*rowIndex).Take(width).ToArray();
        }

        public static double[] GetColumn(this double[,] matrix, int columnIndex)
        {
            int height = matrix.GetLength(0);
            var result = new double[height];
            for (int i = 0; i < height; ++i)
            {
                result[i] = matrix[i, columnIndex];
            }
            return result;
        }

        public static void SetRow(this double[,] matrix, double[] row, int rowIndex)
        {
            if (matrix.GetLength(1) != row.Length)
                throw new Exception("Non equal dimetions");

            for (int i = 0; i < row.Length; ++i)
            {
                matrix[rowIndex, i] = row[i];
            }
        }

        public static void SetColumn(this double[,] matrix, double[] column, int columnIndex)
        {
            if (matrix.GetLength(0) != column.Length)
                throw new Exception("Non equal dimetions");

            for (int i = 0; i < column.Length; ++i)
            {
                matrix[i, columnIndex] = column[i];
            }
        }
    }
}