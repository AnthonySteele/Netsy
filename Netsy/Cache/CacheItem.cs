//-----------------------------------------------------------------------
// <copyright file="CacheItem.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.Cache
{
    using System;

    /// <summary>
    /// An item in the cache
    /// </summary>
    public class CacheItem
    {
        /// <summary>
        /// Initializes a new instance of the CacheItem class
        /// </summary>
        /// <param name="key">the cache key</param>
        /// <param name="value">the value to cache</param>
        public CacheItem(string key, object value)
        {
            this.Key = key;
            this.Value  = value;
            this.LastAccessed = DateTime.Now;
        }

        /// <summary>
        /// Gets the cache key
        /// </summary>
        public string Key { get; private set; }

        /// <summary>
        /// Gets or sets the value cached
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// Gets the date that the item was last accessed
        /// </summary>
        public DateTime LastAccessed { get; private set; }

        /// <summary>
        /// Refresh the last accessed time
        /// </summary>
        public void UpdateLastAccessed()
        {
            this.LastAccessed = DateTime.Now;
        }
    }
}
