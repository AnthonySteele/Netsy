//-----------------------------------------------------------------------
// <copyright file="NullDataCache.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 


namespace Netsy.Test
{
    using Netsy.Cache;

    /// <summary>
    /// A data cache that does not cache anything. For test.
    /// </summary>
    public class NullDataCache : IDataCache
    {
        /// <summary>
        /// Write to the cache
        /// </summary>
        /// <param name="key">the cache key</param>
        /// <param name="data">the value to write</param>
        public void Write(string key, object data)
        {
            // do nothing
        }

        /// <summary>
        /// Read from the cache
        /// </summary>
        /// <param name="key">the cache key</param>
        /// <returns>null since a vlaue is not found</returns>
        public object Read(string key)
        {
            return null;
        }
    }
}


