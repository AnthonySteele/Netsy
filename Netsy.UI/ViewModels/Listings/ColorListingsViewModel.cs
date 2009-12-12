//-----------------------------------------------------------------------
// <copyright file="ColorListingsViewModel.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 
namespace Netsy.UI.ViewModels.Listings
{
    using System.Globalization;
    using System.Windows.Threading;

    using Netsy.DataModel;
    using Netsy.Helpers;
    using Netsy.Interfaces;
    using Netsy.UI.Commands;

    /// <summary>
    /// View model for a collection of listings from the front featured listings service
    /// </summary>
    public class ColorListingsViewModel : ListingsServiceViewModel
    {
        /// <summary>
        /// The color to match
        /// </summary>
        private string colorText;

        /// <summary>
        /// Initializes a new instance of the ColorListingsViewModel class.
        /// </summary>
        /// <param name="listingsService">the listings service</param>
        /// <param name="dispatcher">the thread dispatcher</param>
        public ColorListingsViewModel(IListingsService listingsService, Dispatcher dispatcher)
            : base(listingsService, dispatcher)
        {
            this.ListingsService.GetListingsByColorCompleted += this.ListingsReceived;
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
        /// Show the success message
        /// </summary>
        protected override void ShowLoadedSuccessMessage()
        {
            string status = string.Format(CultureInfo.InvariantCulture, "Loaded {0} listings by color on page {1}", this.Items.Count, this.PageNumber);
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
                    if (string.IsNullOrEmpty(this.ColorText))
                    {
                        this.StatusText = "Enter the color";
                        return;
                    }

                    RgbColor rgbColor = this.ConvertColor();

                    int offset = (this.PageNumber - 1) * this.ItemsPerPage;

                    this.ListingsService.GetListingsByColor(rgbColor, Constants.MaxColorWiggle, offset, this.ItemsPerPage, DetailLevel.Medium);
                    string status = string.Format(CultureInfo.InvariantCulture, "Getting {0} listings by color on page {1}", this.ItemsPerPage, this.PageNumber);
                    this.StatusText = status;
                });
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
    }
}
