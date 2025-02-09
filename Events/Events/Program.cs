﻿using Events.API;
using Events.API.Middleware;
using Events.Application;
using Events.Infrastructure;
using Hangfire;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddPresentation(builder.Configuration);
    builder.Services.AddApplication();
    builder.Services.AddInfrastructure(builder.Configuration);

    builder.Services.AddHttpContextAccessor();
}

var app = builder.Build();
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseMiddleware<ErrorHandlingMiddleware>();
    app.UseHangfireDashboard("/dashboard");
    app.UseHangfireServer();
    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();
    app.Run();
}


