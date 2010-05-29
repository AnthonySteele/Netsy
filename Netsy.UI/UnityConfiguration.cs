//-----------------------------------------------------------------------
// <copyright file="UnityConfiguration.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 
namespace Netsy.UI
{
    using System;
    using System.Windows.Threading;

    using Netsy.Cache;
    using Netsy.DataModel;
    using Netsy.UI.DispatchedServices;
    using Netsy.Interfaces;
    using Netsy.Requests;
    using Netsy.Services;

    using Microsoft.Practices.Unity;
    using Microsoft.Practices.Unity.StaticFactory;

    /// <summary>
    /// Class to configure the Unity IoC Container
    /// </summary>
    public static class UnityConfiguration
    {
        /// <summary>
        /// the key to use when looking up internal services
        /// </summary>
        private const string InternalServiceKey = "Internal";

        /// <summary>
        /// Register services on the unity IoC Container
        /// </summary>
        /// <param name="container">the unity IoC container</param>
        public static void RegisterServices(IUnityContainer container)
        {
            // register the services 
            RegisterInternalServices(container);
            RegisterDispatchedServiceFactories(container);
        }

        /// <summary>
        /// Register the basic services as internal
        /// </summary>
        /// <param name="container">the unity IoC container</param>
        private static void RegisterInternalServices(IUnityContainer container)
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
        /// <param name="container">the unity IoC container</param>
        private static void RegisterDispatchedServiceFactories(IUnityContainer container)
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
