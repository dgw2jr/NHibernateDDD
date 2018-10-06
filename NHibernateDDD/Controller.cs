using System;
using System.Linq;
using CSharpFunctionalExtensions;
using NHibernate;
using NHibernate.Criterion;

namespace NHibernateDDD
{
    public class Controller
    {
        private readonly ISession _session;

        public Controller(ISession session)
        {
            _session = session;
        }

        public void Execute()
        {
            Console.WriteLine("Create an employee...");
            Console.Write("Enter first name:");
            var firstName = Console.ReadLine();
            Console.Write("Enter last name:");
            var lastName = Console.ReadLine();
            Console.Write("Enter role name:");
            var selectedRole = Console.ReadLine();

            using (ITransaction tx = _session.BeginTransaction())
            {
                var role = _session.CreateCriteria(Type.GetType($"NHibernateDDD.{selectedRole}")).List<EmploymentRole>().Single();

                var employee = _session.Query<Employee>().SingleOrDefault(e => e.Name.FirstName == firstName && e.Name.LastName == lastName);

                if (employee == null)
                {
                    Employee.Create(firstName, lastName, role)
                        .OnSuccess(r =>
                        {
                            employee = r;
                            _session.Save(employee);

                            Console.WriteLine(employee.Name);
                            Console.WriteLine(employee.EmployeeId);
                        })
                        .OnFailure(err => Console.WriteLine(err));
                }

                tx.Commit();
            }
        }
    }
}