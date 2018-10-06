using System;
using System.Linq;
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
            using (ITransaction tx = _session.BeginTransaction())
            {
                var leadRole = _session.Query<Lead>().SingleOrDefault();
                if (leadRole == null)
                {
                    leadRole = new Lead();
                    _session.Save(leadRole);
                }

                var e = Employee.Create("Don", "Woodford", leadRole);

                _session.Save(e);

                Console.WriteLine(e.Name);

                tx.Commit();
            }
        }
    }
}