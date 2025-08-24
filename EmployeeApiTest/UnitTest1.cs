using EmployeeApi;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;

namespace EmployeeApiTest
{
    public class BasicTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public BasicTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
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
                LastName = "Doe"
            });
            
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task CreateEmployee_ReturnsBadRequestResult()
        {
            HttpClient client = _factory.CreateClient();
            var response = await client.PostAsJsonAsync("/employees", new { });

            Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}