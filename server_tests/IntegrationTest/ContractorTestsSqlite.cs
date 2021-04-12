using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using AeDirectory;
using AeDirectory.DTO;
using Newtonsoft.Json;
using Xunit;

namespace IntegrationTest
{
    public class ContractorTestSqlite : IClassFixture<CustomSQLiteWebApplicationFactory<Startup>>
    {
        private readonly CustomSQLiteWebApplicationFactory<Startup> _factory;

        public ContractorTestSqlite(CustomSQLiteWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }
        
        [Fact]
        public async Task LoginAndAdd_ShouldReturnCorrectData()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            // {username: "admin", password: "123456"}
            var payload = "{\"username\":\"admin\", \"password\":\"123456\"}";
            HttpContent post = new StringContent(payload, Encoding.UTF8, "application/json");
           
            var response = await client.PostAsync("/api/login", post);
           
            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            var stringResponse = await response.Content.ReadAsStringAsync();

            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + stringResponse);
            
            // Act - Get
            var getResponse = await client.GetAsync("/api/contractors");
           
            // Assert
            getResponse.EnsureSuccessStatusCode();
            var stringGetRes = await getResponse.Content.ReadAsStringAsync();
           
            var contractors = JsonConvert.DeserializeObject<ICollection<EmployeeDTO>>(stringGetRes);

            Assert.Equal(0, contractors.Count);

            // Act - Add then get
            var addPostPayload = "{\"lastName\":\"Contractor\", \"firstName\":\"Name\", \"companyCode\":\"01\", \"officeCode\":\"01\", \"groupCode\": \"01\", \"locationId\": \"01\", \"supervisorEmployeeNumber\": 1}";
            HttpContent addPost = new StringContent(addPostPayload, Encoding.UTF8, "application/json");
           
            var addResponse = await client.PostAsync("/api/contractors", addPost);
            addResponse.EnsureSuccessStatusCode();

            var addThenGetResponse = await client.GetAsync("/api/contractors");
            addThenGetResponse.EnsureSuccessStatusCode();

            var addThenGetRes = await addThenGetResponse.Content.ReadAsStringAsync();
            var addedContractors = JsonConvert.DeserializeObject<ICollection<EmployeeDTO>>(addThenGetRes);

            Assert.Equal(1, addedContractors.Count);
            Assert.Contains(addedContractors, c => c.LastName == "Contractor" 
                                                && c.FirstName == "Name"
                                                && c.EmployeeNumber == 51
                                                && c.SupervisorEmployeeNumber == 1);

        }

        [Fact]
        public async Task LoginAndEdit_ShouldReturnCorrectData()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            // {username: "admin", password: "123456"}
            var payload = "{\"username\":\"admin\", \"password\":\"123456\"}";
            HttpContent post = new StringContent(payload, Encoding.UTF8, "application/json");
           
            var response = await client.PostAsync("/api/login", post);
           
            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            var stringResponse = await response.Content.ReadAsStringAsync();

            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + stringResponse);
            
            // Act - Get
            var getResponse = await client.GetAsync("/api/contractors");
           
            // Assert
            getResponse.EnsureSuccessStatusCode();
            var stringGetRes = await getResponse.Content.ReadAsStringAsync();
           
            var contractors = JsonConvert.DeserializeObject<ICollection<EmployeeDTO>>(stringGetRes);

            Assert.Equal(1, contractors.Count);

            // Act - Edit then get
            var putPayload = "{\"lastName\":\"Contractor\", \"firstName\":\"Edited\", \"supervisorEmployeeNumber\": 10, \"bio\": \"added bio\"}";
            HttpContent put = new StringContent(putPayload, Encoding.UTF8, "application/json");
           
            var putResponse = await client.PutAsync("/api/contractors/51", put);
            putResponse.EnsureSuccessStatusCode();

            var putThenGetResponse = await client.GetAsync("/api/contractors");
            putThenGetResponse.EnsureSuccessStatusCode();

            var putThenGetRes = await putThenGetResponse.Content.ReadAsStringAsync();
            var putContractors = JsonConvert.DeserializeObject<ICollection<EmployeeDTO>>(putThenGetRes);

            Assert.Equal(1, putContractors.Count);
            Assert.Contains(putContractors, c => c.LastName == "Contractor" 
                                                && c.FirstName == "Edited"
                                                && c.Bio == "added bio"
                                                && c.EmployeeNumber == 51
                                                && c.SupervisorEmployeeNumber == 10);

        }

        [Fact]
        public async Task GetAddOrEditWithoutToken_ShouldFail()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act - Get
            var getResponse = await client.GetAsync("/api/contractors");
            Assert.Equal(System.Net.HttpStatusCode.Unauthorized, getResponse.StatusCode);

            var addPostPayload = "{\"lastName\":\"Contractor\", \"firstName\":\"Name\", \"companyCode\":\"01\", \"officeCode\":\"01\", \"groupCode\": \"01\", \"locationId\": \"01\", \"supervisorEmployeeNumber\": 1}";
            HttpContent addPost = new StringContent(addPostPayload, Encoding.UTF8, "application/json");
            
            var addResponse = await client.PostAsync("/api/contractors", addPost);
            Assert.Equal(System.Net.HttpStatusCode.Unauthorized, addResponse.StatusCode);

            var putPayload = "{\"lastName\":\"Contractor\", \"firstName\":\"Edited\", \"supervisorEmployeeNumber\": 10, \"bio\": \"added bio\"}";
            HttpContent put = new StringContent(putPayload, Encoding.UTF8, "application/json");

            var putResponse = await client.PutAsync("/api/contractors/51", put);
            Assert.Equal(System.Net.HttpStatusCode.Unauthorized, putResponse.StatusCode);
           
        }
        
    }
}