using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

using AeDirectory.Models;
using AeDirectory.Search;
using AeDirectory.DTO.FiltersDTO;

namespace AeDirectory.Services
{
    /**
     * Get all filters from db
     * If filters needed to be added/removed, need to modify this class to reflect the change
     */
    public class ImplFiltersService : IFiltersService
    {
        private readonly AEV2Context _context;

        public ImplFiltersService(AEV2Context context)
        {
            _context = context;
        }

        public List<FilterDTO> GetFilters()
        {
            List<FilterDTO> filtersDTO = new List<FilterDTO>();
            filtersDTO.Add(_getCompanyFilters());
            filtersDTO.Add(_getOfficeFilters());
            filtersDTO.Add(_getGroupFilters());
            filtersDTO.Add(_getLocationFilters());
            filtersDTO.Add(_getCategoryFilters());
            filtersDTO.Add(_getSkillFilters());
            filtersDTO.AddRange(_getRegularFilters());

            return filtersDTO;
        }

        private FilterDTO _getCategoryFilters()
        {
            List<FilterInputDTO> inputs = new List<FilterInputDTO>();
            var categories = (from category in _context.Categories select category).ToList();
            foreach (var c in categories)
            {
                inputs.Add(new FilterInputDTO(c.Label, new List<string>() { c.SkillCategoryId }));
            }
            return new HierarchyFilter(
                GetMemberName((Filter f) => f.Category),
                GetMemberName((CompositeId c) => c.CategoryId),
                "Skill Category",
                null,
                false,
                true,
                inputs);
        }

        private FilterDTO _getSkillFilters()
        {
            List<FilterInputDTO> inputs = new List<FilterInputDTO>();
            var skillFilters = (from skill in _context.Skills select skill).ToList();
            foreach (var s in skillFilters)
            {
                inputs.Add(new FilterInputDTO(s.Label, new List<string>() { s.SkillCategoryId, s.SkillId}));
            }
            return new HierarchyFilter(
                GetMemberName((Filter f) => f.Skill),
                GetMemberName((CompositeId c) => c.SkillId),
                "Skill",
                GetMemberName((Filter f) => f.Category),
                false,
                true,
                inputs);
        }

        private FilterDTO _getCompanyFilters()
        {
            List<FilterInputDTO> inputs = new List<FilterInputDTO>();
            var groups = (from grp in _context.Companies select grp).ToList();
            foreach (var v in groups)
            {
                inputs.Add(new FilterInputDTO(v.Label, new List<string>() { v.CompanyCode }));
            }
            return new HierarchyFilter(
                GetMemberName((Filter f) => f.Company),
                GetMemberName((CompositeId c) => c.CompanyId),
                "Company",
                null,
                false,
                true,
                inputs);
        }

        private FilterDTO _getOfficeFilters()
        {
            List<FilterInputDTO> inputs = new List<FilterInputDTO>();
            var offices = (from office in _context.Offices select office).ToList();
            foreach (var v in offices)
            {
                inputs.Add(new FilterInputDTO(v.Label, new List<string>() { v.CompanyCode, v.OfficeCode }));
            }
            return new HierarchyFilter(
                GetMemberName((Filter f) => f.Office),
                GetMemberName((CompositeId c) => c.OfficeId),
                "Office",
                GetMemberName((Filter f) => f.Company),
                true,
                true,
                inputs);
        }

        private FilterDTO _getGroupFilters()
        {
            List<FilterInputDTO> inputs = new List<FilterInputDTO>();
            var groups = (from grp in _context.CompanyOfficeGroups select grp).ToList();
            foreach (var v in groups)
            {
                inputs.Add(new FilterInputDTO(v.Label, new List<string>() { v.CompanyCode, v.OfficeCode, v.GroupCode }));
            }
            return new HierarchyFilter(
                GetMemberName((Filter f) => f.Group),
                GetMemberName((CompositeId c) => c.GroupId),
                "Group",
                GetMemberName((Filter f) => f.Office),
                true,
                true,
                inputs);
        }

        private FilterDTO _getLocationFilters()
        {
            List<FilterInputDTO> inputs = new List<FilterInputDTO>();
            var locations = (from location in _context.Locations select location).ToList();
            foreach (var v in locations)
            {
                inputs.Add(new FilterInputDTO(v.Label, new List<string>() { v.LocationId}));
            }
            return new HierarchyFilter(
                GetMemberName((Filter f) => f.Location),
                GetMemberName((CompositeId c) => c.LocationId),
                "Physical Location",
                null,
                false,
                true,
                inputs);
        }

        private List<FilterDTO> _getRegularFilters()
        {
            List<FilterDTO> regularFilters = new List<FilterDTO>();

            regularFilters.Add(buildRegularFilter(
                GetMemberName((Filter f) => f.FirstName),
                "First Name",
                FilterTypes.text_input));

            regularFilters.Add(buildRegularFilter(
                GetMemberName((Filter f) => f.LastName),
                "Last Name",
                FilterTypes.text_input));

            regularFilters.Add(buildRegularFilter(
                GetMemberName((Filter f) => f.Name),
                "Name",
                FilterTypes.text_input));

            regularFilters.Add(buildRegularFilter(
                GetMemberName((Filter f) => f.Title),
                "Title",
                FilterTypes.text_input));

            regularFilters.Add(buildRegularFilter(
                GetMemberName((Filter f) => f.HireDate),
                "Hire Date",
                FilterTypes.date_input));

            regularFilters.Add(buildRegularFilter(
                GetMemberName((Filter f) => f.TerminationDate),
                "Termination Date",
                FilterTypes.date_input));

            regularFilters.Add(buildRegularFilter(
                GetMemberName((Filter f) => f.YearsPriorExperience),
                "Years of Prior Experience",
                FilterTypes.numeric_input));

            regularFilters.Add(buildRegularFilter(
                GetMemberName((Filter f) => f.Email),
                "Email Address",
                FilterTypes.text_input));

            regularFilters.Add(buildRegularFilter(
                GetMemberName((Filter f) => f.WorkCell),
                "Work Cell",
                FilterTypes.text_input));

            regularFilters.Add(buildRegularFilter(
                GetMemberName((Filter f) => f.WorkPhone),
                "Work Phone",
                FilterTypes.text_input));

            return regularFilters;
        }

        private FilterDTO buildRegularFilter(string name, string display, FilterTypes inputType)
        {
            return new RegularFilter
            (
                name,
                null,
                display,
                null,
                false,
                true,
                inputType
            );
        }

        /**
         * Gets the field name of a class
         */
        public static string GetMemberName<T, TValue>(Expression<Func<T, TValue>> memberAccess)
        {
            return ((MemberExpression)memberAccess.Body).Member.Name;
        }

    }
}
