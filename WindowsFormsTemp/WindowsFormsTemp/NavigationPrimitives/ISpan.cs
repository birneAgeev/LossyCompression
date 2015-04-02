namespace WindowsFormsTemp.NavigationPrimitives
{
    public interface ISpan
    {
        int HorizontalSpan { get; set; }
        int VerticalSpan { get; set; }

        ISpan Add(ISpan other);
        ISpan Subtract(ISpan other);
    }
}