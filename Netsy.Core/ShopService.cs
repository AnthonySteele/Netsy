//-----------------------------------------------------------------------
// <copyright file="UsersService.cs" company="AFS">
// Copyright (c) AFS. All rights reserved.
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.Core
{
    using System;

    using Netsy.DataModel;
    using Netsy.DataModel.ShopData;
    using Netsy.Helpers;
    using Netsy.Interfaces;

    /// <summary>
    /// Implementation of th shop service
    /// </summary>
    public class ShopService : IShopService
    {
        /// <summary>
        /// the Etsy context data
        /// </summary>
        private readonly EtsyContext etsyContext;

        /// <summary>
        /// Initializes a new instance of the UsersService class
        /// </summary>
        /// <param name="etsyContext">the etsy context to use</param>
        public ShopService(EtsyContext etsyContext)
        {
            this.etsyContext = etsyContext;
        }

        #region IShopService Members

        public event EventHandler<ResultEventArgs<Shops>> GetShopDetailsCompleted;

        public event EventHandler<ResultEventArgs<Shops>> GetFeaturedSellersCompleted;

        public event EventHandler<ResultEventArgs<Shops>> GetShopsByNameCompleted;

        public IAsyncResult GetShopDetails(int userId, DetailLevel detailLevel)
        {
            if (string.IsNullOrEmpty(this.etsyContext.ApiKey))
            {
                ResultEventArgs<Shops> errorResult = new ResultEventArgs<Shops>(null, new ResultStatus("No Api key", null));
                ServiceHelper.TestSendEvent(this.GetShopDetailsCompleted, this, errorResult);
                return null;
            }

            string url = this.etsyContext.BaseUrl + "shops/" + userId +
                "?api_key=" + this.etsyContext.ApiKey +
                "&detail_level=" + detailLevel.ToStringLower();

            return ServiceHelper.GenerateRequest(new Uri(url),
                s =>
                {
                    Shops shops = s.Deserialize<Shops>();
                    ResultEventArgs<Shops> sucessResult = new ResultEventArgs<Shops>(shops, new ResultStatus(true));
                    ServiceHelper.TestSendEvent(this.GetShopDetailsCompleted, this, sucessResult);
                });
        }

        public IAsyncResult GetFeaturedSellers(int offset, int limit, DetailLevel detailLevel)
        {
            if (string.IsNullOrEmpty(this.etsyContext.ApiKey))
            {
                ResultEventArgs<Shops> errorResult = new ResultEventArgs<Shops>(null, new ResultStatus("No Api key", null));
                ServiceHelper.TestSendEvent(this.GetShopsByNameCompleted, this, errorResult);
                return null;
            }

            string url = this.etsyContext.BaseUrl + "shops/featured/" + 
                "?api_key=" + this.etsyContext.ApiKey +
                "&offset=" + offset +
                "&limit=" + limit +
                "&detail_level=" + detailLevel.ToStringLower();

            return ServiceHelper.GenerateRequest(new Uri(url),
                s =>
                {
                    Shops shops = s.Deserialize<Shops>();
                    ResultEventArgs<Shops> sucessResult = new ResultEventArgs<Shops>(shops, new ResultStatus(true));
                    ServiceHelper.TestSendEvent(this.GetFeaturedSellersCompleted, this, sucessResult);
                });
        }


        public IAsyncResult GetShopsByName(string searchName, SortOrder sortOrder, int offset, int limit, DetailLevel detailLevel)
        {
            if (string.IsNullOrEmpty(this.etsyContext.ApiKey))
            {
                ResultEventArgs<Shops> errorResult = new ResultEventArgs<Shops>(null, new ResultStatus("No Api key", null));
                ServiceHelper.TestSendEvent(this.GetShopsByNameCompleted, this, errorResult);
                return null;
            }

            string url = this.etsyContext.BaseUrl + "shops/keywords/" + searchName +
                "?api_key=" + this.etsyContext.ApiKey +
                "&sort_order=" + sortOrder.ToStringLower() +
                "&offset=" + offset +
                "&limit=" + limit +
                "&detail_level=" + detailLevel.ToStringLower();

            return ServiceHelper.GenerateRequest(new Uri(url),
                s =>
                {
                    Shops shops = s.Deserialize<Shops>();
                    ResultEventArgs<Shops> sucessResult = new ResultEventArgs<Shops>(shops, new ResultStatus(true));
                    ServiceHelper.TestSendEvent(this.GetShopsByNameCompleted, this, sucessResult);
                });
        }

        #endregion
    }
}
