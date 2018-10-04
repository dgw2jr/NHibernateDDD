using System;

namespace NHibernateDDD
{
    public abstract class EmploymentRole
    {
        public EmploymentRole()
        {
            EmploymentRoleId = Guid.NewGuid();
        }

        public virtual Guid EmploymentRoleId { get; protected set; }

        public abstract decimal CalculateBonus();
    }
}