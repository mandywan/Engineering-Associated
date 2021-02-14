using System;

namespace AeDirectory
{
    public class Office
    {
        public int office_id { get; set; }
        public int comapny_id { get; set; }
        public string label { get; set; }
        public Employee manager { get; set; }
        public Group[] groups { get; set; }


    }
}
