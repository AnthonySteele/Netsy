//-----------------------------------------------------------------------
// <copyright file="IDataCache.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.Cache
{
    /// <summary>
    /// Interface to a data cache
    /// </summary>
    public interface IDataCache
    {
        /// <summary>
        /// Write a value to the cache
        /// </summary>
        /// <param name="key">the cache key</param>
        /// <param name="data">the value cached</param>
        void Write(string key, object data);

        /// <summary>
        /// Read a value from the cache
        /// </summary>
        /// <param name="key">the cache key</param>
        /// <returns>the value read</returns>
        object Read(string key);
    }
}