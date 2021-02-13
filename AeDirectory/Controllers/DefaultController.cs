using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

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

		[Route("test1")]
		[HttpGet]
		public object Get1()
		{
			var responseObject = new
			{
				Status = "Up 1"
			};
			_logger.LogInformation($"Status pinged: {responseObject.Status}");
			return responseObject;
		}

		[Route("test2")]
		[HttpGet]
		public object Get2()
		{
			var responseObject = new
			{
				Status = "Up22"
			};
			_logger.LogInformation($"Status pinged: {responseObject.Status}");
			return responseObject;
		}
	}
}