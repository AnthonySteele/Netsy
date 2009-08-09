﻿//-----------------------------------------------------------------------
// <copyright file="GetFeaturedSellersTest.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.IntegrationTest
{
    using System.Net;
    using System.Threading;

    using DataModel.ShopData;

    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Netsy.Core;
    using Netsy.DataModel;
    using Netsy.Helpers;
    using Netsy.Interfaces;

    /// <summary>
    /// Test etsy shop retrieval
    /// </summary>
    [TestClass]
    public class GetFeaturedSellersTest
    {

        /// <summary>
        /// Test missing API key
        /// </summary>
        [TestMethod]
        public void GetFeaturedSellersMissingApiKeyTest()
        {
            ResultEventArgs<Shops> result = null;
            IShopService shopsService = new ShopService(new EtsyContext(string.Empty));
            shopsService.GetFeaturedSellersCompleted += (s, e) => result = e;

            // ACT
            shopsService.GetFeaturedSellers(0, 10, DetailLevel.Low);

            // check the data
            NetsyData.CheckResultFailure(result);
        }

        /// <summary>
        /// Test invalid API key
        /// </summary>
        [TestMethod]
        public void GetFeaturedSellersApiKeyInvalidTest()
        {
            // ARRANGE
            using (AutoResetEvent waitEvent = new AutoResetEvent(false))
            {
                ResultEventArgs<Shops> result = null;
                IShopService shopsService = new ShopService(new EtsyContext("InvalidKey"));
                shopsService.GetFeaturedSellersCompleted += (s, e) =>
                {
                    result = e;
                    waitEvent.Set();
                };

                // ACT
                shopsService.GetFeaturedSellers(0, 10, DetailLevel.Low);
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
        public void GetTenFeaturedSellersTest()
        {
            // ARANGE
            using (AutoResetEvent waitEvent = new AutoResetEvent(false))
            {
                ResultEventArgs<Shops> result = null;

                IShopService shopsService = new ShopService(new EtsyContext(NetsyData.EtsyApiKey));
                shopsService.GetFeaturedSellersCompleted += (s, e) =>
                {
                    result = e;
                    waitEvent.Set();
                };

                // ACT
                shopsService.GetFeaturedSellers(0, 10, DetailLevel.Low);
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
