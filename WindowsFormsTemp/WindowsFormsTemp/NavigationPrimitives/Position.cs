namespace WindowsFormsTemp.NavigationPrimitives
{
    public class Position : IPosition
    {
        public int Row { get; set; }
        public int Column { get; set; }

        public IPosition Add(ISpan span)
        {
            return new Position
            {
                Column = Column + span.HorizontalSpan,
                Row = Row + span.VerticalSpan
            };
        }

        public IPosition Subtract(ISpan span)
        {
            return new Position
            {
                Column = Column - span.HorizontalSpan,
                Row = Row - span.VerticalSpan
            };
        }

        public ISpan Subtract(IPosition other)
        {
            return new Span
            {
                HorizontalSpan = Row - other.Row,
                VerticalSpan = Column - other.Column
            };
        }
    }
}