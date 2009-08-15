//-----------------------------------------------------------------------
// <copyright file="ListingsService.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.Core
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using Netsy.DataModel;
    using Netsy.DataModel.ListingData;
    using Netsy.Helpers;
    using Netsy.Interfaces;

    /// <summary>
    /// Implementation of the listings service
    /// </summary>
    public class ListingsService : IListingService   
    {
        /// <summary>
        /// the Etsy context data
        /// </summary>
        private readonly EtsyContext etsyContext;

        /// <summary>
        /// Initializes a new instance of the ListingsService class
        /// </summary>
        /// <param name="etsyContext">the etsy context to use</param>
        public ListingsService(EtsyContext etsyContext)
        {
            this.etsyContext = etsyContext;
        }

        #region IListingService Members

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
        /// <param name="listingId"></param>
        /// <param name="detailLevel"></param>
        /// <returns></returns>
        public IAsyncResult GetListingDetails(int listingId, DetailLevel detailLevel)
        {
            if (!ServiceHelper.TestCallPrerequisites(this, this.GetListingDetailsCompleted, this.etsyContext))
            {
                return null;
            }

            UriBuilder uriBuilder = UriBuilder.Start(this.etsyContext, "listings", listingId)
                .DetailLevel(detailLevel);

            return ServiceHelper.GenerateRequest(this, uriBuilder.Result(), this.GetListingDetailsCompleted);
        }

        /// <summary>
        /// Get all active listings on Etsy.
        /// </summary>
        /// <param name="sortOn"></param>
        /// <param name="sortOrder"></param>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        /// <param name="detailLevel"></param>
        /// <returns></returns>
        public IAsyncResult GetAllListings(SortField sortOn, SortOrder sortOrder, int offset, int limit, DetailLevel detailLevel)
        {
            if (!ServiceHelper.TestCallPrerequisites(this, this.GetAllListingsCompleted, this.etsyContext))
            {
                return null;
            }

            UriBuilder uriBuilder = UriBuilder.Start(this.etsyContext, "listings/all")
                .Sort(sortOn, sortOrder)
                .OffsetLimit(offset, limit)
                .DetailLevel(detailLevel);

            return ServiceHelper.GenerateRequest(this, uriBuilder.Result(), this.GetAllListingsCompleted);
        }

        /// <summary>
        /// Search for listings by category.
        /// </summary>
        /// <param name="category"></param>
        /// <param name="sortOn"></param>
        /// <param name="sortOrder"></param>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        /// <param name="detailLevel"></param>
        /// <returns></returns>
        public IAsyncResult GetListingsByCategory(string category, SortField sortOn, SortOrder sortOrder, int offset, int limit, DetailLevel detailLevel)
        {
            if (!ServiceHelper.TestCallPrerequisites(this, this.GetListingsByCategoryCompleted, this.etsyContext))
            {
                return null;
            }

            UriBuilder uriBuilder = UriBuilder.Start(this.etsyContext, "listings/category", category)
                .Sort(sortOn, sortOrder)
                .OffsetLimit(offset, limit)
                .DetailLevel(detailLevel); 
            
            return ServiceHelper.GenerateRequest(this, uriBuilder.Result(), this.GetListingsByCategoryCompleted);
        }

        /// <summary>
        /// Search for listings by average color of primary image.
        /// </summary>
        /// <param name="color"></param>
        /// <param name="wiggle"></param>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        /// <param name="detailLevel"></param>
        /// <returns></returns>
        public IAsyncResult GetListingsByColor(string color, int wiggle, int offset, int limit, DetailLevel detailLevel)
        {
            if (!ServiceHelper.TestCallPrerequisites(this, this.GetListingsByColorCompleted, this.etsyContext))
            {
                return null;
            }

            UriBuilder uriBuilder = UriBuilder.Start(this.etsyContext, "listings/color", color)
                .Param("wiggle", wiggle)
                .OffsetLimit(offset, limit)
                .DetailLevel(detailLevel);

            return ServiceHelper.GenerateRequest(this, uriBuilder.Result(), this.GetListingsByColorCompleted);
        }

        /// <summary>
        /// Search for listings by keywords and average color of primary image.
        /// </summary>
        /// <param name="keywords"></param>
        /// <param name="color"></param>
        /// <param name="wiggle"></param>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        /// <param name="detailLevel"></param>
        /// <returns></returns>
        public IAsyncResult GetListingsByColorAndKeywords(IEnumerable<string> keywords, string color, int wiggle, int offset, int limit, DetailLevel detailLevel)
        {
            if (!ServiceHelper.TestCallPrerequisites(this, this.GetListingsByColorAndKeywordsCompleted, this.etsyContext))
            {
                return null;
            }

            UriBuilder uriBuilder = UriBuilder.Start(this.etsyContext, "listings/color", color)
                .Append("/keywords/").Append(keywords)
                .Param("wiggle", wiggle)
                .OffsetLimit(offset, limit)
                .DetailLevel(detailLevel);

            return ServiceHelper.GenerateRequest(this, uriBuilder.Result(), this.GetListingsByColorAndKeywordsCompleted);
        }

        /// <summary>
        /// Get the featured listings on the front page for the current day.
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        /// <param name="detailLevel"></param>
        /// <returns></returns>
        public IAsyncResult GetFrontFeaturedListings(int offset, int limit, DetailLevel detailLevel)
        {
            if (!ServiceHelper.TestCallPrerequisites(this, this.GetFrontFeaturedListingsCompleted, this.etsyContext))
            {
                return null;
            }

            UriBuilder uriBuilder = UriBuilder.Start(this.etsyContext, "listings/featured/front")
                .OffsetLimit(offset, limit)
                .DetailLevel(detailLevel);

            return ServiceHelper.GenerateRequest(this, uriBuilder.Result(), this.GetFrontFeaturedListingsCompleted);
        }

        /// <summary>
        /// Search for listings by keyword.
        /// </summary>
        /// <param name="searchTerms"></param>
        /// <param name="sortOn"></param>
        /// <param name="sortOrder"></param>
        /// <param name="minPrince"></param>
        /// <param name="maxPrice"></param>
        /// <param name="searchDescription"></param>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        /// <param name="detailLevel"></param>
        /// <returns></returns>
        public IAsyncResult GetListingsByKeyword(IEnumerable<string> searchTerms, SortField sortOn, SortOrder sortOrder, decimal minPrince, decimal maxPrice, bool searchDescription, int offset, int limit, DetailLevel detailLevel)
        {
            if (!ServiceHelper.TestCallPrerequisites(this, this.GetListingsByKeywordCompleted, this.etsyContext))
            {
                return null;
            }

            UriBuilder uriBuilder = UriBuilder.Start(this.etsyContext, "listings/keywords")
                .Append(searchTerms)
                .Sort(sortOn, sortOrder)
                .Param("min_price", minPrince)
                .Param("max_price", maxPrice)
                .Param("search_description", searchDescription)
                .OffsetLimit(offset, limit)
                .DetailLevel(detailLevel);

            return ServiceHelper.GenerateRequest(this, uriBuilder.Result(), this.GetListingsByKeywordCompleted);
        }

        /// <summary>
        /// Search for listings by materials used.
        /// </summary>
        /// <param name="materials"></param>
        /// <param name="sortOn"></param>
        /// <param name="sortOrder"></param>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        /// <param name="detailLevel"></param>
        /// <returns></returns>
        public IAsyncResult GetListingsByMaterials(IEnumerable<string> materials, SortField sortOn, SortOrder sortOrder, int offset, int limit, DetailLevel detailLevel)
        {
            if (!ServiceHelper.TestCallPrerequisites(this, this.GetListingsByMaterialsCompleted, this.etsyContext))
            {
                return null;
            }

            UriBuilder uriBuilder = UriBuilder.Start(this.etsyContext, "listings/materials")
                .Append(materials)
                .Sort(sortOn, sortOrder)
                .OffsetLimit(offset, limit)
                .DetailLevel(detailLevel); 
            
            return ServiceHelper.GenerateRequest(this, uriBuilder.Result(), this.GetListingsByMaterialsCompleted);
        }

        /// <summary>
        /// Search for listings by tags.
        /// </summary>
        /// <param name="tags"></param>
        /// <param name="sortOn"></param>
        /// <param name="sortOrder"></param>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        /// <param name="detailLevel"></param>
        /// <returns></returns>
        public IAsyncResult GetListingsByTags(IEnumerable<string> tags, SortField sortOn, SortOrder sortOrder, int offset, int limit, DetailLevel detailLevel)
        {
            if (!ServiceHelper.TestCallPrerequisites(this, this.GetListingsByTagsCompleted, this.etsyContext))
            {
                return null;
            }

            UriBuilder uriBuilder = UriBuilder.Start(this.etsyContext, "listings/tags")
                .Append(tags)
                .Sort(sortOn, sortOrder)
                .OffsetLimit(offset, limit)
                .DetailLevel(detailLevel);

            return ServiceHelper.GenerateRequest(this, uriBuilder.Result(), this.GetListingsByTagsCompleted);
        }

        #endregion
    }
}
