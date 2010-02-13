//-----------------------------------------------------------------------
// <copyright file="GetListingsByMaterialsTest.cs" company="AFS">
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
    /// Test the GetListingsByMaterials Api function
    /// </summary>
    [TestClass]
    public class GetListingsByMaterialsTest
    {
        /// <summary>
        /// Test missing API key
        /// </summary>
        [TestMethod]
        public void GetListingsByMaterialsApiKeyMissingTest()
        {
            // ARRANGE
            ResultEventArgs<Listings> result = null;
            IListingsService listingsService = new ListingsService(new EtsyContext(string.Empty), new NullDataCache());
            listingsService.GetListingsByMaterialsCompleted += (s, e) => result = e;

            List<string> materials = new List<string>();

            // ACT
            listingsService.GetListingsByMaterials(materials, SortField.Created, SortOrder.Up, 0, 10, DetailLevel.Low);

            // check the data
            TestHelpers.CheckResultFailure(result);
        }

        /// <summary>
        /// Test invalid API key
        /// </summary>
        [TestMethod]
        public void GetListingsByMaterialsApiKeyInvalidTest()
        {
            // ARRANGE
            using (AutoResetEvent waitEvent = new AutoResetEvent(false))
            {
                ResultEventArgs<Listings> result = null;
                IListingsService listingsService = new ListingsService(new EtsyContext("InvalidKey"), new NullDataCache());
                listingsService.GetListingsByMaterialsCompleted += (s, e) =>
                {
                    result = e;
                    waitEvent.Set();
                };

                List<string> materials = new List<string> { "cotton" };

                // ACT
                listingsService.GetListingsByMaterials(materials, SortField.Created, SortOrder.Up, 0, 10, DetailLevel.Low);
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
        public void GetListingsByMaterialsCallTest()
        {
            // ARRANGE
            using (AutoResetEvent waitEvent = new AutoResetEvent(false))
            {
                ResultEventArgs<Listings> result = null;
                IListingsService listingsService = new ListingsService(new EtsyContext(NetsyData.EtsyApiKey), new NullDataCache());
                listingsService.GetListingsByMaterialsCompleted += (s, e) =>
                {
                    result = e;
                    waitEvent.Set();
                };

                List<string> materials = new List<string> { "cotton" };

                // ACT
                listingsService.GetListingsByMaterials(materials, SortField.Created, SortOrder.Up, 0, 10, DetailLevel.Low);
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
        public void GetListingsByMaterialsSortByScoreTest()
        {
            // ARRANGE
            using (AutoResetEvent waitEvent = new AutoResetEvent(false))
            {
                ResultEventArgs<Listings> result = null;
                IListingsService listingsService = new ListingsService(new EtsyContext(NetsyData.EtsyApiKey), new NullDataCache());
                listingsService.GetListingsByMaterialsCompleted += (s, e) =>
                {
                    result = e;
                    waitEvent.Set();
                };

                List<string> materials = new List<string> { "cotton" };

                // ACT
                listingsService.GetListingsByMaterials(materials, SortField.Score, SortOrder.Up, 0, 10, DetailLevel.Low);
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
        public void GetListingsByMaterialsAllDetailLevelsTest()
        {
            TestGetListingsByMaterials(DetailLevel.Low);
            TestGetListingsByMaterials(DetailLevel.Medium);
            TestGetListingsByMaterials(DetailLevel.High);
        }

        /// <summary>
        /// Test GetListingsByMaterials at the given detail level
        /// </summary>
        /// <param name="detailLevel">the detail level</param>
        private static void TestGetListingsByMaterials(DetailLevel detailLevel)
        {
            TestHelpers.WaitABit();

            // ARRANGE
            using (AutoResetEvent waitEvent = new AutoResetEvent(false))
            {
                ResultEventArgs<Listings> result = null;
                IListingsService listingsService = new ListingsService(new EtsyContext(NetsyData.EtsyApiKey), new NullDataCache());
                listingsService.GetListingsByMaterialsCompleted += (s, e) =>
                {
                    result = e;
                    waitEvent.Set();
                };

                List<string> materials = new List<string> { "cotton" };

                // ACT
                listingsService.GetListingsByMaterials(materials, SortField.Created, SortOrder.Up, 0, 10, detailLevel);
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
