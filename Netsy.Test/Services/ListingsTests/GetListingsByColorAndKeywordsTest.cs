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

            RgbColor testColor = new RgbColor("76B3DF");

            // ACT
            listingsService.GetListingsByColorAndKeywords(TestKeywords(), testColor, DefaultWiggle, 0, 10, DetailLevel.Low);

            // check the data
            TestHelpers.CheckResultFailure(result);
        }

        /// <summary>
        /// Test missing API key
        /// </summary>
        [TestMethod]
        public void GetListingsByColorAndKeywordsWiggleTooLargeTest()
        {
            // ARRANGE
            IListingsService listingsService = ServiceCreationHelper.MakeListingsService(string.Empty);
            ResultEventArgs<Listings> result = null;
            listingsService.GetListingsByColorAndKeywordsCompleted += (s, e) => result = e;

            RgbColor testColor = new RgbColor("76B3DF");

            // ACT
            listingsService.GetListingsByColorAndKeywords(TestKeywords(), testColor, 100, 0, 10, DetailLevel.Low);

            // check the data
            TestHelpers.CheckResultFailure(result);
        }

        /// <summary>
        /// Get test keywords
        /// </summary>
        /// <returns>a list of test keywords</returns>
        private static IEnumerable<string> TestKeywords()
        {
            return new List<string> { "bags", "strap" };
        }
    }
}
