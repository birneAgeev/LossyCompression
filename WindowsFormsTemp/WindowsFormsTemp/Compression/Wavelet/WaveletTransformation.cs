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
            {4, new[] {0.6830127, 1.1830127, 0.3169873, -0.1830127}},
            {6, new[] {0.47046721, 1.14111692, 0.650365, -0.19093442, -0.12083221, 0.0498175}},
            {8, new[] {0.32580343, 1.01094572, 0.8922014, -0.03957503, -0.26450717, 0.0436163, 0.0465036, -0.01498699}},
            {20, new[] {0.03771716, 0.26612218, 0.74557507, 0.97362811, 0.39763774, -0.35333620, -0.27710988, 0.18012745, 0.13160299, -0.10096657, -0.04165925, 0.04696981, 5.10043697e-3, -0.01517900, 1.97332536e-3, 2.81768659e-3, -9.69947840e-4, -1.64709006e-4, 1.32354367e-4, -1.875841e-5}}
        };

        private WaveletTransformation()
        {
        }

        public void Transform(double[,] matrix, int width, int height, int order)
        {
            for (var i = 0; i < height; ++i)
            {
                var row = Reorder(Convolute(matrix.GetRow(i, width, height), Transformations[order]));
                matrix.SetRow(row, i);
            }
            for (var i = 0; i < width; ++i)
            {
                var column = Reorder(Convolute(matrix.GetColumn(i, width, height), Transformations[order]));
                matrix.SetColumn(column, i);
            }
        }

        public void Inverse(double[,] matrix, int width, int height, int order)
        {
            for (var i = 0; i < width; ++i)
            {
                var column = Rereorder(matrix.GetColumn(i, width, height));
                matrix.SetColumn(Convolute(column, Transformations[order], true), i);
            }
            for (var i = 0; i < height; ++i)
            {
                var row = Rereorder(matrix.GetRow(i, width, height));
                matrix.SetRow(Convolute(row, Transformations[order], true), i);
            }
        }

        private static double[] Reorder(double[] vector)
        {
            var result = new double[vector.Length];
            for (var i = 0; i < vector.Length; i += 2)
            {
                result[i >> 1] = vector[i];
                result[(vector.Length + i) >> 1] = vector[i + 1];
            }
            return result;
        }

        private static double[] Rereorder(double[] vector)
        {
            var result = new List<double>();
            for (var i = 0; i < (vector.Length >> 1); ++i)
            {
                result.Add(vector[i]);
                result.Add(vector[(vector.Length >> 1) + i]);
            }
            return result.ToArray();
        }

        private static double[] Convolute(double[] vector, double[] transform, bool inverse = false)
        {
            var result = new double[vector.Length];
            var lowFilter = transform;
            var highFilter = GetHighFilter(transform);
            var delta = 0;
            var multiplier = 0.5;

            if (inverse)
            {
                delta = lowFilter.Length - 2;
                var invFilters = GetInverseFilters(lowFilter, highFilter);
                lowFilter = invFilters.Item1;
                highFilter = invFilters.Item2;
                multiplier = 1.0;
            }

            for (var i = 0; i < vector.Length; i += 2)
            {
                var sumLow = 0.0;
                var sumHigh = 0.0;
                for (var j = 0; j < transform.Length; ++j)
                {
                    var index = (i + j - delta + vector.Length)%vector.Length; //out of range
                    while (index < 0)
                        index += vector.Length;
                    sumLow += vector[index]*lowFilter[j];
                    sumHigh += vector[index]*highFilter[j];
                }
                result[i] = sumLow*multiplier;
                result[i + 1] = sumHigh*multiplier;
            }

            return result;
        }

        private static Tuple<double[], double[]> GetInverseFilters(double[] lowFilter, double[] highFilter)
        {
            var invLow = new List<double>();
            var invHigh = new List<double>();
            for (var i = 0; i < lowFilter.Length; i += 2)
            {
                var lowIndex = lowFilter.Length - i - 2;
                var highIndex = highFilter.Length - i - 1;
                invLow.Add(lowFilter[lowIndex]);
                invLow.Add(highFilter[lowIndex]);
                invHigh.Add(lowFilter[highIndex]);
                invHigh.Add(highFilter[highIndex]);
            }
            return Tuple.Create(invLow.ToArray(), invHigh.ToArray());
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