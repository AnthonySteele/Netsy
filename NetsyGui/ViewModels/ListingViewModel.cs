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
    using System.Collections.ObjectModel;
    using System.Globalization;
    using System.Windows;

    using Netsy.DataModel;
    using NetsyGui;

    /// <summary>
    /// View model for an Etsy listing
    /// </summary>
    public class ListingViewModel : BaseViewModel
    {
        /// <summary>
        /// The numeric ID for this listing
        /// </summary>
        private readonly int listingId;

        /// <summary>
        /// The listing state
        /// </summary>
        private readonly string state;

        /// <summary>
        /// The Listing's Display title text
        /// </summary>
        private readonly string title;

        /// <summary>
        /// the listing Url
        /// </summary>
        private readonly Uri url;

        /// <summary>
        /// The full URL to a 25x25 thumbnail of the listing's image.
        /// </summary>
        private readonly Uri imageUrl25X25;

        /// <summary>
        /// The full URL to a 50x50 thumbnail of the listing's image.
        /// </summary>
        private readonly Uri imageUrl50X50;

        /// <summary>
        /// The full URL to a 75x75 thumbnail of the listing's image.
        /// </summary>
        private readonly Uri imageUrl75X75;

        /// <summary>
        /// The full URL to a 155x125 thumbnail of the listing's image.
        /// </summary>
        private readonly Uri imageUrl155X125;

        /// <summary>
        /// The full URL to a 200x200 thumbnail of the listing's image.
        /// </summary>
        private readonly Uri imageUrl200X200;

        /// <summary>
        /// The full URL to the listing's image, always 430 pixels wide.
        /// </summary>
        private readonly Uri imageUrl430XN;

        /// <summary>
        /// Date when the listing was created
        /// </summary>
        private readonly DateTime? creationDate;

        /// <summary>
        /// How many times the item has been viewed.
        /// </summary>
        private readonly int views;

        /// <summary>
        /// the tags on this item
        /// </summary>
        private readonly string tags;

        /// <summary>
        /// The materials used
        /// </summary>
        private readonly string materials;

        /// <summary>
        /// the listing price
        /// </summary>
        private readonly decimal price;

        /// <summary>
        /// The three-letter currency code
        /// </summary>
        private readonly string currencyCode;

        /// <summary>
        /// The symbol for the listing's currency, e.g. "$" or "£"
        /// </summary>
        private readonly string currencySymbol;
        
        /// <summary>
        ///  date when the listing is ending
        /// </summary>
        private readonly DateTime? endingDate;

        /// <summary>
        /// the id of the user selling it
        /// </summary>
        private readonly int userId;

        /// <summary>
        /// the name of the user who is selling it
        /// </summary>
        private readonly string userName;

        /// <summary>
        /// The quantity available
        /// </summary>
        private readonly int quantity;

        private EtsyColor color;

        private string description;

        private double? latitude;
        private double? longitude;

        private string city;

        private int? sectionId;

        private string sectionTitle;

        private ObservableCollection<ListingImageViewModel> AllImages;

        private DateTime? favoriteCreationDate;
        private double? score;

        /// <summary>
        /// Initializes a new instance of the ListingViewModel class
        /// </summary>
        /// <param name="listing">the listing data to show</param>
        public ListingViewModel(Listing listing)
        {
            this.listingId = listing.ListingId;
            this.state = listing.State;
            this.title = listing.Title;
            this.url = new Uri(listing.Url);
            this.imageUrl25X25 = new Uri(listing.ImageUrl25X25);
            this.imageUrl50X50 = new Uri(listing.ImageUrl50X50);
            this.imageUrl75X75 = new Uri(listing.ImageUrl75X75);
            this.imageUrl155X125 = new Uri(listing.ImageUrl155X125);
            this.imageUrl200X200 = new Uri(listing.ImageUrl200X200);
            this.imageUrl430XN = new Uri(listing.ImageUrl430XN);

            this.creationDate = listing.CreationDate;
            this.views = listing.Views;
            this.tags = listing.Tags.ToCsv();
            this.materials = listing.Materials.ToCsv();
            this.price = (decimal)listing.Price;
            this.currencyCode = listing.CurrencyCode;
            this.endingDate = listing.EndingDate;

            this.userId = listing.UserId;
            this.userName = listing.UserName;
            this.quantity = listing.Quantity;
            this.color = listing.Color;
            this.description = listing.Description;
            this.latitude = listing.Latitude;
            this.longitude = listing.Longitude;
            this.city = listing.City;
            this.sectionId = listing.SectionId;
            this.sectionTitle = listing.SectionTitle;
            this.favoriteCreationDate = listing.FavoriteCreationDate;
            this.score = listing.Score;

            if (!string.IsNullOrEmpty(listing.CurrencyCode))
            {
                this.currencySymbol = Helpers.CurrencySymbolFromCurrencyCode(listing.CurrencyCode);
            }
        }

        /// <summary>
        /// Gets the string to display for price, 
        /// Including currency code and formatted amount
        /// e.g. "$12.40 US"
        /// </summary>
        public string PriceData
        {
            get
            {
                return this.currencySymbol + string.Format(CultureInfo.InvariantCulture, "{0:0.00}", this.price) + " " + this.currencyCode;
            }
        }

        /// <summary>
        /// Gets the numeric ID for this listing
        /// </summary>
        public int ListingId
        {
            get { return this.listingId; }
        }

        /// <summary>
        /// Gets the listing state
        /// </summary>
        public string State
        {
            get { return this.state; }
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
                if (this.creationDate.HasValue)
                {
                    string result = this.creationDate.Value.ToShortDateString();
                    if (this.endingDate.HasValue)
                    {
                        result += " to " + this.endingDate.Value.ToShortDateString();
                    }

                    return result;
                }

                return string.Empty;
            }
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

        /// <summary>
        /// Gets how many times the item has been viewed.
        /// </summary>
        public int Views
        {
            get
            {
                return this.views;
            }
        }
    }
}
