using System;
using WindowsFormsTemp.ImagePrimitives;

namespace WindowsFormsTemp.Calculator
{
    public class PsnrCalculator : IMetricCalculator
    {
        public static PsnrCalculator Instance = new PsnrCalculator();

        private PsnrCalculator()
        {
        }

        public double Calculate(IBitmap first, IBitmap second)
        {
            if (first.Width != second.Width ||
                first.Height != second.Height)
                throw new ArgumentException("Not equal imadge dimentions");
            double meanSquareError = 0.0;

            for (int column = 0; column < first.Width; ++column)
            {
                for (int row = 0; row < first.Height; ++row)
                {
                    RgbColor firstPixel = first.GetPixel(row, column);
                    RgbColor secondPixel = second.GetPixel(row, column);

                    meanSquareError += Square(firstPixel.R - secondPixel.R) +
                                       Square(firstPixel.G - secondPixel.G) +
                                       Square(firstPixel.B - secondPixel.B);
                }
            }
            meanSquareError /= 3.0*first.Width*first.Height;

            return Math.Log10(Square(255)/meanSquareError)*10.0;
        }

        private double Square(double a)
        {
            return a*a;
        }
    }
}