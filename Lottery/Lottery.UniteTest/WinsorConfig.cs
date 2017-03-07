using Castle.MicroKernel.Registration;
using Lottery.Core.IRepository;
using Lottery.Core.IServices;
using Lottery.Repository;
using Lottery.Service;
using Lottery.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery.UniteTest
{
    public class WinsorConfig
    {
        public static void ConfigRegister()
        {
            //IoC.Container.Register(Component.For(typeof(ILotteryDbContext)).ImplementedBy(typeof(LotteryContext)).LifestyleSingleton());
            //IoC.Container.Register(Component.For(typeof(IBUserService)).ImplementedBy(typeof(BUserService)).LifestyleSingleton());
            //IoC.Container.Register(Classes.FromAssemblyNamed("Lottery.Service")
            //    .Pick().WithService.DefaultInterfaces().LifestyleTransient());
            //IoC.Container.Register(Classes.FromAssemblyNamed("Lottery.Repository")
            //    .Pick().WithService.DefaultInterfaces().LifestyleTransient());
            //IoC.Container.Register(AllTypes.FromAssemblyNamed("Lottery.Web").Pick().WithService.FirstInterface().LifestylePerWebRequest());
        }
    }
}
