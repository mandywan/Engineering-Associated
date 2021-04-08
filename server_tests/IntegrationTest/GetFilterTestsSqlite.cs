using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AeDirectory;
using AeDirectory.DTO.FiltersDTO;
using Newtonsoft.Json;
using Xunit;

namespace IntegrationTest
{
    public class GetFilterTestsSqlite : IClassFixture<CustomSQLiteWebApplicationFactory<Startup>>
    {
        private readonly CustomSQLiteWebApplicationFactory<Startup> _factory;

        public GetFilterTestsSqlite(CustomSQLiteWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }
        
        [Fact]
        public async Task GetFilters_ShouldReturnCorrectData()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/api/filters");

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            var stringResponse = await response.Content.ReadAsStringAsync();
            
            // HierarchyFilters
            var hierarchyFilters = JsonConvert.DeserializeObject<List<HierarchyFilter>>(stringResponse);
            
            // Company, Office, Group
            Assert.Contains(hierarchyFilters, e => e.filter_name == "Company" && e.filter_inputs.Count == 3);
            Assert.Contains(hierarchyFilters, e =>
                e.filter_id_name == "CompanyId" 
                && e.filter_parent == null
                && e.filter_display == "Company"
                && e.filter_inputs.Exists(input => 
                    input.value_id.SequenceEqual(new List<string> {"01"}) && input.value_name=="Acme Seeds Inc.")
                && e.filter_inputs.Exists(input => 
                    input.value_id.SequenceEqual(new List<string> {"02"}) && input.value_name=="Acme Planting Ltd.")
                && e.filter_inputs.Exists(input => 
                    input.value_id.SequenceEqual(new List<string> {"03"}) && input.value_name=="Acme Harvesting Ltd."));

            Assert.Contains(hierarchyFilters, e => e.filter_name == "Office" && e.filter_inputs.Count == 5);
            Assert.Contains(hierarchyFilters, e =>
                e.filter_id_name == "OfficeId" 
                && e.filter_parent == "Company"
                && e.filter_display == "Office"
                && e.filter_inputs.Exists(input => 
                    input.value_id.SequenceEqual(new List<string> {"01", "01"}) && input.value_name=="Corporate")
                && e.filter_inputs.Exists(input => 
                    input.value_id.SequenceEqual(new List<string> {"02", "02"}) && input.value_name=="Vancouver")
                && e.filter_inputs.Exists(input => 
                    input.value_id.SequenceEqual(new List<string> {"03", "03"}) && input.value_name=="Kelowna")
                && e.filter_inputs.Exists(input => 
                    input.value_id.SequenceEqual(new List<string> {"03", "04"}) && input.value_name=="Prince George")
                && e.filter_inputs.Exists(input => 
                    input.value_id.SequenceEqual(new List<string> {"03", "05"}) && input.value_name=="Victoria"));

