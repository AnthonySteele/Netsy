//-----------------------------------------------------------------------
// <copyright file="GetListingsByColorAndKeywordsTest.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.IntegrationTest.Listings
{
    using System.Collections.Generic;
    using System.Net;
    using System.Threading;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Netsy.DataModel;
    using Netsy.Helpers;
    using Netsy.Interfaces;
    using Netsy.Services;

    /// <summary>
    /// Test the GetListingsByColorTest function on the listings service
    /// </summary>
    [TestClass]
    public class GetListingsByColorAndKeywordsTest
    {
        /// <summary>
        /// Test missing API key
        /// </summary>
        [TestMethod]
        public void GetListingsByColorAndKeywordsApiKeyMissingTest()
        {
            // ARRANGE
            ResultEventArgs<Listings> result = null;
            IListingsService listingsService = new ListingsService(new EtsyContext(string.Empty));
            listingsService.GetListingsByColorAndKeywordsCompleted += (s, e) => result = e;

            RgbColor testColor = new RgbColor("76B3DF");

            // ACT
            listingsService.GetListingsByColorAndKeywords(TestKeywords(), testColor, 10, 0, 10, DetailLevel.Low);

            // check the data
            TestHelpers.CheckResultFailure(result);
        }

        /// <summary>
        /// Test missing API key
        /// </summary>
        [TestMethod]
        public void GetListingsByColorAndKeywordsWiggleTooLargeTest()
        {
            // ARRANGE
            ResultEventArgs<Listings> result = null;
            IListingsService listingsService = new ListingsService(new EtsyContext(string.Empty));
            listingsService.GetListingsByColorAndKeywordsCompleted += (s, e) => result = e;

            RgbColor testColor = new RgbColor("76B3DF");

            // ACT
            listingsService.GetListingsByColorAndKeywords(TestKeywords(), testColor, 100, 0, 10, DetailLevel.Low);

            // check the data
            TestHelpers.CheckResultFailure(result);
        }

        /// <summary>
        /// Test invalid API key
        /// </summary>
        [TestMethod]
        public void GetListingsByColorAndKeywordsApiKeyInvalidTest()
        {
            // ARRANGE
            using (AutoResetEvent waitEvent = new AutoResetEvent(false))
            {
                ResultEventArgs<Listings> result = null;
                IListingsService listingsService = new ListingsService(new EtsyContext("InvalidKey"));
                listingsService.GetListingsByColorAndKeywordsCompleted += (s, e) =>
                {
                    result = e;
                    waitEvent.Set();
                };

                RgbColor testColor = new RgbColor("76B3DF");

                // ACT
                listingsService.GetListingsByColorAndKeywords(TestKeywords(), testColor, 10, 0, 10, DetailLevel.Low);
                bool signalled = waitEvent.WaitOne(NetsyData.WaitTimeout);

                // ASSERT
                // check that the event was fired, did not time out
                Assert.IsTrue(signalled, "Not signalled");

                // check the data
                Assert.IsNotNull(result);

                // check the data - should fail
                Assert.IsNotNull(result);
                Assert.IsNotNull(result.ResultStatus);
                Assert.IsFalse(result.ResultStatus.Success);
                Assert.AreEqual(WebExceptionStatus.ProtocolError, result.ResultStatus.WebStatus);
            }
        }

        /// <summary>
        /// Test success response
        /// </summary>
        [TestMethod]
        public void GetListingsByColorAndKeywordsCallTest()
        {
            // ARRANGE
            using (AutoResetEvent waitEvent = new AutoResetEvent(false))
            {
                ResultEventArgs<Listings> result = null;
                IListingsService listingsService = new ListingsService(new EtsyContext(NetsyData.EtsyApiKey));
                listingsService.GetListingsByColorAndKeywordsCompleted += (s, e) =>
                {
                    result = e;
                    waitEvent.Set();
                };

                RgbColor testColor = new RgbColor("76B3DF");

                // ACT
                listingsService.GetListingsByColorAndKeywords(TestKeywords(), testColor, 10, 0, 10, DetailLevel.Low);
                bool signalled = waitEvent.WaitOne(NetsyData.WaitTimeout);

                // ASSERT
                // check that the event was fired, did not time out
                Assert.IsTrue(signalled, "Not signalled");

                // check the data
                Assert.IsNotNull(result);
                TestHelpers.CheckResultSuccess(result);

                Assert.IsTrue(result.ResultValue.Count > 0, "No items found");
                Assert.IsNotNull(result.ResultValue.Params);
            }
        }

        /// <summary>
        /// Test retrieving listing details, all detail levels
        /// </summary>
        [TestMethod]
        public void GetListingsByColorAndKeywordsAllDetailLevelsTest()
        {
            TestGetListings(DetailLevel.Low);
            TestGetListings(DetailLevel.Medium);
            TestGetListings(DetailLevel.High);
        }

        /// <summary>
        /// Test retrieving listing details at the given detail level
        /// </summary>
        /// <param name="detailLevel">the given detail level</param>
        private static void TestGetListings(DetailLevel detailLevel)
        {
            TestHelpers.WaitABit();

            // ARRANGE
            using (AutoResetEvent waitEvent = new AutoResetEvent(false))
            {
                ResultEventArgs<Listings> result = null;
                IListingsService listingsService = new ListingsService(new EtsyContext(NetsyData.EtsyApiKey));
                listingsService.GetListingsByColorAndKeywordsCompleted += (s, e) =>
                {
                    result = e;
                    waitEvent.Set();
                };

                RgbColor testColor = new RgbColor("76B3DF");

                // ACT
                listingsService.GetListingsByColorAndKeywords(TestKeywords(), testColor, 10, 0, 10, detailLevel);
                bool signalled = waitEvent.WaitOne(NetsyData.WaitTimeout);

                // ASSERT
                // check that the event was fired, did not time out
                Assert.IsTrue(signalled, "Not signalled");

                // check the data
                Assert.IsNotNull(result);
                TestHelpers.CheckResultSuccess(result);

                Assert.IsTrue(result.ResultValue.Count > 1);
                Assert.IsNotNull(result.ResultValue.Params);
            }
        }

        /// <summary>
        /// Get test keywords
        /// </summary>
        /// <returns>a list of test keywords</returns>
        private static IList<string> TestKeywords()
        {
            return new List<string> { "bags", "strap" };
        }
    }
}
