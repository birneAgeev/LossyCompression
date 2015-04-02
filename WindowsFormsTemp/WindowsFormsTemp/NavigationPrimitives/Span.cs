namespace WindowsFormsTemp.NavigationPrimitives
{
    public class Span : ISpan
    {
        public int HorizontalSpan { get; set; }
        public int VerticalSpan { get; set; }

        public ISpan Add(ISpan other)
        {
            return new Span
            {
                HorizontalSpan = HorizontalSpan + other.HorizontalSpan,
                VerticalSpan = VerticalSpan + other.VerticalSpan
            };
        }

        public ISpan Subtract(ISpan other)
        {
            return new Span
            {
                HorizontalSpan = HorizontalSpan - other.HorizontalSpan,
                VerticalSpan = VerticalSpan - other.VerticalSpan
            };
        }
    }
}