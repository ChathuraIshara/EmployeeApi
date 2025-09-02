using EmployeeApi;
using EmployeeApi.Abstractions;
using EmployeeApi.Employees;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IRepository<Employee>, EmployeeRepository>();
builder.Services.AddProblemDetails(); // This is for automatic 400 response for validation errors


// Employees list

var app = builder.Build();

var employeeRoute = app.MapGroup("employees");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


employeeRoute.MapGet(String.Empty, (IRepository<Employee> repository) =>
{
    return Results.Ok(repository.GetAll().Select(employee => new GetEmployeeResponse
    {
        FirstName = employee.FirstName,
        LastName = employee.LastName,
        Address1 = employee.Address1,
        Address2 = employee.Address2,
        City = employee.City,
        State = employee.State,
        ZipCode = employee.ZipCode,
        PhoneNumber = employee.PhoneNumber,
        Email = employee.Email
    }));
});
employeeRoute.MapGet("{id:int}", ([FromRoute]int id,IRepository<Employee> repository) =>
{ 
    var employee = repository.GetById(id);
    if (employee == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(new GetEmployeeResponse {

        FirstName = employee.FirstName,
        LastName = employee.LastName,
        Address1 = employee.Address1,
        Address2 = employee.Address2,
        City = employee.City,
        State = employee.State,
        ZipCode = employee.ZipCode,
        PhoneNumber = employee.PhoneNumber,
        Email = employee.Email

    });
});
employeeRoute.MapPost(String.Empty, ([FromBody] CreateEmployeeRequest employee,IRepository<Employee> repository) =>
{
    var validationProblems = new List<ValidationResult>();
    var isValid = Validator.TryValidateObject(employee, new ValidationContext(employee), validationProblems, true);
    if (!isValid)
    {
        return Results.BadRequest(validationProblems.ToValidationProblemDetails());
    }
    var newEmployee = new Employee
    {
        FirstName = employee.FirstName!,
        LastName = employee.LastName!,
        SocialSecurityNumber = employee.SocialSecurityNumber,
        Address1 = employee.Address1,
        Address2 = employee.Address2,
        City = employee.City,
        State = employee.State,
        ZipCode = employee.ZipCode,
        PhoneNumber = employee.PhoneNumber,
        Email = employee.Email
    };
    repository.Create(newEmployee);
    return Results.Created($"/employees/{newEmployee.id}", newEmployee);

});

employeeRoute.MapPut("{id}",([FromBody] UpdateEmployeeRequest updatedEmployee,[FromRoute]int id,IRepository<Employee> repository) =>{
    var existingEmployee =  repository.GetById(id);
    if(existingEmployee==null)
    {
        return Results.NotFound();
    } 
    existingEmployee.Address1 = updatedEmployee.Address1;
    existingEmployee.Address2 = updatedEmployee.Address2;
    existingEmployee.City = updatedEmployee.City;
    existingEmployee.State = updatedEmployee.State;
    existingEmployee.ZipCode = updatedEmployee.ZipCode;
    existingEmployee.PhoneNumber = updatedEmployee.PhoneNumber;
    existingEmployee.Email = updatedEmployee.Email;

    repository.Update(existingEmployee);

    return Results.Ok(existingEmployee);
});

app.UseHttpsRedirection();

app.MapControllers();

app.Run();


public partial class Program { } // This is required for the integration tests to work with the WebApplicationFactory
