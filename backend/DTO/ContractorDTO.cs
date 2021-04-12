using System;
using AutoMapper;

namespace AeDirectory.DTO
{
    public class ContractorDTO
    {
        public ContractorDTO()
        {
        }
        
        // PK
        public int EmployeeNumber { get; set; }
        
        // FKs
        public string CompanyCode { get; set; }
        public string OfficeCode { get; set; }
        public string GroupCode { get; set; }
        public string LocationId { get; set; }
        public int? SupervisorEmployeeNumber { get; set; }
        
        // fields
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string EmploymentType { get; set; }
        public string Title { get; set; }
        public DateTime? HireDate { get; set; }
        public DateTime? TerminationDate { get; set; }
        public decimal? YearsPriorExperience { get; set; }
        public string Email { get; set; }
        public string WorkPhone { get; set; }
        public string WorkCell { get; set; }
        public string PhotoUrl { get; set; }
        public bool IsContractor { get; set; }
        public string Bio { get; set; }
        public string ExtraInfo { get; set; }

    }
    
    public class ContractorProfile:Profile
    {
        public ContractorProfile()
        {
            CreateMap<Models.Employee, ContractorDTO>();
            CreateMap<ContractorDTO, Models.Employee>();
        }
    }
}