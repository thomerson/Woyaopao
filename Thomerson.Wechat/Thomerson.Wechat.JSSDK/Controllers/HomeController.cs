using Newtonsoft.Json;
using Senparc.Weixin.MP.Containers;
using Senparc.Weixin.MP.Helpers;
using System.Web.Mvc;
using Thomerson.Wechat.JSSDK.Models;

namespace Thomerson.Wechat.JSSDK.Controllers
{
    public class HomeController : Controller
    {
        private static readonly string appid =
        System.Web.Configuration.WebConfigurationManager.AppSettings["wxappid"]?.Trim();

        private static readonly string secret =
        System.Web.Configuration.WebConfigurationManager.AppSettings["wxsecret"]?.Trim();

        private static readonly bool isDedug =
        System.Web.Configuration.WebConfigurationManager.AppSettings["IsDebug"] == "true";


        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 微信jssdk签名
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult WxSignature(string url)
        {
            try
            {
                if (!AccessTokenContainer.CheckRegistered(appid))//检查是否已经注册
                {
                    AccessTokenContainer.Register(appid, secret);//如果没有注册则进行注册
                }

                var tokenResult = HttpRuntimeCache.GetCache("LocalAccessToken") as LocalAccessToken;
                var useCache = true;
                if (tokenResult == null || string.IsNullOrWhiteSpace(tokenResult?.access_token) || tokenResult.ExpiresTime == null || System.DateTime.Now > tokenResult.ExpiresTime) //过期
                {
                    useCache = false;
                    var result = AccessTokenContainer.GetAccessTokenResult(appid, true);
                    if (!string.IsNullOrWhiteSpace(result?.errmsg))
                    {
                        return Json(new { ErrMsg = JsonConvert.SerializeObject(result) }, JsonRequestBehavior.AllowGet);
                        //return Json(new { data = JsonConvert.SerializeObject(new { appid, secret }), ErrMsg = JsonConvert.SerializeObject(tokenResult) }, JsonRequestBehavior.AllowGet);
                    }

                    tokenResult = new LocalAccessToken(result) { };

                    HttpRuntimeCache.SetCache("LocalAccessToken", tokenResult, 7260);
                }


                var ticket = JsApiTicketContainer.GetJsApiTicket(appid);

                var model = new WXShare()
                {
                    appId = appid,
                    timestamp = JSSDKHelper.GetTimestamp(),
                    nonce = JSSDKHelper.GetNoncestr(),
                    url = Server.UrlDecode(url),
                    ticket = ticket,
                    //result = JsonConvert.SerializeObject(tokenResult),
                    useCache = useCache
                };

                model.signature = JSSDKHelper.GetSignature(ticket, model.nonce, model.timestamp, model.url);
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (System.Exception ex)
            {
                return Json(new { ErrMsg = ex.Message, StackTrace = JsonConvert.SerializeObject(ex.StackTrace) }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}