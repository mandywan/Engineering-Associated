using System.Collections.Generic;

#nullable disable

namespace AeDirectory.Models
{
    public partial class Company
    {
        public Company()
        {
            Employees = new HashSet<Employee>();
            Offices = new HashSet<Office>();
        }

        public string CompanyCode { get; set; }
        public string Label { get; set; }
        public int ManagerEmployeeNumber { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<Office> Offices { get; set; }
    }
}
