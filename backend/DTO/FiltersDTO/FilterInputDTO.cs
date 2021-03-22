using System;
using System.Collections.Generic;

namespace AeDirectory.DTO.FiltersDTO
{
    /**
     * Filter input name and values
     * @value_name: display name of the filter
     * @value_id: key of the filter
     */
    public class FilterInputDTO
    {
        public string value_name { get; set; }
        public List<string> value_id { get; set; }
        public FilterInputDTO(string name, List<String> id)
        {
            this.value_name = name;
            this.value_id = id;
        }
    }
}
