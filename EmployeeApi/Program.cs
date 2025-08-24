using EmployeeApi;
using EmployeeApi.Employees;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Employees list
var employees = new List<Employee>
{
    new Employee { id = 1, FirstName = "Chathura", LastName = "Ishara", SocialSecurityNumber = "123" },
    new Employee { id = 2, FirstName = "Sam", LastName = "Brown", SocialSecurityNumber = "456"},
    new Employee { id = 3, FirstName = "Alice", LastName = "Johnson", SocialSecurityNumber = "879" }
};
var app = builder.Build();

var employeeRoute = app.MapGroup("employees");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


employeeRoute.MapGet(String.Empty, () => { 
    return Results.Ok(employees.Select(employee => new GetEmployeeResponse
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
employeeRoute.MapGet("{id:int}", ([FromRoute]int id) =>
{
    var employee = employees.FirstOrDefault(e => e.id == id);
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
employeeRoute.MapPost(String.Empty, ([FromBody] CreateEmployeeRequest employee) =>
{
    var newEmployee = new Employee
    {
        id = employees.Max(e => e.id) + 1, // Generate a new ID
        FirstName = employee.FirstName,
        LastName = employee.LastName,
        SocialSecurityNumber = employee.SocialSecurityNumber,
        Address1 = employee.Address1,
        Address2 = employee.Address2,
        City = employee.City,
        State = employee.State,
        ZipCode = employee.ZipCode,
        PhoneNumber = employee.PhoneNumber,
        Email = employee.Email
    };
    return Results.Created($"/employees/{newEmployee.id}", newEmployee);

});

employeeRoute.MapPut("{id}",([FromBody] UpdateEmployeeRequest updatedEmployee,[FromRoute]int id) =>{
    var existingEmployee =  employees.FirstOrDefault(e => e.id == id);
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

    return Results.Ok(existingEmployee);
});

app.UseHttpsRedirection();

app.MapControllers();

app.Run();


public partial class Program { } // This is required for the integration tests to work with the WebApplicationFactory
