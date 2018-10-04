using FluentNHibernate.Mapping;

namespace NHibernateDDD.Mappings
{
    public class ManagerMap : SubclassMap<Manager>
    {
        public ManagerMap()
        {
            DiscriminatorValue("Manager");
        }
    }
}