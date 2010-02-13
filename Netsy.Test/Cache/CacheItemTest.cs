//-----------------------------------------------------------------------
// <copyright file="CacheItemTest.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.Test.Cache
{
    using System;
    using System.Threading;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Netsy.Cache;

    /// <summary>
    /// Test creating cache items
    /// </summary>
    [TestClass]
    public class CacheItemTest
    {
        /// <summary>
        /// Tets that the cache item can be created
        /// </summary>
        [TestMethod]
        public void CreateTest()
        {
            CacheItem cacheItem = new CacheItem("key", 3);

            Assert.AreEqual("key", cacheItem.Key, "Key");
            Assert.AreEqual(3, (int)cacheItem.Value, "value");
            Assert.IsTrue(cacheItem.LastAccessed <= DateTime.Now, "Now");
            Assert.IsTrue(cacheItem.LastAccessed > DateTime.Today, "Today");
        }

        /// <summary>
        /// Test that the value can be created
        /// </summary>
        [TestMethod]
        public void UpdateValueTest()
        {
            CacheItem cacheItem = new CacheItem("key", 3);

            Assert.AreEqual(3, (int)cacheItem.Value, "value");

            cacheItem.Value = "hello";
            Assert.AreEqual("hello", (string)cacheItem.Value, "value");
        }

        /// <summary>
        /// Test that the lkast access time can be updated
        /// </summary>
        [TestMethod]
        public void UpdateLastAccessedTest()
        {
            CacheItem cacheItem = new CacheItem("key", 3);

            DateTime cacheItemCreated = cacheItem.LastAccessed;

            Thread.Sleep(100);
            cacheItem.UpdateLastAccessed();
            Assert.IsTrue(cacheItem.LastAccessed > cacheItemCreated);
        }
    }
}
