//-----------------------------------------------------------------------
// <copyright file="GetListingsByColorTest.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.Test.Services.ListingsTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Netsy.DataModel;
    using Netsy.Helpers;
    using Netsy.Interfaces;
    using Netsy.Test;

    /// <summary>
    /// Test the GetListingsByColorTest function on the listings service
    /// </summary>
    [TestClass]
    public class GetListingsByColorTest
    {
        /// <summary>
        /// Test missing API key
        /// </summary>
        [TestMethod]
        public void GetListingsByColorApiKeyMissingTest()
        {
            // ARRANGE
            IListingsService listingsService = ServiceCreationHelper.MakeListingsService(string.Empty);
            ResultEventArgs<Listings> result = null;
            listingsService.GetListingsByColorCompleted += (s, e) => result = e;

            // ACT
            listingsService.GetListingsByColor(Constants.TestColor, 10, 0, 10, DetailLevel.Low);

            // check the data
            TestHelpers.CheckResultFailure(result, Constants.EmptyApiKeyErrorMessage);
        }

        /// <summary>
        /// Test missing API key
        /// </summary>
        [TestMethod]
        public void GetListingsByColorWiggleTooLargeTest()
        {
            // ARRANGE
            IListingsService listingsService = ServiceCreationHelper.MakeListingsService(Constants.DummyEtsyApiKey);
            ResultEventArgs<Listings> result = null;
            listingsService.GetListingsByColorCompleted += (s, e) => result = e;

            // ACT
            listingsService.GetListingsByColor(Constants.TestColor, 100, 0, 10, DetailLevel.Low);

            // check the data
            TestHelpers.CheckResultFailure(result, Constants.WiggleTooLargeErrorMessage);
        }

        /// <summary>
        /// Test a negative offset
        /// </summary>
        [TestMethod]
        public void GetListingsByColorNegativeOffsetTest()
        {
            // ARRANGE
            IListingsService listingsService = ServiceCreationHelper.MakeListingsService(Constants.DummyEtsyApiKey);
            ResultEventArgs<Listings> result = null;
            listingsService.GetListingsByColorCompleted += (s, e) => result = e;

            // ACT
            listingsService.GetListingsByColor(Constants.TestColor, 10, -1, 10, DetailLevel.Low);


            // check the data
            TestHelpers.CheckResultFailure(result, "Negative offset of -1");
        }

        /// <summary>
        /// Test a negative offset
        /// </summary>
        [TestMethod]
        public void GetListingsByColorZeroLimitTest()
        {
            // ARRANGE
            IListingsService listingsService = ServiceCreationHelper.MakeListingsService(Constants.DummyEtsyApiKey);
            ResultEventArgs<Listings> result = null;
            listingsService.GetListingsByColorCompleted += (s, e) => result = e;

            // ACT
            listingsService.GetListingsByColor(Constants.TestColor, 10, 0, 0, DetailLevel.Low);

            // check the data
            TestHelpers.CheckResultFailure(result, "Bad limit of 0");
        }
    }
}
