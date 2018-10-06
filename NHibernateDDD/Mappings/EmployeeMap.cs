using FluentNHibernate.Mapping;

namespace NHibernateDDD.Mappings
{
    public class EmployeeMap : ClassMap<Employee>
    {
        public EmployeeMap()
        {
            Id(e => e.EmployeeId);
            Component(e => e.Name, mapper => {
                mapper.Map(e => e.FirstName);
                mapper.Map(e => e.LastName);
            });
            References(e => e.EmploymentRole);
        }
    }
}