//-----------------------------------------------------------------------
// <copyright file="ColorKeywordsListingsViewModel.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 
namespace Netsy.UI.ViewModels
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.Windows.Threading;

    using Netsy.DataModel;
    using Netsy.Helpers;
    using Netsy.Interfaces;
    using Netsy.UI.Commands;

    /// <summary>
    /// View model for a collection of listings from the front featured listings service
    /// </summary>
    public class ColorKeywordsListingsViewModel : PagedCollectionViewModel<ListingViewModel>
    {
        /// <summary>
        /// The service to return listings
        /// </summary>
        private readonly IListingsService listingsService;

        /// <summary>
        /// The theading dispatcher
        /// </summary>
        private readonly Dispatcher dispatcher;

        /// <summary>
        /// The color to match
        /// </summary>
        private string colorText;

        /// <summary>
        /// The keywords to match
        /// </summary>
        private string keywords;

        /// <summary>
        /// Initializes a new instance of the ColorKeywordsListingsViewModel class.
        /// </summary>
        /// <param name="listingsService">the listings service</param>
        /// <param name="dispatcher">the thread dispatcher</param>
        public ColorKeywordsListingsViewModel(IListingsService listingsService, Dispatcher dispatcher)
        {
            this.dispatcher = dispatcher;
            this.listingsService = listingsService;
            this.listingsService.GetListingsByColorAndKeywordsCompleted += this.ListingsReceived;

            this.MakeCommands();
        }

        /// <summary>
        /// Gets or sets the color to match
        /// </summary>
        public string ColorText
        {
            get
            {
                return this.colorText;
            }

            set
            {
                if (this.colorText != value)
                {
                    this.colorText = value;
                    this.OnPropertyChanged("ColorText");
                }
            }
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
        /// Create the load command 
        /// </summary>
        private void MakeCommands()
        {
            this.LoadPageCommand = new DelegateCommand<ListingViewModel>(
                item =>
                {
                    if (!this.HasSearchParams())
                    {
                        this.StatusText = "Enter a color and one or more keywords";
                        return;
                    }

                    const int MaxWiggle = 15;
                    RgbColor rgbColor = this.ConvertColor();

                    int offset = (this.PageNumber - 1) * this.ItemsPerPage;
                    IEnumerable<string> keywordArray = this.ConvertKeywords();

                    this.listingsService.GetListingsByColorAndKeywords(keywordArray, rgbColor, MaxWiggle, offset, this.ItemsPerPage, DetailLevel.Medium);
                    string status = string.Format(CultureInfo.InvariantCulture, "Getting {0} listings by color and keyword on page {1}", this.ItemsPerPage, this.PageNumber);
                    this.StatusText = status;
                });
        }

        /// <summary>
        /// Have search params been entered
        /// </summary>
        /// <returns>true if the UI has text entered</returns>
        private bool HasSearchParams()
        {
            if (string.IsNullOrEmpty(this.Keywords))
            {
                return false;
            }

            if (string.IsNullOrEmpty(this.ColorText))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Get the keywords
        /// </summary>
        /// <returns>keywords for search</returns>
        private IEnumerable<string> ConvertKeywords()
        {
            if (string.IsNullOrEmpty(this.Keywords))
            {
                return new string[0];
            }

            return this.Keywords.Split(new[] { ',', ' ' });
        }

        /// <summary>
        /// Get the color data object
        /// </summary>
        /// <returns>the color data object</returns>
        private RgbColor ConvertColor()
        {
            RgbColor rgbColor = null;
            string trimedColor = string.Empty;
            if (! string.IsNullOrEmpty(this.ColorText))
            {
                trimedColor = this.ColorText.Trim();
            }

            if (!string.IsNullOrEmpty(trimedColor))
            {   
                rgbColor = new RgbColor(trimedColor);
            }

            return rgbColor;
        }

        /// <summary>
        /// Callback for when Listings data has been received
        /// </summary>
        /// <param name="sender">event sender</param>
        /// <param name="e">event params</param>
        private void ListingsReceived(object sender, ResultEventArgs<Listings> e)
        {
            // put it onto the Ui thread
            this.dispatcher.Invoke(
                DispatcherPriority.Normal,
                new ResultsReceivedHandler<Listings>(this.ListingsReceivedSync),
                e);
        }

        /// <summary>
        /// Listings data has been received
        /// </summary>
        /// <param name="listingsReceived">the listings</param>
        private void ListingsReceivedSync(ResultEventArgs<Listings> listingsReceived)
        {
            if (!listingsReceived.ResultStatus.Success)
            {
                this.StatusText = "Failed to load listings by color and keyword " + listingsReceived.ResultStatus.ErrorMessage;
                return;
            }

            this.Items.Clear();
            foreach (Listing item in listingsReceived.ResultValue.Results)
            {
                ListingViewModel viewModel = new ListingViewModel(item);
                this.Items.Add(viewModel);
            }

            string status = string.Format(CultureInfo.InvariantCulture, "Loaded {0} listings by color and keyword on page {1}", this.Items.Count, this.PageNumber);
            this.StatusText = status;
        }
    }
}
