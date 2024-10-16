using Events.API;
using Events.Application;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddPresentation();
    builder.Services.AddApplication();
}

var app = builder.Build();
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();
    app.Run();
}


