namespace WindowsFormsTemp.Jpeg
{
    public interface IJpegBlockStream
    {
        int WidthInBlocks { get; }
        int HeightInBlocks { get; }
        int BlockSize { get; }
        double[,] GetBlock(int row, int column);
    }
}