using System;
using System.Collections.Generic;
using System.Text.Json;
using AeDirectory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

namespace QuickStart.Controllers
{
	[ApiController]
	[Route("")]
	public class DefaultController : ControllerBase
	{
		private readonly ILogger<DefaultController> _logger;

		public DefaultController(ILogger<DefaultController> logger)
		{
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


		[Route("test1")] // GET api/test1
		[HttpGet]
		public ActionResult<Object> TestGetWithNoInput()
		{
			Category testCategory = new Category(222, "label222", "sort222", new List<Skill>());

			var response = new
			{
				Status = "TestGetWithNoInput",
				Data = testCategory,

			};
			return response;
		}

		[Route("test2")] // GET api/test2
		[HttpGet]
		public ActionResult<Object> TestGetWithInput([FromBody] object value)
		{
			Category testCategory = new Category(222, "label222", "sort222", new List<Skill>());

			var response = new
			{
				Status = "TestGetWithInput",
				Data = testCategory,
				Input = value,
			};
			return response;
		}

		[Route("test3")] // POST api/test3
		[HttpPost]
		public ActionResult<Object> TestPostWithInput([FromBody] object value)
		{
			Category testCategory = new Category(222, "label222", "sort222", new List<Skill>());

			var response = new
			{
				Status = "TestPostWithInput",
				Data = testCategory,
				Input = value,
			};
			return response;
		}


	}
}