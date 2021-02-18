using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;

using AeDirectory.Services;
using AeDirectory.DTO;
using AeDirectory.Domain;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AeDirectory.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrgChartController: ControllerBase
    {
        
        private readonly ILogger<OrgChartController> _logger;
        private readonly IOrgChartService _orgChartService;

        
        public OrgChartController(ILogger<OrgChartController> logger, IOrgChartService orgChartService)
        {
            _orgChartService = orgChartService;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet("{id}")]
        // GET: /orgchart/:id
        public List<Domain.OrgChartEmployee> GetOrgChart(int id)
        {
            List<Domain.OrgChartEmployee> OrgChartList = new List<Domain.OrgChartEmployee> (); 

            var OrgChartSuper = new OrgChartEmployee();
            var supervisor = _orgChartService.GetSupervisorByEmployeeNumber(id);
            OrgChartSuper.CompanyCode = supervisor.CompanyCode;
            OrgChartSuper.EmployeeNumber = supervisor.EmployeeNumber;
            OrgChartSuper.FirstName = supervisor.FirstName;
            OrgChartSuper.LastName = supervisor.LastName;
            OrgChartSuper.PhotoUrl = supervisor.PhotoUrl;
            OrgChartSuper.IsContractor = supervisor.IsContractor;
            OrgChartSuper.Level = 0;

            OrgChartList.Add(OrgChartSuper);

            var peers = _orgChartService.GetPeersByEmployeeNumber(id);
            foreach (var p in peers) {
                var OrgChartPeer = new OrgChartEmployee();
                OrgChartPeer.CompanyCode = p.CompanyCode;
                OrgChartPeer.EmployeeNumber = p.EmployeeNumber;
                OrgChartPeer.FirstName = p.FirstName;
                OrgChartPeer.LastName = p.LastName;
                OrgChartPeer.PhotoUrl = p.PhotoUrl;
                OrgChartPeer.IsContractor = p.IsContractor;
                OrgChartPeer.Level = 1;

                OrgChartList.Add(OrgChartPeer);
            }

            var subords = _orgChartService.GetSubordinatesByEmployeeNumber(id);
            foreach (var s in subords) {
                var OrgChartSubor = new OrgChartEmployee();
                OrgChartSubor.CompanyCode = s.CompanyCode;
                OrgChartSubor.EmployeeNumber = s.EmployeeNumber;
                OrgChartSubor.FirstName = s.FirstName;
                OrgChartSubor.LastName = s.LastName;
                OrgChartSubor.PhotoUrl = s.PhotoUrl;
                OrgChartSubor.IsContractor = s.IsContractor;
                OrgChartSubor.Level = 2;

                OrgChartList.Add(OrgChartSubor);
            }

            return OrgChartList;
        }
        
    }
}