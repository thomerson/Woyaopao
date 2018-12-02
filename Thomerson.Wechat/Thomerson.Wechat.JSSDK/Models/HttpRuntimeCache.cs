using System;
using System.Web;
using System.Web.Caching;

namespace Thomerson.Wechat.JSSDK
{
    public class HttpRuntimeCache
    {
        /// <summary>
        /// 获取数据缓存
        /// </summary>
        /// <param name="CacheKey">键</param>
        public static object GetCache(string CacheKey)
        {
            if (!string.IsNullOrWhiteSpace(CacheKey))
            {
                return HttpRuntime.Cache.Get(CacheKey);
            }
            return null;
        }

        //// <summary>
        /// 设置数据缓存
        /// </summary>
        public static void SetCache(string cacheKey, object obj, int timeout = 7200)
        {
            if (!string.IsNullOrWhiteSpace(cacheKey) && obj != null)
            {
                HttpRuntime.Cache.Insert(cacheKey, obj, null, DateTime.Now.AddSeconds(timeout), TimeSpan.Zero, CacheItemPriority.High, null);
            }
        }


        //// <summary>
        /// 移除指定数据缓存
        /// </summary>
        public static void RemoveCache(string CacheKey)
        {
            if (!string.IsNullOrWhiteSpace(CacheKey))
            {
                HttpRuntime.Cache.Remove(CacheKey);
            }
        }

        //// <summary>
        /// 移除全部缓存
        /// </summary>
        public void RemoveAllCache()
        {
            var CacheEnum = HttpRuntime.Cache.GetEnumerator();
            while (CacheEnum.MoveNext())
            {
                HttpRuntime.Cache.Remove(CacheEnum.Key.ToString());
            }
        }
    }
}