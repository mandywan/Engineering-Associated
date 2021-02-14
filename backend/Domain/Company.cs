using System;

namespace AeDirectory
{
    public class Company
    {

        public int Comapny_id { get; set; }
        public string Label { get; set; }
        public Employee Manager { get; set; }
        public Office[] Offices { get; set; }

        public Company(int comapny_id, string label, Employee manager, Office[] offices)
        {
            Comapny_id = comapny_id;
            Label = label;
            Manager = manager;
            Offices = offices;
        }
    }
}
