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

            RgbColor testColor = new RgbColor("76B3DF");

            // ACT
            listingsService.GetListingsByColor(testColor, 10, 0, 10, DetailLevel.Low);

            // check the data
            TestHelpers.CheckResultFailure(result);
        }

        /// <summary>
        /// Test missing API key
        /// </summary>
        [TestMethod]
        public void GetListingsByColorWiggleTooLargeTest()
        {
            // ARRANGE
            IListingsService listingsService = ServiceCreationHelper.MakeListingsService(string.Empty);
            ResultEventArgs<Listings> result = null;
            listingsService.GetListingsByColorCompleted += (s, e) => result = e;

            RgbColor testColor = new RgbColor("76B3DF");

            // ACT
            listingsService.GetListingsByColor(testColor, 100, 0, 10, DetailLevel.Low);

            // check the data
            TestHelpers.CheckResultFailure(result);
        }
    }
}
