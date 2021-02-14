using System;

namespace AeDirectory
{
    public class Filter
    {

        public Company[] companies { get; set; }
        public Office[] offices { get; set; }
        public Group[] groups { get; set; }
        public Location[] locations { get; set; }
        public Skill[] skills { get; set; }
        public Category category { get; set; }
        public string lastName { get; set; }
        public string firstName { get; set; }
        public string title { get; set; }
        public DateTime hireDate { get; set; }
        public DateTime terminationDate { get; set; }
        public int yearsPriorExperience { get; set; }
        public string email { get; set; }
        public string workPhone { get; set; }
        public string workCell { get; set; }

    }
}