using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lottery.UniteTest
{
   public class StaticMethod
    {
       static object o = new object();
       public static void Add()
       {
           lock (o) { 
           Console.WriteLine(Thread.CurrentThread.ManagedThreadId + "comein");
           Thread.Sleep(2000);
           int i = 2;
           int a = 3;
           int b = i + a;
           Console.WriteLine("comeout");
           }
       }
    }
}
