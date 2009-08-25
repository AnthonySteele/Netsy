//-----------------------------------------------------------------------
// <copyright file="GetFrontFeaturedListingsTest.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.IntegrationTest.Listings
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Netsy.Core;
    using Netsy.DataModel;
    using Netsy.DataModel.ListingData;
    using Netsy.Helpers;
    using Netsy.Interfaces;

    /// <summary>
    /// Test the GetFrontFeaturedListings Api function
    /// </summary>
    [TestClass]
    public class GetFrontFeaturedListingsTest
    {
        /// <summary>
        /// Test missing API key
        /// </summary>
        [TestMethod]
        public void GetFrontFeaturedListingsApiKeyMissingTest()
        {
            // ARRANGE
            ResultEventArgs<Listings> result = null;
            IListingsService listingsService = new ListingsService(new EtsyContext(string.Empty));
            listingsService.GetFrontFeaturedListingsCompleted += (s, e) => result = e;

            // ACT
            listingsService.GetFrontFeaturedListings(0, 10, DetailLevel.Low);

            // check the data
            NetsyData.CheckResultFailure(result);
        }
    }
}
