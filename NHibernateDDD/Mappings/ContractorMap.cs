using FluentNHibernate.Mapping;

namespace NHibernateDDD
{
    public class ContractorMap : SubclassMap<Contractor>
    {
        public ContractorMap()
        {
            DiscriminatorValue("Contractor");
        }
    }
}