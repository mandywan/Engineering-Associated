using System;
using Newtonsoft.Json;

namespace AeDirectory.DTO
{
    public class ContractorUpdateDTO
    {
        [JsonProperty("supervisorEmployeeNumber")]
        public int SupervisorEmployeeNumber { get; set; } 

        [JsonProperty("companyCode")] 
        public string CompanyCode { get; set; }
        
        [JsonProperty("officeCode")]
        public string OfficeCode { get; set; }
        
        [JsonProperty("groupCode")]
        public string GroupCode { get; set; }
        
        [JsonProperty("locationId")]
        public string LocationId { get; set; }
        
        [JsonProperty("lastName")]
        public string LastName { get; set; }
        
        [JsonProperty("firstName")]
        public string FirstName { get; set; }
        
        [JsonProperty("employmentType")]
        public string EmploymentType { get; set; }
        
        [JsonProperty("title")]
        public string Title { get; set; }
        
        [JsonProperty("hireDate")]
        public DateTime? HireDate { get; set; }

        [JsonProperty("terminationDate")] public DateTime? TerminationDate { get; set; }
        
        [JsonProperty("yearsPriorExperience")]
        public decimal YearsPriorExperience { get; set; }
        
        [JsonProperty("email")]
        public string Email { get; set; }
        
        [JsonProperty("workPhone")]
        public string WorkPhone { get; set; }
        
        [JsonProperty("workCell")]
        public string WorkCell { get; set; }
        
        [JsonProperty("photoUrl")]
        public string PhotoUrl { get; set; }

        [JsonProperty("bio")]
        public string Bio { get; set; }

        [JsonProperty("extraInfo")]
        public string ExtraInfo { get; set; }

    }
}