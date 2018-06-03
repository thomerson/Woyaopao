using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Thomerson.Woyaopao.Fuban.Core;

namespace Thomerson.Woyaopao.Fuban.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            //clean cache
            if (WoyaopaoConfig.UseRedis)
            {
                AliRedisClient.getRedisConn().GetDatabase().StringSet(WoyaopaoConfig.Redis_Fuban_SourseDataKey, string.Empty);
            }
        }
    }
}
