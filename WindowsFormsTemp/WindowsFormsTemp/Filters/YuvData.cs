namespace WindowsFormsTemp.Filters
{
    public class YuvData : IFilterData
    {
        public byte YQuantizationDegree { get; set; }
        public byte UQuantizationDegree { get; set; }
        public byte VQuantizationDegree { get; set; }
    }
}