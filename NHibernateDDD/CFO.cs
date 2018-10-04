namespace NHibernateDDD
{
    public class CFO : EmploymentRole
    {
        public override decimal CalculateBonus()
        {
            return 10000m;
        }
    }
}