//-----------------------------------------------------------------------
// <copyright file="IShopService.cs" company="AFS">
// Copyright (c) AFS. All rights reserved.
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.Interfaces
{
    using System;

    using DataModel;
    using DataModel.ShopData;

    using Helpers;

    public interface IShopService
    {
        /// <summary>
        /// User details by id completed event
        /// </summary>
        event EventHandler<ResultEventArgs<Shops>> GetShopDetailsCompleted;

        /// <summary>
        /// Query for shop details
        /// </summary>
        /// <param name="userId">the id of the shop</param>
        /// <param name="detailLevel">the level of detail</param>
        /// <returns>the async state</returns>
        IAsyncResult GetShopDetails(int userId, DetailLevel detailLevel);
    }
}
