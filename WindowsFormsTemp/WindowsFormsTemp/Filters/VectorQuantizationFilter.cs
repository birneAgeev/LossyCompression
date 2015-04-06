using System;
using System.Collections.Generic;
using System.Linq;
using WindowsFormsTemp.ImagePrimitives;

namespace WindowsFormsTemp.Filters
{
    public class VectorQuantizationFilter : IFilter
    {
        public static VectorQuantizationFilter Instance = new VectorQuantizationFilter();

        private VectorQuantizationFilter()
        {
        }

        public IBitmap Apply(IBitmap image, IFilterData filterData)
        {
            if (!(filterData is VectorQuantizationData))
                throw new ArgumentException("Filter data is not VectorQuantizationData!");

            var vectorQuantizationData = filterData as VectorQuantizationData;

            List<RgbColor> palete = GeneratePalete(image, vectorQuantizationData);

            if (palete.Count() < vectorQuantizationData.PaleteSize)
                return image;

            IBitmap result = new PlainBitmap(image.Width, image.Height);

            for (int column = 0; column < image.Width; ++column)
            {
                for (int row = 0; row < image.Height; ++row)
                {
                    result.SetPixel(row, column, GetNearest(image.GetPixel(row, column), palete));
                }
            }

            return result;
        }

        private static double Sqr(double a)
        {
            return a*a;
        }

        private static double Distance(RgbColor u, RgbColor v)
        {
            return Sqr(u.R - v.R) + Sqr(u.G - v.G) + Sqr(u.B - v.B);
        }

        private static RgbColor GetNearest(RgbColor pixel, List<RgbColor> palete)
        {
            double minDistance = 1e10;
            RgbColor bestColor = palete.First();

            foreach (RgbColor currentColor in palete)
            {
                double distance = Distance(currentColor, pixel);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    bestColor = currentColor;
                }
            }

            return bestColor;
        }

        private List<RgbColor> GeneratePalete(IBitmap image, VectorQuantizationData vectorQuantizationData)
        {
            List<RgbColor> palete = GetInitialPalete(image, vectorQuantizationData);

            if (palete.Count() < vectorQuantizationData.PaleteSize)
                return palete;

            double previousDistortion = CalculateAverageDistortion(image, palete);

            while (true)
            {
                List<RgbColor> oldPalete = palete;
                palete = Enumerable.Range(0, image.Height)
                                   .SelectMany(
                                       row =>
                                       Enumerable.Range(0, image.Width)
                                                 .Select(
                                                     column =>
                                                     image.GetPixel(row, column)))
                                   .AsParallel()
                                   .GroupBy(pixel => GetNearest(pixel, oldPalete))
                                   .AsParallel()
                                   .Select(GetCentroid)
                                   .ToList();
                double currentDistortion = CalculateAverageDistortion(image, palete);

                if (Math.Abs(previousDistortion - currentDistortion)/currentDistortion < 1e-1)
                    return palete;

                previousDistortion = currentDistortion;
            }
        }

        private static RgbColor GetCentroid(IEnumerable<RgbColor> group)
        {
            var rgbColors = @group as IList<RgbColor> ?? @group.ToList();
            int groupSize = rgbColors.Count();

            var sumR = 0;
            var sumG = 0;
            var sumB = 0;

            foreach (var color in rgbColors)
            {
                sumR += color.R;
                sumG += color.G;
                sumB += color.B;
            }

            return new RgbColor
                {
                    R = (byte) (sumR/groupSize),
                    G = (byte) (sumG/groupSize),
                    B = (byte) (sumB/groupSize),
                };
        }

        private double CalculateAverageDistortion(IBitmap image, List<RgbColor> palete)
        {
            double sum = 0.0;
            for (int column = 0; column < image.Width; ++column)
            {
                for (int row = 0; row < image.Height; ++row)
                {
                    RgbColor pixel = image.GetPixel(row, column);
                    RgbColor nearest = GetNearest(pixel, palete);
                    sum += Distance(nearest, pixel);
                }
            }
            sum /= image.Width*image.Height;
            return sum;
        }

        private static List<RgbColor> GetInitialPalete(IBitmap image, VectorQuantizationData vectorQuantizationData)
        {
            IOrderedEnumerable<Tuple<int, int>> frequencies = Enumerable.Range(0, image.Height)
                                                                        .SelectMany(
                                                                            row =>
                                                                            Enumerable.Range(0, image.Width)
                                                                                      .Select(
                                                                                          column =>
                                                                                          image.GetPixel(row, column)))
                                                                        .GroupBy(
                                                                            color =>
                                                                            (color.R << 16) + (color.G << 8) + color.B)
                                                                        .Select(
                                                                            group =>
                                                                            Tuple.Create(group.Key, group.Count()))
                                                                        .OrderBy(tuple => tuple.Item2);
            var palete = new List<RgbColor>();
            int currentPaleteSize = 0;
            foreach (var tuple in frequencies)
            {
                if (currentPaleteSize == vectorQuantizationData.PaleteSize)
                    return palete;
                var color = new RgbColor
                    {
                        R = (byte) (tuple.Item1 >> 16),
                        G = (byte) (tuple.Item1 << 16 >> 24),
                        B = (byte) (tuple.Item1 << 24 >> 24)
                    };
                palete.Add(color);
                ++currentPaleteSize;
            }

            return palete;
        }
    }
}