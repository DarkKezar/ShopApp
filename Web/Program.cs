using System.Text.Json.Serialization;
using Core.Context;
using Microsoft.EntityFrameworkCore;
using Web.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddDbContext<ShopContext>
        (options => options.UseNpgsql(builder.Configuration["ConnectionStrings:DefaultConnection"]));
builder.DI();
// Add services to the container.

builder.Services.AddControllers();
//!!
builder.Services.AddControllers().AddJsonOptions(options => 
{ 
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.WriteIndented = true;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.AddSwaggerBearer();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();