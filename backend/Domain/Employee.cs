using System;
using System.Collections.Generic;

#nullable enable

namespace AeDirectory.Domain
{
    public class Employee
    {

        public int Employee_id { get; set; }
        public string Company_id { get; set; }
        public string Office_id { get; set; }
        public string Group_id { get; set; }
        public string Location_id { get; set; }
        public List<Skill> Skills { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Title { get; set; }
        public DateTime? HireDate { get; set; }
        public DateTime? TerminationDate { get; set; }
        public int? SupervisorEmployeeNo { get; set; }
        public decimal? YearsPriorExperience { get; set; }
        public string Email { get; set; }
        public string WorkPhone { get; set; }
        public string WorkCell { get; set; }
        public string PhotoUrl { get; set; }
        public Boolean IsContractor { get; set; }

        public Employee(
            int employee_id,
            string company_id,
            string office_id,
            string group_id,
            string location_id,
            List<Skill> skills,
            string lastName,
            string firstName,
            string title,
            DateTime hireDate,
            DateTime terminationDate,
            int supervisorEmployeeNo,
            decimal yearsPriorExperience,
            string email,
            string workPhone,
            string workCell,
            string photoUrl,
            Boolean isContractor
        ) {
            Employee_id = employee_id;
            Company_id = company_id;
            Office_id = office_id;
            Group_id = group_id;
            Location_id = location_id;
            Skills = skills;
            LastName = lastName;
            FirstName = firstName;
            Title = title;
            HireDate = hireDate;
            TerminationDate = terminationDate;
            SupervisorEmployeeNo = supervisorEmployeeNo;
            YearsPriorExperience = yearsPriorExperience;
            Email = email;
            WorkPhone = workPhone;
            WorkCell = workCell;
            PhotoUrl = photoUrl;
            IsContractor = isContractor;
        }

    }
}