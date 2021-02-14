using System;

namespace AeDirectory
{
    public class Location
    {

        public int Location_id { get; set; }
        public string Label { get; set; }
        public string SortValue { get; set; }
        public Employee[] Employees { get; set; }

        public Location(int location_id, string label, string sortValue, Employee[] employees)
        {
            Location_id = location_id;
            Label = label;
            SortValue = sortValue;
            Employees = employees;
        }

    }
}
