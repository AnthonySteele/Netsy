﻿//-----------------------------------------------------------------------
// <copyright file="GetFavorersOfListingTest.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 
namespace Netsy.IntegrationTest.Favorites
{
    using System.Net;
    using System.Threading;

    using Netsy.Core;
    using Netsy.DataModel;
    using Netsy.DataModel.UserData;
    using Netsy.Helpers;
    using Netsy.Interfaces;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Test the GetFavorersOfListing API function
    /// </summary>
    [TestClass]
    public class GetFavorersOfListingTest
    {
        /// <summary>
        /// Test missing API key
        /// </summary>
        [TestMethod]
        public void GetFavorersOfListingMissingApiKeyTest()
        {
            // ARRANGE
            ResultEventArgs<Users> result = null;
            IFavoritesService favoritesService = new FavoritesService(new EtsyContext(string.Empty));
            favoritesService.GetFavorersOfListingCompleted += (s, e) => result = e;

            // ACT
            favoritesService.GetFavorersOfListing(NetsyData.TestListingId, 0, 10, DetailLevel.Low);

            // check the data
            NetsyData.CheckResultFailure(result);
        }

        /// <summary>
        /// Test invalid API key
        /// </summary>
        [TestMethod]
        public void GetFavorersOfListingApiKeyInvalidTest()
        {
            // ARRANGE
            using (AutoResetEvent waitEvent = new AutoResetEvent(false))
            {
                ResultEventArgs<Users> result = null;
                IFavoritesService favoritesService = new FavoritesService(new EtsyContext("InvalidKey"));
                favoritesService.GetFavorersOfListingCompleted += (s, e) =>
                {
                    result = e;
                    waitEvent.Set();
                };

                // ACT
                favoritesService.GetFavorersOfListing(NetsyData.TestListingId, 0, 10, DetailLevel.Low);
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
        /// Test invalid listing key
        /// </summary>
        [TestMethod]
        public void GetFavorersOfListingLIstingIdInvalidTest()
        {
            // ARRANGE
            using (AutoResetEvent waitEvent = new AutoResetEvent(false))
            {
                ResultEventArgs<Users> result = null;
                IFavoritesService favoritesService = new FavoritesService(new EtsyContext(NetsyData.EtsyApiKey));
                favoritesService.GetFavorersOfListingCompleted += (s, e) =>
                    {
                        result = e;
                        waitEvent.Set();
                    };

                // ACT
                favoritesService.GetFavorersOfListing(1, 0, 10, DetailLevel.Low);
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
    }
}
