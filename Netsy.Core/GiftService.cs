//-----------------------------------------------------------------------
// <copyright file="GiftService.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 
namespace Netsy.Core
{
    using System;

    using Netsy.DataModel;
    using Netsy.DataModel.ListingData;
    using Netsy.Helpers;
    using Netsy.Interfaces;

    /// <summary>
    /// Implementation of the Feedback service
    /// </summary>
    public class GiftService : IGiftService
    {
        /// <summary>
        /// the Etsy context data
        /// </summary>
        private readonly EtsyContext etsyContext;

        /// <summary>
        /// Initializes a new instance of the GiftService class
        /// </summary>
        /// <param name="etsyContext">the etsy context to use</param>
        public GiftService(EtsyContext etsyContext)
        {
            this.etsyContext = etsyContext;
        }

        #region IGiftService Members

        /// <summary>
        /// GetGiftGuides completed event
        /// </summary>
        public event EventHandler<ResultEventArgs<Listings>> GetGiftGuidesCompleted;

        /// <summary>
        /// GetGiftGuideListings completed event
        /// </summary>
        public event EventHandler<ResultEventArgs<Listings>> GetGiftGuideListingsCompleted;

        /// <summary>
        /// Get a list of gift guides.
        /// </summary>
        /// <returns>The Async state of the request</returns>
        public IAsyncResult GetGiftGuides()
        {
            if (!ServiceHelper.TestCallPrerequisites(this, this.GetGiftGuidesCompleted, this.etsyContext))
            {
                return null;
            } 
            
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get the listings in a gift guide.
        /// </summary>
        /// <param name="guideId">Specify the numeric ID of a Gift Guide </param>
        /// <param name="offset">To page through large result sets, set offset to a multiple of limit</param>
        /// <param name="limit">Specify the number of results to return</param>
        /// <param name="detailLevel">Control how much information to return</param>
        /// <returns>The Async state of the request</returns>
        public IAsyncResult GetGiftGuideListings(int guideId, int offset, int limit, DetailLevel detailLevel)
        {
            if (!ServiceHelper.TestCallPrerequisites(this, this.GetGiftGuideListingsCompleted, this.etsyContext))
            {
                return null;
            }
            
            throw new NotImplementedException();
        }

        #endregion
    }
}
