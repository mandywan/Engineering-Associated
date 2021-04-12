using System.Collections.Generic;
using System.Linq;
using AutoMapper;

using AeDirectory.DTO;
using AeDirectory.Models;

namespace AeDirectory.Services
{
    public interface IOrgChartService
    {
        EmployeeDTO GetSupervisorByEmployeeNumber(int id);
        List<EmployeeDTO> GetPeersByEmployeeNumber(int id);
        List<EmployeeDTO> GetSubordinatesByEmployeeNumber(int id);

    }

    public class OrgChartService : IOrgChartService {
        private IMapper _mapper{get;set;}
        private readonly AEV2Context _context;
        
        public OrgChartService(IMapper mapper, AEV2Context context)
        {
            _mapper = mapper;
            _context = context;
        }

        private int? getSupervisorID(int id) {
            var supervisorEmployeeID = (from emp in _context.Employees
                where emp.EmployeeNumber == id
                select emp.SupervisorEmployeeNumber).FirstOrDefault();
            return supervisorEmployeeID;
        }

        public EmployeeDTO GetSupervisorByEmployeeNumber(int id) {
            var supervisorEmployeeID = getSupervisorID(id);
            var supervisor = (from emp in _context.Employees
                where emp.EmployeeNumber == supervisorEmployeeID
                select emp).FirstOrDefault();
            EmployeeDTO supervisorDTO = _mapper.Map<Models.Employee, EmployeeDTO>(supervisor);
            return supervisorDTO;
        }
        public List<EmployeeDTO> GetPeersByEmployeeNumber(int id) {
            var supervisorEmployeeID = getSupervisorID(id);
            if (supervisorEmployeeID == id) {
                var selfEmployee = (
                from emp in _context.Employees
                where emp.EmployeeNumber == id
                select emp).ToList();
                List<EmployeeDTO> selfEmployeeDTO = _mapper.Map<List<Models.Employee>, List<EmployeeDTO>>(selfEmployee);
                return selfEmployeeDTO;
            }
            var peersEmployeeList = (
                from emp in _context.Employees
                where emp.SupervisorEmployeeNumber == supervisorEmployeeID
                select emp).ToList();
            List<EmployeeDTO> employeeDTOList = _mapper.Map<List<Models.Employee>, List<EmployeeDTO>>(peersEmployeeList);
            return employeeDTOList;
        } 
        public List<EmployeeDTO> GetSubordinatesByEmployeeNumber(int id) {
            var subordinateEmployeeList = (
                from emp in _context.Employees
                where emp.SupervisorEmployeeNumber == id
                select emp).ToList();
            List<EmployeeDTO> employeeDTOList = _mapper.Map<List<Models.Employee>, List<EmployeeDTO>>(subordinateEmployeeList);
            return employeeDTOList;
        } 
    }
}