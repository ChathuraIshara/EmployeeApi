using Microsoft.AspNetCore.Mvc.Testing;

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
    }
}