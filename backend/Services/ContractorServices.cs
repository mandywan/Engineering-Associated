using System.Collections.Generic;
using System.Linq;
using AeDirectory.DTO;
using AeDirectory.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AeDirectory.Services
{
    public interface IContractorService
    {
        List<ContractorDTO> GetContractorList();
        ContractorDTO GetContractorByEmployeeNumber(int id);

        int AddContractor(ContractorAddDTO contractor);
        
        int UpdateContractor(ContractorUpdateDTO request, int id);
        int DeleteContractor(int id);
    }

    public class ImplIContractorService : IContractorService
    {
        private IMapper _mapper { get; set; }
        private readonly AEV2Context _context;

        public ImplIContractorService(AEV2Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // public List<EmployeeDTO> GetContractorList()
        public List<ContractorDTO> GetContractorList()
        {
            var contractorList = (from emp in _context.Employees
                where emp.IsContractor == true
                select emp).ToList();
            List<ContractorDTO> contractors = _mapper.Map<List<Models.Employee>, List<ContractorDTO>>(contractorList);
            return contractors;
        }

        public ContractorDTO GetContractorByEmployeeNumber(int id)
        // public EmployeeDTO GetContractorByEmployeeNumber(int id)
        {
            // fetch employee basic info
            var employee = (from emp in _context.Employees
                    .Include("SupervisorEmployeeNumberNavigation")
                    // .Include("CompanyCodeNavigation")
                    // .Include("CompanyOfficeGroup")
                    // .Include("Location")
                    // .Include("Office")
                    // many to many relationship, should join three tables together
                    .Include(emp => emp.EmployeeSkills )
                    .ThenInclude(empSkills => empSkills.Skill)
                where emp.EmployeeNumber == id
                where emp.IsContractor == true
                select emp).FirstOrDefault();

            // fetch skill_id and category_id
            var skillList = (from c in _context.EmployeeSkills
                where c.EmployeeNumber == id
                join o in _context.Skills on c.SkillId equals o.SkillId 
                select c).ToList();
            
            ContractorDTO employeeDTO = _mapper.Map<Models.Employee, ContractorDTO>(employee);
            
            // set supervisor in DTO
            // employeeDTO.Supervisor.FirstName = employee.SupervisorEmployeeNumberNavigation.FirstName;
            // employeeDTO.Supervisor.LastName = employee.SupervisorEmployeeNumberNavigation.LastName;
            // employeeDTO.Supervisor.PhotoUrl = employee.SupervisorEmployeeNumberNavigation.PhotoUrl;

            // set categories and skills in DTO
            // foreach (var ele in skillList)
            // {
            //     employeeDTO.Skills.Add(new EmployeeSkillDTO(ele.Skill.SkillCategoryId, ele.Skill.SkillId));
            // }
            
            return employeeDTO;
        }

        public int AddContractor(ContractorAddDTO contractor)
        {
            Employee emp =  _mapper.Map<ContractorAddDTO, Models.Employee>(contractor);
            _context.Add<Models.Employee>(emp);
            return  _context.SaveChanges();
        }
        
        public int UpdateContractor(ContractorUpdateDTO request, int id)
        {
            var existingEmp = _context.Employees.FirstOrDefault(a => a.EmployeeNumber == id);
            
            if (existingEmp != null)
            {
                existingEmp.CompanyCode = request.CompanyCode ?? existingEmp.CompanyCode;
                existingEmp.Email = request.Email ?? existingEmp.Email;
                existingEmp.SupervisorEmployeeNumber = request.SupervisorEmployeeNumber != 0 ? request.SupervisorEmployeeNumber : existingEmp.SupervisorEmployeeNumber;
                existingEmp.YearsPriorExperience = request.YearsPriorExperience != 0 ? request.YearsPriorExperience : existingEmp.YearsPriorExperience;
                existingEmp.OfficeCode = request.OfficeCode ?? existingEmp.OfficeCode;
                existingEmp.GroupCode = request.GroupCode ?? existingEmp.GroupCode;
                existingEmp.LocationId = request.LocationId ?? existingEmp.LocationId;
                existingEmp.LastName = request.LastName ?? existingEmp.LastName;
                existingEmp.FirstName = request.FirstName ?? existingEmp.FirstName;
                existingEmp.EmploymentType = request.EmploymentType ?? existingEmp.EmploymentType;
                existingEmp.Title = request.Title ?? existingEmp.Title;
                existingEmp.WorkPhone = request.WorkPhone ?? existingEmp.WorkPhone;
                existingEmp.PhotoUrl = request.PhotoUrl ?? existingEmp.PhotoUrl;
                existingEmp.HireDate = request.HireDate ?? existingEmp.HireDate;
                existingEmp.TerminationDate = request.TerminationDate ?? existingEmp.TerminationDate;
                _context.SaveChanges();
                return 1;
            }
            
            return -1;
        }

        public int DeleteContractor(int id)
        {
            var existingEmp = _context.Employees.FirstOrDefault(a => a.EmployeeNumber == id);
            if (existingEmp != null)
            {
                _context.Entry(existingEmp).State = EntityState.Deleted;
                return _context.SaveChanges();
            }
            else
            {
                return -1;
            }
        }
    }
}