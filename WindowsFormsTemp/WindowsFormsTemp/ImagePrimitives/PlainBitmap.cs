﻿using WindowsFormsTemp.NavigationPrimitives;

namespace WindowsFormsTemp.ImagePrimitives
{
    public class PlainBitmap<TPixel> : IBitmap<TPixel> where TPixel : IPixel
    {
        private readonly TPixel[,] data;

        public PlainBitmap(int width, int height)
        {
            Width = width;
            Height = height;
            data = new TPixel[Height, Width];
        }

        public int Width { get; private set; }
        public int Height { get; private set; }

        public TPixel GetPixel(IPosition position)
        {
            return data[position.Row, position.Column];
        }

        public TPixel GetPixel(int row, int column)
        {
            return data[row, column];
        }

        public void SetPixel(IPosition position, TPixel color)
        {
            data[position.Row, position.Column] = color;
        }

        public void SetPixel(int row, int column, TPixel color)
        {
            data[row, column] = color;
        }

        public IBitmap<RgbPixel> ToRgbBitmap()
        {
            if (typeof (TPixel) == typeof (RgbPixel))
                return this as IBitmap<RgbPixel>;

            var result = new PlainBitmap<RgbPixel>(Width, Height);
            for (var row = 0; row < Height; ++row)
            {
                for (var column = 0; column < Width; ++column)
                {
                    var pixel = GetPixel(new Position
                    {
                        Column = column,
                        Row = row
                    }).ToRgb();
                    result.SetPixel(row, column, pixel);
                }
            }
            return result;
        }
    }
}