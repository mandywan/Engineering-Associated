using System;

namespace AeDirectory
{
    public class Contractor
    {
        public int Employee_id { get; set; }
        public int Company_id { get; set; }
        public int Office_id { get; set; }
        public int Group_id { get; set; }
        public int Location_id { get; set; }
        public Skill[] Skills { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Title { get; set; }
        public DateTime HireDate { get; set; }
        public DateTime TerminationDate { get; set; }
        public int SupervisorEmployeeNo { get; set; }
        public int YearsPriorExperience { get; set; }
        public string Email { get; set; }
        public string WorkPhone { get; set; }
        public string WorkCell { get; set; }
        public string PhotoUrl { get; set; }

        public Contractor(
            int employee_id, 
            int company_id, 
            int office_id, 
            int group_id, 
            int location_id, 
            Skill[] skills,
            string lastName, 
            string firstName,
            string title, 
            DateTime hireDate, 
            DateTime terminationDate,
            int supervisorEmployeeNo,
            int yearsPriorExperience,
            string email,
            string workPhone,
            string workCell,
            string photoUrl
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
        }

    }
}