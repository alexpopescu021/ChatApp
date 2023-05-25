using ChatApp.BusinessLogic;
using ChatApp.DataAccess;
using ChatApp.WebApi.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
    .AddControllers(options =>
    {
        options.Filters.Add<HttpExceptionFilter>();
        options.Filters.Add(new ProducesResponseTypeAttribute(typeof(string), StatusCodes.Status500InternalServerError));
    })
    .AddXmlSerializerFormatters()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });
builder.Services.AddDataAccess(builder.Configuration.GetConnectionString("Database"));
builder.Services.AddBusinessLogic();

builder.Services.AddSwaggerGen();
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.RoutePrefix = string.Empty;
    options.SwaggerEndpoint("swagger/v1/swagger.json", "ChatApp API");
});

app.MapControllers();
app.Run();
