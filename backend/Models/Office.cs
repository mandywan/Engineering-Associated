using System.Collections.Generic;

#nullable disable

namespace AeDirectory.Models
{
    public partial class Office
    {
        public Office()
        {
            CompanyOfficeGroups = new HashSet<CompanyOfficeGroup>();
            Employees = new HashSet<Employee>();
        }

        public string CompanyCode { get; set; }
        public string OfficeCode { get; set; }
        public string Label { get; set; }
        public int ManagerEmployeeNumber { get; set; }

        public virtual Company CompanyCodeNavigation { get; set; }
        public virtual ICollection<CompanyOfficeGroup> CompanyOfficeGroups { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
