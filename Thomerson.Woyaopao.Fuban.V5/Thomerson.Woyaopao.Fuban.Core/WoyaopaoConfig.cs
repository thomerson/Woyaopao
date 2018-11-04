using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Newtonsoft.Json;

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

        public static string Redis_sourceDataKey
        {
            get
            {
                return ConfigurationManager.AppSettings["Redis_sourceDataKey"].ToString();
            }
        }

        public static string Woyaopao_DataUri
        {
            get
            {
                return ConfigurationManager.AppSettings["Woyaopao_DataUri"].ToString();
            }
        }

        public static string Redis_UserId
        {
            get
            {
                return ConfigurationManager.AppSettings["Redis_UserId"].ToString();
            }
        }


        /// <summary>
        /// redis 设置过期时间
        /// </summary>
        public static int Redis_Overtime
        {
            get
            {
                return int.Parse(ConfigurationManager.AppSettings["Redis_Overtime"]);
            }
        }

        /// <summary>
        /// 获取source的间隔时间
        /// </summary>
        public static int Woyaopao_source_Timespan
        {
            get
            {
                return int.Parse(ConfigurationManager.AppSettings["Woyaopao_source_Timespan"]);
            }
        }

        /// <summary>
        /// 文件(头像)地址
        /// </summary>
        public static string sourceUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["Woyaopao_sourceUrl"];
            }
        }

        /// <summary>
        /// 报名链接
        /// </summary>
        public static string ApplyUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["Woyaopao_ApplyUrl"];
            }
        }
        public static string sourceDataUri
        {
            get
            {
                return ConfigurationManager.AppSettings["Woyaopao_DataUri"].ToString();
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
        /// 获取source的间隔时间
        /// </summary>
        public static int source_Timespan
        {
            get
            {
                return int.Parse(ConfigurationManager.AppSettings["Woyaopao__source_Timespan"]);
            }
        }

        /// <summary>
        /// 文件(头像)地址
        /// </summary>
        public static string Woyaopao_sourceUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["Woyaopao__sourceUrl"];
            }
        }

    }
}
