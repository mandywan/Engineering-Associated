using System;
using AutoMapper;
using System.Collections.Generic;

#nullable disable

namespace AeDirectory.DTO
{
    public partial class EmployeeDTO
    {
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
    }
    
    public class EmployeeProfile:Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Models.Employee, EmployeeDTO>();
            // CreateMap<EmployeeDTO, Models.Employee>();
        }
    }

}