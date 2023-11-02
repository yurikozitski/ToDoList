using System.Net;
using ToDoList.Infrastructure.Exeptions;
using Newtonsoft.Json;

namespace webapi.Middleware
{
	public class ErrorHandlingMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly ILogger<ErrorHandlingMiddleware> logger;

		public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> _logger)
		{
			_next = next;
			logger = _logger;
		}

		public async Task InvokeAsync(HttpContext context)
		{
			try
			{
				await _next(context);
			}
			catch (Exception ex)
			{
				await HandleExceptionAsync(context, ex, logger);
			}
		}

		private async Task HandleExceptionAsync(HttpContext context, Exception ex, ILogger<ErrorHandlingMiddleware> _logger)
		{
			string? message="";

			switch (ex)
			{
				case RestException rest:
					_logger.LogError(ex, "Rest error");
					message = rest.ErrorMessage;
					context.Response.StatusCode = (int)rest.Code;
					break;

				case Exception e:
					_logger.LogError(ex, "Server error");
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
