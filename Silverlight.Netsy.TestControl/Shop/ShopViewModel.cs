//-----------------------------------------------------------------------
// <copyright file="ShopViewModel.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Silverlight.Netsy.TestControl
{
    using global::Netsy.Interfaces;

    /// <summary>
    /// View Model for shop view
    /// </summary>
    public class ShopViewModel : BaseViewModel
    {
        /// <summary>
        /// The service to use to retrieve shop data
        /// </summary>
        private IShopService shopService;

        /// <summary>
        /// Initializes a new instance of the ShopViewModel class 
        /// </summary>
        /// <param name="shopService">the shop service</param>
        public ShopViewModel(IShopService shopService)
        {
            this.shopService = shopService;
        }
    }
}
