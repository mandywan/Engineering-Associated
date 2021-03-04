using System;
using System.Collections.Generic;
using AeDirectory.DTO;
using AeDirectory.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AeDirectory.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContractorsController : ControllerBase
    {
        private readonly ILogger<ContractorsController> _logger;
        private readonly IContractorService _contractorService;
        
        public ContractorsController(ILogger<ContractorsController> logger, IContractorService contractorService)
        {
            _contractorService = contractorService;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        
        [EnableCors("AllowAnyOrigin")]
        [HttpGet]
        // GET: api/contractors
        public List<ContractorDTO> GetContractors()
        {
            return _contractorService.GetContractorList();
        }
        
        [EnableCors("AllowAnyOrigin")]
        [HttpGet("{id}")]
        // GET: api/contractors/:id
        public ContractorDTO GetContractorByEmployeeNumber(int id)
        {
            return _contractorService.GetContractorByEmployeeNumber(id);
        }
        
        [EnableCors("AllowAnyOrigin")]
        [HttpPost]
        // todo uncomment this when this feature is done
        // [Authorize]
        // Admin must login before posting this endpoint
        
        // POST: api/contractors
        public IActionResult AddContractor ([FromBody] ContractorAddDTO request)
        {
            int rowAffected = _contractorService.AddContractor(request);
            if (rowAffected == 1)
            {
                return Ok("Add Success");
            } else {
                return Ok("Add Failure");
            }
        }
        
        [EnableCors("AllowAnyOrigin")]
        [HttpPut("{id}")]
        // todo uncomment this when this feature is done
        // [Authorize]
        // Admin must login before posting this endpoint
        
        // PUT /api/Contractors/{id}
        public IActionResult UpdateContractor (int id, [FromBody] ContractorUpdateDTO request)
        {
            int rowAffected = _contractorService.UpdateContractor(request, id);
            if (rowAffected == 1)
            {
                return Ok("Update Success");
            } else {
                return Ok("Update Failure");
            }
        }
        
        [EnableCors("AllowAnyOrigin")]
        [HttpDelete("{id}")]
        // todo uncomment this when this feature is done
        // [Authorize]
        // Admin must login before posting this endpoint
        
        // Delete /api/Contractors/{id}
        public IActionResult DeleteContractor (int id)
        {
            int rowAffected = _contractorService.DeleteContractor(id);
            if (rowAffected == 1)
            {
                return Ok("Delete Success");
            } else {
                return Ok("Delete Failure");
            }
        }
    }
}