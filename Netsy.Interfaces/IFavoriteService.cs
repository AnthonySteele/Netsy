//-----------------------------------------------------------------------
// <copyright file="IFavoriteService.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 
namespace Netsy.Interfaces
{
    using System;

    using DataModel;
    using DataModel.ListingData;
    using DataModel.ShopData;
    using DataModel.UserData;

    using Helpers;

    /// <summary>
    /// Interface to Favorites Commands on the etsy API
    /// </summary>
    public interface IFavoriteService
    {
        event EventHandler<ResultEventArgs<Users>> GetFavorersOfListingCompleted;
        event EventHandler<ResultEventArgs<Users>> GetFavorersOfShopCompleted;
        event EventHandler<ResultEventArgs<Listings>> GetFavoriteListingsOfUserCompleted;
        event EventHandler<ResultEventArgs<Shops>> GetFavoriteShopOfUserCompleted;

        IAsyncResult GetFavorersOfListing(int listingId, int offset, int limit, DetailLevel detailLevel);

        IAsyncResult GetFavorersOfShop(int shopId, int offset, int limit, DetailLevel detailLevel);
        IAsyncResult GetFavorersOfShop(string shopName, int offset, int limit, DetailLevel detailLevel);

        IAsyncResult GetFavoriteListingsOfUser(int userId, int offset, int limit, DetailLevel detailLevel);
        IAsyncResult GetFavoriteListingsOfUser(string userName, int offset, int limit, DetailLevel detailLevel);

        IAsyncResult GetFavoriteShopOfUser(int userId, int offset, int limit, DetailLevel detailLevel);
        IAsyncResult GetFavoriteShopOfUser(string userName, int offset, int limit, DetailLevel detailLevel);
    }
}
