namespace ESESMT.Infra.Shared.Common
{
    public class Response<TEntity> where TEntity : new()
    {
        public Response() { }

        public Response(TEntity data)
        {
            Succeeded = true;
            Message = string.Empty;
            Errors = null;
            Data = data;
        }

        public TEntity Data { get; set; }
        public bool Succeeded { get; set; }
        public string[] Errors { get; set; }
        public string Message { get; set; }
    }
}
