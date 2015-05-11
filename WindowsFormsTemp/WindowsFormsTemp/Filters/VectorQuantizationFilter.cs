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
            if (!(image is IBitmap<RgbPixel>))
                throw new ArgumentException("Image is not RGB.");
            var img = (IBitmap<RgbPixel>) image;

            if (!(filterData is VectorQuantizationData))
                throw new ArgumentException("Filter data is not VectorQuantizationData!");

            var vectorQuantizationData = filterData as VectorQuantizationData;

            List<RgbPixel> palete = GeneratePalete(img, vectorQuantizationData);

            if (palete.Count() < vectorQuantizationData.PaleteSize)
                return img;

            var result = new PlainBitmap<RgbPixel>(img.Width, img.Height);

            for (int column = 0; column < img.Width; ++column)
            {
                for (int row = 0; row < img.Height; ++row)
                {
                    result.SetPixel(row, column, GetNearest(img.GetPixel(row, column), palete));
                }
            }

            return result;
        }

        private static double Sqr(double a)
        {
            return a*a;
        }

        private static double Distance(RgbPixel u, RgbPixel v)
        {
            return Sqr(u.R - v.R) + Sqr(u.G - v.G) + Sqr(u.B - v.B);
        }

        private static RgbPixel GetNearest(RgbPixel pixel, List<RgbPixel> palete)
        {
            double minDistance = 1e10;
            RgbPixel bestColor = palete.First();

            foreach (RgbPixel currentColor in palete)
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

        private List<RgbPixel> GeneratePalete(IBitmap<RgbPixel> image, VectorQuantizationData vectorQuantizationData)
        {
            List<RgbPixel> palete = GetInitialPalete(image, vectorQuantizationData);

            if (palete.Count() < vectorQuantizationData.PaleteSize)
                return palete;

            double previousDistortion = CalculateAverageDistortion(image, palete);

            while (true)
            {
                List<RgbPixel> oldPalete = palete;
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

        private static RgbPixel GetCentroid(IEnumerable<RgbPixel> group)
        {
            IList<RgbPixel> rgbColors = @group as IList<RgbPixel> ?? @group.ToList();
            int groupSize = rgbColors.Count();

            int sumR = 0;
            int sumG = 0;
            int sumB = 0;

            foreach (RgbPixel color in rgbColors)
            {
                sumR += color.R;
                sumG += color.G;
                sumB += color.B;
            }

            return new RgbPixel
            {
                R = (byte) (sumR/groupSize),
                G = (byte) (sumG/groupSize),
                B = (byte) (sumB/groupSize),
            };
        }

        private double CalculateAverageDistortion(IBitmap<RgbPixel> image, List<RgbPixel> palete)
        {
            double sum = 0.0;
            for (int column = 0; column < image.Width; ++column)
            {
                for (int row = 0; row < image.Height; ++row)
                {
                    RgbPixel pixel = image.GetPixel(row, column);
                    RgbPixel nearest = GetNearest(pixel, palete);
                    sum += Distance(nearest, pixel);
                }
            }
            sum /= image.Width*image.Height;
            return sum;
        }

        private static List<RgbPixel> GetInitialPalete(IBitmap<RgbPixel> image,
            VectorQuantizationData vectorQuantizationData)
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
            var palete = new List<RgbPixel>();
            int currentPaleteSize = 0;
            foreach (var tuple in frequencies)
            {
                if (currentPaleteSize == vectorQuantizationData.PaleteSize)
                    return palete;
                var color = new RgbPixel
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