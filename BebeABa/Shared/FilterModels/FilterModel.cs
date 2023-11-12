namespace Shared.FilterModels
{
    public class FilterModel
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public bool GetAllList { get; set; } = false;
        public bool IncludeAllInformation { get; set; } = false;
    }
}
