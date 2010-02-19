//-----------------------------------------------------------------------
// <copyright file="DataRetrieverTest.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.IntegrationTest.Requests
{
    using System;
    using System.Threading;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Netsy.Cache;
    using Netsy.DataModel;
    using Netsy.Helpers;
    using Netsy.Requests;
    using Netsy.Test;

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

        /// <summary>
        /// Test retrieval from a url
        /// </summary>
        [TestMethod]
        public void DataRetrieverRetrieveTest()
        {
            const string TestUri = "http://beta-api.etsy.com/v1/listings/featured/front?offset=0&limit=10&detail_level=low&api_key=" +
                NetsyData.EtsyApiKey;

            IDataCache dataCache = new NullDataCache();
            IRequestGenerator requestGenerator = new WebRequestGenerator();

            ResultEventArgs<Listings> resultEventArgs = null;

            using (AutoResetEvent waitEvent = new AutoResetEvent(false))
            {
                DataRetriever dataRetriever = new DataRetriever(dataCache, requestGenerator);

                EventHandler<ResultEventArgs<Listings>> completedHandler = (sender, res) =>
                     {
                         resultEventArgs = res;
                         waitEvent.Set();
                     };

                dataRetriever.StartRetrieve<Listings>(new Uri(TestUri), completedHandler);
                bool signalled = waitEvent.WaitOne(NetsyData.WaitTimeout);

                Assert.IsTrue(signalled);
            }

            Assert.IsNotNull(resultEventArgs);
            Assert.IsTrue(resultEventArgs.ResultStatus.Success);
        }
    }
}