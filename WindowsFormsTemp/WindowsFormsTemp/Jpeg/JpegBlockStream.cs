using System;

namespace WindowsFormsTemp.Jpeg
{
    public class JpegBlockStream : IJpegBlockStream
    {
        private readonly float[,] _source;
        private readonly int _sourceHeight;
        private readonly int _sourceWidth;

        public JpegBlockStream(float[,] source, int blockSize = 8)
        {
            BlockSize = blockSize;
            _source = source;
            _sourceHeight = source.GetLength(0);
            _sourceWidth = source.GetLength(1);
            HeightInBlocks = (int) Math.Ceiling((double) _sourceHeight/blockSize);
            WidthInBlocks = (int) Math.Ceiling((double) _sourceWidth/blockSize);
        }

        public int WidthInBlocks { get; private set; }
        public int HeightInBlocks { get; private set; }
        public int BlockSize { get; private set; }

        public float[,] GetBlock(int row, int column)
        {
            var startRowInSource = row*BlockSize;
            var startColumnInSource = column*BlockSize;
            var result = new float[BlockSize, BlockSize];

            var lastInRow = new float[BlockSize];
            var lastInColumn = new float[BlockSize];
            for (var i = 0; i < BlockSize; ++i)
            {
                lastInRow[i] = float.NaN;
                lastInColumn[i] = float.NaN;
            }

            for (var rowInSource = startRowInSource; rowInSource < startRowInSource + BlockSize; ++rowInSource)
            {
                for (var columnInSource = startColumnInSource;
                    columnInSource < startColumnInSource + BlockSize;
                    ++columnInSource)
                {
                    var r = rowInSource - startRowInSource;
                    var c = columnInSource - startColumnInSource;

                    float value;
                    if (InBounds(rowInSource, columnInSource))
                    {
                        value = _source[rowInSource, columnInSource];
                    }
                    else
                    {
                        if (rowInSource >= _sourceHeight && !float.IsNaN(lastInColumn[c]))
                        {
                            value = lastInColumn[c];
                        }
                        else
                        {
                            value = lastInRow[r];
                        }
                    }
                    result[r, c] = value;
                    lastInRow[r] = value;
                    lastInColumn[c] = value;
                }
            }

            return result;
        }

        private bool InBounds(int row, int column)
        {
            return row >= 0 && row < _sourceHeight &&
                   column >= 0 && column < _sourceWidth;
        }
    }
}