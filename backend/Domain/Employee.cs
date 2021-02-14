using System;

namespace AeDirectory
{
    public class Employee
    {

        public int employee_id { get; set; }
        public int company_id { get; set; }
        public int office_id { get; set; }
        public int group_id { get; set; }
        public int location_id { get; set; }
        public Skill[] skills { get; set; }
        public string lastName { get; set; }
        public string firstName { get; set; }
        public string title { get; set; }
        public DateTime hireDate { get; set; }
        public DateTime terminationDate { get; set; }
        public int supervisorEmployeeNo { get; set; }
        public int yearsPriorExperience { get; set; }
        public string email { get; set; }
        public string workPhone { get; set; }
        public string workCell { get; set; }
        public string photoUrl { get; set; }
        public Boolean isContractor { get; set; }

    }
}