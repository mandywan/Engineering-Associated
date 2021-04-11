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
        public async Task SearchByCompany_ShouldReturnCorrectData()
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
            foreach (EmployeeDTO e in employees) {
                Assert.Equal("01", e.CompanyCode);
            }
        }

        [Fact]
        public async Task SearchByOffice_ShouldReturnCorrectData()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var payload = "{\"Office\":{\"type\":\"OR\",\"values\":[{\"CompanyId\":\"02\", \"OfficeId\": \"02\"}]}}";
            HttpContent post = new StringContent(payload, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("/api/search", post);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299

            var stringResponse = await response.Content.ReadAsStringAsync();
            // Console.WriteLine(stringResponse);
            var searchRes = JsonConvert.DeserializeObject<JObject>(stringResponse);
            // Console.WriteLine(searchRes.GetValue("results"));
            var employees = JsonConvert.DeserializeObject<ICollection<EmployeeDTO>>(searchRes.GetValue("results").ToString());
            
            Assert.Equal(11, searchRes.GetValue("total"));
            Assert.Equal("", searchRes.GetValue("msg"));
            Assert.Contains(employees, e => e.LastName == "Price");
            Assert.Contains(employees, e => e.FirstName == "Hassan");
            foreach (EmployeeDTO e in employees) {
                Assert.Equal("02", e.CompanyCode);
                Assert.Equal("02", e.OfficeCode);
            }
        }

        [Fact]
        public async Task SearchByGroup_ShouldReturnCorrectData()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var payload = "{\"Group\":{\"type\":\"OR\",\"values\":[{\"CompanyId\":\"03\", \"OfficeId\": \"05\", \"GroupId\": \"09\"}]}}";
            HttpContent post = new StringContent(payload, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("/api/search", post);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299

            var stringResponse = await response.Content.ReadAsStringAsync();
            // Console.WriteLine(stringResponse);
            var searchRes = JsonConvert.DeserializeObject<JObject>(stringResponse);
            // Console.WriteLine(searchRes.GetValue("results"));
            var employees = JsonConvert.DeserializeObject<ICollection<EmployeeDTO>>(searchRes.GetValue("results").ToString());
            
            Assert.Equal(4, searchRes.GetValue("total"));
            Assert.Equal("", searchRes.GetValue("msg"));
            Assert.Contains(employees, e => e.LastName == "Steuber");
            Assert.Contains(employees, e => e.FirstName == "Cristy");
            foreach (EmployeeDTO e in employees) {
                Assert.Equal("03", e.CompanyCode);
                Assert.Equal("05", e.OfficeCode);
                Assert.Equal("09", e.GroupCode);
            }
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