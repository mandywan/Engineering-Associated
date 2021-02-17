using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using AeDirectory.Services;
using AeDirectory.DTO;
using AeDirectory.Domain;
using System.Text.Json;

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


		[Route("/search")]
		[HttpPost]
		// POST: /search
		public List<EmployeeDTO> GetEmployeeByFilters([FromBody] object filterJSON)
		{
			// example of JSON expected:
/*			{
				"Companies": [],
				"Offices": [],
				"Groups": [],
				"Locations": [],
				"Skills": [],
				"Category_id": null,
				"LastName": "Acme",
				"FirstName": null,
				"Title": null,
				"HireDate": null,
				"TerminationDate": null,
				"YearsPriorExperience": null,
				"Email": null,
				"WorkPhone": null,
				"WorkCell": null,
				"UseAND": false
			}*/

			string jsonString = JsonSerializer.Serialize(filterJSON);
			Filter filters = JsonSerializer.Deserialize<Filter>(jsonString);

			return _employeeService.GetEmployeeByFilters(filters);
		}


	}
}