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
                var e = _session.Get<Employee>(new Guid("49587dbb-596e-4a6f-98c6-d33a997a04b2"));

                Console.WriteLine(e.Name);
                var role = _session.Query<CEO>().Single();
                //e.PromoteTo(role);
                //_session.Save(e);

                tx.Commit();
            }
        }
    }
}