using System;

namespace AeDirectory
{
    public class Filter
    {

        public Company[] Companies { get; set; }
        public Office[] Offices { get; set; }
        public Group[] Groups { get; set; }
        public Location[] Locations { get; set; }
        public Skill[] Skills { get; set; }
        public Category Category { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Title { get; set; }
        public DateTime HireDate { get; set; }
        public DateTime TerminationDate { get; set; }
        public int YearsPriorExperience { get; set; }
        public string Email { get; set; }
        public string WorkPhone { get; set; }
        public string WorkCell { get; set; }

        public Filter(
            Company[] companies,
            Office[] offices,
            Group[] groups,
            Location[] locations,
            Skill[] skills,
            Category category,
            string lastName,
            string firstName,
            string title,
            DateTime hireDate,
            DateTime terminationDate,
            int yearsPriorExperience,
            string email,
            string workPhone,
            string workCell
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
        }

    }
}