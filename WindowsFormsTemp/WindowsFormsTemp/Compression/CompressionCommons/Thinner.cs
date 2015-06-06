using System;
using System.Collections.Generic;
using WindowsFormsTemp.Compression.Jpeg;
using WindowsFormsTemp.ImagePrimitives;

namespace WindowsFormsTemp.Compression.CompressionCommons
{
    public class Thinner
    {
        public static readonly Thinner Instance = new Thinner();

        private Thinner()
        {
        }

        public ThinnerResult ThinOut(IBitmap bitmap, ThinningMode thinningMode)
        {
            var bmp = bitmap.ToYCrCbBitmap();
            return new ThinnerResult
            {
                ThinningMode = thinningMode,
                ImageData = new SeparatedYCrCb
                {
                    Y = ThinOutComponent(bmp, "Y", ThinningMode.None),
                    Cr = ThinOutComponent(bmp, "Cr", thinningMode),
                    Cb = ThinOutComponent(bmp, "Cb", thinningMode)
                }
            };
        }

        public IBitmap Decompress(ThinnerResult compressedData)
        {
            var height = compressedData.ImageData.Y.GetLength(0);
            var width = compressedData.ImageData.Y.GetLength(1);
            var result = new PlainBitmap<YCrCbPixel>(width, height);

            for (var row = 0; row < height; ++row)
            {
                for (var column = 0; column < width; ++column)
                {
                    var pixel = DecompressPixel(row, column, compressedData);
                    result.SetPixel(row, column, pixel);
                }
            }

            return result;
        }

        private static YCrCbPixel DecompressPixel(int row, int column, ThinnerResult compressedData)
        {
            var mode = compressedData.ThinningMode;
            var heightDivider = ModeToDividers[mode].Item1;
            var widthDivider = ModeToDividers[mode].Item2;

            return new YCrCbPixel
            {
                Y = ToByte(compressedData.ImageData.Y[row, column]),
                Cr = ToByte(compressedData.ImageData.Cr[row/heightDivider, column/widthDivider]),
                Cb = ToByte(compressedData.ImageData.Cb[row/heightDivider, column/widthDivider])
            };
        }

        private static byte ToByte(double a)
        {
            var integer = (int) Math.Round(a);
            if (integer > 255)
                return 255;
            if (integer < 0)
                return 0;
            return (byte) integer;
        }

        private static double[,] ThinOutComponent(IBitmap<YCrCbPixel> bitmap, string componentName,
            ThinningMode thinningMode)
        {
            var heightDivider = ModeToDividers[thinningMode].Item1;
            var widthDivider = ModeToDividers[thinningMode].Item2;
            var height = (int) Math.Ceiling((double) bitmap.Height/heightDivider);
            var width = (int) Math.Ceiling((double) bitmap.Width/widthDivider);
            var result = new double[height, width];
            var template = ModeToTemplate[thinningMode];

            for (var row = 0; row < bitmap.Height; row += 2)
            {
                for (var column = 0; column < bitmap.Width; column += 2)
                {
                    for (var i = 0; i < 2; ++i)
                    {
                        for (var j = 0; j < 2; ++j)
                        {
                            if (!template[i][j] || !InBounds(bitmap, row + i, column + j))
                                continue;
                            var pixel = bitmap.GetPixel(row + i, column + j);
                            result[(row + i)/heightDivider, (column + j)/widthDivider] =
                                (byte) pixel.GetType() //трэш-угар. Если поменять тип в YCrCbPixel, то всё сдохнет
                                    .GetProperty(componentName)
                                    .GetValue(pixel, null);
                        }
                    }
                }
            }

            return result;
        }

        private static bool InBounds(IBitmap bitmap, int row, int column)
        {
            return row >= 0 && row < bitmap.Height &&
                   column >= 0 && column < bitmap.Width;
        }

        #region Thinning Mode Constants

        public static readonly Dictionary<ThinningMode, Tuple<int, int>> ModeToDividers = new Dictionary
            <ThinningMode, Tuple<int, int>>
        {
            {ThinningMode.None, Tuple.Create(1, 1)},
            {ThinningMode._1H2V, Tuple.Create(2, 1)},
            {ThinningMode._2H1V, Tuple.Create(1, 2)},
            {ThinningMode._2H2V, Tuple.Create(2, 2)}
        };

        private static readonly Dictionary<ThinningMode, bool[][]> ModeToTemplate = new Dictionary
            <ThinningMode, bool[][]>
        {
            {
                ThinningMode.None, new[]
                {
                    new[] {true, true},
                    new[] {true, true}
                }
            },
            {
                ThinningMode._1H2V, new[]
                {
                    new[] {true, true},
                    new[] {false, false}
                }
            },
            {
                ThinningMode._2H1V, new[]
                {
                    new[] {true, false},
                    new[] {true, false}
                }
            },
            {
                ThinningMode._2H2V, new[]
                {
                    new[] {true, false},
                    new[] {false, false}
                }
            }
        };

        #endregion
    }
}