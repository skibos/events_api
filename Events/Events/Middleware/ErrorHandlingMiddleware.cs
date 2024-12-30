using System.Net;
using Events.Domain.Common.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Events.API.Middleware
{
	public class ErrorHandlingMiddleware
	{
		private readonly RequestDelegate _next;

		public ErrorHandlingMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task Invoke(HttpContext context)
		{
			try
			{
				await _next(context);
			} catch (Exception ex)
			{
				await HandleExceptionAsync(context, ex);
			}
		}

		private static Task HandleExceptionAsync(HttpContext context, Exception exception)
		{
			var (title, message, statusCode) = exception switch
			{
				DomainHttpException domainHttpException => (domainHttpException.GetType().Name, domainHttpException.Message, domainHttpException.StatusCode),
				_ => ("Internal server error", exception.Message, HttpStatusCode.InternalServerError)
			};

			context.Response.ContentType = "application/json";
			context.Response.StatusCode = (int)statusCode;

			var problemDetails = new ProblemDetails
			{
                Title = title,
                Detail = message,
                Status = (int)statusCode,
			};

			return context.Response.WriteAsJsonAsync(problemDetails);
		}
	}
}

