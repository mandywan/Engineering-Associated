using System;
using System.Collections.Generic;
using System.Linq;
using AeDirectory.DTO;
using AeDirectory.Models;
using AeDirectory.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AeDirectory.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeesController: ControllerBase
    {
        
        private readonly ILogger<EmployeesController> _logger;
        private readonly IEmployeeService _employeeService;

        
        public EmployeesController(ILogger<EmployeesController> logger, IEmployeeService employeeService)
        {
            _employeeService = employeeService;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [EnableCors("AllowAnyOrigin")]
        [HttpGet]
        // GET: /employees
        public List<EmployeeDTO> GetEmployees()
        {
            return _employeeService.GetEmployeeList();
        }

        [EnableCors("AllowAnyOrigin")]
        [HttpGet("{id}")]
        // GET: /employees/:id
        public EmployeeDTO GetEmployee(int id)
        {
            return _employeeService.GetEmployeeByEmployeeNumber(id);
        }

        [EnableCors("AllowAnyOrigin")]
        [HttpPost]
        [Authorize]
        // Admin must login before posting this endpoint
        // POST: /employees
        public IActionResult AddEmployee([FromBody] EmployeeDTO request)
        {
            // todo
            // stretch goal
            return Ok();
        }


    }
}