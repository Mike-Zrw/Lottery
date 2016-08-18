using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Lottery.Core.IRepository;
using Lottery.Core.IServices;
using Lottery.Repository;
using Lottery.Service;
using Lottery.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;

namespace Lottery.Web
{
    class WindsorControllerFactory:DefaultControllerFactory
    {
        public WindsorControllerFactory()
        {
            IoC.Container.Register(
                Classes.FromThisAssembly().BasedOn<IController>()
                .WithService.Self()
                .LifestyleTransient());
            ConfigRegister();
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (controllerType == null) return null;
            return (IController)IoC.Container.Resolve(controllerType);
        }
        public void ConfigRegister()
        {
            IoC.Container.Register(Component.For(typeof(ILotteryDbContext)).ImplementedBy(typeof(LotteryContext)).LifeStyle.PerWebRequest);
            IoC.Container.Register(Classes.FromAssemblyNamed("Lottery.Service")
                .Pick().WithService.DefaultInterfaces().LifestylePerWebRequest());
            IoC.Container.Register(Classes.FromAssemblyNamed("Lottery.Repository")
                .Pick().WithService.DefaultInterfaces().LifestylePerWebRequest());
            //IoC.Container.Register(AllTypes.FromAssemblyNamed("Lottery.Web").Pick().WithService.FirstInterface().LifestylePerWebRequest());
        }
    }
}
