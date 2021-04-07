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

            Console.WriteLine(stringResponse);
            
            // HierarchyFilters
            // var hierarchyFilters = JsonConvert.DeserializeObject<List<HierarchyFilter>>(stringResponse);
            
            // Company, Office, Group
            // Assert.Contains(hierarchyFilters, e => e.filter_name == "Company" && e.filter_inputs.Count == 2);
            // Assert.Contains(hierarchyFilters, e =>
            //     e.filter_id_name == "OfficeId" 
            //     && e.filter_parent == "Company"
            //     && e.filter_inputs.Exists(input => 
            //         input.value_id.SequenceEqual(new List<string> {"1", "11"}) && input.value_name=="O1"));
            // Assert.Contains(hierarchyFilters, e =>
            //     e.filter_name == "Group" 
            //     && e.filter_parent == "Office"
            //     && e.filter_inputs.Exists(input => 
            //         input.value_id.SequenceEqual(new List<string> {"1", "11", "111"}) && input.value_name=="G1")
            //     && e.filter_inputs.Exists(input => 
            //         input.value_id.SequenceEqual(new List<string> {"2", "11", "222"}) && input.value_name=="G2")
            //     && e.filter_inputs.Exists(input => 
            //         input.value_id.SequenceEqual(new List<string> {"2", "11", "333"}) && input.value_name=="G3"));
            
            // Skills
            // Assert.Contains(hierarchyFilters, e => 
            //     e.filter_name == "Skill" 
            //     && e.filter_parent == "Category" 
            //     && e.filter_inputs.Count == 1);
            
            // RegularFilters
            // var regularFilters = JsonConvert.DeserializeObject<List<RegularFilter>>(stringResponse);
            // Assert.Contains(regularFilters, e => e.filter_name == "Name" && e.filter_input == FilterTypes.text_input);
            // Assert.Contains(regularFilters, e => e.filter_name == "YearsPriorExperience" && e.filter_input == FilterTypes.numeric_input);
            // Assert.Contains(regularFilters, e => e.filter_name == "HireDate" && e.filter_input == FilterTypes.date_input);

            // total filters
            // Assert.Equal(16, regularFilters.Count);


        }
    }
}