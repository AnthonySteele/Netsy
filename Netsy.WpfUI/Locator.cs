//-----------------------------------------------------------------------
// <copyright file="Locator.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.WpfUI
{
    using System;

    using Microsoft.Practices.Unity;

    using Netsy.DataModel;
    using Netsy.Interfaces;
    using Netsy.Services;

    /// <summary>
    /// Class to hold the only singleton we'll need - the IOC container
    /// </summary>
    public static class Locator
    {
        /// <summary>
        /// The IOC container
        /// </summary>
        private static readonly IUnityContainer container = new UnityContainer();

        /// <summary>
        /// Initializes static members of the Locator class
        /// </summary>
        static Locator()
        {
            RegisterNetsyTypes();
        }

        /// <summary>
        /// Register an instance in the unity container
        /// </summary>
        /// <param name="instance">the instance to register</param>
        public static void RegisterInstance(object instance)
        {
            Type objectType = instance.GetType();
            container.RegisterInstance(objectType, instance);
        }

        /// <summary>
        /// Resolve an object from the IOC container
        /// </summary>
        /// <typeparam name="T">The type to resolve</typeparam>
        /// <returns>the new object</returns>
        public static T Resolve<T>()
        {
            return container.Resolve<T>();
        }

        /// <summary>
        /// Register the Netsy Library types into the IOC container
        /// </summary>
        private static void RegisterNetsyTypes()
        {
            // the etsy context can be a singleton
            const string EtsyApiKey = "rfc35bh98q3a9hvccfsxe4cc";
            RegisterInstance(new EtsyContext(EtsyApiKey));

            // register the services 
            container.RegisterType<IFavoritesService, FavoritesService>();
            container.RegisterType<IFeedbackService, FeedbackService>();
            container.RegisterType<IGiftService, GiftService>();
            container.RegisterType<IListingsService, ListingsService>();
            container.RegisterType<IServerService, ServerService>();
            container.RegisterType<IShopService, ShopService>();
            container.RegisterType<IFavoritesService, FavoritesService>();
            container.RegisterType<ITagCategoryService, TagCategoryService>();
            container.RegisterType<IUsersService, UsersService>();
        }
    }
}
