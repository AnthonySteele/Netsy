//-----------------------------------------------------------------------
// <copyright file="Feedback.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.DataModel
{
    using System;
    using System.Runtime.Serialization;

    using Netsy.Helpers;

    /// <summary>
    /// Etsy Feedback data. Fields from  http://developer.etsy.com/docs#feedback
    /// Feedback records represent a comment left by either the buyer or seller in a transaction.  
    /// Every sale on Etsy creates two opportunities for a user to leave feedback: one, by the buyer commenting on the seller, and the second by the seller commenting on the buyer.
    /// </summary>
    public class Feedback
    {
        #region private

        /// <summary>
        /// Creation datetime, in epoch seconds
        /// </summary>
        private string creationEpoch;

        /// <summary>
        /// Creation date, as DateTime
        /// </summary>
        private DateTime? creationDate;

        #endregion

        /// <summary>
        /// Gets or sets the numeric ID for the feeback record.
        /// </summary>
        [DataMember(Name = "feedback_id")]
        public int FeedbackId { get; set; }

        /// <summary>
        /// Gets or sets the numeric ID of the sold item.
        /// </summary>
        [DataMember(Name = "listing_id")]
        public int ListingId { get; set; }

        /// <summary>
        /// Gets or sets the sold item's title.
        /// </summary>
        [DataMember(Name = "title")]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the fully-qualified URL to the sold item's listing page.
        /// </summary>
        [DataMember(Name = "url")]
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the date and time the feedback was posted, in epoch seconds.
        /// </summary>
        [DataMember(Name = "creation_epoch")]
        public string CreationEpoch
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
        public DateTime? CreationDate
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

        /// <summary>
        /// Gets or sets the user ID of the person user leaving the feedback.
        /// </summary>
        [DataMember(Name = "author_user_id")]
        public int AuthorUserId { get; set; }

        /// <summary>
        /// Gets or sets the user ID of the person receiving feedback.
        /// </summary>
        [DataMember(Name = "subject_user_id")]
        public int SubjectUserId { get; set; }

        /// <summary>
        /// Gets or sets the user ID of the user acting as seller in this transaction
        /// </summary>
        [DataMember(Name = "seller_user_id")]
        public int SellerUserId { get; set; }

        /// <summary>
        /// Gets or sets the user ID of the user acting as buyer in this transaction.
        /// </summary>
        [DataMember(Name = "buyer_user_id")]
        public int BuyerUserId { get; set; }

        /// <summary>
        /// Gets or sets a short message, left by the author.
        /// </summary>
        [DataMember(Name = "message")]
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the disposition as a string. One of positive, neutral or negative.
        /// </summary>
        [DataMember(Name = "disposition")]
        public string Disposition { get; set; }

        /// <summary>
        /// Gets or sets the numeric value of the feedback disposition (-1..1).
        /// </summary>
        [DataMember(Name = "value")]
        public int Value { get; set; }

        /// <summary>
        /// Gets or sets the full URL to a thumbnail of an image posted by the feedback author (may be blank)..
        /// </summary>
        [DataMember(Name = "image_url_25x25")]
        public string ImageUrl25X25 { get; set; }

        /// <summary>
        /// Gets or sets the full URL to an image posted by the feedback author (may be blank)
        /// </summary>
        [DataMember(Name = "image_url_fullxfull")]
        public string ImageUrlFullXFull { get; set; }
    }
}
