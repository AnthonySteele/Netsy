//-----------------------------------------------------------------------
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

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Netsy.DataModel;
    using Netsy.Helpers;
    using Netsy.Interfaces;
    using Netsy.Services;
    using Netsy.Test;

    /// <summary>
    /// Test the GetFavorersOfListing API function
    /// </summary>
    [TestClass]
    public class GetFavorersOfListingTest
    {
        /// <summary>
        /// the listing id to use
        /// </summary>
        private readonly int testListingId;

        /// <summary>
        /// Initializes a new instance of the GetFavorersOfListingTest class
        /// </summary>
        public GetFavorersOfListingTest()
        {
            this.testListingId = TestIdHelper.RetrieveTestListingId();            
        }

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
            favoritesService.GetFavorersOfListing(this.testListingId, 0, 10, DetailLevel.Low);

            // check the data
            TestHelpers.CheckResultFailure(result);
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
                favoritesService.GetFavorersOfListing(this.testListingId, 0, 10, DetailLevel.Low);
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
        /// Test invalid listing key
        /// </summary>
        [TestMethod]
        public void GetFavorersOfListingListingIdInvalidTest()
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
                favoritesService.GetFavorersOfListing(NetsyData.TestBadListingId, 0, 10, DetailLevel.Low);
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
        public void GetFavorersOfListingGetTest()
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
                favoritesService.GetFavorersOfListing(this.testListingId, 0, 10, DetailLevel.Low);
                bool signalled = waitEvent.WaitOne(Constants.WaitTimeout);

                // ASSERT
                // check that the event was fired, did not time out
                Assert.IsTrue(signalled, "Not signalled");

                // check the data - should succeed
                Assert.IsNotNull(result);
                TestHelpers.CheckResultSuccess(result);

                Assert.IsTrue(result.ResultValue.Count > 1);
                //Assert.AreEqual(10, result.ResultValue.Results.Length);
                Assert.IsNotNull(result.ResultValue.Params);
            }
        }

        /// <summary>
        /// Test retrieving FavorersOfListing, all detail levels
        /// </summary>
        [TestMethod]
        public void GetFavorersOfListingAllDetailLevelsTest()
        {
            this.TestGetFavorersOfListing(DetailLevel.Low);
            this.TestGetFavorersOfListing(DetailLevel.Medium);
            this.TestGetFavorersOfListing(DetailLevel.High);
        }

        /// <summary>
        /// Test retrieving FavorersOfListing at the given detail level
        /// </summary>
        /// <param name="detailLevel">the given detail level</param>
        private void TestGetFavorersOfListing(DetailLevel detailLevel)
        {
            TestHelpers.WaitABit();

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
                favoritesService.GetFavorersOfListing(this.testListingId, 0, 10, detailLevel);
                bool signalled = waitEvent.WaitOne(Constants.WaitTimeout);

                // ASSERT
                // check that the event was fired, did not time out
                Assert.IsTrue(signalled, "Not signalled");

                // check the data - should succeed
                Assert.IsNotNull(result);
                TestHelpers.CheckResultSuccess(result);

                Assert.IsTrue(result.ResultValue.Count > 1);
                //Assert.AreEqual(10, result.ResultValue.Results.Length);
                Assert.IsNotNull(result.ResultValue.Params);
            }
        }
    }
}
