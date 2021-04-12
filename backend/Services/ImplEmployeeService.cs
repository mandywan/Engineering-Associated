using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

using AeDirectory.DTO;
using AeDirectory.Search;
using AeDirectory.Models;

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
            employeeDTO.Supervisor.Title = employee.SupervisorEmployeeNumberNavigation.Title;
            employeeDTO.Supervisor.GroupCode = employee.SupervisorEmployeeNumberNavigation.GroupCode;
            employeeDTO.Supervisor.OfficeCode = employee.SupervisorEmployeeNumberNavigation.OfficeCode;
            
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
            var employeesFromOffice = new List<Employee>();
            if (filters.Office != null) {
                if ((filters.Office.type == "OR") || (filters.Office.values.Count == 1)) {
                    employeesFromOffice = (
                        from employee in _context.Employees
                        select employee).ToList();
                    foreach (Employee employee in employeesFromOffice) {
                        foreach (CompositeId compositeId in filters.Office.values) {
                            if (employee.OfficeCode == compositeId.OfficeId &&
                                employee.CompanyCode == compositeId.CompanyId) {
                                employeeIdsFromOffice.Add(employee.EmployeeNumber);
                            }
                        }
                    }
                } else if (filters.Office.type == "AND") {
                    // no such case can exist
                }
            }
            var employeeIdsFromGroup = new List<int>();
            var employeesFromGroup = new List<Employee>();
            if (filters.Group != null) {
                if ((filters.Group.type == "OR") || (filters.Group.values.Count == 1)) {
                    employeesFromGroup = (
                        from employee in _context.Employees
                        select employee).ToList();
                    foreach (Employee employee in employeesFromGroup) {
                        foreach(CompositeId compositeId in filters.Group.values) {
                            if (employee.GroupCode == compositeId.GroupId &&
                                employee.OfficeCode == compositeId.OfficeId &&
                                employee.CompanyCode == compositeId.CompanyId) {
                                employeeIdsFromGroup.Add(employee.EmployeeNumber);
                            }
                        }
                    }
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
            var employeeSkillsFromSkills = new List<EmployeeSkill>();

            if (filters.Skill != null)
            {
                if (filters.Skill.type == "OR")
                {
                    employeeIdsFromSkills = (
                        from employee in _context.EmployeeSkills
                        where filters.Skill.skillIds().Contains(employee.SkillId)
                        select employee.EmployeeNumber).ToList();
                }
                else if (filters.Skill.type == "AND")
                {
                    var employeeSkills = (
                        from employeeSkill in _context.EmployeeSkills
                        where filters.Skill.skillIds().Contains(employeeSkill.SkillId)
                        select employeeSkill).ToList();

                    Dictionary<int, HashSet<string>> employeeSkillDictionary = new Dictionary<int, HashSet<string>>(); // employees with list of skills each
                    foreach (EmployeeSkill employeeSkill in employeeSkills) {
                        if (!employeeSkillDictionary.ContainsKey(employeeSkill.EmployeeNumber)) {
                            employeeSkillDictionary.Add(employeeSkill.EmployeeNumber, new HashSet<string>() { employeeSkill.SkillId });
                        } else {
                            employeeSkillDictionary[employeeSkill.EmployeeNumber].Add(employeeSkill.SkillId);
                        }
                    }
                    foreach (int employee in employeeSkillDictionary.Keys) {
                        bool contains = true;
                        foreach (string skill in filters.Skill.skillIds()) {
                            if (!employeeSkillDictionary[employee].Contains(skill)) {
                                //employeeSkillDictionary.Remove(employee);
                                contains = false;
                            }
                        }
                        if (contains) {
                            employeeIdsFromSkills.Add(employee);
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

            var employeeLastNamesFromFilters = new List<string>();
            var employeeLastNames = new List<string>();
            if (filters.LastName != null) {
                if ((filters.LastName.type == "OR") || (filters.LastName.values.Count == 1)) {
                    employeeLastNames = (
                        from employee in _context.Employees
                        select employee.LastName).ToList();
                } else if (filters.LastName.type == "AND") {
                    // no such case can exist
                }
                foreach (string lastName in employeeLastNames) {
                    foreach (string name in filters.LastName.values) {
                        if (lastName.IndexOf(name, StringComparison.OrdinalIgnoreCase) >= 0) {
                            employeeLastNamesFromFilters.Add(lastName);
                        }
                    }
                }
            }
            var employeeFirstNamesFromFilters = new List<string>();
            var employeeFirstNames = new List<string>();
            if (filters.FirstName != null) {
                if ((filters.FirstName.type == "OR") || (filters.FirstName.values.Count == 1)) {
                    employeeFirstNames = (
                        from employee in _context.Employees
                        select employee.FirstName).ToList();
                } else if (filters.FirstName.type == "AND") {
                    // no such case can exist
                }
                foreach (string firstName in employeeFirstNames) {
                    foreach (string name in filters.FirstName.values) {
                        if (firstName.IndexOf(name, StringComparison.OrdinalIgnoreCase) >= 0) {
                            employeeFirstNamesFromFilters.Add(firstName);
                        }
                    }
                }
            }
            var employeeNamesFromFilters = new List<string>();
            var employeeNames = new List<string>();
            if (filters.Name != null) {
                if ((filters.Name.type == "OR") || (filters.Name.values.Count == 1)) {
                    employeeFirstNames = (
                    from employee in _context.Employees
                    select employee.FirstName).ToList();
                    employeeLastNames = (
                        from employee in _context.Employees
                        select employee.LastName).ToList();
                    employeeNames = employeeFirstNames.Concat(employeeLastNames).ToList();
                    foreach (string firstOrLastName in employeeNames) {
                        foreach (string name in filters.Name.values) {
                            if (firstOrLastName.IndexOf(name, StringComparison.OrdinalIgnoreCase) >= 0) {
                                employeeNamesFromFilters.Add(firstOrLastName);
                            }
                        }
                    }
                } else if (filters.Name.type == "AND" && (filters.Name.values.Count == 2)) {
                    employeeLastNames = (
                        from employee in _context.Employees
                        select employee.LastName).ToList();
                    foreach (string lastName in employeeLastNames) {
                        foreach (string name in filters.Name.values) {
                            if (lastName.IndexOf(name, StringComparison.OrdinalIgnoreCase) >= 0) {
                                employeeLastNamesFromFilters.Add(lastName);
                            }
                        }
                    }
                    employeeFirstNames = (
                        from employee in _context.Employees
                        select employee.FirstName).ToList();
                    foreach (string firstName in employeeFirstNames) {
                        foreach (string name in filters.Name.values) {
                            if (firstName.IndexOf(name, StringComparison.OrdinalIgnoreCase) >= 0) {
                                employeeFirstNamesFromFilters.Add(firstName);
                            }
                        }
                    }
                    filters.LastName = filters.Name;
                    filters.FirstName = filters.Name;
                    filters.Name = null;
                }
            }

            var employeeIdsFromTitle = new List<int>();
            if (filters.Title != null) {
                if ((filters.Title.type == "OR") || (filters.Title.values.Count == 1)) {
                    employeeIdsFromTitle = (
                        from employee in _context.Employees
                        where filters.Title.values.Contains(employee.Title)
                        select employee.EmployeeNumber).ToList();
                } else if (filters.Title.type == "AND") {
                    // no such case can exist
                }
            }
            var employeeIdsFromYearsPriorExperience = new List<int>();
            if (filters.YearsPriorExperience != null) {
                if ((filters.YearsPriorExperience.type == "OR") || (filters.YearsPriorExperience.values.Count == 1)) {
                    employeeIdsFromYearsPriorExperience = (
                        from employee in _context.Employees
                        where filters.YearsPriorExperience.values.Contains(employee.YearsPriorExperience.ToString())
                        select employee.EmployeeNumber).ToList();
                } else if (filters.YearsPriorExperience.type == "AND") {
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
                } else if (filters.Email.type == "AND") {
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
                } else if (filters.WorkPhone.type == "AND") {
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
                } else if (filters.WorkCell.type == "AND") {
                    // no such case can exist
                }
            }

            // search employees

            var employeeList = (
                from employee in _context.Employees
                .Include(employee => employee.EmployeeSkills)
                .ThenInclude(empSkills => empSkills.Skill)
                where   ((filters.Company               == null) ? true : employeeIdsFromCompany.Contains(employee.EmployeeNumber)) &&
                        ((filters.Office                == null) ? true : employeeIdsFromOffice.Contains(employee.EmployeeNumber)) && 
                        ((filters.Group                 == null) ? true : employeeIdsFromGroup.Contains(employee.EmployeeNumber)) && 
                        ((filters.Location              == null) ? true : employeeIdsFromLocation.Contains(employee.EmployeeNumber)) && 
                        ((filters.Skill                 == null) ? true : employeeIdsFromSkills.Contains(employee.EmployeeNumber)) && 
                        ((filters.Category              == null) ? true : employeeIdsFromSkillCategories.Contains(employee.EmployeeNumber)) &&
                        ((filters.LastName              == null) ? true : employeeLastNamesFromFilters.Contains(employee.LastName)) &&
                        ((filters.FirstName             == null) ? true : employeeFirstNamesFromFilters.Contains(employee.FirstName)) &&
                        ((filters.Name                  == null) ? true : (employeeNamesFromFilters.Contains(employee.FirstName) || (employeeNamesFromFilters.Contains(employee.LastName)))) &&
                        ((filters.Title                 == null) ? true : employeeIdsFromTitle.Contains(employee.EmployeeNumber)) &&
                        ((filters.YearsPriorExperience  == null) ? true : employeeIdsFromYearsPriorExperience.Contains(employee.EmployeeNumber)) &&
                        ((filters.Email                 == null) ? true : employeeIdsFromEmail.Contains(employee.EmployeeNumber)) &&
                        ((filters.WorkPhone             == null) ? true : employeeIdsFromWorkPhone.Contains(employee.EmployeeNumber)) &&
                        ((filters.WorkCell              == null) ? true : employeeIdsFromWorkCell.Contains(employee.EmployeeNumber))
                select employee).ToList();
        
            List<EmployeeDTO> employeeDTOList = new List<EmployeeDTO>();

            List<Employee> tempEmployeeList = new List<Employee>();
            if (filters.Name != null) {
                foreach (String name in filters.Name.values) {
                    foreach (Employee employee in employeeList.ToList()) {
                        if (!employee.FirstName.Equals(name, StringComparison.InvariantCultureIgnoreCase) &&
                            !employee.LastName.Equals(name, StringComparison.InvariantCultureIgnoreCase)) {
                            tempEmployeeList.Add(employee);
                            employeeList.Remove(employee);
                        }
                    }
                }
                foreach (Employee employee in tempEmployeeList) {
                    employeeList.Add(employee);
                }
                tempEmployeeList.Clear();
            }
            if (filters.FirstName != null) {
                foreach (String name in filters.FirstName.values) {
                    foreach (Employee employee in employeeList.ToList()) {
                        if (!employee.FirstName.Equals(name, StringComparison.InvariantCultureIgnoreCase)) {
                            tempEmployeeList.Add(employee);
                            employeeList.Remove(employee);
                        }
                    }
                }
                foreach (Employee employee in tempEmployeeList) {
                    employeeList.Add(employee);
                }
                tempEmployeeList.Clear();
            }
            if (filters.LastName != null) {
                foreach (String name in filters.LastName.values) {
                    foreach (Employee employee in employeeList.ToList()) {
                        if (!employee.LastName.Equals(name, StringComparison.InvariantCultureIgnoreCase)) {
                            tempEmployeeList.Add(employee);
                            employeeList.Remove(employee);
                        }
                    }
                }
                foreach (Employee employee in tempEmployeeList) {
                    employeeList.Add(employee);
                }
                tempEmployeeList.Clear();
            }

            foreach (Models.Employee employee in employeeList) {
                var skillList = (from c in _context.EmployeeSkills
                                 where c.EmployeeNumber == employee.EmployeeNumber
                                 join o in _context.Skills on c.SkillId equals o.SkillId
                                 select c).ToList();

                EmployeeDTO employeeDTO = _mapper.Map<Models.Employee, EmployeeDTO>(employee);

                foreach (var ele in skillList) {
                    employeeDTO.Skills.Add(new EmployeeSkillDTO(ele.Skill.SkillCategoryId, ele.Skill.SkillId));
                }

                employeeDTOList.Add(employeeDTO);
            }

            return employeeDTOList;          
        }

    }
}