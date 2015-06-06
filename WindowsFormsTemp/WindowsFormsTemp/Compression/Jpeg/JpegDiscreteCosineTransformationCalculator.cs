using System;

namespace WindowsFormsTemp.Compression.Jpeg
{
    public class JpegDiscreteCosineTransformationCalculator
    {
        private const int BlockSize = 8;

        public static readonly JpegDiscreteCosineTransformationCalculator Instance =
            new JpegDiscreteCosineTransformationCalculator();

        private readonly double[,] _transitionMatrix;
        private readonly double[,] _transposedTransitionMatrix;

        private JpegDiscreteCosineTransformationCalculator()
        {
            _transitionMatrix = new double[BlockSize, BlockSize];
            _transposedTransitionMatrix = new double[BlockSize, BlockSize];
            for (var j = 0; j < BlockSize; ++j)
            {
                _transitionMatrix[0, j] = _transposedTransitionMatrix[j, 0] = 1.0/Math.Sqrt(8.0);
            }
            for (var i = 1; i < BlockSize; ++i)
            {
                for (var j = 0; j < BlockSize; ++j)
                {
                    _transitionMatrix[i, j] =
                        _transposedTransitionMatrix[j, i] = 0.5*Math.Cos((2.0*j + 1.0)*i*Math.PI/16.0);
                }
            }
        }

        public double[,] ForwardTransform(double[,] matrix)
        {
            return _transitionMatrix.Multiply(matrix).Multiply(_transposedTransitionMatrix);
        }

        public double[,] InverseTransform(double[,] matrix)
        {
            return _transposedTransitionMatrix.Multiply(matrix).Multiply(_transitionMatrix);
        }
    }

    public static class MatrixHelper
    {
        public static double[,] Multiply(this double[,] left, double[,] right)
        {
            if (left.GetLength(1) != right.GetLength(0))
                throw new ArgumentException("Bad matrix dimentions.");

            var n = left.GetLength(0);
            var l = left.GetLength(1);
            var m = right.GetLength(1);

            var result = new double[n, m];

            for (var i = 0; i < n; ++i)
            {
                for (var j = 0; j < m; ++j)
                {
                    result[i, j] = 0.0;
                    for (var k = 0; k < l; ++k)
                    {
                        result[i, j] += left[i, k]*right[k, j];
                    }
                }
            }

            return result;
        }
    }
}