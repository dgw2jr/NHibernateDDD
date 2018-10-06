using System;
using System.Linq;
using CSharpFunctionalExtensions;
using NHibernate;

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
            var roles = _session.Query<EmploymentRole>()
                .ToList()
                .Select((v, i) => new {Value = v, Index = i + 1})
                .ToList();

            Console.WriteLine("Create an employee...");
            Console.Write("Enter first name:");
            var firstName = Console.ReadLine();
            Console.Write("Enter last name:");
            var lastName = Console.ReadLine();
            Console.WriteLine("Select a role:");
            foreach (var role in roles)
            {
                Console.WriteLine($"\t{role.Index}: {role.Value.GetType().Name}");
            }

            var selectedRoleIndex = Console.ReadLine();
            var selectedRole = roles.Single(r => r.Index.ToString() == selectedRoleIndex).Value;

            using (ITransaction tx = _session.BeginTransaction())
            {
                var employee = _session.Query<Employee>().SingleOrDefault(e => e.Name.FirstName == firstName && e.Name.LastName == lastName);

                if (employee == null)
                {
                    Employee.Create(firstName, lastName, selectedRole)
                        .OnSuccess(emp =>
                        {
                            _session.Save(emp);

                            Console.WriteLine(emp.Name);
                            Console.WriteLine(emp.EmployeeId);
                        })
                        .OnFailure(err => Console.WriteLine(err));
                }

                tx.Commit();

                Execute();
            }
        }
    }
}