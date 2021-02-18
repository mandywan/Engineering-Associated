using System;
using System.Collections.Generic;
using System.Linq;
using AeDirectory.DTO;
using AeDirectory.Models;
using AeDirectory.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AeDirectory.DTO.FiltersDTO;
using System.Text.Json.Serialization;
using System.Text.Json;

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

        [HttpGet]
        // GET: /api/filters
        [Produces("application/json")]
        public string GetFilters()
        {
            var options = new JsonSerializerOptions
            {
                Converters = {new JsonStringEnumConverter()}
            };

            // hack to serialize subclasses properly
            string jsonResults = "[";
            var filters = _filtersService.GetFilters();
            foreach (var f in filters)
            {
                jsonResults += JsonSerializer.Serialize(f, f.GetType(), options) + ",";
            }
            jsonResults = jsonResults.Trim(',');
            jsonResults += "]";
            return jsonResults;
        }

    }
}