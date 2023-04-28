using System.Text.Json.Serialization;
using Core.Context;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Web.Extensions;

var builder = WebApplication.CreateBuilder(args);

LoggingConfig.ConfigureLogging();
builder.Host.UseSerilog();

builder.Services
    .AddDbContext<ShopContext>
        (options => options.UseNpgsql(builder.Configuration["ConnectionStrings:DefaultConnection"]));
builder.DependencyInjection();

builder.Services.AddControllers();
//!!
builder.Services.AddControllers().AddJsonOptions(options => 
{ 
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.WriteIndented = true;
});

builder.Services.AddEndpointsApiExplorer();

builder.AddSwaggerBearer();
builder.AddJWTAuth();


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

