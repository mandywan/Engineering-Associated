using System.Collections.Generic;

#nullable disable

namespace AeDirectory.Models
{
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
