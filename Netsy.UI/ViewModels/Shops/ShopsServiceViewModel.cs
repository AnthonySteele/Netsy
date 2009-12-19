//-----------------------------------------------------------------------
// <copyright file="ShopsServiceViewModel.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 
namespace Netsy.UI.ViewModels.Shops
{
    using System.Windows;
    using System.Windows.Threading;

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
        /// Callback for when Listings data has been received
        /// </summary>
        /// <param name="sender">event sender</param>
        /// <param name="e">event params</param>
        protected void ShopsReceived(object sender, ResultEventArgs<Shops> e)
        {
            // put it onto the Ui thread
            DispatcherHelper.Invoke(
                new ResultsReceivedHandler<Shops>(this.ShopsReceivedSync),
                e);
        }

        /// <summary>
        /// Show a message after load success
        /// </summary>
        protected abstract void ShowLoadedSuccessMessage();

        /// <summary>
        /// Shops data has been received
        /// </summary>
        /// <param name="shopsReceived">the shops received</param>
        private void ShopsReceivedSync(ResultEventArgs<Shops> shopsReceived)
        {
            if (!shopsReceived.ResultStatus.Success)
            {
                this.StatusText = "Failed to load shops " + shopsReceived.ResultStatus.ErrorMessage;
                return;
            }

            this.Items.Clear();
            foreach (Shop item in shopsReceived.ResultValue.Results)
            {
                ShopViewModel viewModel = new ShopViewModel(item);
                this.Items.Add(viewModel);
            }

            this.ShowLoadedSuccessMessage();
        }
    }
}
