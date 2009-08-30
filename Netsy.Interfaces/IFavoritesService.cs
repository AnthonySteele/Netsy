//-----------------------------------------------------------------------
// <copyright file="IFavoritesService.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 
namespace Netsy.Interfaces
{
    using System;

    using Netsy.DataModel;
    using Netsy.DataModel.ListingData;
    using Netsy.DataModel.ShopData;
    using Netsy.DataModel.UserData;
    using Netsy.Helpers;

    /// <summary>
    /// Interface to Favorites Commands on the etsy API
    /// </summary>
    public interface IFavoritesService
    {
        /// <summary>
        /// GetFavorersOfListing completed event
        /// </summary>
        event EventHandler<ResultEventArgs<Users>> GetFavorersOfListingCompleted;

        /// <summary>
        /// GetFavorersOfShop completed event
        /// </summary>
        event EventHandler<ResultEventArgs<Users>> GetFavorersOfShopCompleted;

        /// <summary>
        /// GetFavoriteListingsOfUser completed event
        /// </summary>
        event EventHandler<ResultEventArgs<Listings>> GetFavoriteListingsOfUserCompleted;

        /// <summary>
        /// GetFavoriteShopOfUser completed event
        /// </summary>
        event EventHandler<ResultEventArgs<Shops>> GetFavoriteShopsOfUserCompleted;

        /// <summary>
        /// Get all the users who call this listing a favorite.
        /// </summary>
        /// <param name="listingId">the listing's numeric ID</param>
        /// <param name="offset">To page through large result sets, set offset to a multiple of limit</param>
        /// <param name="limit">Specify the number of results to return</param>
        /// <param name="detailLevel">Control how much information to return</param>
        /// <returns>The Async state of the request</returns>
        IAsyncResult GetFavorersOfListing(int listingId, int offset, int limit, DetailLevel detailLevel);

        /// <summary>
        /// Get all the users who call this shop a favorite.
        /// </summary>
        /// <param name="shopId">the shop's numeric ID</param>
        /// <param name="offset">To page through large result sets, set offset to a multiple of limit</param>
        /// <param name="limit">Specify the number of results to return</param>
        /// <param name="detailLevel">Control how much information to return</param>
        /// <returns>The Async state of the request</returns>
        IAsyncResult GetFavorersOfShop(int shopId, int offset, int limit, DetailLevel detailLevel);

        /// <summary>
        /// Get all the users who call this shop a favorite.
        /// </summary>
        /// <param name="shopName">the shop's name</param>
        /// <param name="offset">To page through large result sets, set offset to a multiple of limit</param>
        /// <param name="limit">Specify the number of results to return</param>
        /// <param name="detailLevel">Control how much information to return</param>
        /// <returns>The Async state of the request</returns>
        IAsyncResult GetFavorersOfShop(string shopName, int offset, int limit, DetailLevel detailLevel);

        /// <summary>
        /// Get the favorite listings of a user.
        /// </summary>
        /// <param name="userId">the user's numeric ID</param>
        /// <param name="offset">To page through large result sets, set offset to a multiple of limit</param>
        /// <param name="limit">Specify the number of results to return</param>
        /// <param name="detailLevel">Control how much information to return</param>
        /// <returns>The Async state of the request</returns>
        IAsyncResult GetFavoriteListingsOfUser(int userId, int offset, int limit, DetailLevel detailLevel);

        /// <summary>
        /// Get the favorite listings of a user.
        /// </summary>
        /// <param name="userName">the user's name</param>
        /// <param name="offset">To page through large result sets, set offset to a multiple of limit</param>
        /// <param name="limit">Specify the number of results to return</param>
        /// <param name="detailLevel">Control how much information to return</param>
        /// <returns>The Async state of the request</returns>
        IAsyncResult GetFavoriteListingsOfUser(string userName, int offset, int limit, DetailLevel detailLevel);

        /// <summary>
        /// Get the favorite shops of a user.
        /// </summary>
        /// <param name="userId">the user's numeric ID</param>
        /// <param name="offset">To page through large result sets, set offset to a multiple of limit</param>
        /// <param name="limit">Specify the number of results to return</param>
        /// <param name="detailLevel">Control how much information to return</param>
        /// <returns>The Async state of the request</returns>
        IAsyncResult GetFavoriteShopsOfUser(int userId, int offset, int limit, DetailLevel detailLevel);

        /// <summary>
        /// Get the favorite shops of a user.
        /// </summary>
        /// <param name="userName">the user's name</param>
        /// <param name="offset">To page through large result sets, set offset to a multiple of limit</param>
        /// <param name="limit">Specify the number of results to return</param>
        /// <param name="detailLevel">Control how much information to return</param>
        /// <returns>The Async state of the request</returns>
        IAsyncResult GetFavoriteShopsOfUser(string userName, int offset, int limit, DetailLevel detailLevel);
    }
}
