using Events.API;
using Events.API.Middleware;
using Events.Application;
using Events.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddPresentation(builder.Configuration);
    builder.Services.AddApplication();
    builder.Services.AddInfrastructure(builder.Configuration);
}

var app = builder.Build();
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseMiddleware<ErrorHandlingMiddleware>();
    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();
    app.Run();
}


