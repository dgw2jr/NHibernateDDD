using FluentNHibernate.Mapping;

namespace NHibernateDDD
{
    public class EmployeeMap : ClassMap<Employee>
    {
        public EmployeeMap()
        {
            Table("Employees");
            Id(e => e.EmployeeId);
            Component(e => e.Name, mapper => {
                mapper.Map(e => e.FirstName, "Name_FirstName");
                mapper.Map(e => e.LastName, "Name_LastName");
            });
            References(e => e.EmploymentRole, "EmploymentRole_EmploymentRoleId");
        }
    }
}