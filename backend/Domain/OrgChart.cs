using System;

namespace AeDirectory
{
    public class OrgChart
    {

        public Employee supervisor { get; set; }
        public Employee[] peers { get; set; }
        public Employee[] subordinates { get; set; }

    }
}