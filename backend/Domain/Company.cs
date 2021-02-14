using System;
using System.Collections.Generic;

namespace AeDirectory
{
    public class Company
    {

        public int Comapny_id { get; set; }
        public string Label { get; set; }
        public Employee Manager { get; set; }
        public List<Office> Offices { get; set; }

        public Company(int comapny_id, string label, Employee manager, List<Office> offices)
        {
            Comapny_id = comapny_id;
            Label = label;
            Manager = manager;
            Offices = offices;
        }
    }
}
