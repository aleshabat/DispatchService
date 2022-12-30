using System.Configuration;
using System.Data;
using System.Web.Mvc;
using dispatchservice.api.Database;
using dispatchservice.api.Domain;
using dispatchservice.api.Repositories;
using dispatchservice.web.Controllers;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(dispatchservice.web.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(dispatchservice.web.App_Start.NinjectWebCommon), "Stop")]

namespace dispatchservice.web.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            //var db = new SqLiteDatabase(ConfigurationManager.ConnectionStrings["DBConnectionString"].ToString());
            kernel.Bind<IControllerFactory>().To<CustomControllerFactory>().InRequestScope();
            kernel.Bind<ITicketRepository>().To<TicketRepository>().InRequestScope();
            kernel.Bind<IHouseRepository>().To<HouseRpository>().InRequestScope();
            kernel.Bind<IStreetRepository>().To<StreetRepository>().InRequestScope();
            kernel.Bind<IRepository<Estate>>().To<Repository<Estate>>().InRequestScope();
            kernel.Bind<IRepository<Service>>().To<Repository<Service>>().InRequestScope();
            kernel.Bind<IRepository<Injener>>().To<Repository<Injener>>().InRequestScope();
            kernel.Bind<IInjenerHouseRepository>().To<InjenerHouseRepository>().InRequestScope();


            //kernel.Bind<IDatabase>().ToConstant(db);
        }        
    }
}
