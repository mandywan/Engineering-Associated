using System;

namespace AeDirectory
{
    public class Company
    {

        public int comapny_id { get; set; }
        public string label { get; set; }
        public Employee manager { get; set; }
        public Office[] offices { get; set; }


    }
}
