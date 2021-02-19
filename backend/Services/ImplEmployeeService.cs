using System.Collections.Generic;
using System.Linq;
using AeDirectory.DTO;
using AeDirectory.Search;
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
            var employeeIdsFromCompany = new List<int>();
            if (filters.Company != null) {
                if ((filters.Company.type == "OR") || (filters.Company.values.Count == 1)) {
                    employeeIdsFromCompany = (
                        from employee in _context.Employees
                        where filters.Company.companyIds().Contains(employee.CompanyCode)
                        select employee.EmployeeNumber).ToList();
                } else if (filters.Company.type == "AND") {
                    // no such case can exist
                }
            }
            var employeeIdsFromOffice = new List<int>();
            if (filters.Office != null) {
                if ((filters.Office.type == "OR") || (filters.Office.values.Count == 1)) {
                    employeeIdsFromOffice = (
                        from employee in _context.Employees
                        where   //filters.Office.officeIds().Contains(employee.OfficeCode) && 
                                filters.Office.companyIds().Contains(employee.CompanyCode)
                        select employee.EmployeeNumber).ToList();
                } else if (filters.Office.type == "AND") {
                    // no such case can exist
                }
            }
            var employeeIdsFromGroup = new List<int>();
            if (filters.Group != null) {
                if ((filters.Group.type == "OR") || (filters.Group.values.Count == 1)) {
                    employeeIdsFromGroup = (
                        from employee in _context.Employees
                        where   filters.Group.groupIds().Contains(employee.GroupCode) && 
                                filters.Group.officeIds().Contains(employee.OfficeCode) && 
                                filters.Group.companyIds().Contains(employee.CompanyCode)
                        select employee.EmployeeNumber).ToList();
                } else if (filters.Group.type == "AND") {
                    // no such case can exist
                }
            }
            var employeeIdsFromLocation = new List<int>();
            if (filters.Location != null) {
                if ((filters.Location.type == "OR") || (filters.Location.values.Count == 1)) {
                    employeeIdsFromLocation = (
                        from employee in _context.Employees
                        where filters.Location.locationIds().Contains(employee.LocationId)
                        select employee.EmployeeNumber).ToList();
                } else if (filters.Location.type == "AND") {
                    // no such case can exist
                }
            }

            // skill filter prep
            var employeeIdsFromSkills = new List<int>();
            if (filters.Skill != null) {
                if (filters.Skill.type == "OR") {
                    employeeIdsFromSkills = (
                        from employee in _context.EmployeeSkills
                        where filters.Skill.skillIds().Contains(employee.SkillId)
                        select employee.EmployeeNumber).ToList();
                } else if (filters.Skill.type == "AND") {
                    var employeeSkills = (
                        from employeeSkill in _context.EmployeeSkills
                        where filters.Skill.skillIds().Contains(employeeSkill.SkillId)
                        select employeeSkill).ToList();

                    Dictionary<int, HashSet<string>> employeeSkillDictionary = new Dictionary<int, HashSet<string>>();
                    foreach (EmployeeSkill employeeSkill in employeeSkills) {
                        if (!employeeSkillDictionary.ContainsKey(employeeSkill.EmployeeNumber)) {
                            employeeSkillDictionary.Add(employeeSkill.EmployeeNumber, new HashSet<string>() { employeeSkill.SkillId });
                        } else {
                            employeeSkillDictionary[employeeSkill.EmployeeNumber].Add(employeeSkill.SkillId);
                        }
                    }
                    HashSet<string> skillIdsHashSet = filters.Skill.skillIdsHashSet();
                    foreach (int employeeNumber in employeeSkillDictionary.Keys) {
                        HashSet<string> skillIdsHashSetCopy = skillIdsHashSet;
                        int originalCount = skillIdsHashSet.Count();
                        skillIdsHashSetCopy.IntersectWith(employeeSkillDictionary[employeeNumber]);
                        if (skillIdsHashSetCopy.Count() == originalCount) {
                            employeeIdsFromSkills.Add(employeeNumber);
                        }
                    }
                }
            }

            // category filter prep
            var employeeIdsFromSkillCategories = new List<int>();
            if (filters.Category != null) {
                if (filters.Category.type == "OR") {
                    var skillIdsFromCategory = (
                        from skill in _context.Skills
                        where filters.Category.categoryIds().Contains(skill.SkillCategoryId)
                        select skill.SkillId).ToList();
                    employeeIdsFromSkillCategories = (
                        from employee in _context.EmployeeSkills
                        where skillIdsFromCategory.Contains(employee.SkillId)
                        select employee.EmployeeNumber).ToList();
                } else if (filters.Category.type == "AND") {
                    var categorySkills = (
                        from skill in _context.Skills
                        where filters.Category.categoryIds().Contains(skill.SkillCategoryId)
                        select skill).ToList();
                    Dictionary<string, HashSet<string>> categorySkillDictionary = new Dictionary<string, HashSet<string>>();
                    foreach (Skill categorySkill in categorySkills) {
                        if (!categorySkillDictionary.ContainsKey(categorySkill.SkillCategoryId)) {
                            categorySkillDictionary.Add(categorySkill.SkillCategoryId, new HashSet<string>() { categorySkill.SkillId });
                        } else {
                            categorySkillDictionary[categorySkill.SkillCategoryId].Add(categorySkill.SkillId);
                        }
                    }
                    var skills = (
                        from skill in _context.Skills
                        where filters.Category.categoryIds().Contains(skill.SkillCategoryId)
                        select skill.SkillId).ToList();
                    var employeeSkills = (
                        from employeeSkill in _context.EmployeeSkills
                        where skills.Contains(employeeSkill.SkillId)
                        select employeeSkill).ToList();
                    Dictionary<int, HashSet<string>> employeeSkillDictionary = new Dictionary<int, HashSet<string>>();
                    foreach (EmployeeSkill employeeSkill in employeeSkills) {
                        if (!employeeSkillDictionary.ContainsKey(employeeSkill.EmployeeNumber)) {
                            employeeSkillDictionary.Add(employeeSkill.EmployeeNumber, new HashSet<string>() { employeeSkill.SkillId });
                        } else {
                            employeeSkillDictionary[employeeSkill.EmployeeNumber].Add(employeeSkill.SkillId);
                        }
                    }
                    // check each category intersection
                    foreach (int employeeNumber in employeeSkillDictionary.Keys) {
                        bool intersectsCategory = true;
                        foreach(string categoryId in categorySkillDictionary.Keys) {
                            HashSet<string> employeeHashSetCopy = employeeSkillDictionary[employeeNumber];
                            HashSet<string> categoryHashSetCopy = categorySkillDictionary[categoryId];
                            categoryHashSetCopy.IntersectWith(employeeHashSetCopy);
                            if (categoryHashSetCopy.Count() < 1) {
                                intersectsCategory = false;
                                break;
                            }
                        }
                        if (intersectsCategory) {
                            employeeIdsFromSkillCategories.Add(employeeNumber);
                        }
                    }
                }
            }

            var employeeIdsFromLastName = new List<int>();
            if (filters.LastName != null) {
                if ((filters.LastName.type == "OR") || (filters.LastName.values.Count == 1)) {
                    employeeIdsFromLastName = (
                        from employee in _context.Employees
                        where filters.LastName.values.Contains(employee.LastName)
                        select employee.EmployeeNumber).ToList();
                } else if (filters.Location.type == "AND") {
                    // no such case can exist
                }
            }
            var employeeIdsFromFirstName = new List<int>();
            if (filters.FirstName != null) {
                if ((filters.FirstName.type == "OR") || (filters.FirstName.values.Count == 1)) {
                    employeeIdsFromFirstName = (
                        from employee in _context.Employees
                        where filters.FirstName.values.Contains(employee.FirstName)
                        select employee.EmployeeNumber).ToList();
                } else if (filters.Location.type == "AND") {
                    // no such case can exist
                }
            }
            var employeeIdsFromName = new List<int>();
            if (filters.Name != null) {
                employeeIdsFromName = (
                    from employee in _context.Employees
                    where filters.Name.values.Contains(employee.FirstName) || filters.Name.values.Contains(employee.LastName)
                    select employee.EmployeeNumber).ToList();
            }
            var employeeIdsFromTitle = new List<int>();
            if (filters.Title != null) {
                if ((filters.Title.type == "OR") || (filters.Title.values.Count == 1)) {
                    employeeIdsFromTitle = (
                        from employee in _context.Employees
                        where filters.Title.values.Contains(employee.Title)
                        select employee.EmployeeNumber).ToList();
                } else if (filters.Location.type == "AND") {
                    // no such case can exist
                }
            }
            var employeeIdsFromHireDate = new List<int>();
            if (filters.HireDate != null) {
                if ((filters.HireDate.type == "OR") || (filters.HireDate.values.Count == 1)) {
                    employeeIdsFromHireDate = (
                        from employee in _context.Employees
                        where filters.HireDate.values.Contains(employee.HireDate)
                        select employee.EmployeeNumber).ToList();
                } else if (filters.Location.type == "AND") {
                    // no such case can exist
                }
            }
            var employeeIdsFromTerminationDate = new List<int>();
            if (filters.TerminationDate != null) {
                if ((filters.TerminationDate.type == "OR") || (filters.TerminationDate.values.Count == 1)) {
                    employeeIdsFromTerminationDate = (
                        from employee in _context.Employees
                        where filters.TerminationDate.values.Contains(employee.TerminationDate)
                        select employee.EmployeeNumber).ToList();
                } else if (filters.Location.type == "AND") {
                    // no such case can exist
                }
            }
            var employeeIdsFromYearsPriorExperience = new List<int>();
            if (filters.YearsPriorExperience != null) {
                if ((filters.YearsPriorExperience.type == "OR") || (filters.YearsPriorExperience.values.Count == 1)) {
                    employeeIdsFromYearsPriorExperience = (
                        from employee in _context.Employees
                        where filters.YearsPriorExperience.values.Contains(employee.YearsPriorExperience)
                        select employee.EmployeeNumber).ToList();
                } else if (filters.Location.type == "AND") {
                    // no such case can exist
                }
            }
            var employeeIdsFromEmail = new List<int>();
            if (filters.Email != null) {
                if ((filters.Email.type == "OR") || (filters.Email.values.Count == 1)) {
                    employeeIdsFromEmail = (
                        from employee in _context.Employees
                        where filters.Email.values.Contains(employee.Email)
                        select employee.EmployeeNumber).ToList();
                } else if (filters.Location.type == "AND") {
                    // no such case can exist
                }
            }
            var employeeIdsFromWorkPhone = new List<int>();
            if (filters.WorkPhone != null) {
                if ((filters.WorkPhone.type == "OR") || (filters.WorkPhone.values.Count == 1)) {
                    employeeIdsFromWorkPhone = (
                        from employee in _context.Employees
                        where filters.WorkPhone.values.Contains(employee.WorkPhone)
                        select employee.EmployeeNumber).ToList();
                } else if (filters.Location.type == "AND") {
                    // no such case can exist
                }
            }
            var employeeIdsFromWorkCell = new List<int>();
            if (filters.WorkCell != null) {
                if ((filters.WorkCell.type == "OR") || (filters.WorkCell.values.Count == 1)) {
                    employeeIdsFromWorkCell = (
                        from employee in _context.Employees
                        where filters.WorkCell.values.Contains(employee.WorkCell)
                        select employee.EmployeeNumber).ToList();
                } else if (filters.Location.type == "AND") {
                    // no such case can exist
                }
            }

            // search employees

            var employeeList = (
                from employee in _context.Employees
                where   ((filters.Company               == null) ? true : employeeIdsFromCompany.Contains(employee.EmployeeNumber)) &&
                        ((filters.Office                == null) ? true : employeeIdsFromOffice.Contains(employee.EmployeeNumber)) && 
                        ((filters.Group                 == null) ? true : employeeIdsFromGroup.Contains(employee.EmployeeNumber)) && 
                        ((filters.Location              == null) ? true : employeeIdsFromLocation.Contains(employee.EmployeeNumber)) && 
                        ((filters.Skill                 == null) ? true : employeeIdsFromSkills.Contains(employee.EmployeeNumber)) && 
                        ((filters.Category              == null) ? true : employeeIdsFromSkillCategories.Contains(employee.EmployeeNumber)) &&
                        ((filters.LastName              == null) ? true : employeeIdsFromLastName.Contains(employee.EmployeeNumber)) &&
                        ((filters.FirstName             == null) ? true : employeeIdsFromFirstName.Contains(employee.EmployeeNumber)) &&
                        ((filters.Name                  == null) ? true : employeeIdsFromName.Contains(employee.EmployeeNumber)) &&
                        ((filters.Title                 == null) ? true : employeeIdsFromTitle.Contains(employee.EmployeeNumber)) &&
                        ((filters.HireDate              == null) ? true : employeeIdsFromHireDate.Contains(employee.EmployeeNumber)) &&
                        ((filters.TerminationDate       == null) ? true : employeeIdsFromTerminationDate.Contains(employee.EmployeeNumber)) &&
                        ((filters.YearsPriorExperience  == null) ? true : employeeIdsFromYearsPriorExperience.Contains(employee.EmployeeNumber)) &&
                        ((filters.Email                 == null) ? true : employeeIdsFromEmail.Contains(employee.EmployeeNumber)) &&
                        ((filters.WorkPhone             == null) ? true : employeeIdsFromWorkPhone.Contains(employee.EmployeeNumber)) &&
                        ((filters.WorkCell              == null) ? true : employeeIdsFromWorkCell.Contains(employee.EmployeeNumber))
                select employee).ToList();
        
            List<EmployeeDTO> employeeDTOList = _mapper.Map<List<Models.Employee>, List<EmployeeDTO>>(employeeList);
            return employeeDTOList;          
        }

    }
}