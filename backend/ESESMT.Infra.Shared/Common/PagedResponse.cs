namespace ESESMT.Infra.Shared.Common
{
    public class PagedResponse<TEntity> : Response<TEntity> where TEntity : new()
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalRecords { get; set; }

        public PagedResponse(TEntity data, int pageNumber, int pageSize, int totalRecords)
        {
            this.PageIndex = pageNumber;
            this.PageSize = pageSize;
            this.TotalRecords = totalRecords;
            this.Data = data;
            this.Message = null;
            this.Succeeded = true;
            this.Errors = null;
        }
    }
}
