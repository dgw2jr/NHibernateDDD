namespace NHibernateDDD
{
    public class Manager : EmploymentRole
    {
        public override decimal CalculateBonus()
        {
            return 100m;
        }
    }
}