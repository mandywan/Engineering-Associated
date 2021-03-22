using System;
using System.Collections.Generic;

#nullable disable

namespace AeDirectory.Models
{
/**
* If anyone wants to change this class, please discord @Ricky
* But the recommendation is that inheriting this class and making a new one by any change. 
*/
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
