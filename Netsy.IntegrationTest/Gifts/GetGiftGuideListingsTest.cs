//-----------------------------------------------------------------------
// <copyright file="GetGiftGuideListingsTest.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 
namespace Netsy.IntegrationTest.Gifts
{
    using System.Net;
    using System.Threading;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Netsy.DataModel;
    using Netsy.Helpers;
    using Netsy.Interfaces;
    using Netsy.Services;

    /// <summary>
    /// Test the GetGiftGuideListings API Function
    /// </summary>
    [TestClass]
    public class GetGiftGuideListingsTest
    {
        /// <summary>
        /// Test missing API key
        /// </summary>
        [TestMethod]
        public void GetGiftGuidesMissingApiKeyTest()
        {
            // ARRANGE
            ResultEventArgs<Listings> result = null;
            IGiftService giftService = new GiftService(new EtsyContext(string.Empty), new NullDataCache());
            giftService.GetGiftGuideListingsCompleted += (s, e) => result = e;

            // ACT
            giftService.GetGiftGuideListings(NetsyData.TestGiftGuideId, 0, 10, DetailLevel.Low);

            // check the data
            TestHelpers.CheckResultFailure(result);
        }

        /// <summary>
        /// Test invalid API key
        /// </summary>
        [TestMethod]
        public void GetGiftGuidesApiKeyInvalidTest()
        {
            // ARRANGE
            using (AutoResetEvent waitEvent = new AutoResetEvent(false))
            {
                ResultEventArgs<Listings> result = null;
                IGiftService giftService = new GiftService(new EtsyContext("InvalidKey"), new NullDataCache());
                giftService.GetGiftGuideListingsCompleted += (s, e) =>
                    {
                        result = e;
                        waitEvent.Set();
                    };

                // ACT
                giftService.GetGiftGuideListings(NetsyData.TestGiftGuideId, 0, 10, DetailLevel.Low);
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
        /// Test gift guide retrieval
        /// </summary>
        [TestMethod]
        public void GetGiftGuideListingsBadGuideIdTest()
        {
            // ARRANGE
            using (AutoResetEvent waitEvent = new AutoResetEvent(false))
            {
                ResultEventArgs<Listings> result = null;
                IGiftService giftService = new GiftService(new EtsyContext(NetsyData.EtsyApiKey), new NullDataCache());
                giftService.GetGiftGuideListingsCompleted += (s, e) =>
                {
                    result = e;
                    waitEvent.Set();
                };

                // ACT
                giftService.GetGiftGuideListings(NetsyData.TestBadGiftGuideId, 0, 10, DetailLevel.Low);
                bool signalled = waitEvent.WaitOne(NetsyData.WaitTimeout);

                // ASSERT
                // check that the event was fired, did not time out
                Assert.IsTrue(signalled, "Not signalled");

                // check the data - should suceed without results
                Assert.IsNotNull(result);
                Assert.IsNotNull(result.ResultStatus);
                Assert.IsTrue(result.ResultStatus.Success);
                Assert.AreEqual(0, result.ResultValue.Count);
            }
        }

        /// <summary>
        /// Test gift guide retrieval
        /// </summary>
        [TestMethod]
        public void GetGiftGuideListingsGetTest()
        {
            // ARRANGE
            using (AutoResetEvent waitEvent = new AutoResetEvent(false))
            {
                ResultEventArgs<Listings> result = null;
                IGiftService giftService = new GiftService(new EtsyContext(NetsyData.EtsyApiKey), new NullDataCache());
                giftService.GetGiftGuideListingsCompleted += (s, e) =>
                {
                    result = e;
                    waitEvent.Set();
                };

                // ACT
                giftService.GetGiftGuideListings(NetsyData.TestGiftGuideId, 0, 10, DetailLevel.Low);
                bool signalled = waitEvent.WaitOne(NetsyData.WaitTimeout);

                // ASSERT
                // check that the event was fired, did not time out
                Assert.IsTrue(signalled, "Not signalled");

                // check the data
                Assert.IsNotNull(result);
                TestHelpers.CheckResultSuccess(result);

                Assert.IsTrue(result.ResultValue.Count > 1);
                Assert.AreEqual(10, result.ResultValue.Results.Length);
                Assert.IsNotNull(result.ResultValue.Params);
            }
        }

        /// <summary>
        /// Test retrieving shop details, all detail levels
        /// </summary>
        [TestMethod]
        public void GetGiftGuideListingsAllDetailLevelsTest()
        {
            TestGetGiftGuideListings(DetailLevel.Low);
            TestGetGiftGuideListings(DetailLevel.Medium);
            TestGetGiftGuideListings(DetailLevel.High);
        }

        /// <summary>
        /// Test gift guide retrieval at the given detail level
        /// </summary>
        /// <param name="detailLevel">the detail level</param>
        private static void TestGetGiftGuideListings(DetailLevel detailLevel)
        {
            // ARRANGE
            using (AutoResetEvent waitEvent = new AutoResetEvent(false))
            {
                ResultEventArgs<Listings> result = null;
                IGiftService giftService = new GiftService(new EtsyContext(NetsyData.EtsyApiKey), new NullDataCache());
                giftService.GetGiftGuideListingsCompleted += (s, e) =>
                {
                    result = e;
                    waitEvent.Set();
                };

                // ACT
                giftService.GetGiftGuideListings(NetsyData.TestGiftGuideId, 0, 10, detailLevel);
                bool signalled = waitEvent.WaitOne(NetsyData.WaitTimeout);

                // ASSERT
                // check that the event was fired, did not time out
                Assert.IsTrue(signalled, "Not signalled");

                // check the data
                Assert.IsNotNull(result);
                TestHelpers.CheckResultSuccess(result);

                Assert.IsTrue(result.ResultValue.Count > 1);
                Assert.AreEqual(10, result.ResultValue.Results.Length);
                Assert.IsNotNull(result.ResultValue.Params);
            }
        }
    }
}
