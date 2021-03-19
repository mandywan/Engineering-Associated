using System;
using System.Collections.Generic;

#nullable enable

namespace AeDirectory.Search
{
    public class FilterByDateTime
    {
        public string? type { get; set; }
        public List<DateTime?>? values { get; set; }
        
    }
}