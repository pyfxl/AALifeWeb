using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Caching;

/// <summary>
/// CacheHelper 的摘要说明
/// </summary>
public class CacheHelper
{
	public CacheHelper()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

    /// <summary>
    /// 建立缓存
    /// </summary>
    public static object AddCache(string key, object value, CacheItemPriority priority)
    {
        if (HttpRuntime.Cache[key] == null && value != null)
            return HttpRuntime.Cache.Add(key, value, null, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, priority, null);
        else
            return null;
    }

    /// <summary>
    /// 建立定时不访问便移除的缓存
    /// </summary>
    public static object AddCache(string key, object value, TimeSpan slidingExpiration, CacheItemPriority priority)
    {
        if (HttpRuntime.Cache[key] == null && value != null)
            return HttpRuntime.Cache.Add(key, value, null, Cache.NoAbsoluteExpiration, slidingExpiration, priority, null);
        else
            return null;
    }

    /// <summary>
    /// 建立缓存，并在移除时执行事件
    /// </summary>
    public static object AddCache(string key, object value, DateTime absoluteExpiration, TimeSpan slidingExpiration, CacheItemPriority priority, CacheItemRemovedCallback onRemovedCallback)
    {
        if (HttpRuntime.Cache[key] == null && value != null)
            return HttpRuntime.Cache.Add(key, value, null, absoluteExpiration, slidingExpiration, priority, onRemovedCallback);
        else
            return null;
    }
    
    /// <summary>
    /// 移除缓存
    /// </summary>
    public static object RemoveCache(string key)
    {
        if (HttpRuntime.Cache[key] != null)
            return HttpRuntime.Cache.Remove(key);
        else
            return null;
    }

    /// <summary>
    /// 移除键中带某关键字的缓存
    /// </summary>
    public static void RemoveMultiCache(string keyInclude)
    {
        IDictionaryEnumerator CacheEnum = HttpRuntime.Cache.GetEnumerator();
        while (CacheEnum.MoveNext())
        {
            if (CacheEnum.Key.ToString().IndexOf(keyInclude.ToString()) >= 0)
                HttpRuntime.Cache.Remove(CacheEnum.Key.ToString());
        }
    }

    /// <summary>
    /// 移除所有缓存
    /// </summary>
    public static void RemoveAllCache()
    {
        IDictionaryEnumerator CacheEnum = HttpRuntime.Cache.GetEnumerator();
        while (CacheEnum.MoveNext())
        {
            HttpRuntime.Cache.Remove(CacheEnum.Key.ToString());
        }
    }

    /// <summary>
    /// 取缓存
    /// </summary>
    public static DataTable GetFromCache(string key, object value)
    {
        if (HttpContext.Current.Cache[key] != null)
        {
            return (DataTable)HttpContext.Current.Cache[key];
        }
        else
        {
            AddCache(key, value, CacheItemPriority.Normal);
            return (DataTable)value;
        }
    }


}