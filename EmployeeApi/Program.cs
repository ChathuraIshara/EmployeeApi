using EmployeeApi;
using EmployeeApi.Abstractions;
using EmployeeApi.Employees;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "EmployeeAPI.xml"));
});
builder.Services.AddSingleton<IRepository<Employee>, EmployeeRepository>();
builder.Services.AddProblemDetails(); // This is for automatic 400 response for validation errors
builder.Services.AddValidatorsFromAssemblyContaining<Program>();
builder.Services.AddControllers(options =>
{
    options.Filters.Add<FluentValidationFilter>();
});
builder.Services.AddHttpContextAccessor();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=employees.db"));



// Employees list

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();


public partial class Program { } // This is required for the integration tests to work with the WebApplicationFactory
