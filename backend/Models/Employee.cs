using System;
using System.Collections.Generic;

#nullable disable

namespace AeDirectory.Models
{
/**
* If anyone wants to change this class, please discord @Ricky
* But the recommendation is that inheriting this class and making a new one by any change. 
*/
    public partial class Employee
    {
        public Employee()
        {
            EmployeeSkills = new HashSet<EmployeeSkill>();
            InverseSupervisorEmployeeNumberNavigation = new HashSet<Employee>();
        }

        public bool IsContractor { get; set; }
        public int? SupervisorEmployeeNumber { get; set; }
        public int EmployeeNumber { get; set; }
        public string CompanyCode { get; set; }
        public string OfficeCode { get; set; }
        public string GroupCode { get; set; }
        public string LocationId { get; set; }
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

        public virtual Company CompanyCodeNavigation { get; set; }
        public virtual CompanyOfficeGroup CompanyOfficeGroup { get; set; }
        public virtual Location Location { get; set; }
        public virtual Office Office { get; set; }
        public virtual Employee SupervisorEmployeeNumberNavigation { get; set; }
        public virtual ICollection<EmployeeSkill> EmployeeSkills { get; set; }
        public virtual ICollection<Employee> InverseSupervisorEmployeeNumberNavigation { get; set; }
    }
}
