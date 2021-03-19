using System;
using System.Collections.Generic;

#nullable enable

namespace AeDirectory.Domain
{
    public class Filter
    {

        public List<string>? Companies { get; set; }
        public List<string>? Offices { get; set; }
        public List<string>? Groups { get; set; }
        public List<string>? Locations { get; set; }
        public List<string>? Skills { get; set; }
        public string? Category_id { get; set; }
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public string? Title { get; set; }
        public DateTime? HireDate { get; set; }
        public DateTime? TerminationDate { get; set; }
        public decimal? YearsPriorExperience { get; set; }
        public string? Email { get; set; }
        public string? WorkPhone { get; set; }
        public string? WorkCell { get; set; }
        public Boolean UseAND { get; set; }

/*        public Filter(
            List<string> companies,
            List<string> offices,
            List<string> groups,
            List<string> locations,
            List<string> skills,
            Category category,
            string? lastName,
            string? firstName,
            string? title,
            DateTime? hireDate,
            DateTime? terminationDate,
            decimal? yearsPriorExperience,
            string? email,
            string? workPhone,
            string? workCell
        ) {
            Companies = companies;
            Offices = offices;
            Groups = groups;
            Locations = locations;
            Skills = skills;
            LastName = lastName;
            FirstName = firstName;
            Title = title;
            HireDate = hireDate;
            TerminationDate = terminationDate;
            YearsPriorExperience = yearsPriorExperience;
            Email = email;
            WorkPhone = workPhone;
            WorkCell = workCell;
        }*/

    }
}