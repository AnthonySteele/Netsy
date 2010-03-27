//-----------------------------------------------------------------------
// <copyright file="GetListingsByMaterialsTest.cs" company="AFS">
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
    /// Test the GetListingsByMaterials Api function
    /// </summary>
    [TestClass]
    public class GetListingsByMaterialsTest
    {
        /// <summary>
        /// Test missing API key
        /// </summary>
        [TestMethod]
        public void GetListingsByMaterialsApiKeyMissingTest()
        {
            // ARRANGE
            IListingsService listingsService = ServiceCreationHelper.MakeListingsService(string.Empty);
            ResultEventArgs<Listings> result = null;
            listingsService.GetListingsByMaterialsCompleted += (s, e) => result = e;

            // ACT
            listingsService.GetListingsByMaterials(Constants.TestWords, SortField.Created, SortOrder.Up, 0, 10, DetailLevel.Low);

            // check the data
            TestHelpers.CheckResultFailure(result, Constants.EmptyApiKeyErrorMessage);
        }

        /// <summary>
        /// Test missing API key
        /// </summary>
        [TestMethod]
        public void GetListingsByMaterialsMaterialsMissingTest()
        {
            // ARRANGE
            IListingsService listingsService = ServiceCreationHelper.MakeListingsService(Constants.DummyEtsyApiKey);
            ResultEventArgs<Listings> result = null;
            listingsService.GetListingsByMaterialsCompleted += (s, e) => result = e;

            // ACT
            listingsService.GetListingsByMaterials(new List<string>(), SortField.Created, SortOrder.Up, 0, 10, DetailLevel.Low);

            // check the data
            TestHelpers.CheckResultFailure(result, "No materials");
        }

        /// <summary>
        /// Test a negative offset
        /// </summary>
        [TestMethod]
        public void GetListingsByMaterialsNegativeOffsetTest()
        {
            // ARRANGE
            IListingsService listingsService = ServiceCreationHelper.MakeListingsService(Constants.DummyEtsyApiKey);
            ResultEventArgs<Listings> result = null;
            listingsService.GetListingsByMaterialsCompleted += (s, e) => result = e;

            // ACT
            listingsService.GetListingsByMaterials(Constants.TestWords, SortField.Created, SortOrder.Up, -1, 10, DetailLevel.Low);

            // check the data
            TestHelpers.CheckResultFailure(result, "Negative offset of -1");
        }

        /// <summary>
        /// Test a negative offset
        /// </summary>
        [TestMethod]
        public void GetListingsByMaterialsZeroLimitTest()
        {
            // ARRANGE
            IListingsService listingsService = ServiceCreationHelper.MakeListingsService(Constants.DummyEtsyApiKey);
            ResultEventArgs<Listings> result = null;
            listingsService.GetListingsByMaterialsCompleted += (s, e) => result = e;

            // ACT
            listingsService.GetListingsByMaterials(Constants.TestWords, SortField.Created, SortOrder.Up, 0, 0, DetailLevel.Low);

            // check the data
            TestHelpers.CheckResultFailure(result, "Bad limit of 0");
        }
    }
}
