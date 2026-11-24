using System;

namespace LibrarySystem
{
    public class Person
    {
        public string PersonId { get; }
        public string FullName { get; }
        public string Phone { get; }

        protected Person(string personId, string fullName, string phone)
        {
            if (string.IsNullOrWhiteSpace(personId))
            {
                throw new ArgumentException("Person ID cannot be empty.", nameof(personId));
            }

            if (string.IsNullOrWhiteSpace(fullName))
            {
                throw new ArgumentException("Full name cannot be empty.", nameof(fullName));
            }

            if (string.IsNullOrWhiteSpace(phone))
            {
                throw new ArgumentException("Phone cannot be empty.", nameof(phone));
            }

            PersonId = personId.Trim();
            FullName = fullName.Trim();
            Phone = phone.Trim();
        }
    }
}
