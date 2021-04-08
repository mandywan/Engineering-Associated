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
    public class EmployeeTestSqlite : IClassFixture<CustomSQLiteWebApplicationFactory<Startup>>
    {
        private readonly CustomSQLiteWebApplicationFactory<Startup> _factory;

        public EmployeeTestSqlite(CustomSQLiteWebApplicationFactory<Startup> factory)
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
            // Console.WriteLine(stringResponse);
            var employees = JsonConvert.DeserializeObject<ICollection<EmployeeDTO>>(stringResponse);

            Assert.Equal(50, employees.Count);
            Assert.Contains(employees, e => e.LastName == "Schaefer");
            Assert.Contains(employees, e => e.FirstName == "Cristy");

        }

        [Fact]
        public async Task GetOneEmployee_ShouldReturnCorrectData()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/api/employees/02");

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299

            var stringResponse = await response.Content.ReadAsStringAsync();

            var employee = JsonConvert.DeserializeObject<EmployeeDTO>(stringResponse);
            Assert.Equal(2, employee.EmployeeNumber);
            Assert.False(employee.IsContractor);
            Assert.Equal("02", employee.CompanyCode);
            Assert.Equal("02", employee.OfficeCode);
            Assert.Equal("02", employee.GroupCode);
            Assert.Equal("Sammy", employee.FirstName);
            Assert.Equal("Kautzer", employee.LastName);
            Assert.Equal("Salary", employee.EmploymentType);
            Assert.Equal("Supervisor", employee.Title);
            DateTime hireDate = new DateTime(2013, 08, 29, 00, 00, 00);
            Assert.Equal(hireDate, employee.HireDate);
            Assert.Null(employee.TerminationDate);
            Assert.Equal(1, employee.SupervisorEmployeeNumber);
            Assert.Equal(1, employee.YearsPriorExperience);
            Assert.Equal("Tyree@amc.com", employee.Email);
            Assert.Equal("680-466-6645", employee.WorkPhone);
            Assert.Equal("251-708-3081", employee.WorkCell);
            Assert.Equal("02", employee.LocationId);
            Assert.Equal("images/999.jpg", employee.PhotoUrl);
            Assert.Equal("Hi, I am  an employee of Associated Engineering", employee.Bio);
            Assert.Equal("Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.", employee.ExtraInfo);
            Assert.Equal(3, employee.Skills.Count);
            Assert.Contains(employee.Skills, s => s.SkillId == "15" && s.SkillCategoryId == "4");
            Assert.Contains(employee.Skills, s => s.SkillId == "2" && s.SkillCategoryId == "1");
            Assert.Contains(employee.Skills, s => s.SkillId == "7" && s.SkillCategoryId == "2");
            Assert.Equal("Langworth", employee.Supervisor.LastName);
            Assert.Equal("Arletha", employee.Supervisor.FirstName);
            Assert.Equal("images/999.jpg", employee.Supervisor.PhotoUrl);
            Assert.Equal("Associate", employee.Supervisor.Title);
            Assert.Equal("03", employee.Supervisor.GroupCode);
            Assert.Equal("01", employee.Supervisor.OfficeCode);
        }
        
    }
}