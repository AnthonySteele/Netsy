//-----------------------------------------------------------------------
// <copyright file="GetListingsByKeywordTest.cs" company="AFS">
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
    /// Test the GetListingsByKeyword Api function
    /// </summary>
    [TestClass]
    public class GetListingsByKeywordTest
    {
        /// <summary>
        /// Test missing API key
        /// </summary>
        [TestMethod]
        public void GetListingsByKeywordApiKeyMissingTest()
        {
            // ARRANGE
            ResultEventArgs<Listings> result = null;
            IListingsService listingsService = new ListingsService(new EtsyContext(string.Empty));
            listingsService.GetListingsByKeywordCompleted += (s, e) => result = e;

            List<string> searchTerms = new List<string>();

            // ACT
            listingsService.GetListingsByKeyword(searchTerms, SortField.Created, SortOrder.Up, null, null, false, 0, 10, DetailLevel.Low);

            // check the data
            TestHelpers.CheckResultFailure(result);
        }

        /// <summary>
        /// Test invalid price ranges
        /// </summary>
        [TestMethod]
        public void GetListingsByKeywordInvalidPriceRangeTest()
        {
            // ARRANGE
            ResultEventArgs<Listings> result = null;
            IListingsService listingsService = new ListingsService(new EtsyContext(NetsyData.EtsyApiKey));
            listingsService.GetListingsByKeywordCompleted += (s, e) => result = e;

            List<string> searchTerms = new List<string>();

            // ACT
            listingsService.GetListingsByKeyword(searchTerms, SortField.Created, SortOrder.Up, 100, 10, false, 0, 10, DetailLevel.Low);

            // check the data
            TestHelpers.CheckResultFailure(result);
        }

        /// <summary>
        /// Test invalid API key
        /// </summary>
        [TestMethod]
        public void GetListingsByKeywordApiKeyInvalidTest()
        {
            // ARRANGE
            using (AutoResetEvent waitEvent = new AutoResetEvent(false))
            {
                ResultEventArgs<Listings> result = null;
                IListingsService listingsService = new ListingsService(new EtsyContext("InvalidKey"));
                listingsService.GetListingsByKeywordCompleted += (s, e) =>
                {
                    result = e;
                    waitEvent.Set();
                };
                List<string> searchTerms = new List<string> { "bags" };

                // ACT
                listingsService.GetListingsByKeyword(searchTerms, SortField.Created, SortOrder.Up, null, null, false, 0, 10, DetailLevel.Low);
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
        public void GetListingsByKeywordCallTest()
        {
            // ARRANGE
            using (AutoResetEvent waitEvent = new AutoResetEvent(false))
            {
                ResultEventArgs<Listings> result = null;
                IListingsService listingsService = new ListingsService(new EtsyContext(NetsyData.EtsyApiKey));
                listingsService.GetListingsByKeywordCompleted += (s, e) =>
                {
                    result = e;
                    waitEvent.Set();
                };

                List<string> searchTerms = new List<string> { "bags" };

                // ACT
                listingsService.GetListingsByKeyword(searchTerms, SortField.Created, SortOrder.Up, null, null, false, 0, 10, DetailLevel.Low);
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
        /// Test success response, sorting by score
        /// </summary>
        [TestMethod]
        public void GetListingsByKeywordSortOnScoreTest()
        {
            // ARRANGE
            using (AutoResetEvent waitEvent = new AutoResetEvent(false))
            {
                ResultEventArgs<Listings> result = null;
                IListingsService listingsService = new ListingsService(new EtsyContext(NetsyData.EtsyApiKey));
                listingsService.GetListingsByKeywordCompleted += (s, e) =>
                {
                    result = e;
                    waitEvent.Set();
                };

                List<string> searchTerms = new List<string> { "bags" };

                // ACT
                listingsService.GetListingsByKeyword(searchTerms, SortField.Score, SortOrder.Up, null, null, false, 0, 10, DetailLevel.Low);
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
        /// Test retrieving listings ny keyword, all detail levels
        /// </summary>
        [TestMethod]
        public void GetListingsByKeywordAllDetailLevelsTest()
        {
            TestGetListingsByKeyword(DetailLevel.Low);
            TestGetListingsByKeyword(DetailLevel.Medium);
            TestGetListingsByKeyword(DetailLevel.High);
        }

        /// <summary>
        /// Test retrieving listings by keyword, at the appropriate detail level
        /// </summary>
        /// <param name="detailLevel">the detail level</param>
        private static void TestGetListingsByKeyword(DetailLevel detailLevel)
        {
            TestHelpers.WaitABit();

            // ARRANGE
            using (AutoResetEvent waitEvent = new AutoResetEvent(false))
            {
                ResultEventArgs<Listings> result = null;
                IListingsService listingsService = new ListingsService(new EtsyContext(NetsyData.EtsyApiKey));
                listingsService.GetListingsByKeywordCompleted += (s, e) =>
                {
                    result = e;
                    waitEvent.Set();
                };

                List<string> searchTerms = new List<string> { "bags" };

                // ACT
                listingsService.GetListingsByKeyword(searchTerms, SortField.Created, SortOrder.Up, null, null, false, 0, 10, detailLevel);
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
