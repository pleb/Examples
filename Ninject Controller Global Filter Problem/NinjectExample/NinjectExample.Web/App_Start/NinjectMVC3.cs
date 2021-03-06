using System.Web.Mvc;
using Ninject.Web.Common;
using Ninject.Web.Mvc.FilterBindingSyntax;
using NinjectExample.Web.Code;

[assembly: WebActivator.PreApplicationStartMethod(typeof(NinjectExample.Web.App_Start.NinjectMVC3), "Start")]
[assembly: WebActivator.ApplicationShutdownMethodAttribute(typeof(NinjectExample.Web.App_Start.NinjectMVC3), "Stop")]

namespace NinjectExample.Web.App_Start
{
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
	using Ninject.Web.Mvc;

    public static class NinjectMVC3 
	{
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
		{
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestModule));
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
            RegisterServices(kernel);
            return kernel;
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.BindFilter<FilterAlpha>(FilterScope.Controller, 0)
                .WhenControllerHas<AlphaAttribute>();

            //kernel.BindFilter<FilterAlpha>(FilterScope.Global, 0);
            kernel.BindFilter<FilterBeta>(FilterScope.Global, 0);
        }		
    }
}
