using System;
using System.Collections.Generic;
using System.Linq;
using AeDirectory.Search;

#nullable enable

namespace AeDirectory.Search
{
    public class FilterById
    {
        public string? type { get; set; }
        public List<CompositeId>? values { get; set; }

        public List<String> companyIds() {
            var companyIds = new List<string>();
            if (values != null)
            {
                foreach (CompositeId company in values)
                {
                    if (company.CompanyId != null)
                        companyIds.Add(company.CompanyId);
                }
            }
            return companyIds;
        }
        public List<String> officeIds() {
            var officeIds = new List<string>();
            if (values != null)
            {
                foreach (CompositeId office in values)
                {
                    if (office.OfficeId != null)
                        officeIds.Add(office.OfficeId);
                }
            }
            return officeIds;
        }
        public List<String> groupIds() {
            var groupIds = new List<string>();
            if (values != null)
            {
                foreach (CompositeId group in values)
                {
                    if (group.GroupId != null)
                        groupIds.Add(group.GroupId);
                }
            }
            return groupIds;
        }
        public List<String> locationIds() {
            var locationIds = new List<string>();
            if (values != null)
            foreach (CompositeId location in values)
            {
                if (location.LocationId != null)
                locationIds.Add(location.LocationId);
            }
            return locationIds;
        }
        public List<String> skillIds() {
            var skillIds = new List<string>();
            if (values != null)
            foreach (CompositeId skill in values)
            {
                if (skill.SkillId != null)
                skillIds.Add(skill.SkillId);
            }
            return skillIds;
        }
        public HashSet<string> skillIdsHashSet()
        {
            var skillIdsHashSet = new HashSet<string>();
            if (values != null)
                foreach (CompositeId skill in values)
                {
                    if (skill.SkillId != null)
                        skillIdsHashSet.Add(skill.SkillId);
                }
            return skillIdsHashSet;
        }
        public List<String> categoryIds() {
            var categoryIds = new List<string>();
            if (values != null)
            foreach (CompositeId category in values)
            {
                if (category.CategoryId != null)
                categoryIds.Add(category.CategoryId);
            }
            return categoryIds;
        }

    }
}