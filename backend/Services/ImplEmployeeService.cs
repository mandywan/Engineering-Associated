using System.Collections.Generic;
using System.Linq;
using AeDirectory.DTO;
using AeDirectory.Domain;
using AeDirectory.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

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
                select emp).FirstOrDefault();

            // fetch skill_id and category_id
            var skillList = (from c in _context.EmployeeSkills
                where c.EmployeeNumber == id
                join o in _context.Skills on c.SkillId equals o.SkillId 
                select c).ToList();
            
            EmployeeDTO employeeDTO = _mapper.Map<Models.Employee, EmployeeDTO>(employee);
            
            // set supervisor in DTO
            employeeDTO.Supervisor.FirstName = employee.SupervisorEmployeeNumberNavigation.FirstName;
            employeeDTO.Supervisor.LastName = employee.SupervisorEmployeeNumberNavigation.LastName;
            employeeDTO.Supervisor.PhotoUrl = employee.SupervisorEmployeeNumberNavigation.PhotoUrl;

            // set categories and skills in DTO
            foreach (var ele in skillList)
            {
                employeeDTO.Skills.Add(new EmployeeSkillDTO(ele.Skill.SkillCategoryId, ele.Skill.SkillId));
            }
            
            return employeeDTO;
        }

        public List<EmployeeDTO> GetEmployeeByFilters(Filter filters)
        {
            //Filter filters = _mapper.Map<object, Filter>(filtersJSON); 

            // skill filter prep
            var employeeIdsFromSkills = (
                from emp in _context.EmployeeSkills
                where filters.Skills.Contains(emp.SkillId)
                select emp.EmployeeNumber).ToList();

            // category filter prep
            var employeeIdsFromSkillCategory = new List<int>();
            if (!(filters.Category_id == null))
            {
                var CategoryIdFromFilter = (
                    from cat in _context.Categories
                    where ((filters.Category_id == null) ? true : filters.Category_id == cat.SkillCategoryId)
                    select cat.SkillCategoryId).ToList();
                var skillIdsFromCategory = (
                    from skill in _context.Skills
                    where CategoryIdFromFilter.Contains(skill.SkillCategoryId)
                    select skill.SkillId).ToList();
                employeeIdsFromSkillCategory = (
                    from emp in _context.EmployeeSkills
                    where skillIdsFromCategory.Contains(emp.SkillId)
                    select emp.EmployeeNumber).ToList();
            }


            // search employees
            if (filters.UseAND) {
                var employeeList = (
                    from emp in _context.Employees
                    where   ((filters.Companies.Count            == 0) ? true : filters.Companies.Contains(emp.CompanyCode)) &&
                            ((filters.Offices.Count              == 0) ? true : filters.Offices.Contains(emp.OfficeCode)) && 
                            ((filters.Groups.Count               == 0) ? true : filters.Groups.Contains(emp.GroupCode)) && 
                            ((filters.Locations.Count            == 0) ? true : filters.Locations.Contains(emp.LocationId)) && 
                            ((employeeIdsFromSkills.Count        == 0) ? true : employeeIdsFromSkills.Contains(emp.EmployeeNumber)) && 
                            ((filters.Category_id           == null) ? true : employeeIdsFromSkillCategory.Contains(emp.EmployeeNumber)) &&
                            ((filters.LastName              == null) ? true : filters.LastName == emp.LastName) &&
                            ((filters.FirstName             == null) ? true : filters.FirstName == emp.FirstName) &&
                            ((filters.Title                 == null) ? true : filters.Title == emp.Title) &&
                            ((filters.HireDate              == null) ? true : filters.HireDate.Equals(emp.HireDate)) &&
                            ((filters.TerminationDate       == null) ? true : filters.TerminationDate.Equals(emp.TerminationDate)) &&
                            ((filters.YearsPriorExperience  == null) ? true : filters.YearsPriorExperience == emp.YearsPriorExperience) &&
                            ((filters.Email                 == null) ? true : filters.Email == emp.Email) &&
                            ((filters.WorkPhone             == null) ? true : filters.WorkPhone == emp.WorkPhone) &&
                            ((filters.WorkCell              == null) ? true : filters.WorkCell == emp.WorkCell)
                    select emp).ToList();
                List<EmployeeDTO> employeeDTOList = _mapper.Map<List<Models.Employee>, List<EmployeeDTO>>(employeeList);
                return employeeDTOList;
            } else {
                var employeeList = (
                    from emp in _context.Employees
                    where   filters.Companies.Contains(emp.CompanyCode) || 
                            filters.Offices.Contains(emp.OfficeCode) || 
                            filters.Groups.Contains(emp.GroupCode) || 
                            filters.Locations.Contains(emp.LocationId) || 
                            employeeIdsFromSkills.Contains(emp.EmployeeNumber) ||
                            ((filters.Category_id           == null) ? false : employeeIdsFromSkillCategory.Contains(emp.EmployeeNumber)) ||
                            ((filters.LastName              == null) ? false : filters.LastName == emp.LastName) ||
                            ((filters.FirstName             == null) ? false : filters.FirstName == emp.FirstName) ||
                            ((filters.Title                 == null) ? false : filters.Title == emp.Title) ||
                            ((filters.HireDate              == null) ? false : filters.HireDate.Equals(emp.HireDate)) ||
                            ((filters.TerminationDate       == null) ? false : filters.TerminationDate.Equals(emp.TerminationDate)) ||
                            ((filters.YearsPriorExperience  == null) ? false : filters.YearsPriorExperience == emp.YearsPriorExperience) ||
                            ((filters.Email                 == null) ? false : filters.Email == emp.Email) ||
                            ((filters.WorkPhone             == null) ? false : filters.WorkPhone == emp.WorkPhone) ||
                            ((filters.WorkCell              == null) ? false : filters.WorkCell == emp.WorkCell)
                    select emp).ToList();
                List<EmployeeDTO> employeeDTOList = _mapper.Map<List<Models.Employee>, List<EmployeeDTO>>(employeeList);
                return employeeDTOList;
            }

        }


    }
}