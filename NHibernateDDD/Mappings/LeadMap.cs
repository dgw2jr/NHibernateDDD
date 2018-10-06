using FluentNHibernate.Mapping;

namespace NHibernateDDD.Mappings
{
    public class LeadMap : SubclassMap<Lead>
    {
        public LeadMap()
        {
            DiscriminatorValue("Lead");
        }
    }
}