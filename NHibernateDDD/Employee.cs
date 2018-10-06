using System;
using CSharpFunctionalExtensions;
using NHibernateDDD.Events;

namespace NHibernateDDD
{
    public class Employee : Entity
    {
        protected Employee()
        {
            EmployeeId = Guid.NewGuid();
        }

        private Employee(EmployeeName name, EmploymentRole role) : this()
        {
            Name = name;
            EmploymentRole = role;
        }

        public static Result<Employee> Create(string firstName, string lastName, EmploymentRole role)
        {
            var name = EmployeeName.Create(firstName, lastName);
            if (name.IsFailure)
            {
                return Result.Fail<Employee>(name.Error);
            }

            if (role == null)
            {
                return Result.Fail<Employee>("EmploymentRole cannot be null");
            }

            return Result.Ok(new Employee(name.Value, role));
        }

        public virtual Guid EmployeeId { get; protected set; }
        public virtual EmployeeName Name { get; protected set; }

        public virtual EmploymentRole EmploymentRole { get; set; }

        public virtual decimal Bonus => EmploymentRole.CalculateBonus();

        public virtual void PromoteTo(EmploymentRole role)
        {
            EmploymentRole = role;
            DomainEvents.Add(new EmployeeWasPromotedEvent { Employee = this });
        }

        public virtual Result ChangeName(string firstName, string lastName)
        {
            return EmployeeName.Create(firstName, lastName)
                .OnSuccess(r => Name = r);
        }
    }
}