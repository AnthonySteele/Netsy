//-----------------------------------------------------------------------
// <copyright file="FeaturedSellersViewModel.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.UI.ViewModels.Shops
{
    using System.Globalization;

    using Netsy.DataModel;
    using Netsy.Interfaces;
    using Netsy.UI.Commands;

    /// <summary>
    /// View model for a collection of shops from the shops by name service
    /// </summary>
    public class FeaturedSellersViewModel : ShopsServiceViewModel
    {
        /// <summary>
        /// Initializes a new instance of the FeaturedSellersViewModel class.
        /// </summary>
        /// <param name="shopsService">the shopsService service</param>
        public FeaturedSellersViewModel(IShopService shopsService)
            : base(shopsService)
        {
            this.ShopService.GetFeaturedSellersCompleted += this.ShopsReceived;
            this.MakeCommands();
        }

        /// <summary>
        /// Show the success message
        /// </summary>
        protected override void ShowLoadedSuccessMessage()
        {
            string status = string.Format(CultureInfo.InvariantCulture, "Loaded {0} featured shops on page {1}", this.Items.Count, this.PageNumber);
            this.StatusText = status;
        }

        /// <summary>
        /// Create the load command 
        /// </summary>
        private void MakeCommands()
        {
            this.LoadPageCommand = new DelegateCommand<ShopViewModel>(
                item =>
                {
                    int offset = (this.PageNumber - 1) * this.ItemsPerPage;

                    this.ShopService.GetFeaturedSellers(offset, this.ItemsPerPage, DetailLevel.Medium);
                    string status = string.Format(CultureInfo.InvariantCulture, "Getting {0} featured shops on page {1}", this.ItemsPerPage, this.PageNumber);
                    this.StatusText = status;
                });
        }
    }
}