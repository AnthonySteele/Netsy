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

            List<string> searchTerms = new List<string>();

            // ACT
            listingsService.GetListingsByKeyword(searchTerms, SortField.Created, SortOrder.Up, null, null, false, 0, 10, DetailLevel.Low);

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
            IListingsService listingsService = ServiceCreationHelper.MakeListingsService(string.Empty);
            ResultEventArgs<Listings> result = null;
            listingsService.GetListingsByKeywordCompleted += (s, e) => result = e;

            List<string> searchTerms = new List<string>();

            // ACT
            listingsService.GetListingsByKeyword(searchTerms, SortField.Created, SortOrder.Up, 100, 10, false, 0, 10, DetailLevel.Low);

            // check the data
            TestHelpers.CheckResultFailure(result);
        }
    }
}
