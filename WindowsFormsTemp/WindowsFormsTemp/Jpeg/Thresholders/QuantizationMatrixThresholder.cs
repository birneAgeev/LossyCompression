using System;

namespace WindowsFormsTemp.Jpeg.Thresholders
{
    public class QuantizationMatrixThresholder : IJpegThresholder
    {
        public static QuantizationMatrixThresholder Instance = new QuantizationMatrixThresholder();

        protected short[,] QuantizationMatrix;
        protected const int QuantizationMatrixSize = 8;

        protected QuantizationMatrixThresholder()
        {
            QuantizationMatrix = new short[QuantizationMatrixSize, QuantizationMatrixSize];
            for (var i = 0; i < QuantizationMatrixSize; ++i)
            {
                for (var j = 0; j < QuantizationMatrixSize; ++j)
                {
                    QuantizationMatrix[i, j] = 1;
                }
            }
        }

        public virtual double[,] Threshold(double[,] matrix, IJpegThresholderSettings settings)
        {
            var result = new double[QuantizationMatrixSize, QuantizationMatrixSize];
            for (var i = 0; i < QuantizationMatrixSize; ++i)
            {
                for (var j = 0; j < QuantizationMatrixSize; ++j)
                {
// ReSharper disable once PossibleLossOfFraction
                    result[i, j] = (short)Math.Round(matrix[i, j])/QuantizationMatrix[i, j];
                }
            }

            return result;
        }

        public virtual double[,] Restore(double[,] matrix, IJpegThresholderSettings settings)
        {
            var result = new double[QuantizationMatrixSize, QuantizationMatrixSize];
            for (var i = 0; i < QuantizationMatrixSize; ++i)
            {
                for (var j = 0; j < QuantizationMatrixSize; ++j)
                {
                    result[i, j] = matrix[i, j]*QuantizationMatrix[i, j];
                }
            }

            return result;
        }
    }
}