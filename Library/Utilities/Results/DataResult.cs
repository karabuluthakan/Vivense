using System.Net;
using Library.Utilities.Results.Abstract;

namespace Library.Utilities.Results
{
    public class DataResult<T> : Result, IDataResult<T>
    {
        public DataResult(T data, HttpStatusCode statusCode) : base(statusCode)
        {
            Data = data;
        }

        public DataResult(T data, HttpStatusCode statusCode, int totalRecords) : this(data, statusCode)
        {
            TotalRecords = totalRecords;
        }

        public DataResult(T data, HttpStatusCode statusCode, int totalRecords,string message) :base(statusCode, message)
        {
            Data = data;
            TotalRecords = totalRecords;
        }

        public DataResult(T data, HttpStatusCode statusCode, string message) : base(statusCode, message)
        {
            Data = data;
        }

        public T Data { get; }
        public int TotalRecords { get; }
    }
}