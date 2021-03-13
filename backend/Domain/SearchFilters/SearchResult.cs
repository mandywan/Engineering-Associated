using AeDirectory.DTO;
using System;
using System.Collections.Generic;

#nullable enable

namespace AeDirectory.Search
{
    public class SearchResult
    {
        public int? total { get; set; }
        public List<EmployeeDTO>? results { get; set; }

    }
}