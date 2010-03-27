//-----------------------------------------------------------------------
// <copyright file="GetFavorersOfListingTest.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.Test.Services.FavoritesTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Netsy.DataModel;
    using Netsy.Helpers;
    using Netsy.Interfaces;

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
            IFavoritesService favoritesService = ServiceCreationHelper.MakeFavouritesService(string.Empty);
            ResultEventArgs<Users> result = null;
            favoritesService.GetFavorersOfListingCompleted += (s, e) => result = e;

            // ACT
            favoritesService.GetFavorersOfListing(Constants.TestId, 0, 10, DetailLevel.Low);

            // check the data
            TestHelpers.CheckResultFailure(result, "Empty API key");
        }

        /// <summary>
        /// Test a negative offset
        /// </summary>
        [TestMethod]
        public void GetFavorersOfListingNegativeOffsetTest()
        {
            // ARRANGE
            IFavoritesService favoritesService = ServiceCreationHelper.MakeFavouritesService(Constants.DummyEtsyApiKey);
            ResultEventArgs<Users> result = null;
            favoritesService.GetFavorersOfListingCompleted += (s, e) => result = e;

            // ACT
            favoritesService.GetFavorersOfListing(Constants.TestId, -1, 10, DetailLevel.Low);

            // check the data
            TestHelpers.CheckResultFailure(result, "Negative offset of -1");
        }

        /// <summary>
        /// Test a negative offset
        /// </summary>
        [TestMethod]
        public void GetFavorersOfListingZeroLimitTest()
        {
            // ARRANGE
            IFavoritesService favoritesService = ServiceCreationHelper.MakeFavouritesService(Constants.DummyEtsyApiKey);
            ResultEventArgs<Users> result = null;
            favoritesService.GetFavorersOfListingCompleted += (s, e) => result = e;

            // ACT
            favoritesService.GetFavorersOfListing(Constants.TestId, 0, 0, DetailLevel.Low);

            // check the data
            TestHelpers.CheckResultFailure(result, "Bad limit of 0");
        }
    }
}
