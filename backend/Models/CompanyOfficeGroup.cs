using System.Collections.Generic;

#nullable disable

namespace AeDirectory.Models
{
    public partial class CompanyOfficeGroup
    {
        public CompanyOfficeGroup()
        {
            Employees = new HashSet<Employee>();
        }

        public string CompanyCode { get; set; }
        public string OfficeCode { get; set; }
        public string GroupCode { get; set; }
        public string Label { get; set; }
        public int ManagerEmployeeNumber { get; set; }

        public virtual Office Office { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
