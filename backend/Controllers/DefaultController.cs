using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using AeDirectory.Services;
using AeDirectory.DTO;
using AeDirectory.Search;
using System.Text.Json;
using Microsoft.AspNetCore.Cors;

namespace AeDirectory.Controllers
{
	[ApiController]
	[Route("")]
	public class DefaultController : ControllerBase
	{
		private readonly IEmployeeService _employeeService;

		private readonly ILogger<DefaultController> _logger;

		public DefaultController(ILogger<DefaultController> logger, IEmployeeService employeeService)
		{
			_employeeService = employeeService;
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		[Route("")]
		[EnableCors("AllowAnyOrigin")]
		[HttpGet]
		public object Get1()
		{
			var responseObject = new
			{
				Status = "Up",
			};
			_logger.LogInformation($"Status pinged: {responseObject.Status}");
			return responseObject;
		}


		[Route("api/search")]
		[EnableCors("AllowAnyOrigin")]
		[HttpPost]
		// POST: api/search
		public SearchResult GetEmployeeByFilters([FromBody] object filterJSON)
		{
			string jsonString = JsonSerializer.Serialize(filterJSON);
			Filter filters = JsonSerializer.Deserialize<Filter>(jsonString);

			List<EmployeeDTO> results = _employeeService.GetEmployeeByFilters(filters);
			int total = results.Count;

			SearchResult finalResult = new SearchResult();
			finalResult.total = total;

			if (filters.EntriesStart != null && filters.EntriesCount != null) {
				if ( ((int)filters.EntriesStart + (int)filters.EntriesCount) <= total) {
					finalResult.results = results.GetRange((int)filters.EntriesStart, (int)filters.EntriesCount);
					return finalResult;
				} else if (filters.EntriesStart < total)  {
					finalResult.results = results.GetRange((int)filters.EntriesStart, total - (int)filters.EntriesStart);
					return finalResult;
				} else {
					finalResult.results = new List<EmployeeDTO>();
					return finalResult;
				}
			}

			finalResult.results = results;
			return finalResult;
		}


	}
}