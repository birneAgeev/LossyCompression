using WindowsFormsTemp.ImagePrimitives;

namespace WindowsFormsTemp.Calculator
{
    public interface IMetricCalculator
    {
        double Calculate(IBitmap first, IBitmap second);
    }
}