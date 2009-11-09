﻿//-----------------------------------------------------------------------
// <copyright file="Listing.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.DataModel
{
    using System;
    using System.Runtime.Serialization;

    using Helpers;

    /// <summary>
    /// Etsy listing data. Fields from http://developer.etsy.com/docs#listings
    /// Listing records represent an item for sale on Etsy.
    /// </summary>
    [DataContract]
    public class Listing
    {
        #region private

        /// <summary>
        /// The listing state as a strung
        /// </summary>
        private string stateString;

        /// <summary>
        /// The listing state as an enum
        /// </summary>
        private ListingState stateEnum;

        /// <summary>
        /// Creation datetime, in epoch seconds
        /// </summary>
        private double creationEpoch;

        /// <summary>
        /// Creation date, as DateTime
        /// </summary>
        private DateTime creationDate;

        /// <summary>
        /// Ending datetime, in epoch seconds
        /// </summary>
        private double endingEpoch;

        /// <summary>
        /// Ending datetime, as DateTime
        /// </summary>
        private DateTime endingDate;

        #endregion

        #region low detail

        /// <summary>
        /// Gets or sets the numeric ID for this listing.
        /// </summary>
        [DataMember(Name = "listing_id")]
        public int ListingId { get; set; }

        /// <summary>
        /// Gets or sets the state. One of active, removed, soldout, expired, or alchemy.
        /// </summary>
        [DataMember(Name = "state")]
        public string State
        {
            get
            {
                return this.stateString;
            }

            set
            {
                this.stateString = value;
                
                // translate "sold_out" to SoldOut"- the underscore (not there in the docs) causes trouble 
                if (string.Equals(this.stateString, "sold_out", StringComparison.OrdinalIgnoreCase))
                {
                    this.stateEnum = ListingState.SoldOut;
                }
                else
                {
                    this.stateEnum = value.Parse<ListingState>();                    
                }
            }
        }

        /// <summary>
        /// Gets the listing state as an enum
        /// </summary>
        public ListingState StateEnum
        {
          get
          {
              return this.stateEnum;
          }
        }

        /// <summary>
        /// Gets or sets the listing's title.
        /// </summary>
        [DataMember(Name = "title")]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the full URL to the listing's page.
        /// </summary>
        [DataMember(Name = "url")]
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the full URL to a 25x25 thumbnail of the listing's image.
        /// </summary>
        [DataMember(Name = "image_url_25x25")]
        public string ImageUrl25X25 { get; set; }

        /// <summary>
        /// Gets or sets the full URL to a 50x50 thumbnail of the listing's image.
        /// </summary>
        [DataMember(Name = "image_url_50x50")]
        public string ImageUrl50X50 { get; set; }

        /// <summary>
        /// Gets or sets the full URL to a 75x75 thumbnail of the listing's image.
        /// </summary>
        [DataMember(Name = "image_url_75x75")]
        public string ImageUrl75X75 { get; set; }

        /// <summary>
        /// Gets or sets the full URL to a 155x125 thumbnail of the listing's image.
        /// </summary>
        [DataMember(Name = "image_url_155x125")]
        public string ImageUrl155X125 { get; set; }

        /// <summary>
        /// Gets or sets the full URL to a 200x200 thumbnail of the listing's image.
        /// </summary>
        [DataMember(Name = "image_url_200x200")]
        public string ImageUrl200X200 { get; set; }

        /// <summary>
        /// Gets or sets the full URL to the listing's image, always 430 pixels wide.
        /// </summary>
        [DataMember(Name = "image_url_430xN")]
        public string ImageUrl430XN { get; set; }

        /// <summary>
        /// Gets or sets the date and time the listing was posted, in epoch seconds.
        /// </summary>
        [DataMember(Name = "creation_epoch")]
        public double CreationEpoch 
        {
            get
            {
                return this.creationEpoch;
            }

            set
            {
                this.creationEpoch = value;
                this.creationDate = this.creationEpoch.ToDateTimeFromEpoch();
            }
        }

        /// <summary>
        /// Gets or sets the date and time the feedback was posted, as Date time
        /// </summary>
        public DateTime CreationDate
        {
            get
            {
                return this.creationDate;
            }

            set
            {
                this.creationDate = value;
                this.creationEpoch = this.creationDate.ToEpochFromDateTime();
            }
        }
        
        #endregion

        #region medium detail

        /// <summary>
        /// Gets or sets how many times the item has been viewed.
        /// </summary>
        [DataMember(Name = "views")]
        public int Views { get; set; }

        /// <summary>
        /// Gets or sets a list of tags for the item.
        /// </summary>
        [DataMember(Name = "tags")]
        public string[] Tags { get; set; }

        /// <summary>
        /// Gets or sets a list of materials used in the item.
        /// </summary>
        [DataMember(Name = "materials")]
        public string[] Materials { get; set; }

        /// <summary>
        /// Gets or sets the item's price.
        /// </summary>
        [DataMember(Name = "price")]
        public double Price { get; set; }

        /// <summary>
        /// Gets or sets the ISO (alphabetic) code for the item's currency.
        /// </summary>
        [DataMember(Name = "currency_code")]
        public string CurrencyCode { get; set; }

        /// <summary>
        /// Gets or sets the listing's expiration date and time, in epoch seconds.
        /// </summary>
        [DataMember(Name = "ending_epoch")]
        public double EndingEpoch
        {
            get
            {
                return this.endingEpoch;
            }

            set
            {
                this.endingEpoch = value;
                this.endingDate = this.endingEpoch.ToDateTimeFromEpoch();
            }
        }

        /// <summary>
        /// Gets or sets the date and time the feedback was posted, as Date time
        /// </summary>
        public DateTime EndingDate
        {
            get
            {
                return this.endingDate;
            }

            set
            {
                this.endingDate = value;
                this.endingEpoch = this.endingDate.ToEpochFromDateTime();
            }
        }
        
        /// <summary>
        /// Gets or sets the numeric ID of the user who posted the item. (User IDs are also shop IDs).
        /// </summary>
        [DataMember(Name = "user_id")]
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the login name of the user who posted the item
        /// </summary>
        [DataMember(Name = "user_name")]
        public string UserName { get; set; }
        
        /// <summary>
        /// Gets or sets the quantity of this item available for sale.
        /// </summary>
        [DataMember(Name = "quantity")]
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the average color of the listing's primary image, in HSV format.
        /// </summary>
        [DataMember(Name = "hsv_color")]
        public string HsvColor { get; set; }
        
        /// <summary>
        /// Gets or sets the average color of the listing's primary image, in RGB hexadecimal ("web") format.
        /// </summary>
        [DataMember(Name = "rgb_color")]
        public string RgbColor { get; set; }
        
        #endregion

        #region high detail

        /// <summary>
        /// Gets or sets the average color of the listing's primary image, in RGB hexadecimal ("web") format.
        /// </summary>
        [DataMember(Name = "description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the latitude of the user selling the item (may be blank).
        /// </summary>
        [DataMember(Name = "lat")]
        public string Latitude { get; set; }

        /// <summary>
        /// Gets or sets the longitude of the user selling the item (may be blank).
        /// </summary>
        [DataMember(Name = "lon")]
        public string Longitude { get; set; }

        /// <summary>
        /// Gets or sets the user's city and state (user-supplied; may be blank).
        /// </summary>
        [DataMember(Name = "city")]
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the numeric ID of the section to which this listing belongs. If the shop uses sections.
        /// </summary>
        [DataMember(Name = "section_id")]
        public string SectionId { get; set; }

        /// <summary>
        /// Gets or sets the title of the section to which this listing belongs.
        /// </summary>
        [DataMember(Name = "section_title")]
        public string SectionTitle { get; set; }

        /// <summary>
        /// Gets or sets an array of image objects (
        /// </summary>
        [DataMember(Name = "all_images")]
        public ListingImage[] AllImages { get; set; }

        /// <summary>
        /// Gets or sets the date and time that the user was favorited (only available in the command getFavoriteListingsOfUser.)
        /// </summary>
        [DataMember(Name = "favorite_creation_epoch")]
        public string FavoriteCreationEpoch { get; set; }

        #endregion
    }
}
