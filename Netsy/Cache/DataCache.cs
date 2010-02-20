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
    using System.Linq;

    /// <summary>
    /// The data cache
    /// </summary>
    public class DataCache : IDataCache
    {
        /// <summary>
        /// How long to keep items - 5 minutes
        /// </summary>
        private readonly TimeSpan Timeout = new TimeSpan(0, 5, 0);

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
            this.ClearTimedOutItems();

            if (this.cacheData.ContainsKey(key))
            {
                CacheItem item = this.cacheData[key];
                item.UpdateLastAccessed();
                return item.Value;
            }

            return null;
        }

        private void ClearTimedOutItems()
        {
            IEnumerable<string> timedOutKeys = cacheData.Where(item => this.ItemHasTimedOut(item.Value))
                .Select(item => item.Key).ToList();

            foreach (string timedOutKey in timedOutKeys)
            {
                this.cacheData.Remove(timedOutKey);
            }
        }

        /// <summary>
        /// Return true if the item has expired, is past the timeout
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private bool ItemHasTimedOut(CacheItem item)
        {
            return item.LastAccessed < DateTime.Now - this.Timeout;
        }
    }
}
