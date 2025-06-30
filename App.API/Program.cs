using App.Repositories;
using App.Repositories.Extensions;
using App.Repository;
using App.Services.Extensions;
using App.Services.Products;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options => 
    {
        options.Filters.Add<FluentValidationFiltre>();
        options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRepository(builder.Configuration).AddServices(builder.Configuration); //Service and Repoistory extensions



var app = builder.Build();

app.UseExceptionHandler(x => { });

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
