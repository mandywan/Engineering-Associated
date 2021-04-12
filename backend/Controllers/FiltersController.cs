using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Cors;

using AeDirectory.Services;

namespace AeDirectory.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FiltersController : ControllerBase
    {

        private readonly ILogger<FiltersController> _logger;
        private readonly IFiltersService _filtersService;


        public FiltersController(ILogger<FiltersController> logger, IFiltersService filersService)
        {
            _filtersService = filersService;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [EnableCors("AllowAnyOrigin")]
        [HttpGet]
        // GET: api/filters
        public object[] GetFilters()
        {

            var filters = _filtersService.GetFilters();
            return filters.Cast<object>().ToArray();
        }

    }
}