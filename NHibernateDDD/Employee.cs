using System;

namespace NHibernateDDD
{
    public class Employee : Entity
    {
        protected Employee()
        {
            EmployeeId = Guid.NewGuid();
        }

        private Employee(string firstName, string lastName, EmploymentRole role) : this()
        {
            Name = EmployeeName.Create(firstName, lastName);
            EmploymentRole = role;
        }

        public static Employee Create(string firstName, string lastName, EmploymentRole role)
        {
            return new Employee(firstName, lastName, role);
        }

        public virtual Guid EmployeeId { get; protected set; }
        public virtual EmployeeName Name { get; protected set; }

        public virtual EmploymentRole EmploymentRole { get; set; }

        public virtual decimal Bonus => EmploymentRole.CalculateBonus();

        public virtual void PromoteTo(EmploymentRole role)
        {
            EmploymentRole = role;
            //DomainEvents.Add(new EmployeeWasPromotedEvent { Employee = this });
        }
    }
}