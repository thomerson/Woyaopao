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
        public Task Execute(IJobExecutionContext context)
        {
            return new Task(() =>
            {
                Logger.Default.Info("Hello at " + DateTime.Now.ToString());
            });
        }
    }
}
