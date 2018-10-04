using FluentNHibernate.Mapping;

namespace NHibernateDDD
{
    public class CEOMap : SubclassMap<CEO>
    {
        public CEOMap()
        {
            DiscriminatorValue("CEO");
        }
    }
}