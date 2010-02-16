//-----------------------------------------------------------------------
// <copyright file="GetShopDetailsByNameTest.cs" company="AFS">
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
    /// Test the GetShopDetails function on the shop service
    /// </summary>
    [TestClass]
    public class GetShopDetailsByNameTest
    {
        /// <summary>
        /// Test missing API key
        /// </summary>
        [TestMethod]
        public void GetShopDetailsMissingApiKeyTest()
        {
            // ARRANGE
            ResultEventArgs<Shops> result = null;
            IShopService shopsService = new ShopService(new EtsyContext(string.Empty));
            shopsService.GetShopDetailsCompleted += (s, e) => result = e;

            // ACT
            shopsService.GetShopDetails(NetsyData.TestUserName, DetailLevel.Low);

            // check the data
            TestHelpers.CheckResultFailure(result);
        }

        /// <summary>
        /// Test invalid API key
        /// </summary>
        [TestMethod]
        public void GetShopDetailsApiKeyInvalidTest()
        {
            // ARRANGE
            using (AutoResetEvent waitEvent = new AutoResetEvent(false))
            {
                ResultEventArgs<Shops> result = null;
                IShopService shopsService = new ShopService(new EtsyContext("InvalidKey"));
                shopsService.GetShopDetailsCompleted += (s, e) =>
                {
                    result = e;
                    waitEvent.Set();
                };

                // ACT
                shopsService.GetShopDetails(NetsyData.TestUserName, DetailLevel.Low);
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
        /// Test retrieving etsy shops by id
        /// </summary>
        [TestMethod]
        public void GetShopDetailsLowDetailTest()
        {
            // ARRANGE
            using (AutoResetEvent waitEvent = new AutoResetEvent(false))
            {
                ResultEventArgs<Shops> result = null;
                IShopService shopsService = new ShopService(new EtsyContext(NetsyData.EtsyApiKey));
                shopsService.GetShopDetailsCompleted += (s, e) =>
                {
                    result = e;
                    waitEvent.Set();
                };

                // ACT
                shopsService.GetShopDetails(NetsyData.TestUserName, DetailLevel.Low);
                bool signalled = waitEvent.WaitOne(NetsyData.WaitTimeout);

                // ASSERT
                // check that the event was fired, did not time out
                Assert.IsTrue(signalled, "Not signalled");

                // check the data
                TestHelpers.CheckResultSuccess(result);

                Assert.IsNotNull(result.ResultValue.Params);
                Assert.IsNotNull(result.ResultValue.Results);
                Assert.AreEqual(1, result.ResultValue.Count);
            }
        }

        /// <summary>
        /// Test retrieving shop details, all detail levels
        /// </summary>
        [TestMethod]
        public void GetShopDetailsAllDetailLevelsTest()
        {
            TestGetShopDetails(DetailLevel.Low);
            TestGetShopDetails(DetailLevel.Medium);
            TestGetShopDetails(DetailLevel.High);
        }

        /// <summary>
        /// Tets getting shop details with various detail levels
        /// </summary>
        /// <param name="detailLevel">the detail level to use</param>
        private static void TestGetShopDetails(DetailLevel detailLevel)
        {
            // ARRANGE
            using (AutoResetEvent waitEvent = new AutoResetEvent(false))
            {
                ResultEventArgs<Shops> result = null;
                IShopService shopsService = new ShopService(new EtsyContext(NetsyData.EtsyApiKey));
                shopsService.GetShopDetailsCompleted += (s, e) =>
                                                        {
                                                            result = e;
                                                            waitEvent.Set();
                                                        };

                // ACT
                shopsService.GetShopDetails(NetsyData.TestUserName, detailLevel);
                bool signalled = waitEvent.WaitOne(NetsyData.WaitTimeout);

                // ASSERT
                // check that the event was fired, did not time out
                Assert.IsTrue(signalled, "Not signalled");

                // check the data
                TestHelpers.CheckResultSuccess(result);

                Assert.IsNotNull(result.ResultValue.Params);
                Assert.IsNotNull(result.ResultValue.Results);
                Assert.AreEqual(1, result.ResultValue.Count);
            }
        }
    }
}
