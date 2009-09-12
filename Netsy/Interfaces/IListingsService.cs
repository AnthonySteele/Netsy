//-----------------------------------------------------------------------
// <copyright file="IListingsService.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.Interfaces
{
    using System;
    using System.Collections.Generic;

    using Netsy.DataModel;
    using Netsy.Helpers;

    /// <summary>
    /// Interface to listings functions on the Etsy API
    /// </summary>
    public interface IListingsService
    {
        /// <summary>
        /// GetListingDetails completed event
        /// </summary>
        event EventHandler<ResultEventArgs<Listings>> GetListingDetailsCompleted;

        /// <summary>
        /// GetAllListingscompleted event
        /// </summary>
        event EventHandler<ResultEventArgs<Listings>> GetAllListingsCompleted;

        /// <summary>
        /// GetListingsByCategory completed event
        /// </summary>
        event EventHandler<ResultEventArgs<Listings>> GetListingsByCategoryCompleted;

        /// <summary>
        /// GetListingsByColor completed event
        /// </summary>
        event EventHandler<ResultEventArgs<Listings>> GetListingsByColorCompleted;

        /// <summary>
        /// GetListingsByColorAndKeywords completed event
        /// </summary>
        event EventHandler<ResultEventArgs<Listings>> GetListingsByColorAndKeywordsCompleted;

        /// <summary>
        /// GetFrontFeaturedListings completed event
        /// </summary>
        event EventHandler<ResultEventArgs<Listings>> GetFrontFeaturedListingsCompleted;

        /// <summary>
        /// GetListingsByKeyword completed event
        /// </summary>
        event EventHandler<ResultEventArgs<Listings>> GetListingsByKeywordCompleted;

        /// <summary>
        /// GetListingsByMaterials completed event
        /// </summary>
        event EventHandler<ResultEventArgs<Listings>> GetListingsByMaterialsCompleted;

        /// <summary>
        /// GetListingsByTags completed event
        /// </summary>
        event EventHandler<ResultEventArgs<Listings>> GetListingsByTagsCompleted;

        /// <summary>
        /// Get the details of a listing.
        /// </summary>
        /// <param name="listingId">Specify the listing's numeric ID</param>
        /// <param name="detailLevel">Control how much information to return</param>
        /// <returns>the async state of the request</returns>
        IAsyncResult GetListingDetails(int listingId, DetailLevel detailLevel);

        /// <summary>
        /// Get all active listings on Etsy.
        /// </summary>
        /// <param name="sortOn">Specify the field to sort on</param>
        /// <param name="sortOrder">Specify the direction to sort on</param>
        /// <param name="offset">To page through large result sets</param>
        /// <param name="limit">Specify the number of results to return</param>
        /// <param name="detailLevel">control how much information to return</param>
        /// <returns>the async state of the request</returns>
        IAsyncResult GetAllListings(SortField sortOn, SortOrder sortOrder, int offset, int limit, DetailLevel detailLevel);
        
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
        IAsyncResult GetListingsByCategory(string category, SortField sortOn, SortOrder sortOrder, int offset, int limit, DetailLevel detailLevel);
        
        /// <summary>
        /// Search for listings by average color of primary image.
        /// </summary>
        /// <param name="color">The average color of primary image</param>
        /// <param name="wiggle">Specify the degree of tolerance for color matching; where 0 is the most accurate, and 15 is the leas</param>
        /// <param name="offset">To page through large result sets</param>
        /// <param name="limit">Specify the number of results to return</param>
        /// <param name="detailLevel">control how much information to return</param>
        /// <returns>the async state of the request</returns>
        IAsyncResult GetListingsByColor(EtsyColor color, int wiggle, int offset, int limit, DetailLevel detailLevel);
        
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
        IAsyncResult GetListingsByColorAndKeywords(IEnumerable<string> keywords, EtsyColor color, int wiggle, int offset, int limit, DetailLevel detailLevel);
        
        /// <summary>
        /// Get the featured listings on the front page for the current day.
        /// </summary>
        /// <param name="offset">To page through large result sets</param>
        /// <param name="limit">Specify the number of results to return</param>
        /// <param name="detailLevel">control how much information to return</param>
        /// <returns>the async state of the request</returns>
        IAsyncResult GetFrontFeaturedListings(int offset, int limit, DetailLevel detailLevel);
        
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
        IAsyncResult GetListingsByKeyword(IEnumerable<string> searchTerms, SortField sortOn, SortOrder sortOrder, decimal? minPrice, decimal? maxPrice, bool searchDescription, int offset, int limit, DetailLevel detailLevel);
        
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
        IAsyncResult GetListingsByMaterials(IEnumerable<string> materials, SortField sortOn, SortOrder sortOrder, int offset, int limit, DetailLevel detailLevel);
        
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
        IAsyncResult GetListingsByTags(IEnumerable<string> tags, SortField sortOn, SortOrder sortOrder, int offset, int limit, DetailLevel detailLevel);
    }
}
