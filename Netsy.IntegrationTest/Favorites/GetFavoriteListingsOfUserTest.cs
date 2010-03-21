//-----------------------------------------------------------------------
// <copyright file="GetFavoriteListingsOfUserTest.cs" company="AFS">
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
    /// Test the GetFavoriteListings API function
    /// </summary>
    [TestClass]
    public class GetFavoriteListingsOfUserTest
    {
        /// <summary>
        /// Test missing API key
        /// </summary>
        [TestMethod]
        public void GetFavoriteListingsOfUserMissingApiKeyTest()
        {
            // ARRANGE
            ResultEventArgs<Listings> result = null;
            IFavoritesService favoritesService = new FavoritesService(new EtsyContext(string.Empty));
            favoritesService.GetFavoriteListingsOfUserCompleted += (s, e) => result = e;

            // ACT
            favoritesService.GetFavoriteListingsOfUser(NetsyData.TestUserId, 0, 10, DetailLevel.Low);

            // check the data
            TestHelpers.CheckResultFailure(result);
        }

        /// <summary>
        /// Test missing API key
        /// </summary>
        [TestMethod]
        public void GetFavoriteListingsOfUserByNameMissingApiKeyTest()
        {
            // ARRANGE
            ResultEventArgs<Listings> result = null;
            IFavoritesService favoritesService = new FavoritesService(new EtsyContext(string.Empty));
            favoritesService.GetFavoriteListingsOfUserCompleted += (s, e) => result = e;

            // ACT
            favoritesService.GetFavoriteListingsOfUser(NetsyData.TestUserName, 0, 10, DetailLevel.Low);

            // check the data
            TestHelpers.CheckResultFailure(result);
        }

        /// <summary>
        /// Test invalid API key
        /// </summary>
        [TestMethod]
        public void GetFavoriteListingsOfUserApiKeyInvalidTest()
        {
            // ARRANGE
            using (AutoResetEvent waitEvent = new AutoResetEvent(false))
            {
                ResultEventArgs<Listings> result = null;
                IFavoritesService favoritesService = new FavoritesService(new EtsyContext("InvalidKey"));
                favoritesService.GetFavoriteListingsOfUserCompleted += (s, e) =>
                {
                    result = e;
                    waitEvent.Set();
                };

                // ACT
                favoritesService.GetFavoriteListingsOfUser(NetsyData.TestUserId, 0, 10, DetailLevel.Low);
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
        public void GetFavoriteListingsOfUserByNameApiKeyInvalidTest()
        {
            // ARRANGE
            using (AutoResetEvent waitEvent = new AutoResetEvent(false))
            {
                ResultEventArgs<Listings> result = null;
                IFavoritesService favoritesService = new FavoritesService(new EtsyContext("InvalidKey"));
                favoritesService.GetFavoriteListingsOfUserCompleted += (s, e) =>
                {
                    result = e;
                    waitEvent.Set();
                };

                // ACT
                favoritesService.GetFavoriteListingsOfUser(NetsyData.TestUserName, 0, 10, DetailLevel.Low);
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
        public void GetFavoriteListingsOfUserUserIdInvalidTest()
        {
            // ARRANGE
            using (AutoResetEvent waitEvent = new AutoResetEvent(false))
            {
                ResultEventArgs<Listings> result = null;
                IFavoritesService favoritesService = new FavoritesService(new EtsyContext(NetsyData.EtsyApiKey));
                favoritesService.GetFavoriteListingsOfUserCompleted += (s, e) =>
                {
                    result = e;
                    waitEvent.Set();
                };

                // ACT
                favoritesService.GetFavoriteListingsOfUser(NetsyData.TestBadUserId, 0, 10, DetailLevel.Low);
                bool signalled = waitEvent.WaitOne(Constants.WaitTimeout);

                // ASSERT
                // check that the event was fired, did not time out
                Assert.IsTrue(signalled, "Not signalled");

                // check the data - should suceed without results
                Assert.IsNotNull(result);
                Assert.IsNotNull(result.ResultStatus);
                Assert.AreEqual(0, result.ResultValue.Count);
            }
        }

        /// <summary>
        /// Test invalid API key
        /// </summary>
        [TestMethod]
        public void GetFavoriteListingsOfUserUserNameInvalidTest()
        {
            // ARRANGE
            using (AutoResetEvent waitEvent = new AutoResetEvent(false))
            {
                ResultEventArgs<Listings> result = null;
                IFavoritesService favoritesService = new FavoritesService(new EtsyContext(NetsyData.EtsyApiKey));
                favoritesService.GetFavoriteListingsOfUserCompleted += (s, e) =>
                {
                    result = e;
                    waitEvent.Set();
                };

                // ACT
                favoritesService.GetFavoriteListingsOfUser(NetsyData.TestBadUserName, 0, 10, DetailLevel.Low);
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
        public void GetFavoriteListingsOfUserGetTest()
        {
            // ARRANGE
            using (AutoResetEvent waitEvent = new AutoResetEvent(false))
            {
                ResultEventArgs<Listings> result = null;
                IFavoritesService favoritesService = new FavoritesService(new EtsyContext(NetsyData.EtsyApiKey));
                favoritesService.GetFavoriteListingsOfUserCompleted += (s, e) =>
                {
                    result = e;
                    waitEvent.Set();
                };

                // ACT
                favoritesService.GetFavoriteListingsOfUser(NetsyData.TestUserId, 0, 10, DetailLevel.Low);
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
        public void GetFavoriteListingsOfUserGetByNameTest()
        {
            // ARRANGE
            using (AutoResetEvent waitEvent = new AutoResetEvent(false))
            {
                ResultEventArgs<Listings> result = null;
                IFavoritesService favoritesService = new FavoritesService(new EtsyContext(NetsyData.EtsyApiKey));
                favoritesService.GetFavoriteListingsOfUserCompleted += (s, e) =>
                {
                    result = e;
                    waitEvent.Set();
                };

                // ACT
                favoritesService.GetFavoriteListingsOfUser(NetsyData.TestUserName, 0, 10, DetailLevel.Low);
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
        /// Test retrieving GetFavoriteListingsOfUser, all detail levels
        /// </summary>
        [TestMethod]
        public void GetFavoriteListingsOfUserAllDetailLevelsTest()
        {
            TestGetFavoriteListingsOfUser(DetailLevel.Low);
            TestGetFavoriteListingsOfUser(DetailLevel.Medium);
            TestGetFavoriteListingsOfUser(DetailLevel.High);
        }

        /// <summary>
        /// Test retrieving GetFavoriteListingsOfUser by name, all detail levels
        /// </summary>
        [TestMethod]
        public void GetFavoriteListingsOfUserByNameAllDetailLevelsTest()
        {
            TestGetFavoriteListingsOfUserByName(DetailLevel.Low);
            TestGetFavoriteListingsOfUserByName(DetailLevel.Medium);
            TestGetFavoriteListingsOfUserByName(DetailLevel.High);
        }

        /// <summary>
        /// Test GetFavoriteListingsOfUser at the given detail level
        /// </summary>
        /// <param name="detailLevel">the given detail level</param>
        private static void TestGetFavoriteListingsOfUser(DetailLevel detailLevel)
        {
            TestHelpers.WaitABit();
            
            // ARRANGE
            using (AutoResetEvent waitEvent = new AutoResetEvent(false))
            {
                ResultEventArgs<Listings> result = null;
                IFavoritesService favoritesService = new FavoritesService(new EtsyContext(NetsyData.EtsyApiKey));
                favoritesService.GetFavoriteListingsOfUserCompleted += (s, e) =>
                {
                    result = e;
                    waitEvent.Set();
                };

                // ACT
                favoritesService.GetFavoriteListingsOfUser(NetsyData.TestUserId, 0, 10, detailLevel);
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
        /// Test GetFavoriteListingsOfUser by name at the given detail level
        /// </summary>
        /// <param name="detailLevel">the given detail level</param>
        private static void TestGetFavoriteListingsOfUserByName(DetailLevel detailLevel)
        {
            TestHelpers.WaitABit();
            
            // ARRANGE
            using (AutoResetEvent waitEvent = new AutoResetEvent(false))
            {
                ResultEventArgs<Listings> result = null;
                IFavoritesService favoritesService = new FavoritesService(new EtsyContext(NetsyData.EtsyApiKey));
                favoritesService.GetFavoriteListingsOfUserCompleted += (s, e) =>
                {
                    result = e;
                    waitEvent.Set();
                };

                // ACT
                favoritesService.GetFavoriteListingsOfUser(NetsyData.TestUserName, 0, 10, detailLevel);
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
