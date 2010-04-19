//-----------------------------------------------------------------------
// <copyright file="ShopListingsCommand.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.Shop
{
    using System;

    using Netsy.DataModel;
    using Netsy.Helpers;
    using Netsy.Interfaces;
    using Netsy.UI.Commands;
    using Netsy.UI.ViewModels;

    /// <summary>
    /// Command to get shop details for the ShopControlViewModel
    /// </summary>
    public class ShopListingsCommand : GenericCommandBase<ShopControlViewModel>
    {
        /// <summary>
        /// Service for shop details
        /// </summary>
        private readonly IShopService shopService;

        /// <summary>
        /// The view model currently being used
        /// </summary>
        private ShopControlViewModel currentViewModel;

        /// <summary>
        /// Initializes a new instance of the ShopListingsCommand class
        /// </summary>
        /// <param name="shopService">the service to get shop listings</param>
        public ShopListingsCommand(IShopService shopService)
        {
            if (shopService == null)
            {
                throw new ArgumentNullException("shopService");
            }

            this.shopService = shopService;

            this.shopService.GetShopListingsCompleted += this.GetShopListingsCompleted;
        }

        /// <summary>
        /// Execute the command with a ViewModel as parameter
        /// </summary>
        /// <param name="value">the view model</param>
        public override void ExecuteOnValue(ShopControlViewModel value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            if (value.UserId.HasContent())
            {
                this.currentViewModel = value;
                this.shopService.GetShopListings(value.UserId, SortField.Created, SortOrder.Up, null, 0, 10, DetailLevel.Medium);
            }
        }

        /// <summary>
        /// Handler for when shop details are returned
        /// </summary>
        /// <param name="sender">the vent sender</param>
        /// <param name="e">the event params</param>
        private void GetShopListingsCompleted(object sender, ResultEventArgs<Listings> e)
        {
            if (e.ResultStatus.Success)
            {
                Listings listings = e.ResultValue;

                foreach (Listing listing in listings.Results)
                {
                    this.currentViewModel.Listings.Add(new ListingViewModel(listing));                    
                }
            }
        }
    }
}
