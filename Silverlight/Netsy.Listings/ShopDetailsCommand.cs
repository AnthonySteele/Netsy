//-----------------------------------------------------------------------
// <copyright file="ShopDetailsCommand.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.Listings
{
    using System;

    using Netsy.DataModel;
    using Netsy.Helpers;
    using Netsy.Interfaces;
    using Netsy.UI.Commands;

    using UI.ViewModels;

    /// <summary>
    /// Command to get shop details for the FavoritesControlViewModel
    /// </summary>
    public class ShopDetailsCommand : GenericCommandBase<ListingsControlViewModel>
    {
        /// <summary>
        /// Service for shop details
        /// </summary>
        private readonly IShopService shopService;

        /// <summary>
        /// The view model currently being used
        /// </summary>
        private ListingsControlViewModel currentViewModel;

        /// <summary>
        /// Initializes a new instance of the ShopDetailsCommand class
        /// </summary>
        /// <param name="shopService">the service to get shop details</param>
        public ShopDetailsCommand(IShopService shopService)
        {
            if (shopService == null)
            {
                throw new ArgumentNullException("shopService");
            }

            this.shopService = shopService;

            this.shopService.GetShopDetailsCompleted += this.GetShopDetailsCompleted;
        }

        /// <summary>
        /// Execute the command with a ViewModel as parameter
        /// </summary>
        /// <param name="value">the view model</param>
        public override void ExecuteOnValue(ListingsControlViewModel value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            if (value.UserId.HasContent())
            {
                this.currentViewModel = value;
                this.shopService.GetShopDetails(value.UserId, DetailLevel.Medium);
            }
        }

        /// <summary>
        /// Handler for when shop details are returned
        /// </summary>
        /// <param name="sender">the vent sender</param>
        /// <param name="e">the event params</param>
        private void GetShopDetailsCompleted(object sender, ResultEventArgs<Shops> e)
        {
            if (e.ResultStatus.Success && e.ResultValue.Results.Length == 1)
            {
                Shop shop = e.ResultValue.Results[0];
                this.currentViewModel.Shop = new ShopViewModel(shop);
            }
        }
    }
}
