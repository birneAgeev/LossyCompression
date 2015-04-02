using WindowsFormsTemp.ImagePrimitives;

namespace WindowsFormsTemp.Filters
{
    public interface IFilter
    {
        IBitmap Apply(IBitmap image, IFilterData filterData);
    }

    public static class FilterExtensions
    {
        public static IBitmap Apply(this IBitmap image, IFilter filter, IFilterData filterData)
        {
            return filter.Apply(image, filterData);
        }
    }
}