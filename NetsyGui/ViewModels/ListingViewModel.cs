//-----------------------------------------------------------------------
// <copyright file="ListingViewModel.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace NetsyGui.ViewModels
{
    using System;
    using System.Globalization;
    using System.Windows;

    using Netsy.DataModel;
    using NetsyGui;

    /// <summary>
    /// View model for and Etsy listing
    /// </summary>
    public class ListingViewModel : BaseViewModel
    {
        /// <summary>
        /// The symbol for the listing's currency
        /// </summary>
        private readonly string currencySymbol;

        /// <summary>
        /// The Listing's Display title text
        /// </summary>
        private readonly string title;

        /// <summary>
        /// The image url
        /// </summary>
        private readonly string thumbnailImageUrl;

        /// <summary>
        /// The three-letter currency code
        /// </summary>
        private readonly string currencyCode;

        /// <summary>
        /// the listing price
        /// </summary>
        private readonly decimal price;

        /// <summary>
        /// Date when the listing was created
        /// </summary>
        private readonly DateTime? created;

        /// <summary>
        ///  date when the listing is ending
        /// </summary>
        private readonly DateTime? ending;

        /// <summary>
        /// the tags on this item
        /// </summary>
        private readonly string tags;

        /// <summary>
        /// The materials used
        /// </summary>
        private readonly string materials;

        /// <summary>
        /// the name of the user who is selling it
        /// </summary>
        private readonly string userName;

        /// <summary>
        /// the id of the user selling it
        /// </summary>
        private readonly int userId;

        /// <summary>
        /// The quantity avialable
        /// </summary>
        private readonly int quantity;

        /// <summary>
        /// Initializes a new instance of the ListingViewModel class
        /// </summary>
        /// <param name="listing">the listing data to show</param>
        public ListingViewModel(Listing listing)
        {
            this.title = listing.Title;
            this.thumbnailImageUrl = listing.ImageUrl155X125;
            this.currencyCode = listing.CurrencyCode;
            this.price = (decimal)listing.Price;
            this.created = listing.CreationDate;
            this.ending = listing.EndingDate;
            this.userName = listing.UserName;
            this.userId = listing.UserId;
            this.quantity = listing.Quantity;
            this.tags = listing.Tags.ToCsv();
            this.materials = listing.Materials.ToCsv();

            if (!string.IsNullOrEmpty(listing.CurrencyCode))
            {
                this.currencySymbol = Helpers.CurrencySymbolFromCurrencyCode(listing.CurrencyCode);
            }
        }

        /// <summary>
        /// Gets the string to display for price
        /// </summary>
        public string PriceData
        {
            get
            {
                return this.currencySymbol + string.Format(CultureInfo.InvariantCulture, "{0:0.00}", this.price) + " " + this.currencyCode;
            }
        }

        /// <summary>
        /// Gets the listing title
        /// </summary>
        public string Title
        {
            get { return this.title; }
        }

        /// <summary>
        /// Gets data data for display
        /// </summary>
        public string DateDisplay
        {
            get
            {
                if (this.created.HasValue)
                {
                    string result = this.created.Value.ToShortDateString();
                    if (this.ending.HasValue)
                    {
                        result += " to " + this.ending.Value.ToShortDateString();
                    }

                    return result;
                }

                return string.Empty;
            }
        }

        /// <summary>
        /// Gets the image url to show for the Listing thumbnail
        /// </summary>
        public string ThumbnailImageUrl
        {
            get { return this.thumbnailImageUrl; }
        }

        /// <summary>
        /// Gets the tags on the item
        /// </summary>
        public string Tags
        {
            get { return this.tags; }
        }

        /// <summary>
        /// Gets the materials used
        /// </summary>
        public string Materials
        {
            get { return this.materials; }
        }

        /// <summary>
        /// Gets a value that is visible if there are materials
        /// </summary>
        public Visibility HasMaterials
        {
            get
            {
                return string.IsNullOrEmpty(this.Materials) ? Visibility.Collapsed : Visibility.Visible; 
            }
        }

        /// <summary>
        /// Gets the name of the user who is selling it
        /// </summary>
        public string UserName
        {
            get { return this.userName; }
        }

        /// <summary>
        /// Gets the id of the user who is selling it
        /// </summary>
        public int UserId
        {
            get { return this.userId; }
        }

        /// <summary>
        /// Gets the number of items avaialable
        /// </summary>
        public int Quantity
        {
            get { return this.quantity; }
        }
    }
}
