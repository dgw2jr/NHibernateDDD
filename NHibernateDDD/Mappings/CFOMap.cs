using FluentNHibernate.Mapping;

namespace NHibernateDDD
{
    public class CFOMap : SubclassMap<CFO>
    {
        public CFOMap()
        {
            DiscriminatorValue("CFO");
        }
    }
}