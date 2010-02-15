//-----------------------------------------------------------------------
// <copyright file="GiftService.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 
namespace Netsy.Services
{
    using System;

    using Netsy.Cache;
    using Netsy.DataModel;
    using Netsy.Helpers;
    using Netsy.Interfaces;
    using Netsy.Requests;

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
        /// the data retriever
        /// </summary>
        private readonly IDataRetriever dataRetriever;

        /// <summary>
        /// Initializes a new instance of the GiftService class
        /// </summary>
        /// <param name="apiKey">the api key to use</param>
        public GiftService(string apiKey)
            : this(new EtsyContext(apiKey), new DataRetriever())
        {
        }

        /// <summary>
        /// Initializes a new instance of the GiftService class
        /// </summary>
        /// <param name="etsyContext">the etsy context to use</param>
        public GiftService(EtsyContext etsyContext)
            : this(etsyContext, new DataRetriever())
        {
        }

        /// <summary>
        /// Initializes a new instance of the GiftService class
        /// </summary>
        /// <param name="etsyContext">the etsy context to use</param>
        /// <param name="dataRetriever">the data retriever use</param>
        public GiftService(EtsyContext etsyContext, IDataRetriever dataRetriever)
        {
            this.etsyContext = etsyContext;
            this.dataRetriever = dataRetriever;
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
            if (!RequestHelper.TestCallPrerequisites(this, this.GetGiftGuidesCompleted, this.etsyContext))
            {
                return null;
            }

            UriBuilder uriBuilder = UriBuilder.Start(this.etsyContext, "gift-guides");

            return this.dataRetriever.StartRetrieve(uriBuilder.Result(), this.GetGiftGuidesCompleted);
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
            if (!RequestHelper.TestCallPrerequisites(this, this.GetGiftGuideListingsCompleted, this.etsyContext))
            {
                return null;
            }

            UriBuilder uriBuilder = UriBuilder.Start(this.etsyContext, "gift-guides", guideId)
                .Append("/listings")
                .OffsetLimit(offset, limit)
                .DetailLevel(detailLevel);

            return this.dataRetriever.StartRetrieve(uriBuilder.Result(), this.GetGiftGuideListingsCompleted);
        }

        #endregion
    }
}
