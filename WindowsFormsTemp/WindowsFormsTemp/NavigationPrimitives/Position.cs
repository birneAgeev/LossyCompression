namespace WindowsFormsTemp.NavigationPrimitives
{
    public class Position : IPosition
    {
        public int Row { get; set; }
        public int Column { get; set; }

        public Position()
        {
            Row = Column = 0;
        }

        public Position(int row, int column)
        {
            Row = row;
            Column = column;
        }

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