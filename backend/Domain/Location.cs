using System;
using System.Collections.Generic;

namespace AeDirectory.Domain
{
    public class Location
    {

        public string Location_id { get; set; }
        public string Label { get; set; }
        public string SortValue { get; set; }
        public List<Employee> Employees { get; set; }

        public Location(string location_id, string label, string sortValue, List<Employee> employees)
        {
            Location_id = location_id;
            Label = label;
            SortValue = sortValue;
            Employees = employees;
        }

    }
}
