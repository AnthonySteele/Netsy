//-----------------------------------------------------------------------
// <copyright file="ShopsServiceViewModel.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 
namespace Netsy.UI.ViewModels.Shops
{
    using System.Windows.Input;

    using Netsy.DataModel;
    using Netsy.Helpers;
    using Netsy.Interfaces;

    /// <summary>
    /// Base class View model for a collection of listings from the listings service
    /// </summary>
    public abstract class ShopsServiceViewModel : PagedCollectionViewModel<ShopViewModel>
    {
        /// <summary>
        /// The service to return shops
        /// </summary>
        private readonly IShopService shopService;

        /// <summary>
        /// Initializes a new instance of the ShopsServiceViewModel class.
        /// </summary>
        /// <param name="shopService">the shops service</param>
        protected ShopsServiceViewModel(IShopService shopService)
        {
            this.shopService = shopService;
        }

        /// <summary>
        /// Gets or sets the command to show the shop
        /// </summary>
        public ICommand ShowShopCommand { get; set; }

        /// <summary>
        /// Gets the service to return listings
        /// </summary>
        protected IShopService ShopService
        {
            get
            {
                return this.shopService;
            }
        }

        /// <summary>
        /// Show a message after load success
        /// </summary>
        protected abstract void ShowLoadedSuccessMessage();

        /// <summary>
        /// Callback for when Listings data has been received
        /// </summary>
        /// <param name="sender">event sender</param>
        /// <param name="e">event params</param>
        protected void ShopsReceived(object sender, ResultEventArgs<Shops> e)
        {
            if (!e.ResultStatus.Success)
            {
                this.StatusText = "Failed to load shops " + e.ResultStatus.ErrorMessage;
                return;
            }

            this.Items.Clear();
            foreach (Shop item in e.ResultValue.Results)
            {
                ShopViewModel viewModel = new ShopViewModel(item);
                viewModel.ShowShopCommand = this.ShowShopCommand;
                this.Items.Add(viewModel);
            }

            this.ShowLoadedSuccessMessage();
        }
    }
}
