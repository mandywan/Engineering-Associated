using System.Collections.Generic;
using System.Linq;
using AeDirectory.DTO;
using AeDirectory.Models;
using AutoMapper;

namespace AeDirectory.Services
{
    public class ImplEmployeeService : IEmployeeService
    {
        private IMapper _mapper{get;set;}
        private readonly AEV2Context _context;
        
        public ImplEmployeeService(IMapper mapper, AEV2Context context)
        {
            _mapper=mapper;
            _context = context;
        }

        public List<EmployeeDTO> GetEmployeeList()
        {
            var employeeList = (from emp in _context.Employees
                select emp).ToList();
            List<EmployeeDTO> employeeDTOList = _mapper.Map<List<Models.Employee>, List<EmployeeDTO>>(employeeList);
            return employeeDTOList;
        }

        public EmployeeDTO GetEmployeeByEmployeeNumber(int id)
        {
            var employee = (from emp in _context.Employees
                where emp.EmployeeNumber == id
                select emp).FirstOrDefault();
            EmployeeDTO employeeDTO = _mapper.Map<Models.Employee, EmployeeDTO>(employee);
            return employeeDTO;
        }

    }
}