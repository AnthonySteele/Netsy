//-----------------------------------------------------------------------
// <copyright file="ShopService.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.Services
{
    using System;

    using Netsy.DataModel;
    using Netsy.Helpers;
    using Netsy.Interfaces;
    using Netsy.Requests;

    /// <summary>
    /// Implementation of the shop service
    /// </summary>
    public class ShopService : IShopService
    {
        /// <summary>
        /// the Etsy context data
        /// </summary>
        private readonly EtsyContext etsyContext;

        /// <summary>
        /// the data retriever
        /// </summary>
        private readonly IDataRetriever dataRetriever;

        /// <summary>
        /// Initializes a new instance of the ShopService class
        /// </summary>
        /// <param name="etsyContext">the etsy context to use</param>
        public ShopService(EtsyContext etsyContext)
            : this(etsyContext, new DataRetriever())
        {
        }

        /// <summary>
        /// Initializes a new instance of the ShopService class
        /// </summary>
        /// <param name="etsyContext">the etsy context to use</param>
        /// <param name="dataRetriever">the data retreiver to use</param>
        public ShopService(EtsyContext etsyContext, IDataRetriever dataRetriever)
        {
            this.etsyContext = etsyContext;
            this.dataRetriever = dataRetriever;
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
            if (!RequestHelper.TestCallPrerequisites(this, this.GetShopDetailsCompleted, this.etsyContext))
            {
                return null;
            }

            EtsyUriBuilder etsyUriBuilder = EtsyUriBuilder.Start(this.etsyContext, "shops", userId)
                .DetailLevel(detailLevel);

            return this.dataRetriever.StartRetrieve(etsyUriBuilder.Result(), this.GetShopDetailsCompleted);
        }

        /// <summary>
        /// Get the details of a seller's shop by user name.
        /// </summary>
        /// <param name="userName">the name of the shop</param>
        /// <param name="detailLevel">the level of detail</param>
        /// <returns>the async state</returns>
        public IAsyncResult GetShopDetails(string userName, DetailLevel detailLevel)
        {
            if (!RequestHelper.TestCallPrerequisites(this, this.GetShopDetailsCompleted, this.etsyContext))
            {
                return null;
            }

            EtsyUriBuilder etsyUriBuilder = EtsyUriBuilder.Start(this.etsyContext, "shops", userName)
                .DetailLevel(detailLevel);

            return this.dataRetriever.StartRetrieve(etsyUriBuilder.Result(), this.GetShopDetailsCompleted);
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
            if (!RequestHelper.TestCallPrerequisites(this, this.GetFeaturedSellersCompleted, this.etsyContext))
            {
                return null;
            }

            EtsyUriBuilder etsyUriBuilder = EtsyUriBuilder.Start(this.etsyContext, "shops/featured")
                .OffsetLimit(offset, limit)
                .DetailLevel(detailLevel);

            return this.dataRetriever.StartRetrieve(etsyUriBuilder.Result(), this.GetFeaturedSellersCompleted);
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
            if (!RequestHelper.TestCallPrerequisites(this, this.GetShopsByNameCompleted, this.etsyContext))
            {
                return null;
            }

            EtsyUriBuilder etsyUriBuilder = EtsyUriBuilder.Start(this.etsyContext, "shops/keywords", searchName)
                .SortOrder(sortOrder)
                .OffsetLimit(offset, limit)
                .DetailLevel(detailLevel);

            return this.dataRetriever.StartRetrieve(etsyUriBuilder.Result(), this.GetShopsByNameCompleted);
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
            if (!RequestHelper.TestCallPrerequisites(this, this.GetShopListingsCompleted, this.etsyContext))
            {
                return null;
            }

            EtsyUriBuilder etsyUriBuilder = EtsyUriBuilder.Start(this.etsyContext, "shops", userId)
                .Append("/listings")
                .Sort(sortOn, sortOrder)
                .OptionalParam("section_id", sectionId)
                .OffsetLimit(offset, limit)
                .DetailLevel(detailLevel);

            return this.dataRetriever.StartRetrieve(etsyUriBuilder.Result(), this.GetShopListingsCompleted);
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
            if (!RequestHelper.TestCallPrerequisites(this, this.GetShopListingsCompleted, this.etsyContext))
            {
                return null;
            }

            EtsyUriBuilder etsyUriBuilder = EtsyUriBuilder.Start(this.etsyContext, "shops", userName)
                .Append("/listings")
                .Sort(sortOn, sortOrder)
                .OptionalParam("section_id", sectionId)
                .OffsetLimit(offset, limit)
                .DetailLevel(detailLevel);

            return this.dataRetriever.StartRetrieve(etsyUriBuilder.Result(), this.GetShopListingsCompleted);
        }

        /// <summary>
        /// Get the expanded details on featured listings of a shop, ordered by highest ranked featured item.
        /// </summary>
        /// <param name="userName">the user name</param>
        /// <param name="detailLevel">the level of detail</param>
        /// <returns>the async state</returns>
        public IAsyncResult GetFeaturedDetails(string userName, DetailLevel detailLevel)
        {
            if (!RequestHelper.TestCallPrerequisites(this, this.GetFeaturedDetailsCompleted, this.etsyContext))
            {
                return null;
            }

            EtsyUriBuilder etsyUriBuilder = EtsyUriBuilder.Start(this.etsyContext, "shops/", userName)
                .Append("/listings/featured")
                .DetailLevel(detailLevel);

            return this.dataRetriever.StartRetrieve(etsyUriBuilder.Result(), this.GetFeaturedDetailsCompleted);            
        }

        /// <summary>
        /// Get the expanded details on featured listings of a shop, ordered by highest ranked featured item.
        /// </summary>
        /// <param name="userId">the user id</param>
        /// <param name="detailLevel">the level of detail</param>
        /// <returns>the async state</returns>
        public IAsyncResult GetFeaturedDetails(int userId, DetailLevel detailLevel)
        {
            if (!RequestHelper.TestCallPrerequisites(this, this.GetFeaturedDetailsCompleted, this.etsyContext))
            {
                return null;
            }

            EtsyUriBuilder etsyUriBuilder = EtsyUriBuilder.Start(this.etsyContext, "shops/", userId)
                .Append("/listings/featured")
                .DetailLevel(detailLevel);

            return this.dataRetriever.StartRetrieve(etsyUriBuilder.Result(), this.GetFeaturedDetailsCompleted);
        }

        #endregion
    }
}
