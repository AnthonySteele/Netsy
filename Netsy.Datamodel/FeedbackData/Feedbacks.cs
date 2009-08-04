//-----------------------------------------------------------------------
// <copyright file="Feedbacks.cs" company="AFS">
// Copyright (c) AFS. All rights reserved.
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.DataModel.FeedbackData
{
    using System.Runtime.Serialization;

    /// <summary>
    /// A data packet containing Listings
    /// </summary>
    [DataContract]
    public class Feedbacks
    {
        /// <summary>
        /// Gets or sets how many users were returned
        /// </summary>
        [DataMember(Name = "count")]
        public int Count { get; set; }

        /// <summary>
        /// Gets or sets the result users
        /// </summary>
        [DataMember(Name = "results")]
        public Feedback[] Results { get; set; }

        /// <summary>
        /// Gets or sets the etsy query params
        /// </summary>
        [DataMember(Name = "params")]
        public QueryParams Params { get; set; }
    }
}
