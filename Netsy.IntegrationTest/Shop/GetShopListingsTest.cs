//-----------------------------------------------------------------------
// <copyright file="GetShopListingsTest.cs" company="AFS">
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
    /// Test the GetShopListings function on the shop service
    /// </summary>
    [TestClass]
    public class GetShopListingsTest
    {
        /// <summary>
        /// Test missing API key
        /// </summary>
        [TestMethod]
        public void GetShopListingsByIdMissingApiKeyTest()
        {
            // ARRANGE
            ResultEventArgs<Listings> result = null;
            IShopService shopsService = new ShopService(new EtsyContext(string.Empty));
            shopsService.GetShopListingsCompleted += (s, e) => result = e;

            // ACT
            shopsService.GetShopListings(NetsyData.TestUserId, SortField.Created, SortOrder.Up, 0, 0, 10, DetailLevel.Low);

            // check the data
            NetsyData.CheckResultFailure(result);
        }

        /// <summary>
        /// Test missing API key
        /// </summary>
        [TestMethod]
        public void GetShopListingsByNameMissingApiKeyTest()
        {
            ResultEventArgs<Listings> result = null;
            IShopService shopsService = new ShopService(new EtsyContext(string.Empty));
            shopsService.GetShopListingsCompleted += (s, e) => result = e;

            // ACT
            shopsService.GetShopListings(NetsyData.TestUserName, SortField.Created, SortOrder.Up, 0, 0, 10, DetailLevel.Low);

            // check the data
            NetsyData.CheckResultFailure(result);
        }

        /// <summary>
        /// Test invalid API key
        /// </summary>
        [TestMethod]
        public void GetShopListingsByIdApiKeyInvalidTest()
        {
            // ARRANGE
            using (AutoResetEvent waitEvent = new AutoResetEvent(false))
            {
                ResultEventArgs<Listings> result = null;
                IShopService shopsService = new ShopService(new EtsyContext("InvalidKey"));
                shopsService.GetShopListingsCompleted += (s, e) =>
                {
                    result = e;
                    waitEvent.Set();
                };

                // ACT
                shopsService.GetShopListings(NetsyData.TestUserId, SortField.Created, SortOrder.Up, 0, 0, 10, DetailLevel.Low);
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
        /// Test invalid API key
        /// </summary>
        [TestMethod]
        public void GetShopListingsByNameApiKeyInvalidTest()
        {
            // ARRANGE
            using (AutoResetEvent waitEvent = new AutoResetEvent(false))
            {
                ResultEventArgs<Listings> result = null;
                IShopService shopsService = new ShopService(new EtsyContext("InvalidKey"));
                shopsService.GetShopListingsCompleted += (s, e) =>
                {
                    result = e;
                    waitEvent.Set();
                };

                // ACT
                shopsService.GetShopListings(NetsyData.TestUserName, SortField.Created, SortOrder.Up, 0, 0, 10, DetailLevel.Low);
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
        public void GetShopListingsByIdRetrieveLowDetailTest()
        {
            // ARANGE
            using (AutoResetEvent waitEvent = new AutoResetEvent(false))
            {
                ResultEventArgs<Listings> result = null;

                IShopService shopsService = new ShopService(new EtsyContext(NetsyData.EtsyApiKey));
                shopsService.GetShopListingsCompleted += (s, e) =>
                {
                    result = e;
                    waitEvent.Set();
                };

                // ACT
                shopsService.GetShopListings(NetsyData.TestUserId, SortField.Created, SortOrder.Down, 0, 0, 10, DetailLevel.Low);
                bool signalled = waitEvent.WaitOne(NetsyData.WaitTimeout);

                // ASSERT
                // check that the event was fired, did not time out
                Assert.IsTrue(signalled, "Not signalled");

                // check the data
                NetsyData.CheckResultSuccess(result);

                Assert.IsNotNull(result.ResultValue.Results);
                Assert.IsTrue(result.ResultStatus.Success);
                Assert.IsTrue(result.ResultValue.Count > 0);
            }
        }

        /// <summary>
        /// Test searching for etsy shops by name
        /// </summary>
        [TestMethod]
        public void GetShopListingsByNameRetrieveLowDetailTest()
        {
            // ARANGE
            using (AutoResetEvent waitEvent = new AutoResetEvent(false))
            {
                ResultEventArgs<Listings> result = null;

                IShopService shopsService = new ShopService(new EtsyContext(NetsyData.EtsyApiKey));
                shopsService.GetShopListingsCompleted += (s, e) =>
                {
                    result = e;
                    waitEvent.Set();
                };

                // ACT
                shopsService.GetShopListings(NetsyData.TestUserName, SortField.Created, SortOrder.Down, 0, 0, 10, DetailLevel.Low);
                bool signalled = waitEvent.WaitOne(NetsyData.WaitTimeout);

                // ASSERT
                // check that the event was fired, did not time out
                Assert.IsTrue(signalled, "Not signalled");

                // check the data
                NetsyData.CheckResultSuccess(result);

                Assert.IsNotNull(result.ResultValue.Results);
                Assert.IsTrue(result.ResultStatus.Success);
                Assert.IsTrue(result.ResultValue.Count > 0);
            }
        }

        /// <summary>
        /// Test retrieving shop listings, all detail levels
        /// </summary>
        [TestMethod]
        public void GetShopListingsByIdAllDetailLevelsTest()
        {
            TestGetShopListingsById(DetailLevel.Low);
            TestGetShopListingsById(DetailLevel.Medium);
            TestGetShopListingsById(DetailLevel.High);
        }

        /// <summary>
        /// Test retrieving shop listings, all detail levels
        /// </summary>
        [TestMethod]
        public void GetShopListingsByNameAllDetailLevelsTest()
        {
            TestGetShopListingsByName(DetailLevel.Low);
            TestGetShopListingsByName(DetailLevel.Medium);
            TestGetShopListingsByName(DetailLevel.High);
        }

        /// <summary>
        /// Test retrieving shop listings at the given detail level
        /// </summary>
        /// <param name="detailLevel">the given detail level</param>
        private static void TestGetShopListingsById(DetailLevel detailLevel)
        {
            // ARANGE
            using (AutoResetEvent waitEvent = new AutoResetEvent(false))
            {
                ResultEventArgs<Listings> result = null;

                IShopService shopsService = new ShopService(new EtsyContext(NetsyData.EtsyApiKey));
                shopsService.GetShopListingsCompleted += (s, e) =>
                {
                    result = e;
                    waitEvent.Set();
                };

                // ACT
                shopsService.GetShopListings(NetsyData.TestUserId, SortField.Created, SortOrder.Down, 0, 0, 10, detailLevel);
                bool signalled = waitEvent.WaitOne(NetsyData.WaitTimeout);

                // ASSERT
                // check that the event was fired, did not time out
                Assert.IsTrue(signalled, "Not signalled");

                // check the data
                NetsyData.CheckResultSuccess(result);

                Assert.IsNotNull(result.ResultValue.Results);
                Assert.IsTrue(result.ResultStatus.Success);
                Assert.IsTrue(result.ResultValue.Count > 0);
            }
        }

        /// <summary>
        /// Test retrieving shop listings at the given detail level
        /// </summary>
        /// <param name="detailLevel">the given detail level</param>
        private static void TestGetShopListingsByName(DetailLevel detailLevel)
        {
            // ARANGE
            using (AutoResetEvent waitEvent = new AutoResetEvent(false))
            {
                ResultEventArgs<Listings> result = null;

                IShopService shopsService = new ShopService(new EtsyContext(NetsyData.EtsyApiKey));
                shopsService.GetShopListingsCompleted += (s, e) =>
                {
                    result = e;
                    waitEvent.Set();
                };

                // ACT
                shopsService.GetShopListings(NetsyData.TestUserName, SortField.Created, SortOrder.Down, 0, 0, 10, detailLevel);
                bool signalled = waitEvent.WaitOne(NetsyData.WaitTimeout);

                // ASSERT
                // check that the event was fired, did not time out
                Assert.IsTrue(signalled, "Not signalled");

                // check the data
                NetsyData.CheckResultSuccess(result);

                Assert.IsNotNull(result.ResultValue.Results);
                Assert.IsTrue(result.ResultStatus.Success);
                Assert.IsTrue(result.ResultValue.Count > 0);
            }
        }

    }
}
