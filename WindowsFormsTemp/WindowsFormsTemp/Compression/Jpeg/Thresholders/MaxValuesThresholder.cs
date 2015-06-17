using System;
using System.Linq;

namespace WindowsFormsTemp.Compression.Jpeg.Thresholders
{
    public class MaxValuesThresholder : IJpegThresholder
    {
        public static MaxValuesThresholder Instance = new MaxValuesThresholder();

        private MaxValuesThresholder()
        {
        }

        public double[,] Threshold(double[,] matrix, IJpegThresholderSettings settings)
        {
            if (!(settings is MaxValuesThresholderSettings))
                throw new ArgumentException("settings id not MaxValuesThresholderSettings");

            var curSettings = (MaxValuesThresholderSettings) settings;

            var max = matrix.Cast<double>().OrderBy(x => -Math.Abs(x)).Skip(curSettings.MaxCount).FirstOrDefault();

            var result = new double[matrix.GetLength(0), matrix.GetLength(1)];
            for (var i = 0; i < matrix.GetLength(0); ++i)
            {
                for (var j = 0; j < matrix.GetLength(1); ++j)
                {
                    result[i, j] = matrix[i, j];
                    if (Math.Abs(result[i, j]) < max)
                        result[i, j] = 0.0;
                }
            }

            return result;
        }

        public double[,] Restore(double[,] matrix, IJpegThresholderSettings settings)
        {
            return matrix;
        }
    }
}