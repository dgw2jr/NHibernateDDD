using FluentNHibernate.Mapping;

namespace NHibernateDDD
{
    public class ManagerMap : SubclassMap<Manager>
    {
        public ManagerMap()
        {
            DiscriminatorValue("Manager");
        }
    }
}