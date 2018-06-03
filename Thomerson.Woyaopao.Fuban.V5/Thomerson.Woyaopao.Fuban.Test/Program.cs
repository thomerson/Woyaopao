using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thomerson.Woyaopao.Fuban.Core;
using Newtonsoft.Json;

namespace Thomerson.Woyaopao.Fuban.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                WoyaopaoFuban.ServiceTimer();
                //WoyaopaoFuban.GetSourseFromRedis();
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Logger.Default.Error(ex);
                Logger.Default.Info(ex.Message);
            }

        }
    }
}
