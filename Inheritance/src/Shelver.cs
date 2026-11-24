using System;

namespace LibrarySystem
{
    public class Shelver : Staff
    {
        public string Section { get; private set; }

        public Shelver(
            string personId,
            string fullName,
            string phone,
            DateTime hireDate,
            decimal monthlySalary,
            string section)
            : base(personId, fullName, phone, hireDate, monthlySalary, 0m)
        {
            if (string.IsNullOrWhiteSpace(section))
            {
                throw new ArgumentException("Section cannot be empty.", nameof(section));
            }

            Section = section.Trim();
        }

        public void Reassign(string newSection)
        {
            if (string.IsNullOrWhiteSpace(newSection))
            {
                throw new ArgumentException("Section cannot be empty.", nameof(newSection));
            }

            Section = newSection.Trim();
        }
    }
}
