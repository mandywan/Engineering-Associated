using System;
using System.Collections.Generic;

namespace AeDirectory
{
    public class Location
    {

        public int Location_id { get; set; }
        public string Label { get; set; }
        public string SortValue { get; set; }
        public List<Employee> Employees { get; set; }

        public Location(int location_id, string label, string sortValue, List<Employee> employees)
        {
            Location_id = location_id;
            Label = label;
            SortValue = sortValue;
            Employees = employees;
        }

    }
}
