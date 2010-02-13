//-----------------------------------------------------------------------
// <copyright file="DataCacheTest.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.Test.Cache
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Netsy.Cache;

    /// <summary>
    /// Test the DataCache class
    /// </summary>
    [TestClass]
    public class DataCacheTest
    {
        /// <summary>
        /// Test that the cache can be created
        /// </summary>
        [TestMethod]
        public void CreateTest()
        {
            DataCache dataCache = new DataCache();

            Assert.IsNotNull(dataCache);
        }

        /// <summary>
        /// Test that when the key is not found, null is returned
        /// </summary>
        [TestMethod]
        public void UnknownKeyReturnsNullTest()
        {
            DataCache dataCache = new DataCache();
            object value = dataCache.Read("foo");

            Assert.IsNull(value);
        }

        /// <summary>
        /// Test that when the key is known, the value is returned
        /// </summary>
        [TestMethod]
        public void KnownKeyReturnsValueTest()
        {
            DataCache dataCache = new DataCache();
            dataCache.Write("foo", "bar");
            object value = dataCache.Read("foo");

            Assert.IsNotNull(value);
            Assert.AreEqual("bar", value.ToString());
        }


        /// <summary>
        /// Test that the key can be written more than once
        /// And the latest value is retrieved
        /// </summary>
        [TestMethod]
        public void CanWriteTwiceTest()
        {
            DataCache dataCache = new DataCache();
            dataCache.Write("foo", "bar");
            dataCache.Write("foo", "fish");
            object value = dataCache.Read("foo");

            Assert.IsNotNull(value);
            Assert.AreEqual("fish", value.ToString());
        }
    }
}
