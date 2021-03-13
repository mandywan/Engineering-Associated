using System.Collections.Generic;
using AeDirectory.Models;

namespace IntegrationTest
{
    /**
     * Utility class to put mock data in the inMemoryDatabase
     * Modify this class to add more test db data
     * Note: mock db is shared between tests, if new db instances is required for your test,
     * please modify CustomWebApplicationFactory
     */
    public static class Utilities
    {
        public static void InitializeDbForTests(AEV2Context db)
        {
            db.Companies.AddRange(GetMockCompanies());
            db.Offices.AddRange(GetMockOffices());
            db.CompanyOfficeGroups.AddRange(GetMockGroups());
            db.Locations.AddRange(GetMockLocations());
            db.Categories.AddRange(GetMockSkillCategories());
            db.Skills.AddRange(GetMockSkills());
            db.Employees.AddRange(GetMockEmployees());
            db.SaveChanges();
        }
        
        public static void ReinitializeDbForTests(AEV2Context db)
        {
            db.Companies.RemoveRange(db.Companies);
            InitializeDbForTests(db);
        }

        public static List<Company> GetMockCompanies()
        {
            return new List<Company>()
            {
                new Company() {CompanyCode = "1", Label = "C1"},
                new Company() {CompanyCode = "2", Label = "C2"}
            };
        }
        
        public static List<Office> GetMockOffices()
        {
            return new List<Office>()
            {
                new Office() {CompanyCode = "1", OfficeCode = "11", Label = "O1"},
                new Office() {CompanyCode = "2", OfficeCode = "11", Label = "O1"},
            };
        }
        
        public static List<CompanyOfficeGroup> GetMockGroups()
        {
            return new List<CompanyOfficeGroup>()
            {
                new CompanyOfficeGroup() {CompanyCode = "1", OfficeCode = "11", GroupCode = "111", Label = "G1"},
                new CompanyOfficeGroup() {CompanyCode = "2", OfficeCode = "11", GroupCode = "222", Label = "G2"},
                new CompanyOfficeGroup() {CompanyCode = "2", OfficeCode = "11", GroupCode = "333", Label = "G3"},
            };
        }
        
        public static List<Location> GetMockLocations()
        {
            return new List<Location>()
            {
                new Location() {LocationId = "van", Label = "vancouver"},
            };
        }
        
        public static List<Category> GetMockSkillCategories()
        {
            return new List<Category>()
            {
                new Category() {SkillCategoryId = "1", Label = "c#"},
            };
        }
        
        public static List<Skill> GetMockSkills()
        {
            return new List<Skill>()
            {
                new Skill() {SkillCategoryId = "1", SkillId = "11", Label = "f#"},
            };
        }

        public static List<Employee> GetMockEmployees()
        {
            return new List<Employee>()
            {
                new Employee()
                {
                    EmployeeNumber = 0, CompanyCode = "1", OfficeCode = "1", GroupCode = "1", LastName = "Doe", FirstName = "John"
                }
            };
        }
    }
}