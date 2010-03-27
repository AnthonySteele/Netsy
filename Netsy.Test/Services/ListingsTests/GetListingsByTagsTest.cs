//-----------------------------------------------------------------------
// <copyright file="GetListingsByTagsTest.cs" company="AFS">
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
    /// Test the GetListingsByTags Api function
    /// </summary>
    [TestClass]
    public class GetListingsByTagsTest
    {
        /// <summary>
        /// Test missing API key
        /// </summary>
        [TestMethod]
        public void GetListingsByTagsApiKeyMissingTest()
        {
            // ARRANGE
            IListingsService listingsService = ServiceCreationHelper.MakeListingsService(string.Empty);
            ResultEventArgs<Listings> result = null;
            listingsService.GetListingsByTagsCompleted += (s, e) => result = e;

            // ACT
            listingsService.GetListingsByTags(Constants.TestWords, SortField.Created, SortOrder.Up, 0, 10, DetailLevel.Low);

            // check the data
            TestHelpers.CheckResultFailure(result, Constants.EmptyApiKeyErrorMessage);
        }

        /// <summary>
        /// Test missing tags
        /// </summary>
        [TestMethod]
        public void GetListingsByTagsTagsMissingTest()
        {
            // ARRANGE
            IListingsService listingsService = ServiceCreationHelper.MakeListingsService(Constants.DummyEtsyApiKey);
            ResultEventArgs<Listings> result = null;
            listingsService.GetListingsByTagsCompleted += (s, e) => result = e;

            // ACT
            listingsService.GetListingsByTags(new List<string>(), SortField.Created, SortOrder.Up, 0, 10, DetailLevel.Low);

            // check the data
            TestHelpers.CheckResultFailure(result, "No tags");
        }

        /// <summary>
        /// Test a negative offset
        /// </summary>
        [TestMethod]
        public void GetListingsByTagsNegativeOffsetTest()
        {
            // ARRANGE
            IListingsService listingsService = ServiceCreationHelper.MakeListingsService(Constants.DummyEtsyApiKey);
            ResultEventArgs<Listings> result = null;
            listingsService.GetListingsByTagsCompleted += (s, e) => result = e;

            // ACT
            listingsService.GetListingsByTags(Constants.TestWords, SortField.Created, SortOrder.Up, -1, 10, DetailLevel.Low);

            // check the data
            TestHelpers.CheckResultFailure(result, "Negative offset of -1");
        }

        /// <summary>
        /// Test a negative offset
        /// </summary>
        [TestMethod]
        public void GetListingsByTagsZeroLimitTest()
        {
            // ARRANGE
            IListingsService listingsService = ServiceCreationHelper.MakeListingsService(Constants.DummyEtsyApiKey);
            ResultEventArgs<Listings> result = null;
            listingsService.GetListingsByTagsCompleted += (s, e) => result = e;

            // ACT
            listingsService.GetListingsByTags(Constants.TestWords, SortField.Created, SortOrder.Up, 0, 0, DetailLevel.Low);

            // check the data
            TestHelpers.CheckResultFailure(result, "Bad limit of 0");
        }
    }
}
