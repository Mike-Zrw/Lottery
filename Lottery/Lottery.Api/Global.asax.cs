using Autofac;
using Lottery.Core.IRepository;
using Lottery.Core.IServices;
using Lottery.Repository;
using Lottery.Service;
using Lottery.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac.Integration.Mvc;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.IO;
using Autofac.Integration.WebApi;

namespace Lottery.Api
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            SetupResolveRules();
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Tasks.Tasks.Init(); //开启任务
        }

        /// <summary>
        /// Autofac Ioc注册
        /// </summary>
        private void SetupResolveRules()
        {
            var builder = new ContainerBuilder();
            //容器
            builder.RegisterType<LotteryContext>().As<ILotteryDbContext>();
            builder.RegisterGeneric(typeof(CrudRepository<>)).As(typeof(ICrudRepository<>));
            builder.RegisterAssemblyTypes(Assembly.Load("Lottery.Service")).AsImplementedInterfaces();
            builder.RegisterAssemblyTypes(Assembly.Load("Lottery.Repository")).AsImplementedInterfaces(); 
            //注册主项目的Controllers
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly()).PropertiesAutowired();
            IoC.Build = builder;
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(IoC.Container);  //webapi
            //DependencyResolver.SetResolver(new AutofacDependencyResolver(IoC.Container)); //controller
        }
        /// <summary>
        /// 跨域设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            var req = System.Web.HttpContext.Current.Request;
            if (req.HttpMethod == "OPTIONS")//过滤options请求，用于js跨域
            {
                Response.StatusCode = 200;
                Response.SubStatusCode = 200;
                Response.End();
            }
        }
    }
}
