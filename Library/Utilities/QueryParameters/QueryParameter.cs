namespace Library.Utilities.QueryParameters
{
    public class QueryParameter : IQueryParameter
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string OrderBy { get; }
        public string Filter { get; }

        public QueryParameter()
        {
            PageNumber = 1;
            PageSize = 10;
        }

        public QueryParameter(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber < 1 ? 1 : pageNumber;
            PageSize = pageSize < 10 ? 10 : pageSize;
        }
    }
}