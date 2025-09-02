using EmployeeApi;
using EmployeeApi.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Http.Json;

namespace EmployeeApiTest
{
    public class BasicTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public BasicTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
            var repo = _factory.Services.GetRequiredService<IRepository<Employee>>();
            repo.Create(new Employee { FirstName = "John", LastName = "Doe" });
        }

        [Fact]
        public async Task GetAllEmployees_ReturnsOkResult()
        {
            HttpClient client = _factory.CreateClient();
            var response = await client.GetAsync("/employees");

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task GetEmployeeById_RetursnOkResult()
        {
            HttpClient client = _factory.CreateClient();
            var response = await client.GetAsync("/employees/1");
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task CreateEmployee_ReturnsCreatedResult()
        {
            HttpClient client = _factory.CreateClient();
            var response = await client.PostAsJsonAsync("/employees", new
            {
                FirstName = "John",
                LastName = "Doe",
                SocialSecurityNumber = "666"
            });
            
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task CreateEmployee_ReturnsBadRequestResult()
        {
            HttpClient client = _factory.CreateClient();
            var response = await client.PostAsJsonAsync("/employees", new { });
            var problemDetails = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
            Assert.NotNull(problemDetails);
            Assert.Contains("FirstName", problemDetails.Errors.Keys);
            Assert.Contains("LastName", problemDetails.Errors.Keys);
            Assert.Contains("'First Name' must not be empty.", problemDetails.Errors["FirstName"]);
            Assert.Contains("'Last Name' must not be empty.", problemDetails.Errors["LastName"]);
        }

        [Fact]
        public async Task updateEmployee_ReturnsNotFoundNotExistentEmployee()
        {
            HttpClient client = _factory.CreateClient();
            var response = await client.PutAsJsonAsync("/employees/999",new Employee { FirstName="hell",LastName="ddd", SocialSecurityNumber="555"});

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}