using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thomerson.Woyaopao.Core
{
    public class RequestJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            var source = Woyaopao.WQKHospital.WQKHospital.GetDataFromSource();

            //Logger.Default.Info("Hello at " + DateTime.Now.ToString());
        }
    }
}
