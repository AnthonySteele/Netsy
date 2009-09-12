//-----------------------------------------------------------------------
// <copyright file="GetFrontFeaturedListingsTest.cs" company="AFS">
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
    /// Test the GetFrontFeaturedListings Api function
    /// </summary>
    [TestClass]
    public class GetFrontFeaturedListingsTest
    {
        /// <summary>
        /// Test missing API key
        /// </summary>
        [TestMethod]
        public void GetFrontFeaturedListingsApiKeyMissingTest()
        {
            // ARRANGE
            ResultEventArgs<Listings> result = null;
            IListingsService listingsService = new ListingsService(new EtsyContext(string.Empty));
            listingsService.GetFrontFeaturedListingsCompleted += (s, e) => result = e;

            // ACT
            listingsService.GetFrontFeaturedListings(0, 10, DetailLevel.Low);

            // check the data
            NetsyData.CheckResultFailure(result);
        }

        /// <summary>
        /// Test invalid API key
        /// </summary>
        [TestMethod]
        public void GetFrontFeaturedListingsApiKeyInvalidTest()
        {
            // ARRANGE
            using (AutoResetEvent waitEvent = new AutoResetEvent(false))
            {
                ResultEventArgs<Listings> result = null;
                IListingsService listingsService = new ListingsService(new EtsyContext("InvalidKey"));
                listingsService.GetFrontFeaturedListingsCompleted += (s, e) =>
                {
                    result = e;
                    waitEvent.Set();
                };

                // ACT
                listingsService.GetFrontFeaturedListings(0, 10, DetailLevel.Low);
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
        public void GetFrontFeaturedListingsCallTest()
        {
            // ARRANGE
            using (AutoResetEvent waitEvent = new AutoResetEvent(false))
            {
                ResultEventArgs<Listings> result = null;
                IListingsService listingsService = new ListingsService(new EtsyContext(NetsyData.EtsyApiKey));
                listingsService.GetFrontFeaturedListingsCompleted += (s, e) =>
                {
                    result = e;
                    waitEvent.Set();
                };

                // ACT
                listingsService.GetFrontFeaturedListings(0, 10, DetailLevel.Low);
                bool signalled = waitEvent.WaitOne(NetsyData.WaitTimeout);

                // ASSERT
                // check that the event was fired, did not time out
                Assert.IsTrue(signalled, "Not signalled");

                // check the data
                Assert.IsNotNull(result);
                NetsyData.CheckResultSuccess(result);

                Assert.IsTrue(result.ResultValue.Count > 1);
                Assert.AreEqual(10, result.ResultValue.Results.Length);
                Assert.IsNotNull(result.ResultValue.Params);
            }
        }

        /// <summary>
        /// Test retrieving front featured listings, all detail levels
        /// </summary>
        [TestMethod]
        public void GetFrontFeaturedListingsAllDetailLevelsTest()
        {
            TestGetFrontFeaturedListings(DetailLevel.Low);
            TestGetFrontFeaturedListings(DetailLevel.Medium);
            TestGetFrontFeaturedListings(DetailLevel.High);
        }

        /// <summary>
        /// Get front featured listings at the given detail level
        /// </summary>
        /// <param name="detailLevel">the detail level to use</param>
        private static void TestGetFrontFeaturedListings(DetailLevel detailLevel)
        {
            // ARRANGE
            using (AutoResetEvent waitEvent = new AutoResetEvent(false))
            {
                ResultEventArgs<Listings> result = null;
                IListingsService listingsService = new ListingsService(new EtsyContext(NetsyData.EtsyApiKey));
                listingsService.GetFrontFeaturedListingsCompleted += (s, e) =>
                {
                    result = e;
                    waitEvent.Set();
                };

                // ACT
                listingsService.GetFrontFeaturedListings(0, 10, detailLevel);
                bool signalled = waitEvent.WaitOne(NetsyData.WaitTimeout);

                // ASSERT
                // check that the event was fired, did not time out
                Assert.IsTrue(signalled, "Not signalled");

                // check the data
                Assert.IsNotNull(result);
                NetsyData.CheckResultSuccess(result);

                Assert.IsTrue(result.ResultValue.Count > 1);
                Assert.AreEqual(10, result.ResultValue.Results.Length);
                Assert.IsNotNull(result.ResultValue.Params);
            }
        }
    }
}
