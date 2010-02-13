//-----------------------------------------------------------------------
// <copyright file="GetFeaturedDetailsTest.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.IntegrationTest.Shop
{
    using System.Net;
    using System.Threading;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Netsy.DataModel;
    using Netsy.Helpers;
    using Netsy.Interfaces;
    using Netsy.Services;

    /// <summary>
    /// Test the GetFeaturedDetails function on the shop service
    /// </summary>
    [TestClass]
    public class GetFeaturedDetailsTest
    {
        /// <summary>
        /// Test missing API key
        /// </summary>
        [TestMethod]
        public void GetFeaturedSellersMissingApiKeyTest()
        {
            // ARRANGE
            ResultEventArgs<Listings> result = null;
            IShopService shopsService = new ShopService(new EtsyContext(string.Empty), new NullDataCache());
            shopsService.GetFeaturedDetailsCompleted += (s, e) => result = e;

            // ACT
            shopsService.GetFeaturedDetails(NetsyData.TestUserId, DetailLevel.Low);

            // check the data
            TestHelpers.CheckResultFailure(result);
        }

        /// <summary>
        /// Test invalid API key
        /// </summary>
        [TestMethod]
        public void GetFeaturedDetailsApiKeyInvalidTest()
        {
            // ARRANGE
            using (AutoResetEvent waitEvent = new AutoResetEvent(false))
            {
                ResultEventArgs<Listings> result = null;
                IShopService shopsService = new ShopService(new EtsyContext("InvalidKey"), new NullDataCache());
                shopsService.GetFeaturedDetailsCompleted += (s, e) =>
                    {
                        result = e;
                        waitEvent.Set();
                    };

                // ACT
                shopsService.GetFeaturedDetails(NetsyData.TestUserId, DetailLevel.Low);
                bool signalled = waitEvent.WaitOne(NetsyData.WaitTimeout);

                // ASSERT
                // check that the event was fired, did not time out
                Assert.IsTrue(signalled, "Not signalled");

                // check the data - should fail
                Assert.IsNotNull(result);
                Assert.IsNotNull(result.ResultStatus);
                Assert.IsFalse(result.ResultStatus.Success);
                Assert.AreEqual(WebExceptionStatus.ProtocolError, result.ResultStatus.WebStatus);
            }
        }

        /// <summary>
        /// Test searching for etsy shops by name
        /// </summary>
        [TestMethod]
        public void GetFeaturedDetailsLowDetailTest()
        {
            // ARANGE
            using (AutoResetEvent waitEvent = new AutoResetEvent(false))
            {
                ResultEventArgs<Listings> result = null;

                IShopService shopsService = new ShopService(new EtsyContext(NetsyData.EtsyApiKey), new NullDataCache());
                shopsService.GetFeaturedDetailsCompleted += (s, e) =>
                {
                    result = e;
                    waitEvent.Set();
                };

                // ACT
                shopsService.GetFeaturedDetails(NetsyData.TestUserId, DetailLevel.Low);
                bool signalled = waitEvent.WaitOne(NetsyData.WaitTimeout);

                // ASSERT
                // check that the event was fired, did not time out
                Assert.IsTrue(signalled, "Not signalled");

                // check the data
                TestHelpers.CheckResultSuccess(result);

                Assert.IsNotNull(result.ResultValue.Results, "Results");
                Assert.IsTrue(result.ResultStatus.Success, "Success");
                Assert.IsTrue(result.ResultValue.Count > 0, "Count");
            }
        }

        /// <summary>
        /// Test retrieving featured details, all detail levels
        /// </summary>
        [TestMethod]
        public void GetFeaturedDetailsAllDetailLevelsTest()
        {
            TestGetFeaturedDetails(DetailLevel.Low);
            TestGetFeaturedDetails(DetailLevel.Medium);
            TestGetFeaturedDetails(DetailLevel.High);
        }

        /// <summary>
        /// Test retrieving featured details at the given detail level
        /// </summary>
        /// <param name="detailLevel">the given detail level</param>
        private static void TestGetFeaturedDetails(DetailLevel detailLevel)
        {
            // ARANGE
            using (AutoResetEvent waitEvent = new AutoResetEvent(false))
            {
                ResultEventArgs<Listings> result = null;

                IShopService shopsService = new ShopService(new EtsyContext(NetsyData.EtsyApiKey), new NullDataCache());
                shopsService.GetFeaturedDetailsCompleted += (s, e) =>
                {
                    result = e;
                    waitEvent.Set();
                };

                // ACT
                shopsService.GetFeaturedDetails(NetsyData.TestUserId, detailLevel);
                bool signalled = waitEvent.WaitOne(NetsyData.WaitTimeout);

                // ASSERT
                // check that the event was fired, did not time out
                Assert.IsTrue(signalled, "Not signalled");

                // check the data
                TestHelpers.CheckResultSuccess(result);

                Assert.IsNotNull(result.ResultValue.Results, "Results");
                Assert.IsTrue(result.ResultStatus.Success, "Success");
                Assert.IsTrue(result.ResultValue.Count > 0, "Count");
            }
        }
    }
}
