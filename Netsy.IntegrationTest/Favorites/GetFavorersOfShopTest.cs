//-----------------------------------------------------------------------
// <copyright file="GetFavorersOfShopTest.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 
namespace Netsy.IntegrationTest.Favorites
{
    using System.Net;
    using System.Threading;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Netsy.DataModel;
    using Netsy.Helpers;
    using Netsy.Interfaces;
    using Netsy.Services;
    using Netsy.Test;

    /// <summary>
    ///  Test the GetFavorersOfShop API function
    /// </summary>
    [TestClass]
    public class GetFavorersOfShopTest
    {
        /// <summary>
        /// Test missing API key
        /// </summary>
        [TestMethod]
        public void GetFavorersOfShopMissingApiKeyTest()
        {
            // ARRANGE
            ResultEventArgs<Users> result = null;
            IFavoritesService favoritesService = new FavoritesService(new EtsyContext(string.Empty));
            favoritesService.GetFavorersOfShopCompleted += (s, e) => result = e;

            // ACT
            favoritesService.GetFavorersOfShop(NetsyData.TestUserId, 0, 10, DetailLevel.Low);

            // check the data
            TestHelpers.CheckResultFailure(result);
        }

        /// <summary>
        /// Test missing API key
        /// </summary>
        [TestMethod]
        public void GetFavorersOfShopByNameMissingApiKeyTest()
        {
            // ARRANGE
            ResultEventArgs<Users> result = null;
            IFavoritesService favoritesService = new FavoritesService(new EtsyContext(string.Empty));
            favoritesService.GetFavorersOfShopCompleted += (s, e) => result = e;

            // ACT
            favoritesService.GetFavorersOfShop(NetsyData.TestUserName, 0, 10, DetailLevel.Low);

            // check the data
            TestHelpers.CheckResultFailure(result);
        }

