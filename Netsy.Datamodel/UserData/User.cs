//-----------------------------------------------------------------------
// <copyright file="User.cs" company="AFS">
// Copyright (c) AFS. All rights reserved.
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.DataModel.UserData
{
    using System;
    using System.Runtime.Serialization;

    using Netsy.Helpers;

    /// <summary>
    /// Details on an etsy user
    /// Fields copied from http://developer.etsy.com/docs#users
    /// User records represent a single user of the site, who may or may not be a seller.
    /// </summary>
    [DataContract]
    public class User
    {
        #region private

        /// <summary>
        /// The user status as a string
        /// </summary>
        private string statusString;

        /// <summary>
        /// The user status as an enum
        /// </summary>
        private UserStatus statusEnum;

        #endregion

        #region low detail

        /// <summary>
        /// Gets or sets the user's login name.
        /// </summary>
        [DataMember(Name = "user_name")]
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the user's numeric ID.  This is also valid as the user's shop ID.
        /// </summary>
        [DataMember(Name = "user_id")]
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the full URL to the user's shop, if s/he is a seller, or to the user's public profile.
        /// </summary>
        [DataMember(Name = "url")]
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the full URL to the user's 25x25 avatar thumbnail.
        /// </summary>
        [DataMember(Name = "image_url_25x25")]
        public string ImageUrl25x25 { get; set; }

        /// <summary>
        /// Gets or sets the full URL to the user's 30x30 avatar thumbnail.
        /// </summary>
        [DataMember(Name = "image_url_30x30")]
        public string ImageUrl30x30 { get; set; }

        /// <summary>
        /// Gets or sets the full URL to the user's 50x50 avatar thumbnail.
        /// </summary>
        [DataMember(Name = "image_url_50x50")]
        public string ImageUrl50x50 { get; set; }

        /// <summary>
        /// Gets or sets the full URL to the user's 50x50 avatar thumbnail.
        /// </summary>
        [DataMember(Name = "image_url_75x75")]
        public string ImageUrl75x75 { get; set; }

        /// <summary>
        /// Gets or sets the date and time the user joined the site, in epoch seconds.
        /// </summary>
        [DataMember(Name = "join_epoch")]
        public double JoinEpoch { get; set; }

        /// <summary>
        /// Gets or sets the user's city and state (freeform entry; may be blank).
        /// </summary>
        [DataMember(Name = "city")]
        public string City { get; set; }

        #endregion

        #region medium detail level 

        /// <summary>
        /// Gets or sets the user's gender (female, male, or private).
        /// </summary>
        [DataMember(Name = "gender")]
        public string Gender { get; set; }

        /// <summary>
        /// Gets or sets the user's latitude (may be blank).
        /// </summary>
        [DataMember(Name = "lat")]
        public double Latitude { get; set; }

        /// <summary>
        /// Gets or sets the user's latitude (may be blank).
        /// </summary>
        [DataMember(Name = "lon")]
        public double Longitude { get; set; }

        /// <summary>
        /// Gets or sets the number of items the user has purchased.
        /// </summary>
        [DataMember(Name = "transaction_buy_count")]
        public int TransactionBuyCount { get; set; }

        /// <summary>
        /// Gets or sets the number of items the user has sold.
        /// </summary>
        [DataMember(Name = "transaction_sold_count")]
        public int TransactionSoldCount { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the user is a seller
        /// </summary>
        [DataMember(Name = "is_seller")]
        public bool IsSeller { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the user was previously featured.
        /// </summary>
        [DataMember(Name = "was_featured_seller")]
        public bool WasFeaturedSeller { get; set; }

        /// <summary>
        /// Gets or sets the user's favorite materials
        /// </summary>
        [DataMember(Name = "materials")]
        public string[] Materials { get; set; }

        /// <summary>
        /// Gets or sets the date and time of the user's last login
        /// </summary>
        [DataMember(Name = "last_login_epoch")]
        public double LastLogin { get; set; }

        #endregion

        #region high detail level

        /// <summary>
        /// Gets or sets the numeric ID of the user's avatar image
        /// </summary>
        [DataMember(Name = "user_image_id")]
        public int UserImageId { get; set; }

        /// <summary>
        /// Gets or sets the number of members the user has referred to the site
        /// </summary>
        [DataMember(Name = "referred_user_count")]
        public int ReferredUserCount { get; set; }

        /// <summary>
        /// Gets or sets the day portion of the user's birthday (may be blank).
        /// </summary>
        [DataMember(Name = "birth_day")]
        public string BirthDay { get; set; }

        /// <summary>
        /// Gets or sets the month portion of the user's birthday (may be blank)
        /// </summary>
        [DataMember(Name = "birth_month")]
        public string BirthMonth { get; set; }

        /// <summary>
        /// Gets or sets the user's biography (may be blank).
        /// </summary>
        [DataMember(Name = "bio")]
        public string Bio { get; set; }

        /// <summary>
        /// Gets or sets the total count of feedback by and about this user.
        /// </summary>
        [DataMember(Name = "feedback_count")]
        public int FeedbackCount { get; set; }

        /// <summary>
        /// Gets or sets the percentage of feedback by or about this user that is positive.
        /// </summary>
        [DataMember(Name = "feedback_percent_positive")]
        public int FeedbackPercentPositive { get; set; }

        #endregion

        #region favourites

        /// <summary>
        /// Gets or sets the date and time that the user was favorited
        /// </summary>
        [DataMember(Name = "favorite_creation_epoch")]
        public DateTime FavoriteCreationEpoch { get; set; }

        /// <summary>
        /// Gets or sets the user's status 
        /// If private, user is a Secret Admirer, and no information about the user can be shown.
        /// </summary>
        [DataMember(Name = "Status")]
        public string StatusString
        {
            get
            {
                return this.statusString;
            }

            set
            {
                this.statusString = value;
                this.statusEnum = value.Parse<UserStatus>();
            }
        }

        /// <summary>
        /// Gets the status as an enum
        /// </summary>
        public UserStatus StatusEnum
        {
            get
            {
                return this.statusEnum;
            }
        }

        #endregion
    }
}
