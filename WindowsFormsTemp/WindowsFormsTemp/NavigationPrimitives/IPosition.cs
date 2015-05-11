namespace WindowsFormsTemp.NavigationPrimitives
{
    public interface IPosition
    {
        int Row { get; set; }
        int Column { get; set; }
        IPosition Add(ISpan span);
        IPosition Subtract(ISpan span);
        ISpan Subtract(IPosition other);
    }
}