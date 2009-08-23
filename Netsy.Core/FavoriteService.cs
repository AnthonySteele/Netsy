//-----------------------------------------------------------------------
// <copyright file="FavoriteService.cs" company="AFS">
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
    using Netsy.DataModel.ShopData;
    using Netsy.DataModel.UserData;
    using Netsy.Helpers;
    using Netsy.Interfaces;

    /// <summary>
    /// Implementation of the favorite service
    /// </summary>
    public class FavoriteService : IFavoriteService
    {
        /// <summary>
        /// the Etsy context data
        /// </summary>
        private readonly EtsyContext etsyContext;

        /// <summary>
        /// Initializes a new instance of the FavoriteService class
        /// </summary>
        /// <param name="etsyContext">the etsy context to use</param>
        public FavoriteService(EtsyContext etsyContext)
        {
            this.etsyContext = etsyContext;
        }

        #region IFavoriteService Members

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
        public event EventHandler<ResultEventArgs<Shops>> GetFavoriteShopOfUserCompleted;

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
            if (!ServiceHelper.TestCallPrerequisites(this, this.GetFavorersOfListingCompleted, this.etsyContext))
            {
                return null;
            }

            UriBuilder uriBuilder = UriBuilder.Start(this.etsyContext, "listings", listingId)
                .Append("favorers")
                .OffsetLimit(offset, limit)
                .DetailLevel(detailLevel);

            return ServiceHelper.GenerateRequest(this, uriBuilder.Result(), this.GetFavorersOfListingCompleted);
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
            if (!ServiceHelper.TestCallPrerequisites(this, this.GetFavorersOfShopCompleted, this.etsyContext))
            {
                return null;
            }

            UriBuilder uriBuilder = UriBuilder.Start(this.etsyContext, "shops", shopId)
                .Append("favorers")
                .OffsetLimit(offset, limit)
                .DetailLevel(detailLevel);

            return ServiceHelper.GenerateRequest(this, uriBuilder.Result(), this.GetFavorersOfShopCompleted);
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
            if (!ServiceHelper.TestCallPrerequisites(this, this.GetFavorersOfShopCompleted, this.etsyContext))
            {
                return null;
            }

            UriBuilder uriBuilder = UriBuilder.Start(this.etsyContext, "shops", shopName)
                .Append("favorers")
                .OffsetLimit(offset, limit)
                .DetailLevel(detailLevel);

            return ServiceHelper.GenerateRequest(this, uriBuilder.Result(), this.GetFavorersOfShopCompleted);
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
            if (!ServiceHelper.TestCallPrerequisites(this, this.GetFavoriteListingsOfUserCompleted, this.etsyContext))
            {
                return null;
            }

            UriBuilder uriBuilder = UriBuilder.Start(this.etsyContext, "users", userId)
                .Append("favorites/listings")
                .OffsetLimit(offset, limit)
                .DetailLevel(detailLevel);

            return ServiceHelper.GenerateRequest(this, uriBuilder.Result(), this.GetFavoriteListingsOfUserCompleted);
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
            if (!ServiceHelper.TestCallPrerequisites(this, this.GetFavoriteListingsOfUserCompleted, this.etsyContext))
            {
                return null;
            }

            UriBuilder uriBuilder = UriBuilder.Start(this.etsyContext, "users", userName)
                .Append("favorites/listings")
                .OffsetLimit(offset, limit)
                .DetailLevel(detailLevel);

            return ServiceHelper.GenerateRequest(this, uriBuilder.Result(), this.GetFavoriteListingsOfUserCompleted);
        }

        /// <summary>
        /// Get the favorite shops of a user.
        /// </summary>
        /// <param name="userId">the user's numeric ID</param>
        /// <param name="offset">To page through large result sets, set offset to a multiple of limit</param>
        /// <param name="limit">Specify the number of results to return</param>
        /// <param name="detailLevel">Control how much information to return</param>
        /// <returns>The Async state of the request</returns>
        public IAsyncResult GetFavoriteShopOfUser(int userId, int offset, int limit, DetailLevel detailLevel)
        {
            if (!ServiceHelper.TestCallPrerequisites(this, this.GetFavoriteShopOfUserCompleted, this.etsyContext))
            {
                return null;
            }

            UriBuilder uriBuilder = UriBuilder.Start(this.etsyContext, "users", userId)
                .Append("favorites/shops")
                .OffsetLimit(offset, limit)
                .DetailLevel(detailLevel);

            return ServiceHelper.GenerateRequest(this, uriBuilder.Result(), this.GetFavoriteListingsOfUserCompleted);
        }

        /// <summary>
        /// Get the favorite shops of a user.
        /// </summary>
        /// <param name="userName">the user's name</param>
        /// <param name="offset">To page through large result sets, set offset to a multiple of limit</param>
        /// <param name="limit">Specify the number of results to return</param>
        /// <param name="detailLevel">Control how much information to return</param>
        /// <returns>The Async state of the request</returns>
        public IAsyncResult GetFavoriteShopOfUser(string userName, int offset, int limit, DetailLevel detailLevel)
        {
            if (!ServiceHelper.TestCallPrerequisites(this, this.GetFavoriteShopOfUserCompleted, this.etsyContext))
            {
                return null;
            }

            UriBuilder uriBuilder = UriBuilder.Start(this.etsyContext, "users", userName)
                .Append("favorites/shops")
                .OffsetLimit(offset, limit)
                .DetailLevel(detailLevel);

            return ServiceHelper.GenerateRequest(this, uriBuilder.Result(), this.GetFavoriteListingsOfUserCompleted);
        }

        #endregion
    }
}
