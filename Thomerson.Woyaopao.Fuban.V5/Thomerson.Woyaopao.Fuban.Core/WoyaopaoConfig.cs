using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;


namespace Thomerson.Woyaopao.Core
{
    public class WoyaopaoConfig
    {
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

        public static string Redis_Fuban_SourseDataKey
        {
            get
            {
                return ConfigurationManager.AppSettings["Redis_Fuban_SourseDataKey"].ToString();
            }
        }

        public static string Woyaopao_Fuban_DataUri
        {
            get
            {
                return ConfigurationManager.AppSettings["Woyaopao_Fuban_DataUri"].ToString();
            }
        }

        public static int Woyaopao_Fuban_TotalTarget
        {
            get
            {
                return int.Parse(ConfigurationManager.AppSettings["Woyaopao_Fuban_TotalTarget"]);
            }
        }

        public static string Redis_Fuban_UserId
        {
            get
            {
                return ConfigurationManager.AppSettings["Redis_Fuban_UserId"].ToString();
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
        public static int Woyaopao_Fuban_Sourse_Timespan
        {
            get
            {
                return int.Parse(ConfigurationManager.AppSettings["Woyaopao_Fuban_Sourse_Timespan"]);
            }
        }

        /// <summary>
        /// 文件(头像)地址
        /// </summary>
        public static string Woyaopao_Fuban_SourseUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["Woyaopao_Fuban_SourseUrl"];
            }
        }

        /// <summary>
        /// 报名链接
        /// </summary>
        public static string Woyaopao_Fuban_ApplyUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["Woyaopao_Fuban_ApplyUrl"];
            }
        }

    }
}
