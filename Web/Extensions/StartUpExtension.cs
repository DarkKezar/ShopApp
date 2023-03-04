using Core.Repositories.CategoryRepository;
using Core.Repositories.ProductRepository;
using Infrastructure.AutoMappers;
using Infrastructure.Services.AccountService;
using Infrastructure.Services.CategoryService;
using Infrastructure.Services.ProductService;
using Microsoft.OpenApi.Models;

namespace Web.Extensions;

public static class StartUpExtension
{
    public static void DI(this WebApplicationBuilder builder)
    {
        builder.Services.AddAutoMapper(typeof(ProductAutoMapper));
        
        builder.Services.AddTransient<IProductService, ProductService>();
        builder.Services.AddTransient<ICategoryService, CategoryService>();

        builder.Services.AddTransient<IProductRepository, ProductRepository>();
        builder.Services.AddTransient<ICategoryRepository, CategoryRepository>();
    }

    public static void AddSwaggerBearer(this WebApplicationBuilder builder)
    {
        builder.Services.AddSwaggerGen(option =>
        {
            option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
            option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter a valid token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "Bearer"
            });
            option.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type=ReferenceType.SecurityScheme,
                            Id="Bearer"
                        }
                    },
                    new string[]{}
                }
            });
        });
    }
}