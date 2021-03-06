﻿using Quartz;
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="jobName">作业名称</param>
        public static void Start(string jobName)
        {
            try
            {
                //从工厂中获取一个调度器实例化
                IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();
                //开启调度器
                scheduler.Start();

                IJobDetail job = JobBuilder.Create<RequestJob>()
                                .WithIdentity(jobName, "RequestJob")
                                .Build();

                ITrigger trigger = TriggerBuilder.Create()
                                      .WithIdentity(jobName, "RequestTrigger")
                                      .StartNow()
                                      .WithSimpleSchedule(x => x
                                          .WithIntervalInSeconds(Woyaopao.WQKHospital.WQKHospital.Woyaopao_source_Timespan)//间隔时间
                                          .RepeatForever())
                                      .Build();

                scheduler.ScheduleJob(job, trigger);
            }
            catch (Exception ex) 
            {
                Logger.Default.Error(ex);
            }
           
        }
    }
}
