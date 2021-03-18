using System;
using System.Collections.Generic;

#nullable disable

namespace AeDirectory.Models
{
/**
* If anyone wants to change this class, please discord @Ricky
* But the recommendation is that inheriting this class and making a new one by any change. 
*/
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
