namespace NHibernateDDD
{
    public class Lead : EmploymentRole
    {
        public override decimal CalculateBonus()
        {
            return 500m;
        }
    }
}