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
		public List<EmployeeDTO> GetEmployeeByFilters([FromBody] object filterJSON)
		{
			string jsonString = JsonSerializer.Serialize(filterJSON);
			Filter filters = JsonSerializer.Deserialize<Filter>(jsonString);
			return _employeeService.GetEmployeeByFilters(filters);
		}


	}
}