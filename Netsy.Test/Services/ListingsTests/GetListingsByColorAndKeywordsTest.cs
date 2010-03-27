//-----------------------------------------------------------------------
// <copyright file="GetListingsByColorAndKeywordsTest.cs" company="AFS">
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
    /// Test the GetListingsByColorTest function on the listings service
    /// </summary>
    [TestClass]
    public class GetListingsByColorAndKeywordsTest
    {
        /// <summary>
        /// The default colour wiggle room
        /// </summary>
        private const int DefaultWiggle = 12;

        /// <summary>
        /// Test missing API key
        /// </summary>
        [TestMethod]
        public void GetListingsByColorAndKeywordsApiKeyMissingTest()
        {
            // ARRANGE
            IListingsService listingsService = ServiceCreationHelper.MakeListingsService(string.Empty);
            ResultEventArgs<Listings> result = null;
            listingsService.GetListingsByColorAndKeywordsCompleted += (s, e) => result = e;

            // ACT
            listingsService.GetListingsByColorAndKeywords(Constants.TestWords, Constants.TestColor, 
                DefaultWiggle, 0, 10, DetailLevel.Low);

            // check the data
            TestHelpers.CheckResultFailure(result, Constants.EmptyApiKeyErrorMessage);
        }

        /// <summary>
        /// Test wiggle too large
        /// </summary>
        [TestMethod]
        public void GetListingsByColorAndKeywordsWiggleTooLargeTest()
        {
            // ARRANGE
            IListingsService listingsService = ServiceCreationHelper.MakeListingsService(Constants.DummyEtsyApiKey);
            ResultEventArgs<Listings> result = null;
            listingsService.GetListingsByColorAndKeywordsCompleted += (s, e) => result = e;

            // ACT
            listingsService.GetListingsByColorAndKeywords(Constants.TestWords, Constants.TestColor, 
                100, 0, 10, DetailLevel.Low);

            // check the data
            TestHelpers.CheckResultFailure(result, Constants.WiggleTooLargeErrorMessage);
        }

        /// <summary>
        /// Test wiggle too large
        /// </summary>
        [TestMethod]
        public void GetListingsByColorAndKeywordsNoKeywordsTest()
        {
            // ARRANGE
            IListingsService listingsService = ServiceCreationHelper.MakeListingsService(Constants.DummyEtsyApiKey);
            ResultEventArgs<Listings> result = null;
            listingsService.GetListingsByColorAndKeywordsCompleted += (s, e) => result = e;

            // ACT
            listingsService.GetListingsByColorAndKeywords(new List<string>(), Constants.TestColor, 
                10, 0, 10, DetailLevel.Low);

            // check the data
            TestHelpers.CheckResultFailure(result, "No keywords");
        }

        /// <summary>
        /// Test a negative offset
        /// </summary>
        [TestMethod]
        public void GetListingsByColorAndKeywordsNegativeOffsetTest()
        {
            // ARRANGE
            IListingsService listingsService = ServiceCreationHelper.MakeListingsService(Constants.DummyEtsyApiKey);
            ResultEventArgs<Listings> result = null;
            listingsService.GetListingsByColorAndKeywordsCompleted += (s, e) => result = e;

            // ACT
            listingsService.GetListingsByColorAndKeywords(Constants.TestWords, Constants.TestColor,
                10, -1, 10, DetailLevel.Low);

            // check the data
            TestHelpers.CheckResultFailure(result, "Negative offset of -1");
        }

        /// <summary>
        /// Test a negative offset
        /// </summary>
        [TestMethod]
        public void GetListingsByColorAndKeywordsZeroLimitTest()
        {
            // ARRANGE
            IListingsService listingsService = ServiceCreationHelper.MakeListingsService(Constants.DummyEtsyApiKey);
            ResultEventArgs<Listings> result = null;
            listingsService.GetListingsByColorAndKeywordsCompleted += (s, e) => result = e;

            // ACT
            listingsService.GetListingsByColorAndKeywords(Constants.TestWords, Constants.TestColor,
                10, 0, 0, DetailLevel.Low);

            // check the data
            TestHelpers.CheckResultFailure(result, "Bad limit of 0");
        }
    }
}
