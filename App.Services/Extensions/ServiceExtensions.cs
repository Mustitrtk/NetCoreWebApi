using App.Repositories.Products;
using App.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Services.Products;
using FluentValidation;
using System.Reflection;
using App.Services.ExceptionHandlers;
using App.Services.Categories;
using Microsoft.AspNetCore.Mvc;

namespace App.Services.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IProductService, ProductService>();

            services.AddScoped<ICategoryService, CategoryService>();

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly()); //Fluent Validation

            services.AddAutoMapper(Assembly.GetExecutingAssembly()); //Mapping


            services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true); //Manuel Hata Mesajı Kapama

            services.AddExceptionHandler<CriticalExceptionHandler>();
            services.AddExceptionHandler<GlobalExceptionHandler>();
            return services;
        }
    }
}
