using System.Net;
using ToDoList.Infrastructure.Exeptions;
using Newtonsoft.Json;

namespace webapi.Middleware
{
	public class ErrorHandlingMiddleware
	{
		private readonly RequestDelegate _next;

		public ErrorHandlingMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task InvokeAsync(HttpContext context)
		{
			try
			{
				await _next(context);
			}
			catch (Exception ex)
			{
				await HandleExceptionAsync(context, ex);
			}
		}

		private async Task HandleExceptionAsync(HttpContext context, Exception ex)
		{
			string? message="";

			switch (ex)
			{
				case RestException rest:
					message = rest.ErrorMessage;
					context.Response.StatusCode = (int)rest.Code;
					break;

				case Exception e:
					message = string.IsNullOrWhiteSpace(e.Message) ? "error" : e.Message;
					context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
					break;
			}

			context.Response.ContentType = "appliation/json";

			if (message != "")
			{
				var result = JsonConvert.SerializeObject(new
				{
					ErrorMessage = message,
				});

				await context.Response.WriteAsync(result);
			}
		}
	}
}
