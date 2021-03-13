using System;
using System.Collections.Generic;

#nullable enable

namespace AeDirectory.Search
{
    public class Filter
    {

        public FilterById? Company { get; set; }
        public FilterById? Office { get; set; }
        public FilterById? Group { get; set; }
        public FilterById? Location { get; set; }
        public FilterById? Skill { get; set; }
        public FilterById? Category { get; set; }
        public FilterByString? LastName { get; set; }
        public FilterByString? FirstName { get; set; }
        public FilterByString? Name { get; set; }
        public FilterByString? Title { get; set; }
        public FilterByDateTime? HireDate { get; set; }
        public FilterByDateTime? TerminationDate { get; set; }
        public FilterByDecimal? YearsPriorExperience { get; set; }
        public FilterByString? Email { get; set; }
        public FilterByString? WorkPhone { get; set; }
        public FilterByString? WorkCell { get; set; }

        public int? EntriesStart { get; set; }
        public int? EntriesCount { get; set; }
        public int? Total { get; set; }
    }
}


/*
    {
        "Company" : {
            "type": "OR",
            "values": [{ComapnyId: "0"}, {ComapnyId: "0"}]
        },
  
        "Office" : {
            "type": "OR",
            "values": [{ComapnyId: "0", OfficeId:"0"}, {ComapnyId: "0", OfficeId:"0"}]
        },
   
        "FirstName": {
            "type": null,
            "values": ["George"]
        },
  
        "LastName": {
            "type": null,
            "values": ["Zhang"]
        },
  
        "Name": {
            "type": null,
            "values": ["George"]
        }
    }
*/