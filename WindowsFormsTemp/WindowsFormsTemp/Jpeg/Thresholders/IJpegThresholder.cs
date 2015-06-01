namespace WindowsFormsTemp.Jpeg.Thresholders
{
    public interface IJpegThresholder
    {
        double[,] Threshold(double[,] matrix, IJpegThresholderSettings settings);
        double[,] Restore(double[,] matrix, IJpegThresholderSettings settings);
    }

    public static class JpegThresholderExtensions
    {
        public static double[,] Threshold(
            this double[,] matrix,
            IJpegThresholder thresholder,
            IJpegThresholderSettings settings = null)
        {
            return thresholder.Threshold(matrix, settings);
        }

        public static double[,] Restore(
            this double[,] matrix,
            IJpegThresholder thresholder,
            IJpegThresholderSettings settings = null)
        {
            return thresholder.Restore(matrix, settings);
        }
    }
}