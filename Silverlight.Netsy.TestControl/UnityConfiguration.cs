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
    using global::Netsy.DataModel;
    using global::Netsy.Interfaces;

    using Microsoft.Practices.Unity;

    /// <summary>
    /// Configure a container with Netsy Services
    /// </summary>
    public static class UnityConfiguration
    {
        /// <summary>
        /// Configure the mappings of interfaces to types
        /// for the IOC container
        /// </summary>
        /// <param name="container">the container to populate</param>
        public static void RegisterServiceTypes(IUnityContainer container)
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

        /// <summary>
        /// The API key to use 
        /// </summary>
        public const string EtsyApiKey = "rfc35bh98q3a9hvccfsxe4cc";

        /// <summary>
        /// COnfigure the injection of the Etsy context
        /// </summary>
        /// <param name="container">the container to populate</param>
        public static void RegisterEtsyContext(IUnityContainer container)
        {
            // for injecting the EtsyContext, use this instance that contains the API key
            container.RegisterInstance(new EtsyContext(EtsyApiKey));

        }
    }
}
