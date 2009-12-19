//-----------------------------------------------------------------------
// <copyright file="FrontFeaturedListingsViewModel.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 
namespace Netsy.UI.ViewModels.Listings
{
    using System.Globalization;

    using Netsy.DataModel;
    using Netsy.Interfaces;
    using Netsy.UI.Commands;

    /// <summary>
    /// View model for a collection of listings from the front featured listings service
    /// </summary>
    public class FrontFeaturedListingsViewModel : ListingsServiceViewModel
    {
        /// <summary>
        /// Initializes a new instance of the FrontFeaturedListingsViewModel class.
        /// </summary>
        /// <param name="listingsService">the listings service</param>
        public FrontFeaturedListingsViewModel(IListingsService listingsService)
            : base(listingsService)
        {
            this.ListingsService.GetFrontFeaturedListingsCompleted += this.ListingsReceived;
            this.MakeCommands();
        }

        /// <summary>
        /// Show the success message
        /// </summary>
        protected override void ShowLoadedSuccessMessage()
        {
            string status = string.Format(CultureInfo.InvariantCulture, "Loaded {0} front listings on page {1}", this.Items.Count, this.PageNumber);
            this.StatusText = status;
        }

        /// <summary>
        /// Create the load command 
        /// </summary>
        private void MakeCommands()
        {
            this.LoadPageCommand = new DelegateCommand<ListingViewModel>(
                item =>
                {
                    int offset = (this.PageNumber - 1) * this.ItemsPerPage;

                    this.ListingsService.GetFrontFeaturedListings(offset, this.ItemsPerPage, DetailLevel.Medium);
                    string status = string.Format(CultureInfo.InvariantCulture, "Getting {0} front listings on page {1}", this.ItemsPerPage, this.PageNumber);
                    this.StatusText = status;
                });
        }
    }
}
