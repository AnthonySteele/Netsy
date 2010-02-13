//-----------------------------------------------------------------------
// <copyright file="DispatchedGiftService.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.UI.DispatchedServices
{
    using System;
    using System.Windows.Threading;

    using Netsy.DataModel;
    using Netsy.Helpers;
    using Netsy.Interfaces;

    /// <summary>
    /// Gift service wrapped to use a dispatcher 
    /// To put the results back on the Dispatcher's thread
    /// </summary>
    public class DispatchedGiftService : DispatchedService, IGiftService
    {
        /// <summary>
        /// The wrapped service
        /// </summary>
        private readonly IGiftService wrappedService;

        /// <summary>
        /// Initializes a new instance of the DispatchedGiftService class
        /// </summary>
        /// <param name="wrappedService">the wrapped service</param>
        /// <param name="dispatcher">the thread dispatcher</param>
        public DispatchedGiftService(IGiftService wrappedService, Dispatcher dispatcher) 
            : base(dispatcher)
        {
            if (wrappedService == null)
            {
                throw new ArgumentNullException("wrappedService");
            }

            this.wrappedService = wrappedService;

            this.wrappedService.GetGiftGuideListingsCompleted += (s, e) => this.DispatchEvent(this.GetGiftGuideListingsCompleted, s, e);
            this.wrappedService.GetGiftGuidesCompleted += (s, e) => this.DispatchEvent(this.GetGiftGuidesCompleted, s, e);
        }

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
            return this.wrappedService.GetGiftGuides();
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
            return this.wrappedService.GetGiftGuideListings(guideId, offset, limit, detailLevel);
        }
    }
}
