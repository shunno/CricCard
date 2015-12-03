using Data;
using Microsoft.Practices.Unity;
using Model;
using Repository.Pattern.DataContext;
using Repository.Pattern.Ef6;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;
using Service;
using System.Web.Http;
using Service.Pattern;
using Unity.WebApi;

namespace WebMvc
{
    public static class UnityConfig
    {
        public static IUnityContainer container;


        public static void RegisterComponents()
        {
            container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            container.RegisterType<IDataContextAsync, ApplicationContext>(new HierarchicalLifetimeManager());
            container.RegisterType<IUnitOfWorkAsync, UnitOfWork>(new HierarchicalLifetimeManager());
            container.RegisterType(typeof(IRepositoryAsync<>), typeof(Repository<>));
            container.RegisterType<IMatchService, MatchService>();
            container.RegisterType<IOverDetailService, OverDetailService>();
            //GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
            GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);
        }
    }
}