using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Thomerson.Woyaopao.Core;
using Thomerson.Woyaopao.WQKHospital.Models;

namespace Thomerson.Woyaopao.WQKHospital.Controllers
{
    public class HomeController : Controller
    {

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult RunInfo()
        {
            try
            {
                var sourceFromRedis = false;
                if (WoyaopaoConfig.UseRedis)
                {
                    //var result = AliRedisClient.getRedisConn().GetDatabase().StringGet(WoyaopaoConfig.Redis_sourceDataKey);
                    var result = HttpRuntimeCache.GetCache(WoyaopaoConfig.Redis_sourceDataKey) as string;

                    if (string.IsNullOrWhiteSpace(result))
                    {
                        var source = WQKHospital.GetDataFromSource();
                        var entity = WQKHospital.DataTransfor(source.data);
                        var json = JsonConvert.SerializeObject(entity);
                        HttpRuntimeCache.SetCache(WoyaopaoConfig.Redis_sourceDataKey, json, WoyaopaoConfig.Redis_Overtime);
                        //AliRedisClient.getRedisConn().GetDatabase().StringSet(WoyaopaoConfig.Redis_sourceDataKey, json, new TimeSpan(WoyaopaoConfig.Woyaopao_source_Timespan));
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
                    var source = WQKHospital.GetDataFromSource();
                    var entity = WQKHospital.DataTransfor(source.data);

                    return Json(new { sourceFromRedis = sourceFromRedis, Data = entity }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                Logger.Default.Error(ex);
                return Json(new { Success = false, Msg = JsonConvert.SerializeObject(ex) }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}