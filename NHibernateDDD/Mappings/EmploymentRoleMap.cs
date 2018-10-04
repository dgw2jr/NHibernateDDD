using FluentNHibernate.Mapping;

namespace NHibernateDDD
{
    public class EmploymentRoleMap : ClassMap<EmploymentRole>
    {
        public EmploymentRoleMap()
        {
            Table("EmploymentRoles");
            Id(e => e.EmploymentRoleId);
            DiscriminateSubClassesOnColumn("Discriminator");
        }
    }
}