//-----------------------------------------------------------------------
// <copyright file="DataRetrieverTest.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.Test.Requests
{
    using System;
    using System.Threading;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Netsy.Cache;
    using Netsy.DataModel;
    using Netsy.Helpers;
    using Netsy.Requests;

    /// <summary>
    /// Test the DataRetriever class using mocks
    /// </summary>
    [TestClass]
    public class DataRetrieverTest
    {
        /// <summary>
        /// Timeout for waiting fora thread. Set low since this uses mocks not actual connections
        /// </summary>
        private const int WaitTimeout = 500;

        /// <summary>
        /// Not actually used by the mocks
        /// </summary>
        private const string TestUri = "http://example.com";

        /// <summary>
        /// Re-retrieved response data for the mock to return
        /// </summary>
        private const string CannedResponse = @"{""count"":50000,""results"":[{""listing_id"":17738128,""state"":""active"",""title"":""Honeycomb"",""url"":""http:\/\/www.etsy.com\/view_listing.php?listing_id=17738128"",""image_url_25x25"":""http:\/\/ny-image0.etsy.com\/il_25x25.46283840.jpg"",""image_url_50x50"":""http:\/\/ny-image0.etsy.com\/il_50x50.46283840.jpg"",""image_url_75x75"":""http:\/\/ny-image0.etsy.com\/il_75x75.46283840.jpg"",""image_url_155x125"":""http:\/\/ny-image0.etsy.com\/il_155x125.46283840.jpg"",""image_url_200x200"":""http:\/\/ny-image0.etsy.com\/il_200x200.46283840.jpg"",""image_url_430xN"":""http:\/\/ny-image0.etsy.com\/il_430xN.46283840.jpg"",""creation_epoch"":1259352433.8,""user_id"":6245787,""user_name"":""emilyryan""}],""params"":{""offset"":0,""limit"":1,""detail_level"":""low""},""type"":""listing""}";

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
            IRequestGenerator requestGenerator = new MockFixedDataRequestGenerator(string.Empty);

            DataRetriever dataRetriever = new DataRetriever(dataCache, requestGenerator);
            Assert.IsNotNull(dataRetriever);
        }

        /// <summary>
        /// Test retrieval from a url - success
        /// </summary>
        [TestMethod]
        public void DataRetrieverRetrieveSuccessTest()
        {
            IDataCache dataCache = new NullDataCache();
            IRequestGenerator requestGenerator = new MockFixedDataRequestGenerator(CannedResponse);

            ResultEventArgs<Listings> resultEventArgs = null;

            using (AutoResetEvent waitEvent = new AutoResetEvent(false))
            {
                DataRetriever dataRetriever = new DataRetriever(dataCache, requestGenerator);

                EventHandler<ResultEventArgs<Listings>> completedHandler = (sender, res) =>
                     {
                         resultEventArgs = res;
                         waitEvent.Set();
                     };

                dataRetriever.StartRetrieve(new Uri(TestUri), completedHandler);
                bool signalled = waitEvent.WaitOne(WaitTimeout);

                Assert.IsTrue(signalled);
            }

            Assert.IsNotNull(resultEventArgs);
            Assert.IsTrue(resultEventArgs.ResultStatus.Success);
        }

        /// <summary>
        /// Test retrieval from a url - failure
        /// </summary>
        [TestMethod]
        public void DataRetrieverRetrieveFailTest()
        {
            IDataCache dataCache = new NullDataCache();
            IRequestGenerator requestGenerator = new MockFailingRequestGenerator();

            ResultEventArgs<Listings> resultEventArgs = null;

            using (AutoResetEvent waitEvent = new AutoResetEvent(false))
            {
                DataRetriever dataRetriever = new DataRetriever(dataCache, requestGenerator);

                EventHandler<ResultEventArgs<Listings>> completedHandler = (sender, res) =>
                {
                    resultEventArgs = res;
                    waitEvent.Set();
                };

                dataRetriever.StartRetrieve(new Uri(TestUri + "bad"), completedHandler);
                bool signalled = waitEvent.WaitOne(WaitTimeout);

                Assert.IsTrue(signalled);
            }

            Assert.IsNotNull(resultEventArgs);
            Assert.IsFalse(resultEventArgs.ResultStatus.Success);
        }
    }
}