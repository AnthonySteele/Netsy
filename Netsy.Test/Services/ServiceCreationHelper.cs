//-----------------------------------------------------------------------
// <copyright file="ServiceCreationHelper.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.Test.Services
{
    using Netsy.DataModel;
    using Netsy.Interfaces;
    using Netsy.Requests;
    using Netsy.Services;
    using Netsy.Test.Requests;
    
    /// <summary>
    /// Make services for test
    /// </summary>
    public static class ServiceCreationHelper
    {
        /// <summary>
        /// Make a FavoritesService
        /// </summary>
        /// <param name="etsyApiKey">the Etsy Api key</param>
        /// <returns>the FavoritesService</returns>
        public static IFavoritesService MakeFavouritesService(string etsyApiKey)
        {
            return new FavoritesService(new EtsyContext(etsyApiKey), MakeDataRetriever());
        }

        /// <summary>
        /// Make a FeedbackService
        /// </summary>
        /// <param name="etsyApiKey">the Etsy Api key</param>
        /// <returns>the FeedbackService</returns>
        public static IFeedbackService MakeFeedbackService(string etsyApiKey)
        {
            return new FeedbackService(new EtsyContext(etsyApiKey), MakeDataRetriever());
        }

        /// <summary>
        /// Make a GiftService
        /// </summary>
        /// <param name="etsyApiKey">the Etsy Api key</param>
        /// <returns>the GiftService</returns>
        public static IGiftService MakeGiftService(string etsyApiKey)
        {
            return new GiftService(new EtsyContext(etsyApiKey), MakeDataRetriever());
        }

        /// <summary>
        /// Make a ListingsService
        /// </summary>
        /// <param name="etsyApiKey">the Etsy Api key</param>
        /// <returns>the ListingsService</returns>
        public static IListingsService MakeListingsService(string etsyApiKey)
        {
            return new ListingsService(new EtsyContext(etsyApiKey), MakeDataRetriever());
        }

        /// <summary>
        /// Make a ServerService
        /// </summary>
        /// <param name="etsyApiKey">the Etsy Api key</param>
        /// <returns>the ServerService</returns>
        public static IServerService MakeServerService(string etsyApiKey)
        {
            return new ServerService(new EtsyContext(etsyApiKey), MakeDataRetriever());
        }

        /// <summary>
        /// Make a ShopService
        /// </summary>
        /// <param name="etsyApiKey">the Etsy Api key</param>
        /// <returns>the ShopService</returns>
        public static IShopService MakeShopService(string etsyApiKey)
        {
            return new ShopService(new EtsyContext(etsyApiKey), MakeDataRetriever());
        }


        /// <summary>
        /// Make a TagCategoryService
        /// </summary>
        /// <param name="etsyApiKey">the Etsy Api key</param>
        /// <returns>the TagCategoryService</returns>
        public static ITagCategoryService MakeTagCategoryService(string etsyApiKey)
        {
            return new TagCategoryService(new EtsyContext(etsyApiKey), MakeDataRetriever());
        }

        /// <summary>
        /// Make a UsersService
        /// </summary>
        /// <param name="etsyApiKey">the Etsy Api key</param>
        /// <returns>the UsersService</returns>
        public static IUsersService MakeUsersService(string etsyApiKey)
        {
            return new UsersService(new EtsyContext(etsyApiKey), MakeDataRetriever());
        }

        /// <summary>
        /// Make a mock data retriever
        /// </summary>
        /// <returns>the Data retriever</returns>
        private static IDataRetriever MakeDataRetriever()
        {
            IDataRetriever dataRetriever = new DataRetriever(new NullDataCache(), new MockFixedDataRequestGenerator(string.Empty));
            return dataRetriever;
        }
    }
}
