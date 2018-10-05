using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using MediatR.Extensions.Autofac.DependencyInjection;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Event;
using NHibernate.Tool.hbm2ddl;
using NHibernateDDD.Mappings;

namespace NHibernateDDD.Console
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var container = BuildContainer();

            using (var scope = container.BeginLifetimeScope())
            {
                scope.Resolve<Controller>().Execute();
            }

            System.Console.ReadKey(true);
        }

        private static IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<TextWriter>().AsSelf();
            builder.RegisterType<TestListener>().AsSelf().AsImplementedInterfaces();

            builder.Register(CreateSessionFactory).SingleInstance();

            builder.Register(c => c.Resolve<ISessionFactory>().OpenSession()).InstancePerDependency();
            builder.RegisterType<Controller>().AsSelf();

            builder.AddMediatR(Assembly.GetExecutingAssembly());

            return builder.Build();
        }

        private static ISessionFactory CreateSessionFactory(IComponentContext context)
        {
            var cfg = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2012.ConnectionString(
                    @"Data Source=.\sqlexpress;Initial Catalog=Employees;Integrated Security=true;"))
                .Mappings(m => m.FluentMappings.AddFromAssembly(Assembly.GetAssembly(typeof(EmployeeMap))))
                .ExposeConfiguration(SchemaUpdateConfiguration)
                .BuildConfiguration();

            var postLoadListeners = cfg.EventListeners.PostLoadEventListeners.ToList();
            postLoadListeners.AddRange(context.Resolve<IEnumerable<IPostLoadEventListener>>().ToList());
            cfg.EventListeners.PostLoadEventListeners = postLoadListeners.ToArray();

            var postInsertListeners = cfg.EventListeners.PostInsertEventListeners.ToList();
            postInsertListeners.AddRange(context.Resolve<IEnumerable<IPostInsertEventListener>>().ToList());
            cfg.EventListeners.PostInsertEventListeners = postInsertListeners.ToArray();

            var postUpdateListeners = cfg.EventListeners.PostUpdateEventListeners.ToList();
            postUpdateListeners.AddRange(context.Resolve<IEnumerable<IPostUpdateEventListener>>().ToList());
            cfg.EventListeners.PostUpdateEventListeners = postUpdateListeners.ToArray();
            return cfg.BuildSessionFactory();
        }

        private static void SchemaUpdateConfiguration(Configuration configuration)
        {
            var update = new SchemaUpdate(configuration);
            update.Execute(false, true);
        }
    }
}
