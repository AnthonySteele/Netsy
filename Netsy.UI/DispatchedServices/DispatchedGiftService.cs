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
    public class DispatchedGiftService : IGiftService
    {
        /// <summary>
        /// The wrapped service
        /// </summary>
        private readonly IGiftService wrappedService;

        /// <summary>
        /// The thread dispatcher
        /// </summary>
        private readonly Dispatcher dispatcher;

        /// <summary>
        /// Initializes a new instance of the DispatchedGiftService class
        /// </summary>
        /// <param name="wrappedService">the wrapped service</param>
        /// <param name="dispatcher">the thread dispatcher</param>
        public DispatchedGiftService(IGiftService wrappedService, Dispatcher dispatcher)
        {
            this.wrappedService = wrappedService;
            this.wrappedService.GetGiftGuideListingsCompleted += this.WrappedServiceGetGiftGuideListingsCompleted;
            this.wrappedService.GetGiftGuidesCompleted += this.WrappedServiceGetGiftGuidesCompleted;

            this.dispatcher = dispatcher;
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

        /// <summary>
        /// The wrapped service operation has completed
        /// </summary>
        /// <param name="sender">the event sender</param>
        /// <param name="e">the event args</param>
        private void WrappedServiceGetGiftGuidesCompleted(object sender, ResultEventArgs<Listings> e)
        {
            if (this.GetGiftGuidesCompleted != null)
            {
                Action completedSynch = () => this.GetGiftGuidesCompleted(sender, e);
                this.dispatcher.Invoke(completedSynch);
            }
        }

        /// <summary>
        /// The wrapped service operation has completed
        /// </summary>
        /// <param name="sender">the event sender</param>
        /// <param name="e">the event args</param>
        private void WrappedServiceGetGiftGuideListingsCompleted(object sender, ResultEventArgs<Listings> e)
        {
            if (this.GetGiftGuideListingsCompleted != null)
            {
                Action completedSynch = () => this.GetGiftGuideListingsCompleted(sender, e);
                this.dispatcher.Invoke(completedSynch);
            }
        }
    }
}
