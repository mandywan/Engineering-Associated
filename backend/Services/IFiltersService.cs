using System;
using AeDirectory.DTO.FiltersDTO;
using System.Collections.Generic;

namespace AeDirectory.Services
{
    public interface IFiltersService
    {
        public List<FilterDTO> GetFilters();
    }
}
