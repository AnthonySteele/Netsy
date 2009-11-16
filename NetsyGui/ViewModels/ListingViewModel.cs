//-----------------------------------------------------------------------
// <copyright file="ListingViewModel.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace NetsyGui.ViewModels
{
    using System.Globalization;

    using Netsy.DataModel;

    /// <summary>
    /// View model for and Etsy listing
    /// </summary>
    public class ListingViewModel : BaseViewModel
    {
        /// <summary>
        /// The symbol for the listing's currency
        /// </summary>
        private string currencySymbol;

        /// <summary>
        /// Initializes a new instance of the ListingViewModel class
        /// </summary>
        /// <param name="listing">the listing data to show</param>
        public ListingViewModel(Listing listing)
        {
            this.Listing = listing;

            if (! string.IsNullOrEmpty(listing.CurrencyCode))
            {
                this.currencySymbol = Helpers.CurrencySymbolFromCurrencyCode(listing.CurrencyCode);
            }
        }

        /// <summary>
        /// Gets the listing 
        /// </summary>
        public Listing Listing { get; private set;  }

        /// <summary>
        /// Gets the string to display for price
        /// </summary>
        public string PriceData
        {
            get
            {
                return this.currencySymbol + string.Format(CultureInfo.InvariantCulture, "{0:0.00}", Listing.Price) + " " + Listing.CurrencyCode;
            }
        }

        /// <summary>
        /// Gets the listing title
        /// </summary>
        public string Title
        {
            get
            {
                return Listing.Title;
            }
        }

        /// <summary>
        /// Gets the image url to show for the Listing thumbnail
        /// </summary>
        public string ThumbnailImageUrl
        {
            get
            {
                return Listing.ImageUrl155X125;
            }
        }
    }
}
