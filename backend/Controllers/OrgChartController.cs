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
using Microsoft.AspNetCore.Cors;

namespace AeDirectory.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrgChartController: ControllerBase
    {
        
        private readonly ILogger<OrgChartController> _logger;
        private readonly IOrgChartService _orgChartService;

        
        public OrgChartController(ILogger<OrgChartController> logger, IOrgChartService orgChartService)
        {
            _orgChartService = orgChartService;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [EnableCors("AllowAnyOrigin")]
        [HttpGet("{id}")]
        // GET: api/orgchart/:id
        public List<Domain.OrgChartEmployee> GetOrgChart(int id)
        {
            List<Domain.OrgChartEmployee> OrgChartList = new List<Domain.OrgChartEmployee> (); 

            var OrgChartSuper = new OrgChartEmployee();
            var supervisor = _orgChartService.GetSupervisorByEmployeeNumber(id);
            OrgChartSuper.EmployeeNumber = supervisor.EmployeeNumber;
            OrgChartSuper.FirstName = supervisor.FirstName;
            OrgChartSuper.LastName = supervisor.LastName;
            OrgChartSuper.Title = supervisor.Title;
            OrgChartSuper.PhotoUrl = supervisor.PhotoUrl;
            OrgChartSuper.CompanyCode = supervisor.CompanyCode;
            OrgChartSuper.OfficeCode = supervisor.OfficeCode;
            OrgChartSuper.GroupCode = supervisor.GroupCode;
            OrgChartSuper.Email = supervisor.Email;
            OrgChartSuper.IsContractor = supervisor.IsContractor;
            OrgChartSuper.Level = 0;

            OrgChartList.Add(OrgChartSuper);

            var peers = _orgChartService.GetPeersByEmployeeNumber(id);
            foreach (var p in peers) {
                var OrgChartPeer = new OrgChartEmployee();
                OrgChartPeer.EmployeeNumber = p.EmployeeNumber;
                OrgChartPeer.FirstName = p.FirstName;
                OrgChartPeer.LastName = p.LastName;
                OrgChartPeer.Title = p.Title;
                OrgChartPeer.PhotoUrl = p.PhotoUrl;
                OrgChartPeer.CompanyCode = p.CompanyCode;
                OrgChartPeer.OfficeCode = p.OfficeCode;
                OrgChartPeer.GroupCode = p.GroupCode;
                OrgChartPeer.Email = p.Email;
                OrgChartPeer.IsContractor = p.IsContractor;
                OrgChartPeer.Level = 1;

                OrgChartList.Add(OrgChartPeer);
            }

            var subords = _orgChartService.GetSubordinatesByEmployeeNumber(id);
            foreach (var s in subords) {
                var OrgChartSubor = new OrgChartEmployee();
                OrgChartSubor.EmployeeNumber = s.EmployeeNumber;
                OrgChartSubor.FirstName = s.FirstName;
                OrgChartSubor.LastName = s.LastName;
                OrgChartSubor.Title = s.Title;
                OrgChartSubor.PhotoUrl = s.PhotoUrl;
                OrgChartSubor.CompanyCode = s.CompanyCode;
                OrgChartSubor.OfficeCode = s.OfficeCode;
                OrgChartSubor.GroupCode = s.GroupCode;
                OrgChartSubor.Email = s.Email;
                OrgChartSubor.IsContractor = s.IsContractor;
                OrgChartSubor.Level = 2;

                OrgChartList.Add(OrgChartSubor);
            }

            return OrgChartList;
        }
        
    }
}