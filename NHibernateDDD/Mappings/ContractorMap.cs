using FluentNHibernate.Mapping;

namespace NHibernateDDD.Mappings
{
    public class ContractorMap : SubclassMap<Contractor>
    {
        public ContractorMap()
        {
            DiscriminatorValue("Contractor");
        }
    }
}