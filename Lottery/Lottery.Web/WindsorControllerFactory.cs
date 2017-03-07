using Autofac;
using Autofac.Integration.Mvc;
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
using System.Reflection;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;

namespace Lottery.Web
{
    class WindsorControllerFactory : DefaultControllerFactory
    {
        public WindsorControllerFactory()
        {
            ConfigRegister();
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (controllerType == null) return null;
            return (IController)IoC.Container.Resolve(controllerType);
        }
        public void ConfigRegister()
        {
            var builder = new ContainerBuilder();
            //容器
            builder.RegisterType<LotteryContext>().As<ILotteryDbContext>();
            builder.RegisterGeneric(typeof(CrudRepository<>)).As(typeof(ICrudRepository<>));
            builder.RegisterAssemblyTypes(Assembly.Load("Lottery.Service")).AsImplementedInterfaces();
            builder.RegisterAssemblyTypes(Assembly.Load("Lottery.Repository")).AsImplementedInterfaces();
            //注册主项目的Controllers
            builder.RegisterControllers(Assembly.GetExecutingAssembly()).PropertiesAutowired();
            IoC.Build = builder;
            //GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(IoC.Container);  //webapi
            DependencyResolver.SetResolver(new AutofacDependencyResolver(IoC.Container)); //controller
        }
    }
}
