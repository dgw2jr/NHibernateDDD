using FluentNHibernate.Mapping;

namespace NHibernateDDD.Mappings
{
    public class CFOMap : SubclassMap<CFO>
    {
        public CFOMap()
        {
            DiscriminatorValue("CFO");
        }
    }
}