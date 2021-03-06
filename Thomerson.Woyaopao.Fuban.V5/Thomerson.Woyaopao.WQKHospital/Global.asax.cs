﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Thomerson.Woyaopao.WQKHospital
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            if (Core.WoyaopaoConfig.UseRedis)
            {
                Core.Logger.Default.Info("开始执行计划任务");
                //执行计划任务
                Core.Scheduler.Start("XQKHospital");
            }
        }
    }
}
