using System.Net;
using ToDoList.Infrastructure.Exeptions;
using Newtonsoft.Json;

namespace webapi.Middleware
{
	public class ErrorHandlingMiddleware
	{
		private readonly RequestDelegate next;
		private readonly ILogger<ErrorHandlingMiddleware> logger;

		public ErrorHandlingMiddleware(RequestDelegate _next, ILogger<ErrorHandlingMiddleware> _logger)
		{
			next = _next;
			logger = _logger;
		}

		public async Task InvokeAsync(HttpContext context)
		{
			try
			{
				await next(context);
			}
			catch (Exception ex)
			{
				await HandleExceptionAsync(context, ex, logger);
			}
		}

		private async Task HandleExceptionAsync(HttpContext context, Exception ex, ILogger<ErrorHandlingMiddleware> _logger)
		{
			string? message = string.Empty;

			switch (ex)
			{
				case RestException rest:
					_logger.LogError(ex, "Rest error");
					message = rest.ErrorMessage;
					context.Response.StatusCode = (int)rest.Code;
					break;

				case Exception e:
					_logger.LogError(e, "Server error");
					message = "Server error occured";
					context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
					break;
			}

			context.Response.ContentType = "appliation/json";

			if (message != string.Empty)
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
