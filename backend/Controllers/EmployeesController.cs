using System;
using System.Collections.Generic;
using System.Linq;
using AeDirectory.DTO;
using AeDirectory.Models;
using AeDirectory.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AeDirectory.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController: ControllerBase
    {
        
        private readonly ILogger<DefaultController> _logger;
        private readonly IEmployeeService _employeeService;

        
        public EmployeesController(ILogger<DefaultController> logger, IEmployeeService employeeService)
        {
            _employeeService = employeeService;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        
        [HttpGet]
        // GET: /api/employees
        public List<EmployeeDTO> GetEmployees()
        {
            return _employeeService.GetEmployeeList();
        }

        [HttpGet("{id}")]
        // GET: /api/employees/:id
        public EmployeeDTO GetEmployee(int id)
        {
            return _employeeService.GetEmployeeByEmployeeNumber(id);
        }




    }
}