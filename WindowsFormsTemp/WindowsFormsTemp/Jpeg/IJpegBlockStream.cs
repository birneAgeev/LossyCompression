namespace WindowsFormsTemp.Jpeg
{
    public interface IJpegBlockStream
    {
        int WidthInBlocks { get; }
        int HeightInBlocks { get; }
        int BlockSize { get; }
        float[,] GetBlock(int row, int column);
    }
}