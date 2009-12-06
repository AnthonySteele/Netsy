//-----------------------------------------------------------------------
// <copyright file="ShopViewModel.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 
namespace Netsy.UI.ViewModels
{
    using Netsy.DataModel;

    /// <summary>
    /// View model for a shop
    /// </summary>
    public class ShopViewModel
    {
        /// <summary>
        /// the listing data transfer object
        /// </summary>
        private readonly Shop shop;

        /// <summary>
        /// Initializes a new instance of the ShopViewModel class
        /// </summary>
        /// <param name="shop">the shop Data transfer object</param>
        public ShopViewModel(Shop shop)
        {
            this.shop = shop;
        }

        /// <summary>
        /// Gets the shop data transfer object
        /// </summary>
        public Shop Shop
        {
            get { return this.shop; }
        }
    }
}
