using EmployeeApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Employees list
var employees = new List<Employee>
{
    new Employee { id = 1, FirstName = "Chathura", LastName = "Ishara" },
    new Employee { id = 2, FirstName = "Sam", LastName = "Brown" },
    new Employee { id = 3, FirstName = "Alice", LastName = "Johnson" }
};
var app = builder.Build();

var employeeRoute = app.MapGroup("employees");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


employeeRoute.MapGet(String.Empty, () => { return Results.Ok(employees); });
employeeRoute.MapGet("{id:int}", (int id) =>
{
    var employee = employees.FirstOrDefault(e => e.id == id);
    if (employee == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(employee);
});
employeeRoute.MapPost(String.Empty, (Employee employee) =>
{
    employee.id = employees.Max(e => e.id) + 1; // Assign a new ID
    employees.Add(employee);
    return Results.Created($"/employees/{employee.id}", employee);

});

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
