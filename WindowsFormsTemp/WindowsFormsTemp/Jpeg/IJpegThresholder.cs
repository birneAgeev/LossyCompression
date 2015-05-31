namespace WindowsFormsTemp.Jpeg
{
    public interface IJpegThresholder
    {
        double[,] Threshold(double[,] matrix, IJpegThresholderSettings settings);
    }

    public interface IJpegThresholderSettings
    {
    }

    public static class JpegThresholderExtensions
    {
        public static double[,] Apply(
            this double[,] matrix,
            IJpegThresholder thresholder,
            IJpegThresholderSettings settings = null)
        {
            return thresholder.Threshold(matrix, settings);
        }
    }
}