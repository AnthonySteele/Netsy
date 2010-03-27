//-----------------------------------------------------------------------
// <copyright file="GetFavorersOfShopTest.cs" company="AFS">
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
            IFavoritesService favoritesService = ServiceCreationHelper.MakeFavouritesService(string.Empty);
            ResultEventArgs<Users> result = null;
            favoritesService.GetFavorersOfShopCompleted += (s, e) => result = e;

            // ACT
            favoritesService.GetFavorersOfShop(Constants.TestId, 0, 10, DetailLevel.Low);

            // check the data
            TestHelpers.CheckResultFailure(result, Constants.EmptyApiKeyErrorMessage);
        }

        /// <summary>
        /// Test missing API key
        /// </summary>
        [TestMethod]
        public void GetFavorersOfShopByNameMissingApiKeyTest()
        {
            // ARRANGE
            IFavoritesService favoritesService = ServiceCreationHelper.MakeFavouritesService(string.Empty);
            ResultEventArgs<Users> result = null;
            favoritesService.GetFavorersOfShopCompleted += (s, e) => result = e;

            // ACT
            favoritesService.GetFavorersOfShop(Constants.TestName, 0, 10, DetailLevel.Low);

            // check the data
            TestHelpers.CheckResultFailure(result, Constants.EmptyApiKeyErrorMessage);
        }

        /// <summary>
        /// Test a negative offset
        /// </summary>
        [TestMethod]
        public void GetFavorersOfShopNegativeOffsetTest()
        {
            // ARRANGE
            IFavoritesService favoritesService = ServiceCreationHelper.MakeFavouritesService(Constants.DummyEtsyApiKey);
            ResultEventArgs<Users> result = null;
            favoritesService.GetFavorersOfShopCompleted += (s, e) => result = e;

            // ACT
            favoritesService.GetFavorersOfShop(Constants.TestName, -1, 10, DetailLevel.Low);

            // check the data
            TestHelpers.CheckResultFailure(result, "Negative offset of -1");
        }

        /// <summary>
        /// Test a negative offset
        /// </summary>
        [TestMethod]
        public void GetFavorersOfShopZeroLimitTest()
        {
            // ARRANGE
            IFavoritesService favoritesService = ServiceCreationHelper.MakeFavouritesService(Constants.DummyEtsyApiKey);
            ResultEventArgs<Users> result = null;
            favoritesService.GetFavorersOfShopCompleted += (s, e) => result = e;

            // ACT
            favoritesService.GetFavorersOfShop(Constants.TestName, 0, 0, DetailLevel.Low);


            // check the data
            TestHelpers.CheckResultFailure(result, "Bad limit of 0");
        }
    }
}
