//-----------------------------------------------------------------------
// <copyright file="DispatchedFavoritesService.cs" company="AFS">
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
    /// Favorites service wrapped to use a dispatcher 
    /// To put the results back on the Dispatcher's thread
    /// </summary>
    public class DispatchedFavoritesService : DispatchedService, IFavoritesService
    {
        /// <summary>
        /// The wrapped service
        /// </summary>
        private readonly IFavoritesService wrappedService;

        /// <summary>
        /// Initializes a new instance of the DispatchedFavoritesService class
        /// </summary>
        /// <param name="wrappedService">the wrapped service</param>
        /// <param name="dispatcher">the thread dispatcher</param>
        public DispatchedFavoritesService(IFavoritesService wrappedService, Dispatcher dispatcher)
            : base(dispatcher)
        {
            this.wrappedService = wrappedService;

            this.wrappedService.GetFavorersOfListingCompleted += (s, e) => this.DispatchEvent(this.GetFavorersOfListingCompleted, s, e);
            this.wrappedService.GetFavorersOfShopCompleted += (s, e) => this.DispatchEvent(this.GetFavorersOfShopCompleted, s, e);
            this.wrappedService.GetFavoriteListingsOfUserCompleted += (s, e) => this.DispatchEvent(this.GetFavoriteListingsOfUserCompleted, s, e);
            this.wrappedService.GetFavoriteShopsOfUserCompleted += (s, e) => this.DispatchEvent(this.GetFavoriteShopsOfUserCompleted, s, e);
        }

        /// <summary>
        /// GetFavorersOfListing completed event
        /// </summary>
        public event EventHandler<ResultEventArgs<Users>> GetFavorersOfListingCompleted;

        /// <summary>
        /// GetFavorersOfShop completed event
        /// </summary>
        public event EventHandler<ResultEventArgs<Users>> GetFavorersOfShopCompleted;

        /// <summary>
        /// GetFavoriteListingsOfUser completed event
        /// </summary>
        public event EventHandler<ResultEventArgs<Listings>> GetFavoriteListingsOfUserCompleted;

        /// <summary>
        /// GetFavoriteShopOfUser completed event
        /// </summary>
        public event EventHandler<ResultEventArgs<Shops>> GetFavoriteShopsOfUserCompleted;

        /// <summary>
        /// Get all the users who call this listing a favorite.
        /// </summary>
        /// <param name="listingId">the listing's numeric ID</param>
        /// <param name="offset">To page through large result sets, set offset to a multiple of limit</param>
        /// <param name="limit">Specify the number of results to return</param>
        /// <param name="detailLevel">Control how much information to return</param>
        /// <returns>The Async state of the request</returns>
        public IAsyncResult GetFavorersOfListing(int listingId, int offset, int limit, DetailLevel detailLevel)
        {
            return this.wrappedService.GetFavorersOfListing(listingId, offset, limit, detailLevel);
        }

        /// <summary>
        /// Get all the users who call this shop a favorite.
        /// </summary>
        /// <param name="shopId">the shop's numeric ID</param>
        /// <param name="offset">To page through large result sets, set offset to a multiple of limit</param>
        /// <param name="limit">Specify the number of results to return</param>
        /// <param name="detailLevel">Control how much information to return</param>
        /// <returns>The Async state of the request</returns>
        public IAsyncResult GetFavorersOfShop(int shopId, int offset, int limit, DetailLevel detailLevel)
        {
            return this.wrappedService.GetFavorersOfShop(shopId, offset, limit, detailLevel);
        }

        /// <summary>
        /// Get all the users who call this shop a favorite.
        /// </summary>
        /// <param name="shopName">the shop's name</param>
        /// <param name="offset">To page through large result sets, set offset to a multiple of limit</param>
        /// <param name="limit">Specify the number of results to return</param>
        /// <param name="detailLevel">Control how much information to return</param>
        /// <returns>The Async state of the request</returns>
        public IAsyncResult GetFavorersOfShop(string shopName, int offset, int limit, DetailLevel detailLevel)
        {
            return this.wrappedService.GetFavorersOfShop(shopName, offset, limit, detailLevel);
        }

        /// <summary>
        /// Get the favorite listings of a user.
        /// </summary>
        /// <param name="userId">the user's numeric ID</param>
        /// <param name="offset">To page through large result sets, set offset to a multiple of limit</param>
        /// <param name="limit">Specify the number of results to return</param>
        /// <param name="detailLevel">Control how much information to return</param>
        /// <returns>The Async state of the request</returns>
        public IAsyncResult GetFavoriteListingsOfUser(int userId, int offset, int limit, DetailLevel detailLevel)
        {
            return this.wrappedService.GetFavoriteListingsOfUser(userId, offset, limit, detailLevel);
        }

        /// <summary>
        /// Get the favorite listings of a user.
        /// </summary>
        /// <param name="userName">the user's name</param>
        /// <param name="offset">To page through large result sets, set offset to a multiple of limit</param>
        /// <param name="limit">Specify the number of results to return</param>
        /// <param name="detailLevel">Control how much information to return</param>
        /// <returns>The Async state of the request</returns>
        public IAsyncResult GetFavoriteListingsOfUser(string userName, int offset, int limit, DetailLevel detailLevel)
        {
            return this.wrappedService.GetFavoriteListingsOfUser(userName, offset, limit, detailLevel);
        }

        /// <summary>
        /// Get the favorite shops of a user.
        /// </summary>
        /// <param name="userId">the user's numeric ID</param>
        /// <param name="offset">To page through large result sets, set offset to a multiple of limit</param>
        /// <param name="limit">Specify the number of results to return</param>
        /// <param name="detailLevel">Control how much information to return</param>
        /// <returns>The Async state of the request</returns>
        public IAsyncResult GetFavoriteShopsOfUser(int userId, int offset, int limit, DetailLevel detailLevel)
        {
            return this.wrappedService.GetFavoriteShopsOfUser(userId, offset, limit, detailLevel);
        }

        /// <summary>
        /// Get the favorite shops of a user.
        /// </summary>
        /// <param name="userName">the user's name</param>
        /// <param name="offset">To page through large result sets, set offset to a multiple of limit</param>
        /// <param name="limit">Specify the number of results to return</param>
        /// <param name="detailLevel">Control how much information to return</param>
        /// <returns>The Async state of the request</returns>
        public IAsyncResult GetFavoriteShopsOfUser(string userName, int offset, int limit, DetailLevel detailLevel)
        {
            return this.wrappedService.GetFavoriteShopsOfUser(userName, offset, limit, detailLevel);
        }
    }
}
