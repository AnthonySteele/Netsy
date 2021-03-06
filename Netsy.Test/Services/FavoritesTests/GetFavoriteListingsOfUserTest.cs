﻿//-----------------------------------------------------------------------
// <copyright file="GetFavoriteListingsOfUserTest.cs" company="AFS">
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
            IFavoritesService favoritesService = ServiceCreationHelper.MakeFavouritesService(string.Empty);
            ResultEventArgs<Listings> result = null;
            favoritesService.GetFavoriteListingsOfUserCompleted += (s, e) => result = e;

            // ACT
            favoritesService.GetFavoriteListingsOfUser(Constants.TestId, 0, 10, DetailLevel.Low);

            // check the data
            TestHelpers.CheckResultFailure(result, Constants.EmptyApiKeyErrorMessage);
        }

        /// <summary>
        /// Test missing API key
        /// </summary>
        [TestMethod]
        public void GetFavoriteListingsOfUserByNameMissingApiKeyTest()
        {
            // ARRANGE
            IFavoritesService favoritesService = ServiceCreationHelper.MakeFavouritesService(string.Empty);
            ResultEventArgs<Listings> result = null;
            favoritesService.GetFavoriteListingsOfUserCompleted += (s, e) => result = e;

            // ACT
            favoritesService.GetFavoriteListingsOfUser(Constants.TestName, 0, 10, DetailLevel.Low);

            // check the data
            TestHelpers.CheckResultFailure(result, Constants.EmptyApiKeyErrorMessage);
        }

        /// <summary>
        /// Test a negative offset
        /// </summary>
        [TestMethod]
        public void GetFavoriteListingsOfUserNegativeOffsetTest()
        {
            // ARRANGE
            IFavoritesService favoritesService = ServiceCreationHelper.MakeFavouritesService(Constants.DummyEtsyApiKey);
            ResultEventArgs<Listings> result = null;
            favoritesService.GetFavoriteListingsOfUserCompleted += (s, e) => result = e;

            // ACT
            favoritesService.GetFavoriteListingsOfUser(Constants.TestName, -1, 10, DetailLevel.Low);

            // check the data
            TestHelpers.CheckResultFailure(result, "Negative offset of -1");
        }

        /// <summary>
        /// Test a negative offset
        /// </summary>
        [TestMethod]
        public void GetFavoriteListingsOfUserZeroLimitTest()
        {
            // ARRANGE
            IFavoritesService favoritesService = ServiceCreationHelper.MakeFavouritesService(Constants.DummyEtsyApiKey);
            ResultEventArgs<Listings> result = null;
            favoritesService.GetFavoriteListingsOfUserCompleted += (s, e) => result = e;

            // ACT
            favoritesService.GetFavoriteListingsOfUser(Constants.TestName, 0, 0, DetailLevel.Low);


            // check the data
            TestHelpers.CheckResultFailure(result, "Bad limit of 0");
        }
    }
}
