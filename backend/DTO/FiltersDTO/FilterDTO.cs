using System;
using System.Collections.Generic;

namespace AeDirectory.DTO.FiltersDTO
{
    /**
     * Filter DTO object. A filter can be a hierarchy filter or a regular filter
     * @filter_name: name of the filter
     * @filter_id_name: id name of the filter. Used to assemble hierarchy filter id's
     * @filter_display: display name of the filter
     * @filter_parent: name of the parent of the filter. Used to assemble hierarchy filter
     * @attach_parent: if composite search value is required
     * @isFilterable: whether the filter can be used for filtering
     * @filter_input: possible inputs of the filter:
     *  - for hierarchy filter, this will be an array of composite keys
     *  - for regular filter, this will be possible input format
     */
    public class FilterDTO
    {
        public string filter_name { get; set; }
        public string filter_id_name { get; set; }
        public string filter_display { get; set; }
        public string filter_parent { get; set; }
        public Boolean attach_parent { get; set; }
        public Boolean isFilterable { get; set; }

        public FilterDTO()
        {
        }
    }

    public class HierarchyFilter : FilterDTO
    {
        public List<FilterInputDTO> filter_inputs { get; set; }

        public HierarchyFilter(
            string name,
            string id_name,
            string display,
            string parent,
            Boolean attach_parent,
            Boolean isFilterable,
            List<FilterInputDTO> inputs)
        {
            this.filter_name = name;
            this.filter_id_name = id_name;
            this.filter_display = display;
            this.filter_parent = parent;
            this.attach_parent = attach_parent;
            this.isFilterable = isFilterable;
            this.filter_inputs = inputs;
        
        }
    }

    public class RegularFilter : FilterDTO
    {
        public FilterTypes filter_input { get; set; }

        public RegularFilter(
            string name,
            string id_name,
            string display,
            string parent,
            Boolean attach_parent,
            Boolean isFilterable,
            FilterTypes type)
        {
            this.filter_name = name;
            this.filter_id_name = id_name;
            this.filter_display = display;
            this.filter_parent = parent;
            this.attach_parent = attach_parent;
            this.isFilterable = isFilterable;
            this.filter_input = type;

        }
    }


}
