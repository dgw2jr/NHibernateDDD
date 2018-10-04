using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Event;
using System;

namespace NHibernateDDD.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var cfg = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2008.ConnectionString(
                    @"Data Source=np:\\.\pipe\LOCALDB#B4222CC7\tsql\query;Initial Catalog=Employees;Integrated Security=true;"))
                .Mappings(m => m.FluentMappings.AddFromAssembly(Assembly.GetAssembly(typeof(EmployeeMap))))
                .BuildConfiguration();

            var builder = new ContainerBuilder();
            builder.RegisterType<TextWriter>().AsSelf();
            builder.RegisterType<TestListener>().AsSelf().AsImplementedInterfaces();
            builder.Register(c =>
            {
                var postLoadListeners = cfg.EventListeners.PostLoadEventListeners.ToList();
                postLoadListeners.AddRange(c.Resolve<IEnumerable<IPostLoadEventListener>>().ToList());
                cfg.EventListeners.PostLoadEventListeners = postLoadListeners.ToArray();

                var postInsertListeners = cfg.EventListeners.PostInsertEventListeners.ToList();
                postInsertListeners.AddRange(c.Resolve<IEnumerable<IPostInsertEventListener>>().ToList());
                cfg.EventListeners.PostInsertEventListeners = postInsertListeners.ToArray();

                var postUpdateListeners = cfg.EventListeners.PostUpdateEventListeners.ToList();
                postUpdateListeners.AddRange(c.Resolve<IEnumerable<IPostUpdateEventListener>>().ToList());
                cfg.EventListeners.PostUpdateEventListeners = postUpdateListeners.ToArray();
                return cfg.BuildSessionFactory();
            }).SingleInstance();
            builder.Register(c => c.Resolve<ISessionFactory>().OpenSession()).InstancePerDependency();
            builder.RegisterType<Controller>().AsSelf();
            var container = builder.Build();

            using (var scope = container.BeginLifetimeScope())
            {
                scope.Resolve<Controller>().Execute();
            }

            System.Console.ReadKey(true);
        }
    }
}
