using System.Text.Json.Serialization;
using Core.Context;
using Microsoft.AspNetCore.Mvc;
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

//cache
builder.Services.AddResponseCaching();
builder.Services.AddControllers(options =>
{
    options.CacheProfiles.Add("Default30",
        new CacheProfile()
        {
            Duration = 30 * 60, //30 minuts
            Location= ResponseCacheLocation.Any
        });
});

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

app.UseResponseCaching();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

