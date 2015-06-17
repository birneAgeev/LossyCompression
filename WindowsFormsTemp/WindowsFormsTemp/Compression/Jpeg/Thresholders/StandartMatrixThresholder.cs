using System;

namespace WindowsFormsTemp.Compression.Jpeg.Thresholders
{
    public class StandartMatrixThresholder : QuantizationMatrixThresholder
    {
        public new static StandartMatrixThresholder Instance = new StandartMatrixThresholder();

        private readonly short[,] crcbMatrix =
        {
            {17, 18, 24, 47, 99, 99, 99, 99},
            {18, 21, 26, 66, 99, 99, 99, 99},
            {24, 26, 56, 99, 99, 99, 99, 99},
            {47, 66, 99, 99, 99, 99, 99, 99},
            {99, 99, 99, 99, 99, 99, 99, 99},
            {99, 99, 99, 99, 99, 99, 99, 99},
            {99, 99, 99, 99, 99, 99, 99, 99},
            {99, 99, 99, 99, 99, 99, 99, 99}
        };

        private readonly short[,] yMatrix =
        {
            {16, 11, 10, 16, 24, 40, 51, 61},
            {12, 12, 14, 19, 26, 58, 60, 55},
            {14, 13, 16, 24, 40, 57, 69, 56},
            {14, 17, 22, 29, 51, 87, 80, 62},
            {18, 22, 37, 56, 68, 109, 103, 77},
            {24, 35, 55, 64, 81, 104, 113, 92},
            {49, 64, 78, 87, 103, 121, 120, 101},
            {72, 92, 95, 98, 112, 100, 103, 99}
        };

        private StandartMatrixThresholder()
        {
        }

        public override double[,] Threshold(double[,] matrix, IJpegThresholderSettings settings)
        {
            if (!(settings is StandartMatrixThresholderSettings))
                throw new ArgumentException("settings is not StandartMatrixThresholderSettings.");

            var curSettings = (StandartMatrixThresholderSettings) settings;

            QuantizationMatrix = GetMatrix(curSettings);

            return base.Threshold(matrix, curSettings);
        }

        public override double[,] Restore(double[,] matrix, IJpegThresholderSettings settings)
        {
            if (!(settings is StandartMatrixThresholderSettings))
                throw new ArgumentException("settings is not StandartMatrixThresholderSettings.");

            var curSettings = (StandartMatrixThresholderSettings) settings;

            QuantizationMatrix = GetMatrix(curSettings);

            return base.Restore(matrix, curSettings);
        }

        private short[,] GetMatrix(StandartMatrixThresholderSettings settings)
        {
            var matrix =
                (short[,]) (settings.StandartMatrixType == StandartMatrixType.Y ? yMatrix.Clone() : crcbMatrix.Clone());

            if (settings.Divisor != 1)
            {
                for (var i = 0; i < QuantizationMatrixSize; ++i)
                {
                    for (var j = 0; j < QuantizationMatrixSize; ++j)
                    {
                        matrix[i, j] = (short) (matrix[i, j]/settings.Divisor);
                    }
                }
            }

            return matrix;
        }
    }
}