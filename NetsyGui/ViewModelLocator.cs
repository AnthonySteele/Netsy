//-----------------------------------------------------------------------
// <copyright file="ViewModelLocator.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace NetsyGui
{
    using System;

    using Microsoft.Practices.Unity;

    using Netsy.DataModel;
    using Netsy.Interfaces;
    using Netsy.Services;

    using NetsyGui.Main;

    /// <summary>
    /// Class to hold the only singleton we'll need - the IOC container
    /// </summary>
    public static class ViewModelLocator
    {
        /// <summary>
        /// The IOC container
        /// </summary>
        private static readonly IUnityContainer container = new UnityContainer();

        /// <summary>
        /// Initializes static members of the ViewModelLocator class
        /// </summary>
        static ViewModelLocator()
        {
            RegisterTypes();
        }

        /// <summary>
        /// Gets a new main window view model
        /// </summary>
        /// <returns>the view model</returns>
        public static MainWindowViewModel MainWindowViewModel
        {
            get
            {
                return Resolve<MainWindowViewModel>();
            }
        }

        /// <summary>
        /// Register an instance in the unity container
        /// </summary>
        /// <param name="type">The type to register</param>
        /// <param name="instance">the instance to register</param>
        public static void RegisterInstance(Type type, object instance)
        {
            container.RegisterInstance(type, instance); 
        }

        /// <summary>
        /// Resolve an object from  in the IOC container
        /// </summary>
        /// <typeparam name="T">The type to resolve</typeparam>
        /// <returns>the new object</returns>
        public static T Resolve<T>()
        {
            return container.Resolve<T>();
        }

        /// <summary>
        /// Register the types into the IOC container
        /// </summary>
        private static void RegisterTypes()
        {
            // the etsy context can be a singleton
            const string EtsyApiKey = "rfc35bh98q3a9hvccfsxe4cc";
            RegisterInstance(typeof(EtsyContext), new EtsyContext(EtsyApiKey));

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
