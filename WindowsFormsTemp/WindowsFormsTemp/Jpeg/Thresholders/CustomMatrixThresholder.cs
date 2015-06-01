using System;
using System.Collections.Generic;

namespace WindowsFormsTemp.Jpeg.Thresholders
{
    public class CustomMatrixThresholder : QuantizationMatrixThresholder
    {
        public new static CustomMatrixThresholder Instance = new CustomMatrixThresholder();
        private readonly Dictionary<CustomMatrixThresholderSettings, short[,]> cache;

        private CustomMatrixThresholder()
        {
            cache = new Dictionary<CustomMatrixThresholderSettings, short[,]>();
        }

        public override double[,] Threshold(double[,] matrix, IJpegThresholderSettings settings)
        {
            if (!(settings is CustomMatrixThresholderSettings))
                throw new ArgumentException("settings is not CustomMatrixThresholderSettings.");

            var curSettings = (CustomMatrixThresholderSettings) settings;

            QuantizationMatrix = GetMatrix(curSettings);

            return base.Threshold(matrix, curSettings);
        }

        public override double[,] Restore(double[,] matrix, IJpegThresholderSettings settings)
        {
            if (!(settings is CustomMatrixThresholderSettings))
                throw new ArgumentException("settings is not CustomMatrixThresholderSettings.");

            var curSettings = (CustomMatrixThresholderSettings) settings;

            QuantizationMatrix = GetMatrix(curSettings);

            return base.Restore(matrix, curSettings);
        }

        private short[,] GetMatrix(CustomMatrixThresholderSettings settings)
        {
            short[,] result;
            if (cache.TryGetValue(settings, out result))
                return result;

            result = new short[QuantizationMatrixSize, QuantizationMatrixSize];
            for (int i = 0; i < QuantizationMatrixSize; ++i)
            {
                for (int j = 0; j < QuantizationMatrixSize; ++j)
                {
                    result[i, j] = (short)(settings.Alpha*(1 + settings.Gamma*(i + j + 2)));
                }
            }

            cache.Add(settings, result);
            return result;
        }
    }
}