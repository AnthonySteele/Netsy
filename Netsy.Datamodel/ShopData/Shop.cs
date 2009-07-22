﻿//-----------------------------------------------------------------------
// <copyright file="Shop.cs" company="AFS">
// Copyright (c) AFS. All rights reserved.
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.DataModel.ShopData
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Details on an etsy shop
    /// Fields copied from http://developer.etsy.com/docs#shops
    /// </summary>
    [DataContract]
    public class Shop
    {
        #region low detail

        /// <summary>
        /// Gets or sets the full URL to the shops's banner image.
        /// </summary>
        [DataMember(Name = "banner_image_url")]
        public string BannerImageUrl { get; set; }

        /// <summary>
        /// Gets or sets the date and time the shop was last updated, in epoch seconds.
        /// </summary>
        [DataMember(Name = "last_updated_epoch")]
        public string LastUpdatedEpoch { get; set; }

        /// <summary>
        /// Gets or sets the date and time the shop was created, in epoch seconds.
        /// </summary>
        [DataMember(Name = "creation_epoch")]
        public string CreationEpoch { get; set; }

        /// <summary>
        /// Gets or sets the number of active listings in the shop.
        /// </summary>
        [DataMember(Name = "listing_count")]
        public int ListingCount { get; set; }

        #endregion

        #region medium detail

        /// <summary>
        /// Gets or sets the shop's name.
        /// </summary>
        [DataMember(Name = "shop_name")]
        public string ShopName { get; set; }
        
        /// <summary>
        /// Gets or sets a brief heading for the shop's main page.
        /// </summary>
        [DataMember(Name = "title")]
        public string Title { get; set; }
        
        /// <summary>
        /// Gets or sets a message that is sent to users who buy from this shop.
        /// </summary>
        [DataMember(Name = "sale_message")]
        public string SaleMessage { get; set; }
        
        #endregion

        #region high detail

        /// <summary>
        /// Gets or sets an announcement to buyers that displays on the shop's homepage.
        /// </summary>
        [DataMember(Name = "announcement")]
        public string Announcement { get; set; }

        /// <summary>
        /// Gets or sets the vacation flag - 1 if the seller is not currently accepting purchases, 0 otherwise.
        /// </summary>
        [DataMember(Name = "is_vacation")]
        public int IsVacation { get; set; }

        /// <summary>
        /// Gets or sets the vacation message, If is_vacation=1, a message to buyers.
        /// </summary>
        [DataMember(Name = "vacation_message")]
        public string VacationMessage { get; set; }

        /// <summary>
        /// Gets or sets the ISO code (alphabetic) for the seller's native currency.
        /// </summary>
        [DataMember(Name = "currency_code")]
        public string CurrencyCode { get; set; }

        /// <summary>
        /// Gets or sets the introductory text from the top of the seller's policies page (may be blank).
        /// </summary>
        [DataMember(Name = "policy_welcome")]
        public string PolicyWelcome { get; set; }
        
        /// <summary>
        /// Gets or sets the seller's policy on payment (may be blank).
        /// </summary>
        [DataMember(Name = "policy_payment")]
        public string PolicyPayment { get; set; }
        
        /// <summary>
        /// Gets or sets the seller's policy on shipping (may be blank).
        /// </summary>
        [DataMember(Name = "policy_shipping")]
        public string PolicyShipping { get; set; }
        
        /// <summary>
        /// Gets or sets the seller's policy on refunds (may be blank).
        /// </summary>
        [DataMember(Name = "policy_refunds")]
        public string PolicyRefunds { get; set; }
        
        /// <summary>
        /// Gets or sets any additional policy information the seller provides (may be blank).
        /// </summary>
        [DataMember(Name = "policy_additional")]
        public string PolicyAdditional { get; set; }
        
        /// <summary>
        /// Gets or sets an array of shop-section objects
        /// </summary>
        [DataMember(Name = "sections")]
        public ShopSection[] Sections { get; set; }

        #endregion
    }
}
