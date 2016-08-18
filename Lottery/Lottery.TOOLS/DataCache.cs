using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Lottery.Tools
{
    public class DataCache
    {
        /// <summary>
        /// Reference类型数据缓存时间（分钟）
        /// </summary>
        public static int ReferenceModelCacheMinutes
        {
            get { return ConfigHelper.GetConfigInt("ReferenceModelCacheMinutes"); }
        }
        /// <summary>
        /// 普通数据缓存时间（分钟）
        /// </summary>
        public static int CommonModelCacheMinutes
        {
            get { return ConfigHelper.GetConfigInt("CommonModelCacheMinutes"); }
        }
        /// <summary>
        /// 获取当前应用程序指定CacheKey的Cache值
        /// </summary>
        /// <param name="CacheKey"></param>
        /// <returns></returns>
        public static object GetCache(string CacheKey)
        {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            return objCache[CacheKey];

        }

        /// <summary>
        /// 设置当前应用程序指定CacheKey的Cache值
        /// </summary>
        /// <param name="CacheKey"></param>
        /// <param name="objObject"></param>
        public static void SetCache(string CacheKey, object objObject)
        {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            objCache.Insert(CacheKey, objObject, null, DateTime.Now.AddMinutes(CommonModelCacheMinutes), System.Web.Caching.Cache.NoSlidingExpiration);
        }

        /// <summary>
        /// 设置当前应用程序指定CacheKey的Cache值
        /// </summary>
        /// <param name="CacheKey"></param>
        /// <param name="objObject"></param>
        public static void SetCache(string CacheKey, object objObject, DateTime absoluteExpiration, TimeSpan slidingExpiration)
        {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            objCache.Insert(CacheKey, objObject, null, absoluteExpiration, slidingExpiration);
        }
        /// <summary>
        /// 从缓存中移除指定CacheKey的缓存值
        /// </summary>
        /// <param name="cacheKey"></param>
        public static void RemoveCache(string cacheKey)
        {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            objCache.Remove(cacheKey);
        }
    }
}
