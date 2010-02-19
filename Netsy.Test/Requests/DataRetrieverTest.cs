//-----------------------------------------------------------------------
// <copyright file="DataRetrieverTest.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.Test.Requests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Netsy.Cache;
    using Netsy.Requests;

    /// <summary>
    /// Test the DataRetriever class
    /// </summary>
    [TestClass]
    public class DataRetrieverTest
    {
        /// <summary>
        /// Test simple creation
        /// </summary>
        [TestMethod]
        public void DataRetrieverSimpleCreateTest()
        {
            DataRetriever dataRetriever = new DataRetriever();
            Assert.IsNotNull(dataRetriever);
        }

        /// <summary>
        /// Test creation with parameters
        /// </summary>
        [TestMethod]
        public void DataRetrieverCreateWithParamsTest()
        {
            IDataCache dataCache = new NullDataCache();
            IRequestGenerator requestGenerator = new WebRequestGenerator();

            DataRetriever dataRetriever = new DataRetriever(dataCache, requestGenerator);
            Assert.IsNotNull(dataRetriever);
        }
    }
}
