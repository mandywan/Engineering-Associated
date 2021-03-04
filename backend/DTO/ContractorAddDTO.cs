using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace AeDirectory.DTO
{
    public class ContractorAddDTO
    {
        [Required]
        [JsonProperty("lastName")]
        public string Lastname { get; set; }


        [Required]
        [JsonProperty("firstName")]
        public string Firstname { get; set; }
        
        // requirement fields below to add a new contractor due to db constrain
        [Required]
        [JsonProperty("companyCode")]
        public string CompanyCode { get; set; }
        
        [Required]
        [JsonProperty("officeCode")]
        public string OfficeCode { get; set; }
        
        [Required]
        [JsonProperty("groupCode")]
        public string GroupCode { get; set; }
        
        [Required]
        [JsonProperty("locationId")]
        public string LocationId { get; set; }
        
        [JsonProperty("supervisorEmployeeNumber")]
        public int SupervisorEmployeeNumber { get; set; }
        
        // pre-defined 
        public bool isContractor = true;
        
        // non-requirement fields below when posting
        [JsonProperty("employmentType")]
        public string EmploymentType { get; set; }
        
        [JsonProperty("title")]
        public string Title { get; set; }
        
        [JsonProperty("hireDate")]
        public DateTime? HireDate { get; set; }
        
        [JsonProperty("terminationDate")] 
        public DateTime? TerminationDate { get; set; }
        
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