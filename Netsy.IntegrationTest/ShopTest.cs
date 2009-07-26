//-----------------------------------------------------------------------
// <copyright file="ShopTest.cs" company="AFS">
// Copyright (c) AFS. All rights reserved.
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.IntegrationTest
{
    using System.Threading;

    using DataModel.ShopData;

    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Netsy.Core;
    using Netsy.DataModel;
    using Netsy.DataModel.UserData;
    using Netsy.Helpers;
    using Netsy.Interfaces;

    /// <summary>
    /// Test etsy shop retrieval
    /// </summary>
    [TestClass]
    public class ShopTest
    {
        /// <summary>
        /// how long to wait before timing out - 100 seconds
        /// </summary>
        private const int WaitTimeout = 100000;

        /// <summary>
        /// The API key to use for testing
        /// </summary>
        private const string EtsyApiKey = "rfc35bh98q3a9hvccfsxe4cc";

        /// <summary>
        /// the use if to test on
        /// </summary>
        private const int TestUserId = 7394192;

        /// <summary>
        /// Synchronisation object to wait until the shop details get completes
        /// </summary>
        private AutoResetEvent shopDetailsGetCompletedEvent;


        /// <summary>
        /// Result data from the call to get shops by id
        /// </summary>
        private Shops shopsResultData;


        /// <summary>
        /// Result status from the call to get shops by id
        /// </summary>
        private ResultStatus shopsResultStatus;

        /// <summary>
        /// Test retrieving etsy shops by id
        /// </summary>
        [TestMethod]
        public void ShopLowDetailRetrievalTest()
        {
            this.shopDetailsGetCompletedEvent = new AutoResetEvent(false);
            try
            {
                IShopService shopsService = new ShopService(EtsyApiKey);

                this.shopsResultData = null;
                shopsService.GetShopDetailsCompleted += this.GetShopDetailsCompleted;
                shopsService.GetShopDetails(TestUserId, DetailLevel.Low);

                // wait for up to 20 seconds for it to complete
                bool signalled = this.shopDetailsGetCompletedEvent.WaitOne(WaitTimeout);

                // check that the event was fired, did not time out
                Assert.IsTrue(signalled, "Not signalled");

                Assert.IsNotNull(this.shopsResultStatus);
                Assert.IsTrue(this.shopsResultStatus.Success, "Call failed");
                Assert.IsNotNull(this.shopsResultData);
                Assert.IsNotNull(this.shopsResultData.Params);
                Assert.IsNotNull(this.shopsResultData.Results);
                Assert.AreEqual(1, this.shopsResultData.Count);
            }
            finally
            {
                this.shopDetailsGetCompletedEvent = null;
            }
        }

        
        /// <summary>
        /// Test searching for etsy shops by name
        /// </summary>
        [TestMethod]
        public void ShopSearchLowDetailRetrievalTest()
        {
            using (AutoResetEvent shopSearchEvent = new AutoResetEvent(false))
            {
                ResultEventArgs<Shops> result = null;

                IShopService shopsService = new ShopService(EtsyApiKey);

                shopsService.GetShopsByNameCompleted += (s, e) =>
                    {
                        result = e;
                        shopSearchEvent.Set();
                    };
                shopsService.GetShopsByName("fred", SortOrder.Up, 0, 10, DetailLevel.Low);

                // wait for up to 20 seconds for it to complete
                bool signalled = shopSearchEvent.WaitOne(WaitTimeout);

                // check that the event was fired, did not time out
                Assert.IsTrue(signalled, "Not signalled");     
            
                Assert.IsNotNull(result);
                Assert.IsNotNull(result.ResultStatus);
                Assert.IsNotNull(result.ResultValue);
                Assert.IsNotNull(result.ResultValue.Results);
                Assert.IsTrue(result.ResultStatus.Success);
                Assert.IsTrue(result.ResultValue.Count > 0);
            }
        }

        /// <summary>
        /// User details by Id Retrieve completed
        /// </summary>
        /// <param name="sender">the event sender</param>
        /// <param name="e">the event params</param>
        private void GetShopDetailsCompleted(object sender, ResultEventArgs<Shops> e)
        {
            this.shopsResultData = e.ResultValue;
            this.shopsResultStatus = e.ResultStatus;

            // signal that the data is retrieved, ready for testing
            this.shopDetailsGetCompletedEvent.Set();
        }
    }
}