        /// <summary>
        /// Test invalid API key
        /// </summary>
        [TestMethod]
        public void GetFavorersOfShopApiKeyInvalidTest()
        {
            // ARRANGE
            using (AutoResetEvent waitEvent = new AutoResetEvent(false))
            {
                ResultEventArgs<Users> result = null;
                IFavoritesService favoritesService = new FavoritesService(new EtsyContext("InvalidKey"));
                favoritesService.GetFavorersOfShopCompleted += (s, e) =>
                {
                    result = e;
                    waitEvent.Set();
                };

                // ACT
                favoritesService.GetFavorersOfShop(NetsyData.TestUserId, 0, 10, DetailLevel.Low);
                bool signalled = waitEvent.WaitOne(Constants.WaitTimeout);

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
        public void GetFavorersOfShopByNameApiKeyInvalidTest()
        {
            // ARRANGE
            using (AutoResetEvent waitEvent = new AutoResetEvent(false))
            {
                ResultEventArgs<Users> result = null;
                IFavoritesService favoritesService = new FavoritesService(new EtsyContext("InvalidKey"));
                favoritesService.GetFavorersOfShopCompleted += (s, e) =>
                {
                    result = e;
                    waitEvent.Set();
                };

                // ACT
                favoritesService.GetFavorersOfShop(NetsyData.TestUserName, 0, 10, DetailLevel.Low);
                bool signalled = waitEvent.WaitOne(Constants.WaitTimeout);

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
        public void GetFavorersOfShopShopIdInvalidTest()
        {
            // ARRANGE
            using (AutoResetEvent waitEvent = new AutoResetEvent(false))
            {
                ResultEventArgs<Users> result = null;
                IFavoritesService favoritesService = new FavoritesService(new EtsyContext(NetsyData.EtsyApiKey));
                favoritesService.GetFavorersOfShopCompleted += (s, e) =>
                {
                    result = e;
                    waitEvent.Set();
                };

                // ACT
                favoritesService.GetFavorersOfShop(NetsyData.TestBadUserId, 0, 10, DetailLevel.Low);
                bool signalled = waitEvent.WaitOne(Constants.WaitTimeout);

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
        /// Test invalid API key
        /// </summary>
        [TestMethod]
        public void GetFavorersOfShopShopNameInvalidTest()
        {
            // ARRANGE
            using (AutoResetEvent waitEvent = new AutoResetEvent(false))
            {
                ResultEventArgs<Users> result = null;
                IFavoritesService favoritesService = new FavoritesService(new EtsyContext(NetsyData.EtsyApiKey));
                favoritesService.GetFavorersOfShopCompleted += (s, e) =>
                {
                    result = e;
                    waitEvent.Set();
                };

                // ACT
                favoritesService.GetFavorersOfShop(NetsyData.TestBadUserName, 0, 10, DetailLevel.Low);
                bool signalled = waitEvent.WaitOne(Constants.WaitTimeout);

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
        /// Test retrieval
        /// </summary>
        [TestMethod]
        public void GetFavorersOfShopGetTest()
        {
            // ARRANGE
            using (AutoResetEvent waitEvent = new AutoResetEvent(false))
            {
                ResultEventArgs<Users> result = null;
                IFavoritesService favoritesService = new FavoritesService(new EtsyContext(NetsyData.EtsyApiKey));
                favoritesService.GetFavorersOfShopCompleted += (s, e) =>
                {
                    result = e;
                    waitEvent.Set();
                };

                // ACT
                favoritesService.GetFavorersOfShop(NetsyData.TestUserId, 0, 10, DetailLevel.Low);
                bool signalled = waitEvent.WaitOne(Constants.WaitTimeout);

                // ASSERT
                // check that the event was fired, did not time out
                Assert.IsTrue(signalled, "Not signalled");

                // check the data - should succeed
                Assert.IsNotNull(result);
                TestHelpers.CheckResultSuccess(result);

                Assert.IsTrue(result.ResultValue.Count > 1);
                Assert.AreEqual(10, result.ResultValue.Results.Length);
                Assert.IsNotNull(result.ResultValue.Params);
            }
        }

        /// <summary>
        /// Test retrieval
        /// </summary>
        [TestMethod]
        public void GetFavorersOfShopGetByNameTest()
        {
            // ARRANGE
            using (AutoResetEvent waitEvent = new AutoResetEvent(false))
            {
                ResultEventArgs<Users> result = null;
                IFavoritesService favoritesService = new FavoritesService(new EtsyContext(NetsyData.EtsyApiKey));
                favoritesService.GetFavorersOfShopCompleted += (s, e) =>
                {
                    result = e;
                    waitEvent.Set();
                };

                // ACT
                favoritesService.GetFavorersOfShop(NetsyData.TestUserName, 0, 10, DetailLevel.Low);
                bool signalled = waitEvent.WaitOne(Constants.WaitTimeout);

                // ASSERT
                // check that the event was fired, did not time out
                Assert.IsTrue(signalled, "Not signalled");

                // check the data - should succeed
                Assert.IsNotNull(result);
                TestHelpers.CheckResultSuccess(result);

                Assert.IsTrue(result.ResultValue.Count > 1);
                Assert.AreEqual(10, result.ResultValue.Results.Length);
                Assert.IsNotNull(result.ResultValue.Params);
            }
        }

        /// <summary>
        /// Test retrieving FavorersOfShop, all detail levels
        /// </summary>
        [TestMethod]
        public void GetFavorersOfShopAllDetailLevelsTest()
        {
            TestGetFavorersOfShop(DetailLevel.Low);
            TestGetFavorersOfShop(DetailLevel.Medium);
            TestGetFavorersOfShop(DetailLevel.High);
        }

        /// <summary>
        /// Test retrieving FavorersOfShop by name, all detail levels
        /// </summary>
        [TestMethod]
        public void GetFavorersOfShopByNameAllDetailLevelsTest()
        {
            TestGetFavorersOfShopByName(DetailLevel.Low);
            TestGetFavorersOfShopByName(DetailLevel.Medium);
            TestGetFavorersOfShopByName(DetailLevel.High);
        }

        /// <summary>
        /// Test retrieving FavorersOfShop, at the given detail level
        /// </summary>
        /// <param name="detailLevel">the given detail level</param>
        private static void TestGetFavorersOfShop(DetailLevel detailLevel)
        {
            TestHelpers.WaitABit();
            
            // ARRANGE
            using (AutoResetEvent waitEvent = new AutoResetEvent(false))
            {
                ResultEventArgs<Users> result = null;
                IFavoritesService favoritesService = new FavoritesService(new EtsyContext(NetsyData.EtsyApiKey));
                favoritesService.GetFavorersOfShopCompleted += (s, e) =>
                {
                    result = e;
                    waitEvent.Set();
                };

                // ACT
                favoritesService.GetFavorersOfShop(NetsyData.TestBadUserName, 0, 10, detailLevel);
                bool signalled = waitEvent.WaitOne(Constants.WaitTimeout);

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
        /// Test retrieving FavorersOfShop by name, at the given detail level
        /// </summary>
        /// <param name="detailLevel">the given detail level</param>
        private static void TestGetFavorersOfShopByName(DetailLevel detailLevel)
        {
            TestHelpers.WaitABit();
            
            // ARRANGE
            using (AutoResetEvent waitEvent = new AutoResetEvent(false))
            {
                ResultEventArgs<Users> result = null;
                IFavoritesService favoritesService = new FavoritesService(new EtsyContext(NetsyData.EtsyApiKey));
                favoritesService.GetFavorersOfShopCompleted += (s, e) =>
                {
                    result = e;
                    waitEvent.Set();
                };

                // ACT
                favoritesService.GetFavorersOfShop(NetsyData.TestUserName, 0, 10, detailLevel);
                bool signalled = waitEvent.WaitOne(Constants.WaitTimeout);

                // ASSERT
                // check that the event was fired, did not time out
                Assert.IsTrue(signalled, "Not signalled");

                // check the data - should succeed
                Assert.IsNotNull(result);
                TestHelpers.CheckResultSuccess(result);

                Assert.IsTrue(result.ResultValue.Count > 1);
                Assert.AreEqual(10, result.ResultValue.Results.Length);
                Assert.IsNotNull(result.ResultValue.Params);
            }
        }
    }
}
