//-----------------------------------------------------------------------
// <copyright file="DataCache.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 
namespace Netsy.Cache
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The data cache
    /// </summary>
    public class DataCache : IDataCache
    {
        /// <summary>
        /// How long to keep items
        /// </summary>
        private readonly TimeSpan Timeout = new TimeSpan(0, 10, 0);

        /// <summary>
        /// The cached data
        /// </summary>
        private readonly Dictionary<string, CacheItem> cacheData = new Dictionary<string, CacheItem>();

        /// <summary>
        /// Write a value to the cache
        /// </summary>
        /// <param name="key">the cache key</param>
        /// <param name="data">the value cached</param>
        public void Write(string key, object data)
        {
            if (this.cacheData.ContainsKey(key))
            {
                this.cacheData[key].Value = data;
            }
            else
            {
                CacheItem newCacheItem = new CacheItem(key, data);
                this.cacheData.Add(key, newCacheItem);
            }
        }
        
        /// <summary>
        /// Read a value from the cache
        /// </summary>
        /// <param name="key">the cache key</param>
        /// <returns>the value read</returns>
        public object Read(string key)
        {
            if (this.cacheData.ContainsKey(key))
            {
                CacheItem item = this.cacheData[key];

                if (item.LastAccessed < DateTime.Now - this.Timeout)
                {
                    this.cacheData.Remove(key);
                    return null;
                }

                item.UpdateLastAccessed();
                return item.Value;
            }

            return null;
        }
    }
}
