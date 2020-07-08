using System.Collections.Generic;
using System.Net;
using Library.Utilities.Results.Abstract;

namespace Library.Utilities.Results
{
    public class ErrorResult : IResult
    {
        public int StatusCode { get; }
        public string Message { get; set; }
        public IDictionary<string, string> Errors { get; set; }

        public ErrorResult(HttpStatusCode statusCode)
        {
            StatusCode = (int) statusCode;
            Message = "An error occured";
        }

        public ErrorResult(HttpStatusCode statusCode, IDictionary<string, string> errors) : this(statusCode)
        {
            Errors = errors;
        } 
    }
}