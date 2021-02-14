using System;
using System.Collections.Generic;

namespace AeDirectory
{
    public class OrgChart
    {

        public Employee Supervisor { get; set; }
        public List<Employee> Peers { get; set; }
        public List<Employee> Subordinates { get; set; }

        public OrgChart(Employee supervisor, List<Employee> peers, List<Employee> subordinates)
        {
            Supervisor = supervisor;
            Peers = peers;
            Subordinates = subordinates;
        }

    }
}