//-----------------------------------------------------------------------
// <copyright file="ShopWindowLoadShopCommand.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.WpfUI.Windows.Shop
{
    using System;
    using System.Globalization;

    using Netsy.DataModel;
    using Netsy.Helpers;
    using Netsy.Interfaces;

    using Netsy.UI;
    using Netsy.UI.Commands;
    using Netsy.UI.ViewModels;

    /// <summary>
    /// Command to load the show details
    /// </summary>
    public class ShopWindowLoadShopCommand : GenericCommandBase<ShopWindowViewModel>
    {
                /// <summary>
        /// The service to return listings
        /// </summary>
        private readonly IShopService shopService;

        /// <summary>
        /// the view model currently in use
        /// </summary>
        private ShopWindowViewModel currentViewModel;

        /// <summary>
        /// Initializes a new instance of the ShopWindowLoadShopCommand class.
        /// </summary>
        /// <param name="shopService">the shops service</param>
        public ShopWindowLoadShopCommand(IShopService shopService)
        {
            if (shopService == null)
            {
                throw new ArgumentNullException("shopService");
            } 
            
            this.shopService = shopService;
            this.shopService.GetShopDetailsCompleted += this.ShopDetailsReceived;
        }

        /// <summary>
        /// Execute the command and move to the next page
        /// </summary>
        /// <param name="value">the view model</param>
        public override void ExecuteOnValue(ShopWindowViewModel value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            } 
            
            this.currentViewModel = value;

            this.shopService.GetShopDetails(this.currentViewModel.UserId, DetailLevel.High);
            string status = string.Format(CultureInfo.InvariantCulture, "Getting shop details for {0}", this.currentViewModel.UserId);
            value.ShopListingsViewModel.StatusText = status;
        }

        /// <summary>
        /// Callback for when shop data has been received
        /// </summary>
        /// <param name="sender">the event sender</param>
        /// <param name="e">the event params</param>
        private void ShopDetailsReceived(object sender, ResultEventArgs<Shops> e)
        {
            // put it onto the Ui thread
            DispatcherHelper.Invoke(
                new ResultsReceivedHandler<Shops>(this.ShopDetailsReceivedSync),
                e);
        }

        /// <summary>
        /// shop data has been received
        /// </summary>
        /// <param name="shopsReceived">the shop details</param>
        private void ShopDetailsReceivedSync(ResultEventArgs<Shops> shopsReceived)
        {
            if (!shopsReceived.ResultStatus.Success)
            {
                this.currentViewModel.ShopListingsViewModel.StatusText = "Failed to load shops " + shopsReceived.ResultStatus.ErrorMessage;
                return;
            }

            if (shopsReceived.ResultValue.Results.Length < 1)
            {
                this.currentViewModel.ShopListingsViewModel.StatusText = "No shop found";
                return;
            }

            Shop firstShop = shopsReceived.ResultValue.Results[0];
            this.currentViewModel.Shop = new ShopViewModel(firstShop);

            this.currentViewModel.ShopListingsViewModel.StatusText = "Loaded shop details";
        }
    }
}
