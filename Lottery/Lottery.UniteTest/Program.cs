using Castle.MicroKernel.Registration;
using Castle.Windsor.Installer;
using Lottery.Core.DataModel;
using Lottery.Core.DTO;
using Lottery.Core.DTO.Common;
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
    class Program
    {
         static void Main(string[] args)
        {
            WinsorConfig.ConfigRegister();
            Services ser=new Services();
            //JsonResult<IEnumerable<BUser>> re = ser.DeleteUsers();
            //Console.WriteLine(re.Success);
            //List<BDeskUserDto> list = ser.findDescUser();
            //Console.WriteLine(list.Count);
            Console.Read();
        }
    }
}
