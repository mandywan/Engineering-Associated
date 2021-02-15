using System;
using System.Collections.Generic;

#nullable disable

namespace AeDirectory.Models
{
/**
* If anyone wants to change this class, please discord @Ricky
* But the recommendation is that inheriting this class and making a new one by any change. 
*/
    public partial class Location
    {
        public Location()
        {
            Employees = new HashSet<Employee>();
        }

        public string LocationId { get; set; }
        public string Label { get; set; }
        public string SortValue { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
