namespace NHibernateDDD
{
    public class Contractor : EmploymentRole
    {
        public override decimal CalculateBonus()
        {
            return 50m;
        }
    }
}