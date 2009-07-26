//-----------------------------------------------------------------------
// <copyright file="UsersService.cs" company="AFS">
// Copyright (c) AFS. All rights reserved.
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.Core
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Net;

    using DataModel.ShopData;

    using Netsy.DataModel;
    using Netsy.Helpers;
    using Netsy.Interfaces;

    public class ShopService : IShopService
    {
        /// <summary>
        /// the API to use for authentication
        /// </summary>
        private readonly string ApiKey;

        /// <summary>
        /// Initializes a new instance of the UsersService class
        /// </summary>
        /// <param name="apiKey">the API key to use</param>
        public ShopService(string apiKey)
        {
            this.ApiKey = apiKey;
        }

        #region IShopService Members

        public event EventHandler<ResultEventArgs<Shops>> GetShopDetailsCompleted;

        public IAsyncResult GetShopDetails(int userId, DetailLevel detailLevel)
        {
            if (string.IsNullOrEmpty(this.ApiKey))
            {
                ResultEventArgs<Shops> errorResult = new ResultEventArgs<Shops>(null, new ResultStatus("No Api key", null));
                ServiceHelper.TestSendEvent(this.GetShopDetailsCompleted, this, errorResult);
                return null;
            }

            string url = Constants.BaseUrl + "shops/" + userId +
                "?api_key=" + this.ApiKey +
                "&detail_level=" + detailLevel.ToStringLower();

            return ServiceHelper.GenerateRequest(new Uri(url),
                s =>
                {
                    Shops shops = s.Deserialize<Shops>();
                    ResultEventArgs<Shops> sucessResult = new ResultEventArgs<Shops>(shops, new ResultStatus(true));
                    ServiceHelper.TestSendEvent(this.GetShopDetailsCompleted, this, sucessResult);
                });
        }

        #endregion
    }
}
