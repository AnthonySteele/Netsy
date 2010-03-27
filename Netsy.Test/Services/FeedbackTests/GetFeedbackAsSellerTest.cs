//-----------------------------------------------------------------------
// <copyright file="GetFeedbackAsSellerTest.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.Test.Services.FeedbackTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Netsy.DataModel;
    using Netsy.Helpers;
    using Netsy.Interfaces;
    using Netsy.Test;
    using Netsy.Test.Services;

    /// <summary>
    /// Test the GetFeedbackAsSeller API Function
    /// </summary>
    [TestClass]
    public class GetFeedbackAsSellerTest
    {
        /// <summary>
        /// Test missing API key
        /// </summary>
        [TestMethod]
        public void GetFeedbackAsSellerMissingApiKeyTest()
        {
            // ARRANGE
            IFeedbackService feedbackService = ServiceCreationHelper.MakeFeedbackService(string.Empty);
            ResultEventArgs<Feedbacks> result = null;
            feedbackService.GetFeedbackAsSellerCompleted += (s, e) => result = e;

            // ACT
            feedbackService.GetFeedbackAsSeller(Constants.TestId, 0, 10);

            // check the data
            TestHelpers.CheckResultFailure(result, Constants.EmptyApiKeyErrorMessage);
        }

        /// <summary>
        /// Test missing API key
        /// </summary>
        [TestMethod]
        public void GetFeedbackAsSellerByNameMissingApiKeyTest()
        {
            // ARRANGE
            IFeedbackService feedbackService = ServiceCreationHelper.MakeFeedbackService(string.Empty);
            ResultEventArgs<Feedbacks> result = null;
            feedbackService.GetFeedbackAsSellerCompleted += (s, e) => result = e;

            // ACT
            feedbackService.GetFeedbackAsSeller(Constants.TestName, 0, 10);

            // check the data
            TestHelpers.CheckResultFailure(result, Constants.EmptyApiKeyErrorMessage);
        }

        /// <summary>
        /// Test a negative offset
        /// </summary>
        [TestMethod]
        public void GetFeedbackAsSellerNegativeOffsetTest()
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
        public void GetFeedbackAsSellerZeroLimitTest()
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
