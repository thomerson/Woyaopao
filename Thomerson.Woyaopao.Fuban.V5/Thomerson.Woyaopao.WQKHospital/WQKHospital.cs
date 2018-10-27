using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Web;
using Thomerson.Woyaopao.Core;
using Thomerson.Woyaopao.Core.Model;

namespace Thomerson.Woyaopao.WQKHospital
{
    public class WQKHospital
    {
        #region config
        public static bool UseRedis
        {
            get
            {
                return ConfigurationManager.AppSettings["UseRedis"].ToString() == "1";
            }
        }

        public static string RedisUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["RedisUrl"].ToString();
            }
        }

        public static string RedisPassword
        {
            get
            {
                return ConfigurationManager.AppSettings["RedisPassword"].ToString();
            }
        }

        public static string Redis_SourseDataKey
        {
            get
            {
                return ConfigurationManager.AppSettings["Redis__SourseDataKey"].ToString();
            }
        }

        public static string SourseDataUri
        {
            get
            {
                return ConfigurationManager.AppSettings["Woyaopao__DataUri"].ToString();
            }
        }

        public static string Redis_UserId
        {
            get
            {
                return ConfigurationManager.AppSettings["Redis__UserId"].ToString();
            }
        }


        /// <summary>
        /// redis 设置过期时间
        /// </summary>
        public static int Redis_Fuban_Overtime
        {
            get
            {
                return int.Parse(ConfigurationManager.AppSettings["Redis_Fuban_Overtime"]);
            }
        }

        /// <summary>
        /// 获取sourse的间隔时间
        /// </summary>
        public static int Woyaopao_Sourse_Timespan
        {
            get
            {
                return int.Parse(ConfigurationManager.AppSettings["Woyaopao__Sourse_Timespan"]);
            }
        }

        /// <summary>
        /// 文件(头像)地址
        /// </summary>
        public static string Woyaopao_SourseUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["Woyaopao__SourseUrl"];
            }
        }

        /// <summary>
        /// 报名链接
        /// </summary>
        public static string Woyaopao_ApplyUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["Woyaopao__ApplyUrl"];
            }
        }

        #endregion

        public static SourseData GetDataFromSource()
        {
            var result = string.Empty;
            try
            {
                var client = new HttpClient();
                result = client.GetStringAsync(SourseDataUri).Result;

                Logger.Default.Info(result);
                return JsonConvert.DeserializeObject<SourseData>(result);
            }
            catch (Exception ex)
            {
                Logger.Default.Error(ex);
                Logger.Default.Info(result);
                return null;
            }

        }

    }
}