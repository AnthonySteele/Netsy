//-----------------------------------------------------------------------
// <copyright file="UnityConfiguration.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Silverlight.Netsy.TestControl
{
    using global::Netsy.Core;
    using global::Netsy.Interfaces;

    using Microsoft.Practices.Unity;

    /// <summary>
    /// Configure a IOC container 
    /// </summary>
    public static class UnityConfiguration
    {

                /// <summary>
        /// Configure the mappings of interfaces to types
        /// for the IOC container
        /// </summary>
        /// <param name="container">the container to populate</param>
        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<IFavoritesService, FavoritesService>();
            container.RegisterType<IFeedbackService, FeedbackService>();
            container.RegisterType<IGiftService, GiftService>();
            container.RegisterType<IListingsService, ListingsService>();
            container.RegisterType<IServerService, ServerService>();
            container.RegisterType<IShopService, ShopService>();
            container.RegisterType<ITagCategoryService, TagCategoryService>();
            container.RegisterType<IUsersService, UsersService>();
        }
    }
}
