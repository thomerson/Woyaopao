using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Thomerson.Woyaopao.Core;
using Thomerson.Woyaopao.Core.Model;

namespace Thomerson.Woyaopao.Fuban.Web.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetSourseData()
        {
            try
            {
                var sourseFromRedis = false;
                if (WoyaopaoConfig.UseRedis)
                {
                    var result = AliRedisClient.getRedisConn().GetDatabase().StringGet(WoyaopaoConfig.Redis_Fuban_SourseDataKey);
                    if (string.IsNullOrWhiteSpace(result))
                    {
                        var sourse = WoyaopaoFuban.GetDataFromSource();

                        var entity = WoyaopaoFuban.Sourse2Transfer(sourse);
                        var json = JsonConvert.SerializeObject(entity);
                        AliRedisClient.getRedisConn().GetDatabase().StringSet(WoyaopaoConfig.Redis_Fuban_SourseDataKey, json, new TimeSpan(WoyaopaoConfig.Woyaopao_Fuban_Sourse_Timespan));
                        return Json(new { SourseFromRedis = sourseFromRedis, Data = entity }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        sourseFromRedis = true;
                        var entity = JsonConvert.DeserializeObject<DataTransfer>(result);
                        return Json(new { SourseFromRedis = sourseFromRedis, Data = entity }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    var sourse = WoyaopaoFuban.GetDataFromSource();

                    var entity = WoyaopaoFuban.Sourse2Transfer(sourse);
                    return Json(new { SourseFromRedis = sourseFromRedis, Data = entity }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { Success = false, Msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}