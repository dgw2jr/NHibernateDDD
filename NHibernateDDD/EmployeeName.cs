using System;
using System.Collections.Generic;
using CSharpFunctionalExtensions;

namespace NHibernateDDD
{
    public class EmployeeName : ValueObject
    {
        private EmployeeName() { }
        private EmployeeName(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public static Result<EmployeeName> Create(string firstName, string lastName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
            {
                //throw new ArgumentException("FirstName can't be null or empty", nameof(firstName));
                return Result.Fail<EmployeeName>("FirstName can't be null or empty");
            }

            if (string.IsNullOrWhiteSpace(lastName))
            {
                //throw new ArgumentException("LastName can't be null or empty", nameof(lastName));
                return Result.Fail<EmployeeName>("LastName can't be null or empty");
            }

            return Result.Ok(new EmployeeName(firstName, lastName));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return FirstName;
            yield return LastName;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }
    }
}