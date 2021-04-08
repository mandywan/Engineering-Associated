using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using AeDirectory;
using AeDirectory.DTO;
using Newtonsoft.Json;
using Xunit;
using AeDirectory.Domain;

namespace IntegrationTest
{
    public class OrgChartTestSqlite : IClassFixture<CustomSQLiteWebApplicationFactory<Startup>>
    {
        private readonly CustomSQLiteWebApplicationFactory<Startup> _factory;

        public OrgChartTestSqlite(CustomSQLiteWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }
        
        [Fact]
        public async Task GetOrgChartSelfSupervisor_ShouldReturnCorrectData()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/api/orgchart/01");

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299

            var stringResponse = await response.Content.ReadAsStringAsync();

            var orgchart = JsonConvert.DeserializeObject<ICollection<OrgChartEmployee>>(stringResponse);

            Assert.Equal(5, orgchart.Count);
            Assert.Contains(orgchart, o => o.EmployeeNumber == 1 && o.Level == 1);
            Assert.Contains(orgchart, o => o.EmployeeNumber == 2 && o.Level == 2);
            Assert.Contains(orgchart, o => o.EmployeeNumber == 3 && o.Level == 2);
            Assert.Contains(orgchart, o => o.EmployeeNumber == 4 && o.Level == 2);
            Assert.Contains(orgchart, o => o.EmployeeNumber == 12 && o.Level == 2);
        }

                [Fact]
        public async Task GetOrgChartThreeLevelWithPeerSelfSupervisor_ShouldReturnCorrectData()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/api/orgchart/02");

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299

            var stringResponse = await response.Content.ReadAsStringAsync();
            // Console.WriteLine(stringResponse);
            var orgchart = JsonConvert.DeserializeObject<ICollection<OrgChartEmployee>>(stringResponse);

            Assert.Equal(11, orgchart.Count);
            Assert.Contains(orgchart, o => o.EmployeeNumber == 1 && o.Level == 0);
            Assert.Contains(orgchart, o => o.EmployeeNumber == 1 && o.Level == 1);
            Assert.Contains(orgchart, o => o.EmployeeNumber == 2 && o.Level == 1);
            Assert.Contains(orgchart, o => o.EmployeeNumber == 3 && o.Level == 1);
            Assert.Contains(orgchart, o => o.EmployeeNumber == 4 && o.Level == 1);
            Assert.Contains(orgchart, o => o.EmployeeNumber == 12 && o.Level == 1);
            Assert.Contains(orgchart, o => o.EmployeeNumber == 11 && o.Level == 2);
            Assert.Contains(orgchart, o => o.EmployeeNumber == 13 && o.Level == 2);
            Assert.Contains(orgchart, o => o.EmployeeNumber == 22 && o.Level == 2);
            Assert.Contains(orgchart, o => o.EmployeeNumber == 26 && o.Level == 2);
            Assert.Contains(orgchart, o => o.EmployeeNumber == 41 && o.Level == 2);
        }

                
        [Fact]
        public async Task GetOrgChartNoSupervisor_ShouldReturnCorrectData()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/api/orgchart/49");

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299

            var stringResponse = await response.Content.ReadAsStringAsync();
            // Console.WriteLine(stringResponse);
            var orgchart = JsonConvert.DeserializeObject<ICollection<OrgChartEmployee>>(stringResponse);

            Assert.Equal(2, orgchart.Count);
            Assert.Contains(orgchart, o => o.EmployeeNumber == 49 && o.Level == 1);
            Assert.Contains(orgchart, o => o.EmployeeNumber == 50 && o.Level == 2);
        }

        [Fact]
        public async Task GetOrgChartNoSubord_ShouldReturnCorrectData()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/api/orgchart/40");

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299

            var stringResponse = await response.Content.ReadAsStringAsync();
            // Console.WriteLine(stringResponse);
            var orgchart = JsonConvert.DeserializeObject<ICollection<OrgChartEmployee>>(stringResponse);

            Assert.Equal(2, orgchart.Count);
            Assert.Contains(orgchart, o => o.EmployeeNumber == 6 && o.Level == 0);
            Assert.Contains(orgchart, o => o.EmployeeNumber == 40 && o.Level == 1);
        }

        [Fact]
        public async Task GetOrgChartAllLevels_ShouldReturnCorrectData()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/api/orgchart/10");

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299

            var stringResponse = await response.Content.ReadAsStringAsync();
            // Console.WriteLine(stringResponse);
            var orgchart = JsonConvert.DeserializeObject<ICollection<OrgChartEmployee>>(stringResponse);

            Assert.Equal(8, orgchart.Count);
            Assert.Contains(orgchart, o => o.EmployeeNumber == 3 && o.Level == 0);
            Assert.Contains(orgchart, o => o.EmployeeNumber == 5 && o.Level == 1);
            Assert.Contains(orgchart, o => o.EmployeeNumber == 6 && o.Level == 1);
            Assert.Contains(orgchart, o => o.EmployeeNumber == 7 && o.Level == 1);
            Assert.Contains(orgchart, o => o.EmployeeNumber == 8 && o.Level == 1);
            Assert.Contains(orgchart, o => o.EmployeeNumber == 10 && o.Level == 1);
            Assert.Contains(orgchart, o => o.EmployeeNumber == 19 && o.Level == 1);
            Assert.Contains(orgchart, o => o.EmployeeNumber == 20 && o.Level == 2);
        }
        
    }
}