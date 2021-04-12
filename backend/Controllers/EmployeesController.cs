using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using AeDirectory.DTO;
using AeDirectory.Services;

namespace AeDirectory.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
        // GET: api/employees
        public List<EmployeeDTO> GetEmployees()
        {
            return _employeeService.GetEmployeeList();
        }

        [EnableCors("AllowAnyOrigin")]
        [HttpGet("{id}")]
        // GET: api/employees/:id
        public EmployeeDTO GetEmployee(int id)
        {
            return _employeeService.GetEmployeeByEmployeeNumber(id);
        }

    }
}