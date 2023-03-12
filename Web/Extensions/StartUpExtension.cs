using Core.Repositories.CategoryRepository;
using Core.Repositories.OrderRepository;
using Core.Repositories.ProductRepository;
using Core.Repositories.RoleRepository;
using Core.Repositories.UserRepository;
using Infrastructure.AutoMappers;
using Infrastructure.Services.AccountService;
using Infrastructure.Services.AdminService;
using Infrastructure.Services.CategoryService;
using Infrastructure.Services.ProductService;
using Infrastructure.Services.ShopService;
using Microsoft.OpenApi.Models;

namespace Web.Extensions;

public static class StartUpExtension
{
    public static void DependencyInjection(this WebApplicationBuilder builder)
    {
        builder.Services.AddAutoMapper(typeof(ProductAutoMapper));
        
        builder.Services.AddTransient<IShopService, ShopService>();
        builder.Services.AddTransient<IAdminService, AdminService>();
        builder.Services.AddTransient<IAccountService, AccountService>();
        builder.Services.AddTransient<IProductService, ProductService>();
        builder.Services.AddTransient<ICategoryService, CategoryService>();

        
        builder.Services.AddTransient<IRoleRepository, RoleRepository>();
        builder.Services.AddTransient<IUserRepository, UserRepository>();
        builder.Services.AddTransient<IOrderRepository, OrderRepository>();
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