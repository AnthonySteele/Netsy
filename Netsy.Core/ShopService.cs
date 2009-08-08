//-----------------------------------------------------------------------
// <copyright file="ShopService.cs" company="AFS">
// Copyright (c) AFS. All rights reserved.
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.Core
{
    using System;

    using Netsy.DataModel;
    using Netsy.DataModel.ListingData;
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
        /// Initializes a new instance of the ShopService class
        /// </summary>
        /// <param name="etsyContext">the etsy context to use</param>
        public ShopService(EtsyContext etsyContext)
        {
            this.etsyContext = etsyContext;
        }

        #region IShopService Members

        /// <summary>
        ///  Event handler for when GetShopDetails completes
        /// </summary>
        public event EventHandler<ResultEventArgs<Shops>> GetShopDetailsCompleted;

        /// <summary>
        ///  Event handler for when GetFeaturedSellers completes
        /// </summary>
        public event EventHandler<ResultEventArgs<Shops>> GetFeaturedSellersCompleted;

        /// <summary>
        ///  Event handler for when GetShopsByName completes
        /// </summary>
        public event EventHandler<ResultEventArgs<Shops>> GetShopsByNameCompleted;

        /// <summary>
        /// Event handler for when GetShopListings completes
        /// </summary>
        public event EventHandler<ResultEventArgs<Listings>> GetShopListingsCompleted;

        /// <summary>
        /// Get Featured details completed event
        /// </summary>
        public event EventHandler<ResultEventArgs<Listings>> GetFeaturedDetailsCompleted;
        
        /// <summary>
        /// Get the shop details
        /// </summary>
        /// <param name="userId">the uset Id</param>
        /// <param name="detailLevel">thje detail level</param>
        /// <returns>the async state</returns>
        public IAsyncResult GetShopDetails(int userId, DetailLevel detailLevel)
        {
            if (!ServiceHelper.TestCallPrerequisites(this, this.GetShopDetailsCompleted, this.etsyContext))
            {
                return null;
            }

            string url = this.etsyContext.BaseUrl + "shops/" + userId +
                "?api_key=" + this.etsyContext.ApiKey +
                "&detail_level=" + detailLevel.ToStringLower();

            return ServiceHelper.GenerateRequest(this, new Uri(url), this.GetShopDetailsCompleted);
        }

        /// <summary>
        /// Get featured sellers
        /// </summary>
        /// <param name="offset">the offset in results</param>
        /// <param name="limit">the limit of results</param>
        /// <param name="detailLevel">the detail level</param>
        /// <returns>the async state</returns>
        public IAsyncResult GetFeaturedSellers(int offset, int limit, DetailLevel detailLevel)
        {
            if (!ServiceHelper.TestCallPrerequisites(this, this.GetFeaturedSellersCompleted, this.etsyContext))
            {
                return null;
            }

            string url = this.etsyContext.BaseUrl + "shops/featured" + 
                "?api_key=" + this.etsyContext.ApiKey +
                "&offset=" + offset +
                "&limit=" + limit +
                "&detail_level=" + detailLevel.ToStringLower();

            return ServiceHelper.GenerateRequest(this, new Uri(url), this.GetFeaturedSellersCompleted);
        }

        /// <summary>
        /// Get shops by name
        /// </summary>
        /// <param name="searchName">the text to search for</param>
        /// <param name="sortOrder">the results order</param>
        /// <param name="offset">the results offset</param>
        /// <param name="limit">the results limit</param>
        /// <param name="detailLevel">detail level</param>
        /// <returns>the async state</returns>
        public IAsyncResult GetShopsByName(string searchName, SortOrder sortOrder, int offset, int limit, DetailLevel detailLevel)
        {
            if (!ServiceHelper.TestCallPrerequisites(this, this.GetShopsByNameCompleted, this.etsyContext))
            {
                return null;
            }

            string url = this.etsyContext.BaseUrl + "shops/keywords/" + searchName +
                "?api_key=" + this.etsyContext.ApiKey +
                "&sort_order=" + sortOrder.ToStringLower() +
                "&offset=" + offset +
                "&limit=" + limit +
                "&detail_level=" + detailLevel.ToStringLower();

            return ServiceHelper.GenerateRequest(this, new Uri(url), this.GetShopsByNameCompleted);
        }

        /// <summary>
        /// Get all the listings in a shop.
        /// </summary>
        /// <param name="userId">the user id</param>
        /// <param name="sortOn">field to sort on</param>
        /// <param name="sortOrder">sort ascending or descending</param>
        /// <param name="sectionId">shop section to show</param>
        /// <param name="offset">the search results offset</param>
        /// <param name="limit">the search limit</param>
        /// <param name="detailLevel">the level of detail</param>
        /// <returns>the async state</returns>
        public IAsyncResult GetShopListings(int userId, SortField sortOn, SortOrder sortOrder, int? sectionId, int offset, int limit, DetailLevel detailLevel)
        {
            if (!ServiceHelper.TestCallPrerequisites(this, this.GetShopListingsCompleted, this.etsyContext))
            {
                return null;
            }

            string url = this.etsyContext.BaseUrl + "shops/" + userId + "/listings" +
                "?api_key=" + this.etsyContext.ApiKey +
                "&sort_on=" + sortOn.ToStringLower() +
                "&sort_order=" + sortOrder.ToStringLower() +
                OptionalParam("section_id", sectionId) +
                "&offset=" + offset +
                "&limit=" + limit +
                "&detail_level=" + detailLevel.ToStringLower();

            return ServiceHelper.GenerateRequest(this, new Uri(url), this.GetShopListingsCompleted);
        }

        /// <summary>
        /// Get all the listings in a shop.
        /// </summary>
        /// <param name="userName">the user name</param>
        /// <param name="sortOn">field to sort on</param>
        /// <param name="sortOrder">sort ascending or descending</param>
        /// <param name="sectionId">shop section to show</param>
        /// <param name="offset">the search results offset</param>
        /// <param name="limit">the search limit</param>
        /// <param name="detailLevel">the level of detail</param>
        /// <returns>the async state</returns>
        public IAsyncResult GetShopListings(string userName, SortField sortOn, SortOrder sortOrder, int? sectionId, int offset, int limit, DetailLevel detailLevel)
        {
            if (!ServiceHelper.TestCallPrerequisites(this, this.GetShopListingsCompleted, this.etsyContext))
            {
                return null;
            }

            string url = this.etsyContext.BaseUrl + "shops/" + userName + "/listings" +
                "?api_key=" + this.etsyContext.ApiKey +
                "&sort_on=" + sortOn.ToStringLower() +
                "&sort_order=" + sortOrder.ToStringLower() +
                OptionalParam("section_id", sectionId) +
                "&offset=" + offset +
                "&limit=" + limit +
                "&detail_level=" + detailLevel.ToStringLower();

            return ServiceHelper.GenerateRequest(this, new Uri(url), this.GetShopListingsCompleted);
        }

        /// <summary>
        /// Get the expanded details on featured listings of a shop, ordered by highest ranked featured item.
        /// </summary>
        /// <param name="userName">the user name</param>
        /// <param name="detailLevel">the level of detail</param>
        /// <returns>the async state</returns>
        public IAsyncResult GetFeaturedDetails(string userName, DetailLevel detailLevel)
        {
            if (!ServiceHelper.TestCallPrerequisites(this, this.GetFeaturedDetailsCompleted, this.etsyContext))
            {
                return null;
            }

            string url = this.etsyContext.BaseUrl + "shops/" + userName + "/listings/featured" +
                "?api_key=" + this.etsyContext.ApiKey +
                "&detail_level=" + detailLevel.ToStringLower();

            return ServiceHelper.GenerateRequest(this, new Uri(url), this.GetFeaturedDetailsCompleted);            
        }

        /// <summary>
        /// Get the expanded details on featured listings of a shop, ordered by highest ranked featured item.
        /// </summary>
        /// <param name="userId">the user id</param>
        /// <param name="detailLevel">the level of detail</param>
        /// <returns>the async state</returns>
        public IAsyncResult GetFeaturedDetails(int userId, DetailLevel detailLevel)
        {
            if (!ServiceHelper.TestCallPrerequisites(this, this.GetFeaturedDetailsCompleted, this.etsyContext))
            {
                return null;
            }

            string url = this.etsyContext.BaseUrl + "shops/" + userId + "/listings/featured" +
                "?api_key=" + this.etsyContext.ApiKey +
                "&detail_level=" + detailLevel.ToStringLower();

            return ServiceHelper.GenerateRequest(this, new Uri(url), this.GetFeaturedDetailsCompleted);
        }

        /// <summary>
        /// Generate an optional param, containing the value or nothing
        /// </summary>
        /// <param name="paramName">the param name</param>
        /// <param name="paramValue">the aparam value</param>
        /// <returns>the param string fragment, if needed</returns>
        private static string OptionalParam(string paramName, int? paramValue)
        {
            if (paramValue.HasValue)
            {
                return "&" + paramName + "=" + paramValue.Value;
            }
                
            return string.Empty;
        }

        #endregion
    }
}
