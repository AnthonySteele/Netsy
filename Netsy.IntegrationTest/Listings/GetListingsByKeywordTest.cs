//-----------------------------------------------------------------------
// <copyright file="GetListingsByKeywordTest.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.IntegrationTest.Listings
{
    using System.Collections.Generic;
    using System.Net;
    using System.Threading;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Netsy.Core;
    using Netsy.DataModel;
    using Netsy.DataModel.ListingData;
    using Netsy.Helpers;
    using Netsy.Interfaces;

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
            ResultEventArgs<Listings> result = null;
            IListingsService listingsService = new ListingsService(new EtsyContext(string.Empty));
            listingsService.GetListingsByKeywordCompleted += (s, e) => result = e;

            List<string> searchTerms = new List<string>();

            // ACT
            listingsService.GetListingsByKeyword(searchTerms, SortField.Created, SortOrder.Up, null, null, false, 0, 10, DetailLevel.Low);

            // check the data
            NetsyData.CheckResultFailure(result);
        }

        /// <summary>
        /// Test invalid price ranges
        /// </summary>
        [TestMethod]
        public void GetListingsByKeywordInvalidPriceRangeTest()
        {
            // ARRANGE
            ResultEventArgs<Listings> result = null;
            IListingsService listingsService = new ListingsService(new EtsyContext(NetsyData.EtsyApiKey));
            listingsService.GetListingsByKeywordCompleted += (s, e) => result = e;

            List<string> searchTerms = new List<string>();

            // ACT
            listingsService.GetListingsByKeyword(searchTerms, SortField.Created, SortOrder.Up, 100, 10, false, 0, 10, DetailLevel.Low);

            // check the data
            NetsyData.CheckResultFailure(result);
        }

        /// <summary>
        /// Test invalid API key
        /// </summary>
        [TestMethod]
        public void GetListingsByKeywordApiKeyInvalidTest()
        {
            // ARRANGE
            using (AutoResetEvent waitEvent = new AutoResetEvent(false))
            {
                ResultEventArgs<Listings> result = null;
                IListingsService listingsService = new ListingsService(new EtsyContext("InvalidKey"));
                listingsService.GetListingsByKeywordCompleted += (s, e) =>
                {
                    result = e;
                    waitEvent.Set();
                };
                List<string> searchTerms = new List<string> { "bags" };

                // ACT
                listingsService.GetListingsByKeyword(searchTerms, SortField.Created, SortOrder.Up, null, null, false, 0, 10, DetailLevel.Low);
                bool signalled = waitEvent.WaitOne(NetsyData.WaitTimeout);

                // ASSERT
                // check that the event was fired, did not time out
                Assert.IsTrue(signalled, "Not signalled");

                // check the data
                Assert.IsNotNull(result);

                // check the data - should fail
                Assert.IsNotNull(result);
                Assert.IsNotNull(result.ResultStatus);
                Assert.IsFalse(result.ResultStatus.Success);
                Assert.AreEqual(WebExceptionStatus.ProtocolError, result.ResultStatus.WebStatus);
            }
        }
    }
}
