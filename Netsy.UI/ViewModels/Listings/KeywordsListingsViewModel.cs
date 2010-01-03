//-----------------------------------------------------------------------
// <copyright file="KeywordsListingsViewModel.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 
namespace Netsy.UI.ViewModels.Listings
{
    using System.Collections.Generic;
    using System.Globalization;

    using Netsy.DataModel;
    using Netsy.Helpers;
    using Netsy.Interfaces;
    using Netsy.UI.Commands;

    /// <summary>
    /// View model for a collection of listings from the front featured listings service
    /// </summary>
    public class KeywordsListingsViewModel : ListingsServiceViewModel
    {
        /// <summary>
        /// The keywords to match
        /// </summary>
        private string keywords;

        /// <summary>
        /// Initializes a new instance of the KeywordsListingsViewModel class.
        /// </summary>
        /// <param name="listingsService">the listings service</param>
        public KeywordsListingsViewModel(IListingsService listingsService)
            : base(listingsService)
        {
            this.ListingsService.GetListingsByKeywordCompleted += this.ListingsReceived;
            this.MakeCommands();
        }

        /// <summary>
        /// Gets or sets the keywords to match
        /// </summary>
        public string Keywords
        {
            get
            {
                return this.keywords;
            }

            set
            {
                if (this.keywords != value)
                {
                    this.keywords = value;
                    this.OnPropertyChanged("Keywords");
                }
            }
        }

        /// <summary>
        /// Show the success message
        /// </summary>
        protected override void ShowLoadedSuccessMessage()
        {
            string status = string.Format(CultureInfo.InvariantCulture, "Loaded {0} listings by keyword on page {1}", this.Items.Count, this.PageNumber);
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
                    if (string.IsNullOrEmpty(this.Keywords))
                    {
                        this.StatusText = "Enter one or more keywords";
                        return;
                    }

                    int offset = (this.PageNumber - 1) * this.ItemsPerPage;
                    IEnumerable<string> keywordArray = this.Keywords.ToEnumerable();

                    this.ListingsService.GetListingsByKeyword(keywordArray, SortField.Score, SortOrder.Down,  null, null, true, offset, this.ItemsPerPage, DetailLevel.Medium);
                    string status = string.Format(CultureInfo.InvariantCulture, "Getting {0} listings by keyword on page {1}", this.ItemsPerPage, this.PageNumber);
                    this.StatusText = status;
                });
        }

    }
}
