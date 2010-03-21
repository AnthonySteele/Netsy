//-----------------------------------------------------------------------
// <copyright file="ListingHelper.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.IntegrationTest
{
    using System.Threading;

    using Netsy.DataModel;
    using Netsy.Interfaces;
    using Netsy.Services;
    using Netsy.Test;

    /// <summary>
    /// Helpers to retrieve Ids
    /// </summary>
    public static class TestIdHelper
    {
        /// <summary>
        /// The stored listing id from a previous call
        /// </summary>
        private static int cachedListingId;

        /// <summary>
        /// Get a valid listing id for use in tests.
        /// Any one will do so get one off the front features listing
        /// </summary>
        /// <returns>a valid listing id</returns>
        public static int RetrieveTestListingId()
        {
            if (cachedListingId > 0)
            {
                return cachedListingId;
            }

            int result = 0;

            using (AutoResetEvent waitEvent = new AutoResetEvent(false))
            {
                IListingsService listingsService = new ListingsService(new EtsyContext(NetsyData.EtsyApiKey));
                listingsService.GetFrontFeaturedListingsCompleted += (s, e) =>
                {
                    result = e.ResultValue.Results[0].ListingId;
                    waitEvent.Set();
                };

                listingsService.GetFrontFeaturedListings(0, 1, DetailLevel.Low);
                waitEvent.WaitOne(Constants.WaitTimeout);
            }

            cachedListingId = result;
            return result;
        }
    }
}
