using FluentNHibernate.Mapping;

namespace NHibernateDDD.Mappings
{
    public class EmploymentRoleMap : ClassMap<EmploymentRole>
    {
        public EmploymentRoleMap()
        {
            Id(e => e.EmploymentRoleId);
            DiscriminateSubClassesOnColumn("Discriminator").Unique();
        }
    }
}