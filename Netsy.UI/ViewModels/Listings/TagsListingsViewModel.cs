//-----------------------------------------------------------------------
// <copyright file="TagsListingsViewModel.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.UI.ViewModels.Listings
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.Windows.Threading;

    using Netsy.DataModel;
    using Netsy.Interfaces;
    using Netsy.UI.Commands;

    /// <summary>
    /// View model for a collection of listings from the istings by tags service
    /// </summary>
    public class TagsListingsViewModel : ListingsServiceViewModel
    {
        /// <summary>
        /// The tags to match
        /// </summary>
        private string tags;

        /// <summary>
        /// Initializes a new instance of the TagsListingsViewModel class.
        /// </summary>
        /// <param name="listingsService">the listings service</param>
        public TagsListingsViewModel(IListingsService listingsService)
            : base(listingsService)
        {
            this.ListingsService.GetListingsByTagsCompleted += this.ListingsReceived;
            this.MakeCommands();
        }

        /// <summary>
        /// Gets or sets the Tags to match
        /// </summary>
        public string Tags
        {
            get
            {
                return this.tags;
            }

            set
            {
                if (this.tags != value)
                {
                    this.tags = value;
                    this.OnPropertyChanged("Tags");
                }
            }
        }

        /// <summary>
        /// Show the success message
        /// </summary>
        protected override void ShowLoadedSuccessMessage()
        {
            string status = string.Format(CultureInfo.InvariantCulture, "Loaded {0} listings by tags on page {1}", this.Items.Count, this.PageNumber);
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
                    if (string.IsNullOrEmpty(this.Tags))
                    {
                        this.StatusText = "Enter the tags";
                        return;
                    } 

                    int offset = (this.PageNumber - 1) * this.ItemsPerPage;
                    IEnumerable<string> tagsArray = this.Tags.ToEnumerable();

                    this.ListingsService.GetListingsByTags(tagsArray, SortField.Score, SortOrder.Down, offset, this.ItemsPerPage, DetailLevel.Medium);
                    string status = string.Format(CultureInfo.InvariantCulture, "Getting {0} listings by tags on page {1}", this.ItemsPerPage, this.PageNumber);
                    this.StatusText = status;
                });
        }
    }
}
