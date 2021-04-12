using System.Collections.Generic;

using AeDirectory.DTO.FiltersDTO;

namespace AeDirectory.Services
{
    public interface IFiltersService
    {
        public List<FilterDTO> GetFilters();
    }
}
