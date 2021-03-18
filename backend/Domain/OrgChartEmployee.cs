using System;
using System.Collections.Generic;

namespace AeDirectory.Domain
{
    public class OrgChartEmployee
    {
        public int EmployeeNumber { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string PhotoUrl { get; set; }
        public string CompanyCode { get; set; }
        public bool IsContractor { get; set; }        
        public int Level { get; set; }

    }
}