using WeTicket.API.Extensions;
using WeTicket.Data.Configurations;
using WeTicket.Services.Configurations;
using Scalar.AspNetCore;
using WeTicket.API.Extensions;

var builder = WebApplication.CreateBuilder(args);
// Data Layer
builder.Services.AddProjectDataLayer(builder.Configuration);
// Business Layer (Services Layer)
builder.Services.AddApplicationServices();
// API Layer
builder.Services.AddApiLayer(builder.Configuration);
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
    //builder.WebHost.UseUrls("http://0.0.0.0:7072");
}
app.UseCors("WeTicket");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers().RequireAuthorization();
app.Run();