            Assert.Contains(hierarchyFilters, e => e.filter_name == "Group" && e.filter_inputs.Count == 16);
            Assert.Contains(hierarchyFilters, e =>
                e.filter_id_name == "GroupId" 
                && e.filter_parent == "Office"
                && e.filter_display == "Group"
                && e.filter_inputs.Exists(input => 
                    input.value_id.SequenceEqual(new List<string> {"01", "01", "01"}) && input.value_name=="Administration")
                && e.filter_inputs.Exists(input => 
                    input.value_id.SequenceEqual(new List<string> {"01", "01", "02"}) && input.value_name=="Marketing")
                && e.filter_inputs.Exists(input => 
                    input.value_id.SequenceEqual(new List<string> {"01", "01", "03"}) && input.value_name=="Sales")
                && e.filter_inputs.Exists(input => 
                    input.value_id.SequenceEqual(new List<string> {"01", "01", "04"}) && input.value_name=="Accounting")
                && e.filter_inputs.Exists(input => 
                    input.value_id.SequenceEqual(new List<string> {"01", "01", "05"}) && input.value_name=="Human Resources")
                && e.filter_inputs.Exists(input => 
                    input.value_id.SequenceEqual(new List<string> {"02", "02", "01"}) && input.value_name=="Administration")
                && e.filter_inputs.Exists(input => 
                    input.value_id.SequenceEqual(new List<string> {"02", "02", "02"}) && input.value_name=="Marketing")
                && e.filter_inputs.Exists(input => 
                    input.value_id.SequenceEqual(new List<string> {"02", "02", "03"}) && input.value_name=="Database")
                && e.filter_inputs.Exists(input => 
                    input.value_id.SequenceEqual(new List<string> {"03", "04", "04"}) && input.value_name=="Sales")
                && e.filter_inputs.Exists(input => 
                    input.value_id.SequenceEqual(new List<string> {"03", "04", "05"}) && input.value_name=="IT")
                && e.filter_inputs.Exists(input => 
                    input.value_id.SequenceEqual(new List<string> {"03", "04", "06"}) && input.value_name=="Service")
                && e.filter_inputs.Exists(input => 
                    input.value_id.SequenceEqual(new List<string> {"03", "03", "01"}) && input.value_name=="Administration")
                && e.filter_inputs.Exists(input => 
                    input.value_id.SequenceEqual(new List<string> {"03", "03", "03"}) && input.value_name=="Marketing & Sales")
                && e.filter_inputs.Exists(input => 
                    input.value_id.SequenceEqual(new List<string> {"03", "03", "04"}) && input.value_name=="Distribution")
                && e.filter_inputs.Exists(input => 
                    input.value_id.SequenceEqual(new List<string> {"03", "03", "05"}) && input.value_name=="Sales")
                && e.filter_inputs.Exists(input => 
                    input.value_id.SequenceEqual(new List<string> {"03", "05", "09"}) && input.value_name=="Operations"));
            
            Assert.Contains(hierarchyFilters, e => e.filter_name == "Location" && e.filter_inputs.Count == 6);
            Assert.Contains(hierarchyFilters, e =>
                e.filter_id_name == "LocationId" 
                && e.filter_parent == null
                && e.filter_display == "Physical Location"
                && e.filter_inputs.Exists(input => 
                    input.value_id.SequenceEqual(new List<string> {"01"}) && input.value_name=="Burnaby")
                && e.filter_inputs.Exists(input => 
                    input.value_id.SequenceEqual(new List<string> {"02"}) && input.value_name=="Vancouver")
                && e.filter_inputs.Exists(input => 
                    input.value_id.SequenceEqual(new List<string> {"03"}) && input.value_name=="Kelowna")
                && e.filter_inputs.Exists(input => 
                    input.value_id.SequenceEqual(new List<string> {"04"}) && input.value_name=="Prince George")
                && e.filter_inputs.Exists(input => 
                    input.value_id.SequenceEqual(new List<string> {"05"}) && input.value_name=="Victoria")
                && e.filter_inputs.Exists(input => 
                    input.value_id.SequenceEqual(new List<string> {"06"}) && input.value_name=="Surrey"));

            // Skills
            Assert.Contains(hierarchyFilters, e => e.filter_name == "Category" && e.filter_inputs.Count == 4);
            Assert.Contains(hierarchyFilters, e =>
                e.filter_id_name == "CategoryId" 
                && e.filter_parent == null
                && e.filter_display == "Skill Category"
                && e.filter_inputs.Exists(input => 
                    input.value_id.SequenceEqual(new List<string> {"1"}) && input.value_name=="Agriculture")
                && e.filter_inputs.Exists(input => 
                    input.value_id.SequenceEqual(new List<string> {"2"}) && input.value_name=="Accounting")
                && e.filter_inputs.Exists(input => 
                    input.value_id.SequenceEqual(new List<string> {"3"}) && input.value_name=="Management")
                && e.filter_inputs.Exists(input => 
                    input.value_id.SequenceEqual(new List<string> {"4"}) && input.value_name=="Marketing & Sales"));

