using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Thomerson.Woyaopao.Core;

namespace Thomerson.Woyaopao.Fuban.Web.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetsourceData()
        {
            try
            {
                var sourceFromRedis = false;
                if (WoyaopaoConfig.UseRedis)
                {
                    var result = AliRedisClient.getRedisConn().GetDatabase().StringGet(WoyaopaoConfig.Redis_sourceDataKey);
                    if (string.IsNullOrWhiteSpace(result))
                    {
                        var source = FubanPlatform.GetDataFromSource();

                        var entity = FubanPlatform.source2Transfer(source);
                        var json = JsonConvert.SerializeObject(entity);
                        AliRedisClient.getRedisConn().GetDatabase().StringSet(WoyaopaoConfig.Redis_sourceDataKey, json, new TimeSpan(WoyaopaoConfig.Woyaopao_source_Timespan));
                        return Json(new { sourceFromRedis = sourceFromRedis, Data = entity }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        sourceFromRedis = true;
                        var entity = JsonConvert.DeserializeObject<CustPageInfo>(result);
                        return Json(new { sourceFromRedis = sourceFromRedis, Data = entity }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    var source = FubanPlatform.GetDataFromSource();

                    var entity = FubanPlatform.source2Transfer(source);
                    return Json(new { sourceFromRedis = sourceFromRedis, Data = entity }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { Success = false, Msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}