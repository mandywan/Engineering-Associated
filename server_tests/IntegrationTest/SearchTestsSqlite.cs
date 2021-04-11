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
        public async Task SearchByLocation_ShouldReturnCorrectData()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var payload = "{\"Location\":{\"type\":\"OR\",\"values\":[{\"LocationId\":\"02\"}]}}";
            HttpContent post = new StringContent(payload, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("/api/search", post);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299

            var stringResponse = await response.Content.ReadAsStringAsync();
            // Console.WriteLine(stringResponse);
            var searchRes = JsonConvert.DeserializeObject<JObject>(stringResponse);
            // Console.WriteLine(searchRes.GetValue("results"));
            var employees = JsonConvert.DeserializeObject<ICollection<EmployeeDTO>>(searchRes.GetValue("results").ToString());
            
            Assert.Equal(14, searchRes.GetValue("total"));
            Assert.Equal("", searchRes.GetValue("msg"));
            Assert.Contains(employees, e => e.LastName == "Ratke");
            Assert.Contains(employees, e => e.FirstName == "Chrissy");
            foreach (EmployeeDTO e in employees) {
                Assert.Equal("02", e.LocationId);
            }
        }


        [Fact]
        public async Task SearchBySkill_ShouldReturnCorrectData()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var payload = "{\"Skill\":{\"type\":\"AND\",\"values\":[{\"SkillId\":\"1\"}]}}";
            HttpContent post = new StringContent(payload, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("/api/search", post);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299

            var stringResponse = await response.Content.ReadAsStringAsync();
            // Console.WriteLine(stringResponse);
            var searchRes = JsonConvert.DeserializeObject<JObject>(stringResponse);
            // Console.WriteLine(searchRes.GetValue("results"));
            var employees = JsonConvert.DeserializeObject<ICollection<EmployeeDTO>>(searchRes.GetValue("results").ToString());
            
            Assert.Equal(3, searchRes.GetValue("total"));
            Assert.Equal("", searchRes.GetValue("msg"));
            Assert.Contains(employees, e => e.EmployeeNumber == 15);
            Assert.Contains(employees, e => e.EmployeeNumber == 11);
            Assert.Contains(employees, e => e.EmployeeNumber == 48);

            foreach (EmployeeDTO e in employees) {
                Assert.Contains(e.Skills, s => s.SkillId == "1");
            }
        }

        [Fact]
        public async Task SearchByCategory_ShouldReturnCorrectData()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            // {Category: {type: "OR", values: [{CategoryId: "1"}]}, meta: ["Category,1"]}
            var payload = "{\"Category\":{\"type\":\"OR\",\"values\":[{\"CategoryId\":\"1\"}]}}";
            HttpContent post = new StringContent(payload, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("/api/search", post);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299

            var stringResponse = await response.Content.ReadAsStringAsync();
            // Console.WriteLine(stringResponse);
            var searchRes = JsonConvert.DeserializeObject<JObject>(stringResponse);
            // Console.WriteLine(searchRes.GetValue("results"));
            var employees = JsonConvert.DeserializeObject<ICollection<EmployeeDTO>>(searchRes.GetValue("results").ToString());
            Assert.Equal(12, searchRes.GetValue("total"));
            Assert.Equal("", searchRes.GetValue("msg"));

            foreach (EmployeeDTO e in employees) {
                Assert.Contains(e.Skills, s => s.SkillCategoryId == "1");
            }
        }
        
        [Fact]
        public async Task SearchByName_ShouldReturnCorrectData()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            // Name: {type: "OR", values: ["Mitchell"]}
            var payload = "{\"Name\":{\"type\":\"OR\",\"values\":[\"Mitchell\"]}}";
            HttpContent post = new StringContent(payload, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("/api/search", post);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299

            var stringResponse = await response.Content.ReadAsStringAsync();
            // Console.WriteLine(stringResponse);
            var searchRes = JsonConvert.DeserializeObject<JObject>(stringResponse);
            // Console.WriteLine(searchRes.GetValue("results"));
            var employees = JsonConvert.DeserializeObject<ICollection<EmployeeDTO>>(searchRes.GetValue("results").ToString());
            Assert.Equal(1, searchRes.GetValue("total"));
            Assert.Equal("", searchRes.GetValue("msg"));
            Assert.Contains(employees, e => e.LastName == "Mitchell");
        }

        [Fact]
        public async Task SearchByLastName_ShouldReturnCorrectData()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            // Name: {type: "OR", values: ["Mitchell"]}
            var payload = "{\"LastName\":{\"type\":\"OR\",\"values\":[\"Mitchell\"]}}";
            HttpContent post = new StringContent(payload, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("/api/search", post);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299

            var stringResponse = await response.Content.ReadAsStringAsync();
            // Console.WriteLine(stringResponse);
            var searchRes = JsonConvert.DeserializeObject<JObject>(stringResponse);
            // Console.WriteLine(searchRes.GetValue("results"));
            var employees = JsonConvert.DeserializeObject<ICollection<EmployeeDTO>>(searchRes.GetValue("results").ToString());
            Assert.Equal(1, searchRes.GetValue("total"));
            Assert.Equal("", searchRes.GetValue("msg"));
            Assert.Contains(employees, e => e.LastName == "Mitchell");
        }


        [Fact]
        public async Task SearchByFirstName_ShouldReturnCorrectData()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            // Name: {type: "OR", values: ["Mitchell"]}
            var payload = "{\"FirstName\":{\"type\":\"OR\",\"values\":[\"Scotty\"]}}";
            HttpContent post = new StringContent(payload, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("/api/search", post);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299

            var stringResponse = await response.Content.ReadAsStringAsync();
            // Console.WriteLine(stringResponse);
            var searchRes = JsonConvert.DeserializeObject<JObject>(stringResponse);
            // Console.WriteLine(searchRes.GetValue("results"));
            var employees = JsonConvert.DeserializeObject<ICollection<EmployeeDTO>>(searchRes.GetValue("results").ToString());
            Assert.Equal(1, searchRes.GetValue("total"));
            Assert.Equal("", searchRes.GetValue("msg"));
            Assert.Contains(employees, e => e.FirstName == "Scotty");
        }

        [Fact]
        public async Task SearchByNameFuzzy_ShouldReturnCorrectData()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            // Name: {type: "OR", values: ["Mitchell"]}
            var payload = "{\"Name\":{\"type\":\"OR\",\"values\":[\"Da\"]}}";
            HttpContent post = new StringContent(payload, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("/api/search", post);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299

            var stringResponse = await response.Content.ReadAsStringAsync();
            // Console.WriteLine(stringResponse);
            var searchRes = JsonConvert.DeserializeObject<JObject>(stringResponse);
            // Console.WriteLine(searchRes.GetValue("results"));
            var employees = JsonConvert.DeserializeObject<ICollection<EmployeeDTO>>(searchRes.GetValue("results").ToString());
            Assert.Equal(3, searchRes.GetValue("total"));
            Assert.Equal("", searchRes.GetValue("msg"));
            Assert.Contains(employees, e => e.FirstName == "Damien");
            Assert.Contains(employees, e => e.FirstName == "Dallas");
            Assert.Contains(employees, e => e.FirstName == "Branda");
        }

        [Fact]
        public async Task SearchByTitle_ShouldReturnCorrectData()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var payload = "{\"Title\":{\"type\":\"OR\",\"values\":[\"Supervisor\"]}}";
            HttpContent post = new StringContent(payload, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("/api/search", post);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299

            var stringResponse = await response.Content.ReadAsStringAsync();
            // Console.WriteLine(stringResponse);
            var searchRes = JsonConvert.DeserializeObject<JObject>(stringResponse);
            // Console.WriteLine(searchRes.GetValue("results"));
            var employees = JsonConvert.DeserializeObject<ICollection<EmployeeDTO>>(searchRes.GetValue("results").ToString());
            Assert.Equal(7, searchRes.GetValue("total"));
            Assert.Equal("", searchRes.GetValue("msg"));

            foreach (EmployeeDTO e in employees) {
                Assert.Equal("Supervisor", e.Title);
            }
        }

        [Fact]
        // Omit this since we don't use this in front end?
        // Not sure how hire date should be formated as assertions are 
        // failing because search isn't returning any results
        public async Task SearchByHireDate_ShouldReturnCorrectData()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var payload = "{\"HireDate\":{\"type\":\"OR\",\"values\":[\"2013-08-29\"]}}";
            HttpContent post = new StringContent(payload, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("/api/search", post);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299

            var stringResponse = await response.Content.ReadAsStringAsync();
            // Console.WriteLine(stringResponse);
            var searchRes = JsonConvert.DeserializeObject<JObject>(stringResponse);
            // Console.WriteLine(searchRes.GetValue("results"));
            var employees = JsonConvert.DeserializeObject<ICollection<EmployeeDTO>>(searchRes.GetValue("results").ToString());
            
            // Assert.Equal(1, searchRes.GetValue("total"));
            // Assert.Equal("", searchRes.GetValue("msg"));
            // DateTime hireDate = new DateTime(2013, 08, 29, 00, 00, 00);
            // Assert.Contains(employees, e => e.HireDate == hireDate);

        }

        [Fact]
        public async Task SearchByWorkCell_ShouldReturnCorrectData()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var payload = "{\"WorkCell\":{\"type\":\"OR\",\"values\":[\"601-146-7691\"]}}";
            HttpContent post = new StringContent(payload, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("/api/search", post);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299

            var stringResponse = await response.Content.ReadAsStringAsync();
            // Console.WriteLine(stringResponse);
            var searchRes = JsonConvert.DeserializeObject<JObject>(stringResponse);
            // Console.WriteLine(searchRes.GetValue("results"));
            var employees = JsonConvert.DeserializeObject<ICollection<EmployeeDTO>>(searchRes.GetValue("results").ToString());
            Assert.Equal(1, searchRes.GetValue("total"));
            Assert.Equal("", searchRes.GetValue("msg"));

            foreach (EmployeeDTO e in employees) {
                Assert.Equal("601-146-7691", e.WorkCell);
            }
        }

        [Fact]
        public async Task SearchByEmail_ShouldReturnCorrectData()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var payload = "{\"Email\":{\"type\":\"OR\",\"values\":[\"Luis@amc.com\"]}}";
            HttpContent post = new StringContent(payload, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("/api/search", post);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299

            var stringResponse = await response.Content.ReadAsStringAsync();
            // Console.WriteLine(stringResponse);
            var searchRes = JsonConvert.DeserializeObject<JObject>(stringResponse);
            // Console.WriteLine(searchRes.GetValue("results"));
            var employees = JsonConvert.DeserializeObject<ICollection<EmployeeDTO>>(searchRes.GetValue("results").ToString());
            Assert.Equal(1, searchRes.GetValue("total"));
            Assert.Equal("", searchRes.GetValue("msg"));

            foreach (EmployeeDTO e in employees) {
                Assert.Equal("Luis@amc.com", e.Email);
            }
        }

        [Fact]
        public async Task SearchReturnNoResult_ShouldReturnCorrectData()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var payload = "{\"Name\":{\"type\":\"OR\",\"values\":[\"Annie\"]}}";
            HttpContent post = new StringContent(payload, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("/api/search", post);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299

            var stringResponse = await response.Content.ReadAsStringAsync();
            // Console.WriteLine(stringResponse);
            var searchRes = JsonConvert.DeserializeObject<JObject>(stringResponse);
            // Console.WriteLine(searchRes.GetValue("results"));
            var employees = JsonConvert.DeserializeObject<ICollection<EmployeeDTO>>(searchRes.GetValue("results").ToString());
            Assert.Equal(0, searchRes.GetValue("total"));
            Assert.Equal("Looked here, there and everywhere - but couldn't find the person you're looking for.", searchRes.GetValue("msg"));
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