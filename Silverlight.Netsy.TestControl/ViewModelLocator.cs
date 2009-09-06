//-----------------------------------------------------------------------
// <copyright file="ViewModelLocator.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Silverlight.Netsy.TestControl
{
    using Microsoft.Practices.Unity;

    /// <summary>
    /// static class to expose viewModels for binding to the XAML
    /// Uses IOC inside
    /// </summary>
    public static class ViewModelLocator
    {
        /// <summary>
        /// Gets or sets the IOC container to locate object instances
        /// </summary>
        public static IUnityContainer Container { get; set; }

        /// <summary>
        /// Gets the shop control view model
        /// </summary>
        public static ShopViewModel ShopViewModel
        {
            get { return Container.Resolve<ShopViewModel>(); }
        }
    }
}
