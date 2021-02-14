using System;

namespace AeDirectory
{
    public class OrgChart
    {

        public Employee Supervisor { get; set; }
        public Employee[] Peers { get; set; }
        public Employee[] Subordinates { get; set; }

        public OrgChart(Employee supervisor, Employee[] peers, Employee[] subordinates)
        {
            Supervisor = supervisor;
            Peers = peers;
            Subordinates = subordinates;
        }

    }
}