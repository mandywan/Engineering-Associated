using System;
using System.Text.Json;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Cors;

using AeDirectory.Services;
using AeDirectory.DTO;
using AeDirectory.Search;

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
			SearchResult finalResult = new SearchResult();
			if (filters.Category == null && filters.Company == null && filters.Email == null && filters.FirstName == null && filters.Group == null && filters.HireDate == null && filters.LastName == null && filters.Location == null && filters.Name == null && filters.Office == null && filters.Skill == null && filters.TerminationDate == null && filters.Title == null && filters.WorkCell == null && filters.WorkPhone == null && filters.YearsPriorExperience == null) {
				finalResult.total = 0;
				finalResult.results = new List<EmployeeDTO>();
				finalResult.msg = "Please enter a keyword or select a filter to search";
				return finalResult;
			}

			List<EmployeeDTO> results = _employeeService.GetEmployeeByFilters(filters);
			int total = results.Count;
			finalResult.total = total;
			if (total == 0) {
				finalResult.msg = "Looked here, there and everywhere - but couldn't find the person you're looking for.";
			} else {
				finalResult.msg = "";
			}

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