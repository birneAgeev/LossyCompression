using System;

namespace WindowsFormsTemp.Compression.Jpeg.Thresholders
{
    public class GeneralizedThresholder : IJpegThresholder
    {
        public static GeneralizedThresholder Instance = new GeneralizedThresholder();

        private GeneralizedThresholder()
        {
        }

        public double[,] Threshold(double[,] matrix, IJpegThresholderSettings settings)
        {
            if (!(settings is GeneralizedThresholderSettings))
                throw new ArgumentException("settings is not GeneralizedThresholderSettings.");

            var curSettings = (GeneralizedThresholderSettings) settings;

            if (curSettings.ThresholderType == ThresholderType.CustomMatrixThresholder)
                return matrix.Threshold(CustomMatrixThresholder.Instance,
                    curSettings.CustomMatrixThresholderSettings);
            if (curSettings.ThresholderType == ThresholderType.MaxValuesThresholder)
                return matrix.Threshold(MaxValuesThresholder.Instance,
                    curSettings.MaxValuesThresholderSettings);
            if (curSettings.ThresholderType == ThresholderType.StandartMatrixThresholder)
                return matrix.Threshold(StandartMatrixThresholder.Instance,
                    curSettings.StandartMatrixThresholderSettings);

            return matrix;
        }

        public double[,] Restore(double[,] matrix, IJpegThresholderSettings settings)
        {
            if (!(settings is GeneralizedThresholderSettings))
                throw new ArgumentException("settings is not GeneralizedThresholderSettings.");

            var curSettings = (GeneralizedThresholderSettings) settings;

            if (curSettings.ThresholderType == ThresholderType.CustomMatrixThresholder)
                return matrix.Restore(CustomMatrixThresholder.Instance,
                    curSettings.CustomMatrixThresholderSettings);
            if (curSettings.ThresholderType == ThresholderType.MaxValuesThresholder)
                return matrix.Restore(MaxValuesThresholder.Instance,
                    curSettings.MaxValuesThresholderSettings);
            if (curSettings.ThresholderType == ThresholderType.StandartMatrixThresholder)
                return matrix.Restore(StandartMatrixThresholder.Instance,
                    curSettings.StandartMatrixThresholderSettings);

            return matrix;
        }
    }
}