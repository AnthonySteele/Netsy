﻿//-----------------------------------------------------------------------
// <copyright file="Locator.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.WpfUI
{
    using System;
    using System.Windows.Threading;

    using Microsoft.Practices.Unity;

    using Netsy.Cache;
    using Netsy.DataModel;
    using Netsy.Interfaces;
    using Netsy.Services;
    using Netsy.UI.DispatchedServices;
    using Netsy.Requests;

    /// <summary>
    /// Class to hold the only singleton we'll need - the IOC container
    /// </summary>
    public static class Locator
    {
        /// <summary>
        /// the key to use when looking up internal services
        /// </summary>
        private const string InternalServiceKey = "Internal";

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
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            } 
            
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
            RegisterInternalServices();
            RegisterDispatchedServiceFactories();
        }

        /// <summary>
        /// Register the basic services as internal
        /// </summary>
        private static void RegisterInternalServices()
        {
            container.RegisterType<IDataRetriever, DataRetriever>(new ContainerControlledLifetimeManager());
            container.RegisterType<IRequestGenerator, WebRequestGenerator>(new ContainerControlledLifetimeManager());
            container.RegisterType<IDataCache, DataCache>(new ContainerControlledLifetimeManager());

            container.RegisterType<IFavoritesService, FavoritesService>(InternalServiceKey);
            container.RegisterType<IFeedbackService, FeedbackService>(InternalServiceKey);
            container.RegisterType<IGiftService, GiftService>(InternalServiceKey);
            container.RegisterType<IListingsService, ListingsService>(InternalServiceKey);
            container.RegisterType<IServerService, ServerService>(InternalServiceKey);
            container.RegisterType<IShopService, ShopService>(InternalServiceKey);
            container.RegisterType<ITagCategoryService, TagCategoryService>(InternalServiceKey);
            container.RegisterType<IUsersService, UsersService>(InternalServiceKey);
        }

        /// <summary>
        /// Register dispatched services to use as services
        /// Have to use factories as they wrap the internal services that have the same interface
        /// </summary>
        private static void RegisterDispatchedServiceFactories()
        {
            container.RegisterType<IFavoritesService>(new InjectionFactory(cont =>
                new DispatchedFavoritesService(
                    cont.Resolve<IFavoritesService>(InternalServiceKey),
                    cont.Resolve<Dispatcher>())));

            container.RegisterType<IFeedbackService>(new InjectionFactory(cont =>
                new DispatchedFeedbackService(
                    cont.Resolve<IFeedbackService>(InternalServiceKey),
                    cont.Resolve<Dispatcher>())));

            container.RegisterType<IGiftService>(new InjectionFactory(cont =>
                new DispatchedGiftService(
                    cont.Resolve<IGiftService>(InternalServiceKey),
                    cont.Resolve<Dispatcher>())));

            container.RegisterType<IListingsService>(new InjectionFactory(cont =>
                new DispatchedListingsService(
                    cont.Resolve<IListingsService>(InternalServiceKey),
                    cont.Resolve<Dispatcher>())));

            container.RegisterType<IServerService>(new InjectionFactory(cont =>
                new DispatchedServerService(
                    cont.Resolve<IServerService>(InternalServiceKey),
                    cont.Resolve<Dispatcher>())));

            container.RegisterType<IShopService>(new InjectionFactory(cont =>
                new DispatchedShopService(
                    cont.Resolve<IShopService>(InternalServiceKey),
                    cont.Resolve<Dispatcher>())));

            container.RegisterType<ITagCategoryService>(new InjectionFactory(cont =>
               new DispatchedTagCategoryService(
                    cont.Resolve<ITagCategoryService>(InternalServiceKey),
                    cont.Resolve<Dispatcher>())));

            container.RegisterType<IUsersService>(new InjectionFactory(cont =>
                new DispatchedUsersService(
                    cont.Resolve<IUsersService>(InternalServiceKey),
                    cont.Resolve<Dispatcher>())));
        }
    }
}
