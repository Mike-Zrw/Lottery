using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery.Tools
{
    /// <summary>
    ///  Singleton：一个组件只有一个实例被创建，所有请求的客户使用程序得到的都是同一个实例，同时这也是Castle IOC容器默认的一种处理方式。
    ///     Transient：这种处理方式与我们平时使用new的效果是一样的，对于每次的请求得到的都是一个新的实例。
    ///  PerThread：对于每一个线程来说是使用了Singleton，也就是每一个线程得到的都是同一个实例。
    ///Pooled：对象池的处理方式，对于不再需用的实例会保存到一个对象池中。
    /// Custom：自定义的生命处理方式。
    /// </summary>
    public class IoC
    {
        private static readonly object LockObj = new object();
        private static ContainerBuilder build;
        private static IContainer container;
        static IoC()
        {
            build = new ContainerBuilder(); 
            container = build.Build();
        }
        public static ContainerBuilder Build
        {
            get { return build; }

            set
            {
                lock (LockObj)
                {
                    build = value;
                    container = build.Build();
                }
            }
        }
        public static IContainer Container
        {
            get { return container; }
        }
        public static T Resolve<T>()
        {
            return container.Resolve<T>();
        }


        public static object Resolve(Type type)
        {
            return container.Resolve(type);
        }
        public static void Configure()
        {

        }
    }
}
