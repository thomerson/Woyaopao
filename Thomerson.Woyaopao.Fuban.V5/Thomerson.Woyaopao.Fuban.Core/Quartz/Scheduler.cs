using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thomerson.Woyaopao.Core
{
    public class Scheduler
    {
        public static void Start()
        {
            Task<IScheduler> scheduler = StdSchedulerFactory.GetDefaultScheduler();
            scheduler.Start();
            IJobDetail job = JobBuilder.Create<RequestJob>().Build();
            ITrigger trigger = TriggerBuilder.Create()
              .WithIdentity("triggerName", "groupName")
              .WithSimpleSchedule(t =>
                t.WithIntervalInSeconds(5)
                 .RepeatForever())
                 .Build();

            scheduler.ScheduleJob(job, trigger);
        }
    }
}
