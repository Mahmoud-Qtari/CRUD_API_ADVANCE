﻿using CRUD_ADVANCE.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace CRUD_ADVANCE.Error
{
    public class GlobalHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalHandler> logger;

        public GlobalHandler(ILogger<GlobalHandler> logger)
        {
            this.logger = logger;
        }
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            logger.LogError(exception,"catch error",exception.Message);
            var problemDetails = new ProblemDetails()
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Server Error",
                Detail=exception.Message,
            };

            httpContext.Response.StatusCode = problemDetails.Status.Value;
            await httpContext.Response.WriteAsJsonAsync(problemDetails);
            return true;
        }
    }
}
