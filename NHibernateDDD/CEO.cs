namespace NHibernateDDD
{
    public class CEO : EmploymentRole
    {
        public override decimal CalculateBonus()
        {
            return 100000m;
        }
    }
}