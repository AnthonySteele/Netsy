//-----------------------------------------------------------------------
// <copyright file="GetListingsByTagsTest.cs" company="AFS">
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
    using Netsy.Test;

    /// <summary>
    /// Test the GetListingsByTags Api function
    /// </summary>
    [TestClass]
    public class GetListingsByTagsTest
    {
        /// <summary>
        /// Test missing API key
        /// </summary>
        [TestMethod]
        public void GetListingsByTagsApiKeyMissingTest()
        {
            // ARRANGE
            ResultEventArgs<Listings> result = null;
            IListingsService listingsService = new ListingsService(new EtsyContext(string.Empty));
            listingsService.GetListingsByTagsCompleted += (s, e) => result = e;

            List<string> tags = new List<string>();

            // ACT
            listingsService.GetListingsByTags(tags, SortField.Created, SortOrder.Up, 0, 10, DetailLevel.Low);

            // check the data
            TestHelpers.CheckResultFailure(result);
        }

        /// <summary>
        /// Test invalid API key
        /// </summary>
        [TestMethod]
        public void GetListingsByTagsApiKeyInvalidTest()
        {
            // ARRANGE
            using (AutoResetEvent waitEvent = new AutoResetEvent(false))
            {
                ResultEventArgs<Listings> result = null;
                IListingsService listingsService = new ListingsService(new EtsyContext("InvalidKey"));
                listingsService.GetListingsByTagsCompleted += (s, e) =>
                {
                    result = e;
                    waitEvent.Set();
                };

                List<string> tags = new List<string> { "bag" };

                // ACT
                listingsService.GetListingsByTags(tags, SortField.Created, SortOrder.Up, 0, 10, DetailLevel.Low);
                bool signalled = waitEvent.WaitOne(Constants.WaitTimeout);

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
        public void GetListingsByTagsCallTest()
        {
            // ARRANGE
            using (AutoResetEvent waitEvent = new AutoResetEvent(false))
            {
                ResultEventArgs<Listings> result = null;
                IListingsService listingsService = new ListingsService(new EtsyContext(NetsyData.EtsyApiKey));
                listingsService.GetListingsByTagsCompleted += (s, e) =>
                {
                    result = e;
                    waitEvent.Set();
                };

                List<string> tags = new List<string> { "bag" };

                // ACT
                listingsService.GetListingsByTags(tags, SortField.Created, SortOrder.Up, 0, 10, DetailLevel.Low);
                bool signalled = waitEvent.WaitOne(Constants.WaitTimeout);

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
        public void GetListingsByTagsSortByScoreTest()
        {
            // ARRANGE
            using (AutoResetEvent waitEvent = new AutoResetEvent(false))
            {
                ResultEventArgs<Listings> result = null;
                IListingsService listingsService = new ListingsService(new EtsyContext(NetsyData.EtsyApiKey));
                listingsService.GetListingsByTagsCompleted += (s, e) =>
                {
                    result = e;
                    waitEvent.Set();
                };

                List<string> tags = new List<string> { "bag" };

                // ACT
                listingsService.GetListingsByTags(tags, SortField.Score, SortOrder.Up, 0, 10, DetailLevel.Low);
                bool signalled = waitEvent.WaitOne(Constants.WaitTimeout);

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
        public void GetListingsByTagsAllDetailLevelsTest()
        {
            TestGetListingsByTags(DetailLevel.Low);
            TestGetListingsByTags(DetailLevel.Medium);
            TestGetListingsByTags(DetailLevel.High);
        }

        /// <summary>
        /// Test getting listings by tags at the appropriate detail level
        /// </summary>
        /// <param name="detailLevel">he detail level</param>
        private static void TestGetListingsByTags(DetailLevel detailLevel)
        {
            TestHelpers.WaitABit();

            // ARRANGE
            using (AutoResetEvent waitEvent = new AutoResetEvent(false))
            {
                ResultEventArgs<Listings> result = null;
                IListingsService listingsService = new ListingsService(new EtsyContext(NetsyData.EtsyApiKey));
                listingsService.GetListingsByTagsCompleted += (s, e) =>
                {
                    result = e;
                    waitEvent.Set();
                };

                List<string> tags = new List<string> { "bag" };

                // ACT
                listingsService.GetListingsByTags(tags, SortField.Created, SortOrder.Up, 0, 10, detailLevel);
                bool signalled = waitEvent.WaitOne(Constants.WaitTimeout);

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
