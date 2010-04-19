//-----------------------------------------------------------------------
// <copyright file="UnityHelper.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

// Todo: common with Listings

namespace Netsy.Shop
{
    using System;

    using Microsoft.Practices.Unity;
    using Microsoft.Practices.Unity.StaticFactory;

    /// <summary>
    /// Extension methods for Unity
    /// </summary>
    public static class UnityHelper
    {
        /// <summary>
        /// Register a factory method
        /// </summary>
        /// <typeparam name="T">the type to be constructed by the factory</typeparam>
        /// <param name="container">the container</param>
        /// <param name="factoryDelegate">the factory code</param>
        public static void RegisterFactory<T>(this IUnityContainer container, FactoryDelegate factoryDelegate)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            container.AddNewExtension<StaticFactoryExtension>()
                    .Configure<IStaticFactoryConfiguration>()
                    .RegisterFactory<T>(factoryDelegate);
        }
    }
}


