//-----------------------------------------------------------------------
// <copyright file="GetListingsByCategoryTest.cs" company="AFS">
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
    /// Test the GetListingsByCategory function on the listings service
    /// </summary>
    [TestClass]
    public class GetListingsByCategoryTest
    {
        /// <summary>
        /// Test missing API key
        /// </summary>
        [TestMethod]
        public void GetListingsByCategoryApiKeyMissingTest()
        {
            // ARRANGE
            IListingsService listingsService = ServiceCreationHelper.MakeListingsService(string.Empty);
            ResultEventArgs<Listings> result = null;
            listingsService.GetListingsByCategoryCompleted += (s, e) => result = e;

            // ACT
            listingsService.GetListingsByCategory(Constants.TestName, SortField.Created, SortOrder.Down, 0, 10, DetailLevel.Low);

            // check the data
            TestHelpers.CheckResultFailure(result);
        }
    }
}
