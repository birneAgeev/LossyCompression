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

            var palete = GeneratePalete(img, vectorQuantizationData);

            if (palete.Count() < vectorQuantizationData.PaleteSize)
                return img;

            var result = new PlainBitmap<RgbPixel>(img.Width, img.Height);

            for (var column = 0; column < img.Width; ++column)
            {
                for (var row = 0; row < img.Height; ++row)
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
            var minDistance = 1e10;
            var bestColor = palete.First();

            foreach (var currentColor in palete)
            {
                var distance = Distance(currentColor, pixel);
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
            var palete = GetInitialPalete(image, vectorQuantizationData);

            if (palete.Count() < vectorQuantizationData.PaleteSize)
                return palete;

            var previousDistortion = CalculateAverageDistortion(image, palete);

            while (true)
            {
                var oldPalete = palete;
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
                var currentDistortion = CalculateAverageDistortion(image, palete);

                if (Math.Abs(previousDistortion - currentDistortion)/currentDistortion < 1e-1)
                    return palete;

                previousDistortion = currentDistortion;
            }
        }

        private static RgbPixel GetCentroid(IEnumerable<RgbPixel> group)
        {
            var rgbColors = @group as IList<RgbPixel> ?? @group.ToList();
            var groupSize = rgbColors.Count();

            var sumR = 0;
            var sumG = 0;
            var sumB = 0;

            foreach (var color in rgbColors)
            {
                sumR += color.R;
                sumG += color.G;
                sumB += color.B;
            }

            return new RgbPixel
            {
                R = (byte) (sumR/groupSize),
                G = (byte) (sumG/groupSize),
                B = (byte) (sumB/groupSize)
            };
        }

        private double CalculateAverageDistortion(IBitmap<RgbPixel> image, List<RgbPixel> palete)
        {
            var sum = 0.0;
            for (var column = 0; column < image.Width; ++column)
            {
                for (var row = 0; row < image.Height; ++row)
                {
                    var pixel = image.GetPixel(row, column);
                    var nearest = GetNearest(pixel, palete);
                    sum += Distance(nearest, pixel);
                }
            }
            sum /= image.Width*image.Height;
            return sum;
        }

        private static List<RgbPixel> GetInitialPalete(IBitmap<RgbPixel> image,
            VectorQuantizationData vectorQuantizationData)
        {
            var frequencies = Enumerable.Range(0, image.Height)
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
            var currentPaleteSize = 0;
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