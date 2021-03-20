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
        public string Title { get; set; }
        public string CompanyCode { get; set; }
         public string OfficeCode { get; set; }
        public string GroupCode { get; set; }
        public bool IsContractor { get; set; }        
        public int Level { get; set; }

    }
}