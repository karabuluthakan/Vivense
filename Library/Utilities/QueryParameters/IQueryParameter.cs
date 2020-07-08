namespace Library.Utilities.QueryParameters
{
    public interface IQueryParameter
    {
        int PageNumber { get; }
        int PageSize { get; }
        string OrderBy { get; }
        string Filter { get; }
    }
}