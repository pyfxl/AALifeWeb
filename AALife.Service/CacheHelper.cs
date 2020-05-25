using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AALife.Service
{
    /// <summary>
    /// Represents a manager for caching between HTTP requests (long term caching)
    /// </summary>
    public static class CacheHelper
    {
        /// <summary>
        /// Cache object
        /// </summary>
        static ObjectCache Cache
        {
            get
            {
                return MemoryCache.Default;
            }
        }

        public static IEnumerable<string> GetCacheKeys()
        {
            var list = new List<string>();
            foreach (var item in MemoryCache.Default)
            {
                //add the item.keys to list
                list.Add(item.Key);
            }
            return list;
        }

        /// <summary>
        /// Gets or sets the value associated with the specified key.
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="key">The key of the value to get.</param>
        /// <returns>The value associated with the specified key.</returns>
        public static T Get<T>(string key)
        {
            return (T)Cache[key];
        }

        /// <summary>
        /// Get a cached item. If it's not in the cache yet, then load and cache it
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="cacheManager">Cache manager</param>
        /// <param name="key">Cache key</param>
        /// <param name="acquire">Function to load item if it's not in the cache yet</param>
        /// <returns>Cached item</returns>
        public static T Get<T>(string key, Func<T> acquire)
        {
            return Get(key, 60, acquire);
        }

        /// <summary>
        /// Get a cached item. If it's not in the cache yet, then load and cache it
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="cacheManager">Cache manager</param>
        /// <param name="key">Cache key</param>
        /// <param name="cacheTime">Cache time in minutes (0 - do not cache)</param>
        /// <param name="acquire">Function to load item if it's not in the cache yet</param>
        /// <returns>Cached item</returns>
        public static T Get<T>(string key, int cacheTime, Func<T> acquire)
        {
            if (IsSet(key))
            {
                return Get<T>(key);
            }

            var result = acquire();
            if (cacheTime > 0)
                Set(key, result, cacheTime);
            return result;
        }

        /// <summary>
        /// Adds the specified key and object to the cache.
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="data">Data</param>
        /// <param name="cacheTime">Cache time</param>
        public static void Set(string key, object data, int cacheTime)
        {
            if (data == null)
                return;

            var policy = new CacheItemPolicy();
            policy.AbsoluteExpiration = DateTime.Now + TimeSpan.FromMinutes(cacheTime);
            Cache.Add(new CacheItem(key, data), policy);
        }

        /// <summary>
        /// Gets a value indicating whether the value associated with the specified key is cached
        /// </summary>
        /// <param name="key">key</param>
        /// <returns>Result</returns>
        public static bool IsSet(string key)
        {
            return (Cache.Contains(key));
        }

        /// <summary>
        /// Removes the value with the specified key from the cache
        /// </summary>
        /// <param name="key">/key</param>
        public static void Remove(string key)
        {
            Cache.Remove(key);
        }

        /// <summary>
        /// Removes items by pattern
        /// </summary>
        /// <param name="pattern">pattern</param>
        public static void RemoveByPattern(string pattern)
        {
            //this.RemoveByPattern(pattern, Cache.Select(p => p.Key));
            var keys = Cache.Select(p => p.Key);
            var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            foreach (var key in keys.Where(p => regex.IsMatch(p.ToString())).ToList())
                Remove(key);
        }

        /// <summary>
        /// Clear all cache data
        /// </summary>
        public static void Clear()
        {
            foreach (var item in Cache)
                Remove(item.Key);
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public static void Dispose()
        {
        }
    }

}