            Assert.Contains(hierarchyFilters, e => e.filter_name == "Skill" && e.filter_inputs.Count == 15);
            Assert.Contains(hierarchyFilters, e =>
                e.filter_id_name == "SkillId" 
                && e.filter_parent == "Category"
                && e.filter_display == "Skill"
                && e.filter_inputs.Exists(input => 
                    input.value_id.SequenceEqual(new List<string> {"1", "1"}) && input.value_name=="Planting")
                && e.filter_inputs.Exists(input => 
                    input.value_id.SequenceEqual(new List<string> {"1", "2"}) && input.value_name=="Harvesting")
                && e.filter_inputs.Exists(input => 
                    input.value_id.SequenceEqual(new List<string> {"1", "3"}) && input.value_name=="Fertilizing")
                && e.filter_inputs.Exists(input => 
                    input.value_id.SequenceEqual(new List<string> {"1", "4"}) && input.value_name=="Irrigating")
                && e.filter_inputs.Exists(input => 
                    input.value_id.SequenceEqual(new List<string> {"2", "6"}) && input.value_name=="Maths")
                && e.filter_inputs.Exists(input => 
                    input.value_id.SequenceEqual(new List<string> {"2", "7"}) && input.value_name=="Financial Statements")
                && e.filter_inputs.Exists(input => 
                    input.value_id.SequenceEqual(new List<string> {"2", "8"}) && input.value_name=="Statement Analysis")
                && e.filter_inputs.Exists(input => 
                    input.value_id.SequenceEqual(new List<string> {"2", "9"}) && input.value_name=="Projection")
                && e.filter_inputs.Exists(input => 
                    input.value_id.SequenceEqual(new List<string> {"3", "10"}) && input.value_name=="People Skills")
                && e.filter_inputs.Exists(input => 
                    input.value_id.SequenceEqual(new List<string> {"3", "11"}) && input.value_name=="Conflict Resolution")
                && e.filter_inputs.Exists(input => 
                    input.value_id.SequenceEqual(new List<string> {"3", "12"}) && input.value_name=="Work Safe")
                && e.filter_inputs.Exists(input => 
                    input.value_id.SequenceEqual(new List<string> {"3", "13"}) && input.value_name=="Wage Negotiation")
                && e.filter_inputs.Exists(input => 
                    input.value_id.SequenceEqual(new List<string> {"4", "14"}) && input.value_name=="Marketing")
                && e.filter_inputs.Exists(input => 
                    input.value_id.SequenceEqual(new List<string> {"4", "15"}) && input.value_name=="Sales")
                && e.filter_inputs.Exists(input => 
                    input.value_id.SequenceEqual(new List<string> {"4", "16"}) && input.value_name=="Customer Service"));
            
            // RegularFilters
            var regularFilters = JsonConvert.DeserializeObject<List<RegularFilter>>(stringResponse);
            Assert.Contains(regularFilters, e => e.filter_name == "FirstName" && e.filter_input == FilterTypes.text_input);
            Assert.Contains(regularFilters, e => e.filter_name == "LastName" && e.filter_input == FilterTypes.text_input);
            Assert.Contains(regularFilters, e => e.filter_name == "Name" && e.filter_input == FilterTypes.text_input);
            Assert.Contains(regularFilters, e => e.filter_name == "YearsPriorExperience" && e.filter_input == FilterTypes.numeric_input);
            Assert.Contains(regularFilters, e => e.filter_name == "HireDate" && e.filter_input == FilterTypes.date_input);
            Assert.Contains(regularFilters, e => e.filter_name == "TerminationDate" && e.filter_input == FilterTypes.date_input);
            Assert.Contains(regularFilters, e => e.filter_name == "Title" && e.filter_input == FilterTypes.text_input);
            Assert.Contains(regularFilters, e => e.filter_name == "Email" && e.filter_input == FilterTypes.text_input);
            Assert.Contains(regularFilters, e => e.filter_name == "WorkCell" && e.filter_input == FilterTypes.text_input);
            Assert.Contains(regularFilters, e => e.filter_name == "WorkPhone" && e.filter_input == FilterTypes.text_input);
          
            // total filters
            Assert.Equal(16, regularFilters.Count);


        }
    }
}