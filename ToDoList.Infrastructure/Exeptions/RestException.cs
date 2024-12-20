using System.Net;

namespace ToDoList.Infrastructure.Exeptions
{
    public class RestException : Exception
	{
		public HttpStatusCode Code { get; }

		public string? ErrorMessage { get; set; }

		public RestException(HttpStatusCode code, string? errorMessage)
		{
			Code = code;
			ErrorMessage = errorMessage;
		}
	}
}
