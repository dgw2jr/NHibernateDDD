using FluentNHibernate.Mapping;

namespace NHibernateDDD.Mappings
{
    public class CEOMap : SubclassMap<CEO>
    {
        public CEOMap()
        {
            DiscriminatorValue("CEO");
        }
    }
}