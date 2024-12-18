using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Infrastructure.Exeptions
{
	public class RestException : Exception
	{
		public RestException(HttpStatusCode code, string? errorMessage)
		{
			Code = code;
			ErrorMessage = errorMessage;
		}

		public HttpStatusCode Code { get; }

		public string? ErrorMessage { get; set; }
	}
}
