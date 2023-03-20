using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

// add services to DI container
{
    var services = builder.Services;
    services.AddControllers();
}

builder.Services.AddSwaggerGen();
var app = builder.Build();

// configure HTTP request pipeline
{
    app.MapControllers();
}
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});
app.Run();