//-----------------------------------------------------------------------
// <copyright file="ListingViewModel.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 
namespace Netsy.UI.ViewModels
{
    using System.Globalization;

    using Netsy.DataModel;

    /// <summary>
    /// View model for a listing
    /// </summary>
    public class ListingViewModel
    {
        /// <summary>
        /// the listing data transfer object
        /// </summary>
        private readonly Listing listing;

        /// <summary>
        /// The currency cymbol, e.g. "$" or "£"
        /// </summary>
        private readonly string currencySymbol = string.Empty;

        /// <summary>
        /// Initializes a new instance of the ListingViewModel class
        /// </summary>
        /// <param name="listing">the listing Data transfer object</param>
        public ListingViewModel(Listing listing)
        {
            this.listing = listing;

            if (!string.IsNullOrEmpty(listing.CurrencyCode))
            {
                this.currencySymbol = CurrencySymbolLookup.CurrencySymbolFromCurrencyCode(listing.CurrencyCode);
            }
        }

        /// <summary>
        /// Gets the Listing data transfer object
        /// </summary>
        public Listing Listing
        {
            get { return this.listing; }
        }

        /// <summary>
        /// Gets the price display data 
        /// </summary>
        public string PriceData
        {
            get
            {
                return this.currencySymbol + 
                    string.Format(CultureInfo.InvariantCulture, "{0:0.00}", this.Listing.Price) + 
                    " " + 
                    this.Listing.CurrencyCode;
            }
        }
    }
}
