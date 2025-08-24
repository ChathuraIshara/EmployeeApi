# EmployeeApi
Minimal API in .NET refers to a lightweight way of building HTTP APIs with minimal boilerplate code introduced in ASP.NET Core 6.
Why was Minimal API introduced?
* Before .NET 6, you had to use Controllers, Attributes, and a lot of startup configuration for Web APIs.
* For small services or microservices, that was too much code.

Minimal API lets you create an API endpoint with just a few lines.

Key Features of Minimal API

* No need for Controllers or Action methods.
* Uses top-level statements in Program.cs.
* Perfect for small APIs, microservices, prototypes.
* Built on top of the same ASP.NET Core pipeline as MVC, so it’s not a separate framework.

  ✅ Minimal API Example
  var builder = WebApplication.CreateBuilder(args);
  var app = builder.Build();

  app.MapGet("/", () => "Hello, World!"); // ✅ Minimal API route
  app.MapGet("/hello/{name}", (string name) => $"Hello, {name}!");
  
  app.Run();
