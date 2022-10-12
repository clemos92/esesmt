namespace ESESMT.Infra.Shared.Common
{
    public abstract class BasePaginationFilter
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string SortDirection { get; set; }
        public string SortBy { get; set; }

        public BasePaginationFilter()
        {
            this.PageIndex = 0;
            this.PageSize = 10;
        }

        public BasePaginationFilter(int pageNumber, int pageSize)
        {
            this.PageIndex = pageNumber;
            this.PageSize = pageSize == 0 ? 10 : pageSize;
        }
    }
}
