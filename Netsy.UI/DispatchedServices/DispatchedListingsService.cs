//-----------------------------------------------------------------------
// <copyright file="DispatchedListingsService.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.UI.DispatchedServices
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Threading;

    using Netsy.DataModel;
    using Netsy.Helpers;
    using Netsy.Interfaces;

    /// <summary>
    /// Listings service wrapped to use a dispatcher 
    /// To put the results back on the Dispatcher's thread
    /// </summary>
    public class DispatchedListingsService : IListingsService
    {
        /// <summary>
        /// The wrapped service
        /// </summary>
        private readonly IListingsService wrappedService;

        /// <summary>
        /// The thread dispatcher
        /// </summary>
        private readonly Dispatcher dispatcher;

        /// <summary>
        /// Initializes a new instance of the DispatchedListingsService class
        /// </summary>
        /// <param name="wrappedService">the wrapped service</param>
        /// <param name="dispatcher">the thread dispatcher</param>
        public DispatchedListingsService(IListingsService wrappedService, Dispatcher dispatcher)
        {
            this.wrappedService = wrappedService;
            this.wrappedService.GetAllListingsCompleted += this.WrappedServiceGetAllListingsCompleted;
            this.wrappedService.GetListingDetailsCompleted += this.WrappedServiceGetListingDetailsCompleted;
            this.wrappedService.GetListingsByCategoryCompleted += this.WrappedServiceGetListingsByCategoryCompleted;
            this.wrappedService.GetListingsByColorAndKeywordsCompleted += this.WrappedServiceGetListingsByColorAndKeywordsCompleted;
            this.wrappedService.GetFrontFeaturedListingsCompleted += this.WrappedServiceGetFrontFeaturedListingsCompleted;
            this.wrappedService.GetListingsByColorCompleted += this.WrappedServiceGetListingsByColorCompleted;
            this.wrappedService.GetListingsByKeywordCompleted += this.WrappedServiceGetListingsByKeywordCompleted;
            this.wrappedService.GetListingsByMaterialsCompleted += this.WrappedServiceGetListingsByMaterialsCompleted;
            this.wrappedService.GetListingsByTagsCompleted += this.WrappedServiceGetListingsByTagsCompleted;

            this.dispatcher = dispatcher;
        }

        /// <summary>
        /// GetListingDetails completed event
        /// </summary>
        public event EventHandler<ResultEventArgs<Listings>> GetListingDetailsCompleted;

        /// <summary>
        /// GetAllListingscompleted event
        /// </summary>
        public event EventHandler<ResultEventArgs<Listings>> GetAllListingsCompleted;

        /// <summary>
        /// GetListingsByCategory completed event
        /// </summary>
        public event EventHandler<ResultEventArgs<Listings>> GetListingsByCategoryCompleted;

        /// <summary>
        /// GetListingsByColor completed event
        /// </summary>
        public event EventHandler<ResultEventArgs<Listings>> GetListingsByColorCompleted;

        /// <summary>
        /// GetListingsByColorAndKeywords completed event
        /// </summary>
        public event EventHandler<ResultEventArgs<Listings>> GetListingsByColorAndKeywordsCompleted;

        /// <summary>
        /// GetFrontFeaturedListings completed event
        /// </summary>
        public event EventHandler<ResultEventArgs<Listings>> GetFrontFeaturedListingsCompleted;

        /// <summary>
        /// GetListingsByKeyword completed event
        /// </summary>
        public event EventHandler<ResultEventArgs<Listings>> GetListingsByKeywordCompleted;

        /// <summary>
        /// GetListingsByMaterials completed event
        /// </summary>
        public event EventHandler<ResultEventArgs<Listings>> GetListingsByMaterialsCompleted;

        /// <summary>
        /// GetListingsByTags completed event
        /// </summary>
        public event EventHandler<ResultEventArgs<Listings>> GetListingsByTagsCompleted;

        /// <summary>
        /// Get the details of a listing.
        /// </summary>
        /// <param name="listingId">Specify the listing's numeric ID</param>
        /// <param name="detailLevel">Control how much information to return</param>
        /// <returns>the async state of the request</returns>
        public IAsyncResult GetListingDetails(int listingId, DetailLevel detailLevel)
        {
            return this.wrappedService.GetListingDetails(listingId, detailLevel);
        }

        /// <summary>
        /// Get all active listings on Etsy.
        /// </summary>
        /// <param name="sortOn">Specify the field to sort on</param>
        /// <param name="sortOrder">Specify the direction to sort on</param>
        /// <param name="offset">To page through large result sets</param>
        /// <param name="limit">Specify the number of results to return</param>
        /// <param name="detailLevel">control how much information to return</param>
        /// <returns>the async state of the request</returns>
        public IAsyncResult GetAllListings(SortField sortOn, SortOrder sortOrder, int offset, int limit, DetailLevel detailLevel)
        {
            return this.wrappedService.GetAllListings(sortOn, sortOrder, offset, limit, detailLevel);
        }

        /// <summary>
        /// Search for listings by category.
        /// </summary>
        /// <param name="category">the category name</param>
        /// <param name="sortOn">Specify the field to sort on</param>
        /// <param name="sortOrder">Specify the direction to sort on</param>
        /// <param name="offset">To page through large result sets</param>
        /// <param name="limit">Specify the number of results to return</param>
        /// <param name="detailLevel">control how much information to return</param>
        /// <returns>the async state of the request</returns>
        public IAsyncResult GetListingsByCategory(string category, SortField sortOn, SortOrder sortOrder, int offset, int limit, DetailLevel detailLevel)
        {
            return this.wrappedService.GetListingsByCategory(category, sortOn, sortOrder, offset, limit, detailLevel);
        }

        /// <summary>
        /// Search for listings by average color of primary image.
        /// </summary>
        /// <param name="color">The average color of primary image</param>
        /// <param name="wiggle">Specify the degree of tolerance for color matching; where 0 is the most accurate, and 15 is the leas</param>
        /// <param name="offset">To page through large result sets</param>
        /// <param name="limit">Specify the number of results to return</param>
        /// <param name="detailLevel">control how much information to return</param>
        /// <returns>the async state of the request</returns>
        public IAsyncResult GetListingsByColor(EtsyColor color, int wiggle, int offset, int limit, DetailLevel detailLevel)
        {
            return this.wrappedService.GetListingsByColor(color, wiggle, offset, limit, detailLevel);
        }

        /// <summary>
        /// Search for listings by keywords and average color of primary image.
        /// </summary>
        /// <param name="keywords">Specify keywords to search on, separated by spaces or semicolons. You can also use the operators AND and NOT to control keyword matching.</param>
        /// <param name="color">Specify an HSV color</param>
        /// <param name="wiggle">Specify the degree of tolerance for color matching; where 0 is the most accurate, and 15 is the least.</param>
        /// <param name="offset">To page through large result sets</param>
        /// <param name="limit">Specify the number of results to return</param>
        /// <param name="detailLevel">control how much information to return</param>
        /// <returns>the async state of the request</returns>
        public IAsyncResult GetListingsByColorAndKeywords(IEnumerable<string> keywords, EtsyColor color, int wiggle, int offset, int limit, DetailLevel detailLevel)
        {
            return this.wrappedService.GetListingsByColorAndKeywords(keywords, color, wiggle, offset, limit, detailLevel);
        }

        /// <summary>
        /// Get the featured listings on the front page for the current day.
        /// </summary>
        /// <param name="offset">To page through large result sets</param>
        /// <param name="limit">Specify the number of results to return</param>
        /// <param name="detailLevel">control how much information to return</param>
        /// <returns>the async state of the request</returns>
        public IAsyncResult GetFrontFeaturedListings(int offset, int limit, DetailLevel detailLevel)
        {
            return this.wrappedService.GetFrontFeaturedListings(offset, limit, detailLevel);
        }

        /// <summary>
        /// Search for listings by keyword.
        /// </summary>
        /// <param name="searchTerms">Specify keywords to search on, separated by spaces or semicolons. You can also use the operators AND and NOT to control keyword matching.</param>
        /// <param name="sortOn">Specify the field to sort on</param>
        /// <param name="sortOrder">Specify the direction to sort on </param>
        /// <param name="minPrice">Minimum for restricting price ranges. Values are in US dollars and may include cents.</param>
        /// <param name="maxPrice">Maximum for restricting price ranges. Values are in US dollars and may include cents.</param>
        /// <param name="searchDescription">If true, listing descriptions will count towards search matches. (This may produce less relevant results.)</param>
        /// <param name="offset">To page through large result sets</param>
        /// <param name="limit">Specify the number of results to return</param>
        /// <param name="detailLevel">control how much information to return</param>
        /// <returns>the async state of the request</returns>
        public IAsyncResult GetListingsByKeyword(IEnumerable<string> searchTerms, SortField sortOn, SortOrder sortOrder, decimal? minPrice, decimal? maxPrice, bool searchDescription, int offset, int limit, DetailLevel detailLevel)
        {
            return this.wrappedService.GetListingsByKeyword(searchTerms, sortOn, sortOrder, minPrice, maxPrice, searchDescription, offset, limit, detailLevel);
        }

        /// <summary>
        /// Search for listings by materials used.
        /// </summary>
        /// <param name="materials">Specify one or more materials, separated by spaces or semicolons.</param>
        /// <param name="sortOn">Specify the field to sort on</param>
        /// <param name="sortOrder">Specify the direction to sort on </param>
        /// <param name="offset">To page through large result sets</param>
        /// <param name="limit">Specify the number of results to return</param>
        /// <param name="detailLevel">control how much information to return</param>
        /// <returns>the async state of the request</returns>
        public IAsyncResult GetListingsByMaterials(IEnumerable<string> materials, SortField sortOn, SortOrder sortOrder, int offset, int limit, DetailLevel detailLevel)
        {
            return this.wrappedService.GetListingsByMaterials(materials, sortOn, sortOrder, offset, limit, detailLevel);
        }

        /// <summary>
        /// Search for listings by tags.
        /// </summary>
        /// <param name="tags">Specify one or more tags, separated by spaces or semicolons.</param>
        /// <param name="sortOn">Specify the field to sort on</param>
        /// <param name="sortOrder">Specify the direction to sort on </param>
        /// <param name="offset">To page through large result sets</param>
        /// <param name="limit">Specify the number of results to return</param>
        /// <param name="detailLevel">control how much information to return</param>
        /// <returns>the async state of the request</returns>
        public IAsyncResult GetListingsByTags(IEnumerable<string> tags, SortField sortOn, SortOrder sortOrder, int offset, int limit, DetailLevel detailLevel)
        {
            return this.wrappedService.GetListingsByTags(tags, sortOn, sortOrder, offset, limit, detailLevel);
        }

        /// <summary>
        /// The wrapped service operation has completed
        /// </summary>
        /// <param name="sender">the event sender</param>
        /// <param name="e">the event args</param>
        private void WrappedServiceGetListingsByTagsCompleted(object sender, ResultEventArgs<Listings> e)
        {
            if (this.GetListingsByTagsCompleted != null)
            {
                Action completedSynch = () => this.GetListingsByTagsCompleted(sender, e);
                this.dispatcher.Invoke(completedSynch);
            }
        }

        /// <summary>
        /// The wrapped service operation has completed
        /// </summary>
        /// <param name="sender">the event sender</param>
        /// <param name="e">the event args</param>
        private void WrappedServiceGetListingsByMaterialsCompleted(object sender, ResultEventArgs<Listings> e)
        {
            if (this.GetListingsByMaterialsCompleted != null)
            {
                Action completedSynch = () => this.GetListingsByMaterialsCompleted(sender, e);
                this.dispatcher.Invoke(completedSynch);
            }
        }

        /// <summary>
        /// The wrapped service operation has completed
        /// </summary>
        /// <param name="sender">the event sender</param>
        /// <param name="e">the event args</param>
        private void WrappedServiceGetListingsByKeywordCompleted(object sender, ResultEventArgs<Listings> e)
        {
            if (this.GetListingsByKeywordCompleted != null)
            {
                Action completedSynch = () => this.GetListingsByKeywordCompleted(sender, e);
                this.dispatcher.Invoke(completedSynch);
            }
        }

        /// <summary>
        /// The wrapped service operation has completed
        /// </summary>
        /// <param name="sender">the event sender</param>
        /// <param name="e">the event args</param>
        private void WrappedServiceGetListingsByColorCompleted(object sender, ResultEventArgs<Listings> e)
        {
            if (this.GetListingsByColorCompleted != null)
            {
                Action completedSynch = () => this.GetListingsByColorCompleted(sender, e);
                this.dispatcher.Invoke(completedSynch);
            }
        }

        /// <summary>
        /// The wrapped service operation has completed
        /// </summary>
        /// <param name="sender">the event sender</param>
        /// <param name="e">the event args</param>
        private void WrappedServiceGetListingsByColorAndKeywordsCompleted(object sender, ResultEventArgs<Listings> e)
        {
            if (this.GetListingsByColorAndKeywordsCompleted != null)
            {
                Action completedSynch = () => this.GetListingsByColorAndKeywordsCompleted(sender, e);
                this.dispatcher.Invoke(completedSynch);
            }
        }

        /// <summary>
        /// The wrapped service operation has completed
        /// </summary>
        /// <param name="sender">the event sender</param>
        /// <param name="e">the event args</param>
        private void WrappedServiceGetFrontFeaturedListingsCompleted(object sender, ResultEventArgs<Listings> e)
        {
            if (this.GetFrontFeaturedListingsCompleted != null)
            {
                Action completedSynch = () => this.GetFrontFeaturedListingsCompleted(sender, e);
                this.dispatcher.Invoke(completedSynch);
            }
        }

        /// <summary>
        /// The wrapped service operation has completed
        /// </summary>
        /// <param name="sender">the event sender</param>
        /// <param name="e">the event args</param>
        private void WrappedServiceGetListingsByCategoryCompleted(object sender, ResultEventArgs<Listings> e)
        {
            if (this.GetListingsByCategoryCompleted != null)
            {
                Action completedSynch = () => this.GetListingsByCategoryCompleted(sender, e);
                this.dispatcher.Invoke(completedSynch);
            }
        }

        /// <summary>
        /// The wrapped service operation has completed
        /// </summary>
        /// <param name="sender">the event sender</param>
        /// <param name="e">the event args</param>
        private void WrappedServiceGetListingDetailsCompleted(object sender, ResultEventArgs<Listings> e)
        {
            if (this.GetListingDetailsCompleted != null)
            {
                Action completedSynch = () => this.GetListingDetailsCompleted(sender, e);
                this.dispatcher.Invoke(completedSynch);
            }
        }

        /// <summary>
        /// The wrapped service operation has completed
        /// </summary>
        /// <param name="sender">the event sender</param>
        /// <param name="e">the event args</param>
        private void WrappedServiceGetAllListingsCompleted(object sender, ResultEventArgs<Listings> e)
        {
            if (this.GetAllListingsCompleted != null)
            {
                Action completedSynch = () => this.GetAllListingsCompleted(sender, e);
                this.dispatcher.Invoke(completedSynch);
            }
        }
    }
}
