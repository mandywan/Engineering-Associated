using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using AeDirectory;
using AeDirectory.DTO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using Xunit;

namespace IntegrationTest
{
    public class SearchTestSqlite : IClassFixture<CustomSQLiteWebApplicationFactory<Startup>>
    {
        private readonly CustomSQLiteWebApplicationFactory<Startup> _factory;

        public SearchTestSqlite(CustomSQLiteWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }
        
        [Fact]
        public async Task SearchCompany_ShouldReturnCorrectData()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            // {"Company":{"type":"OR","values":[{"CompanyId":"01"}]},"meta":["Company,01"]}
            var payload = "{\"Company\":{\"type\":\"OR\",\"values\":[{\"CompanyId\":\"01\"}]},\"meta\":[\"Company,01\"]}";
            HttpContent post = new StringContent(payload, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("/api/search", post);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299

            var stringResponse = await response.Content.ReadAsStringAsync();
            // Console.WriteLine(stringResponse);
            var searchRes = JsonConvert.DeserializeObject<JObject>(stringResponse);
            // Console.WriteLine(searchRes.GetValue("results"));
            var employees = JsonConvert.DeserializeObject<ICollection<EmployeeDTO>>(searchRes.GetValue("results").ToString());
            
            Assert.Equal(9, searchRes.GetValue("total"));
            Assert.Equal("", searchRes.GetValue("msg"));
            Assert.Contains(employees, e => e.LastName == "Schaden");
            Assert.Contains(employees, e => e.FirstName == "Arletha");

        }

        [Fact]
        public async Task SearchEmpty_ShouldReturnCorrectData()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var payload = "{}";
            HttpContent post = new StringContent(payload, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("/api/search", post);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299

            var stringResponse = await response.Content.ReadAsStringAsync();
            // Console.WriteLine(stringResponse);
            var searchRes = JsonConvert.DeserializeObject<JObject>(stringResponse);

            Assert.Equal(0, searchRes.GetValue("total"));
            Assert.Equal("Please enter a keyword or select a filter to search", searchRes.GetValue("msg"));
        }
        
    }
}