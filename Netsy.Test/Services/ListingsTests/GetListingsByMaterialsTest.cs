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

            List<string> materials = new List<string>();

            // ACT
            listingsService.GetListingsByMaterials(materials, SortField.Created, SortOrder.Up, 0, 10, DetailLevel.Low);

            // check the data
            TestHelpers.CheckResultFailure(result);
        }
    }
}
