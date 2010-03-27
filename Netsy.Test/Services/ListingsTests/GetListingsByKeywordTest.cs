//-----------------------------------------------------------------------
// <copyright file="GetListingsByKeywordTest.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.Test.Services.ListingsTests
{
    using System.Collections.Generic;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Netsy.DataModel;
    using Netsy.Helpers;
    using Netsy.Interfaces;
    using Netsy.Test;

    /// <summary>
    /// Test the GetListingsByKeyword Api function
    /// </summary>
    [TestClass]
    public class GetListingsByKeywordTest
    {
        /// <summary>
        /// Test missing API key
        /// </summary>
        [TestMethod]
        public void GetListingsByKeywordApiKeyMissingTest()
        {
            // ARRANGE
            IListingsService listingsService = ServiceCreationHelper.MakeListingsService(string.Empty);
            ResultEventArgs<Listings> result = null;
            listingsService.GetListingsByKeywordCompleted += (s, e) => result = e;

            // ACT
            listingsService.GetListingsByKeyword(Constants.TestWords, SortField.Created, SortOrder.Up, null, null, false, 0, 10, DetailLevel.Low);

            // check the data
            TestHelpers.CheckResultFailure(result);
        }

        /// <summary>
        /// Test invalid price ranges
        /// </summary>
        [TestMethod]
        public void GetListingsByKeywordInvalidPriceRangeTest()
        {
            // ARRANGE
            IListingsService listingsService = ServiceCreationHelper.MakeListingsService(Constants.DummyEtsyApiKey);
            ResultEventArgs<Listings> result = null;
            listingsService.GetListingsByKeywordCompleted += (s, e) => result = e;

            // ACT
            listingsService.GetListingsByKeyword(Constants.TestWords, SortField.Created, SortOrder.Up, 100, 10, false, 0, 10, DetailLevel.Low);

            // check the data
            TestHelpers.CheckResultFailure(result, "Invalid price range");
        }

        /// <summary>
        /// Test invalid price ranges
        /// </summary>
        [TestMethod]
        public void GetListingsByKeywordNoKeywordsTest()
        {
            // ARRANGE
            IListingsService listingsService = ServiceCreationHelper.MakeListingsService(Constants.DummyEtsyApiKey);
            ResultEventArgs<Listings> result = null;
            listingsService.GetListingsByKeywordCompleted += (s, e) => result = e;

            // ACT
            listingsService.GetListingsByKeyword(new List<string>(), SortField.Created, SortOrder.Up, null, null, false, 0, 10, DetailLevel.Low);

            // check the data
            TestHelpers.CheckResultFailure(result, "No keywords");
        }

        /// <summary>
        /// Test a negative offset
        /// </summary>
        [TestMethod]
        public void GetListingsByKeywordNegativeOffsetTest()
        {
            // ARRANGE
            IListingsService listingsService = ServiceCreationHelper.MakeListingsService(Constants.DummyEtsyApiKey);
            ResultEventArgs<Listings> result = null;
            listingsService.GetListingsByKeywordCompleted += (s, e) => result = e;

            // ACT
            listingsService.GetListingsByKeyword(Constants.TestWords, SortField.Created, SortOrder.Up,
                null, null, false, -1, 10, DetailLevel.Low);

            // check the data
            TestHelpers.CheckResultFailure(result, "Negative offset of -1");
        }

        /// <summary>
        /// Test a negative offset
        /// </summary>
        [TestMethod]
        public void GetListingsByKeywordZeroLimitTest()
        {
            // ARRANGE
            IListingsService listingsService = ServiceCreationHelper.MakeListingsService(Constants.DummyEtsyApiKey);
            ResultEventArgs<Listings> result = null;
            listingsService.GetListingsByKeywordCompleted += (s, e) => result = e;

            // ACT
            listingsService.GetListingsByKeyword(Constants.TestWords, SortField.Created, SortOrder.Up, 
                null, null, false, 0, 0, DetailLevel.Low);

            // check the data
            TestHelpers.CheckResultFailure(result, "Bad limit of 0");
        }
    }
}
