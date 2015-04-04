using WindowsFormsTemp.ImagePrimitives;

namespace WindowsFormsTemp.Filters
{
    public interface IFilter
    {
        IBitmap Apply(IBitmap image, IFilterData filterData = null);
    }

    public static class FilterExtensions
    {
        public static IBitmap Apply(this IBitmap image, IFilter filter, IFilterData filterData = null)
        {
            return filter.Apply(image, filterData);
        }
    }
}