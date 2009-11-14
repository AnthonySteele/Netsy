//-----------------------------------------------------------------------
// <copyright file="GetListingsByCategoryTest.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.IntegrationTest.Listings
{
    using System.Net;
    using System.Threading;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Netsy.DataModel;
    using Netsy.Helpers;
    using Netsy.Interfaces;
    using Netsy.Services;

    /// <summary>
    /// Test the GetListingsByCategory function on the listings service
    /// </summary>
    [TestClass]
    public class GetListingsByCategoryTest
    {
        /// <summary>
        /// Test missing API key
        /// </summary>
        [TestMethod]
        public void GetListingsByCategoryApiKeyMissingTest()
        {
            // ARRANGE
            ResultEventArgs<Listings> result = null;
            IListingsService listingsService = new ListingsService(new EtsyContext(string.Empty));
            listingsService.GetListingsByCategoryCompleted += (s, e) => result = e;

            // ACT
            listingsService.GetListingsByCategory(NetsyData.TestCategory, SortField.Created, SortOrder.Down, 0, 10, DetailLevel.Low);

            // check the data
            NetsyData.CheckResultFailure(result);
        }

        /// <summary>
        /// Test invalid API key
        /// </summary>
        [TestMethod]
        public void GetListingsByCategoryApiKeyInvalidTest()
        {
            // ARRANGE
            using (AutoResetEvent waitEvent = new AutoResetEvent(false))
            {
                ResultEventArgs<Listings> result = null;
                IListingsService listingsService = new ListingsService(new EtsyContext("InvalidKey"));
                listingsService.GetListingsByCategoryCompleted += (s, e) =>
                {
                    result = e;
                    waitEvent.Set();
                };

                // ACT
                listingsService.GetListingsByCategory(NetsyData.TestCategory, SortField.Created, SortOrder.Down, 0, 10, DetailLevel.Low);
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
        public void GetListingsByCategoryCallTest()
        {
            // ARRANGE
            using (AutoResetEvent waitEvent = new AutoResetEvent(false))
            {
                ResultEventArgs<Listings> result = null;
                IListingsService listingsService = new ListingsService(new EtsyContext(NetsyData.EtsyApiKey));
                listingsService.GetListingsByCategoryCompleted += (s, e) =>
                {
                    result = e;
                    waitEvent.Set();
                };

                // ACT
                listingsService.GetListingsByCategory(NetsyData.TestCategory, SortField.Created, SortOrder.Down, 0, 10, DetailLevel.Low);
                bool signalled = waitEvent.WaitOne(NetsyData.WaitTimeout);

                // ASSERT
                // check that the event was fired, did not time out
                Assert.IsTrue(signalled, "Not signalled");

                // check the data
                Assert.IsNotNull(result);
                NetsyData.CheckResultSuccess(result);

                Assert.IsTrue(result.ResultValue.Count > 1, "No Results found");
                Assert.AreEqual(10, result.ResultValue.Results.Length);
                Assert.IsNotNull(result.ResultValue.Params);
            }
        }

        /// <summary>
        /// Test retrieving listing details, all detail levels
        /// </summary>
        [TestMethod]
        public void GetListingsByCategoryAllDetailLevelsTest()
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
            // ARRANGE
            using (AutoResetEvent waitEvent = new AutoResetEvent(false))
            {
                ResultEventArgs<Listings> result = null;
                IListingsService listingsService = new ListingsService(new EtsyContext(NetsyData.EtsyApiKey));
                listingsService.GetListingsByCategoryCompleted += (s, e) =>
                {
                    result = e;
                    waitEvent.Set();
                };

                // ACT
                listingsService.GetListingsByCategory(NetsyData.TestCategory, SortField.Created, SortOrder.Down, 0, 10, detailLevel);
                bool signalled = waitEvent.WaitOne(NetsyData.WaitTimeout);

                // ASSERT
                // check that the event was fired, did not time out
                Assert.IsTrue(signalled, "Not signalled");

                // check the data
                Assert.IsNotNull(result);
                NetsyData.CheckResultSuccess(result);

                Assert.IsTrue(result.ResultValue.Count > 1, "No results retreived");
                Assert.AreEqual(10, result.ResultValue.Results.Length);
                Assert.IsNotNull(result.ResultValue.Params);
            }
        }
    }
}
