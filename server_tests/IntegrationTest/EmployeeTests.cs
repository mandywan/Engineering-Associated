using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using AeDirectory;
using AeDirectory.DTO;
using Newtonsoft.Json;
using Xunit;

namespace IntegrationTest
{
    public class EmployeeTest : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public EmployeeTest(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }
        
        [Fact]
        public async Task GetEmployees_ShouldReturnCorrectData()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/api/employees");

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299

            var stringResponse = await response.Content.ReadAsStringAsync();
            var employees = JsonConvert.DeserializeObject<List<EmployeeDTO>>(stringResponse);

            Assert.Contains(employees, e => e.FirstName == "John");
        }
    }
}
