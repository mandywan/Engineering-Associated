using System;

namespace AeDirectory
{
    public class Location
    {

        public int location_id { get; set; }
        public string label { get; set; }
        public string sortValue { get; set; }
        public Employee[] employees { get; set; }

    }
}
