using System.Net;
using System.Text.Json;

namespace Library.Models.Helpers
{
    public class ErrorDetails
    {
        public int StatusCode { get; }
        public string Message { get; } 

        public ErrorDetails(HttpStatusCode statusCode, string message)
        {
            this.StatusCode = (int) statusCode;
            this.Message = message;
        }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}