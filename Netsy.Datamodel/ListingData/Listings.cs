//-----------------------------------------------------------------------
// <copyright file="Listings.cs" company="AFS">
// Copyright (c) AFS. All rights reserved.
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.DataModel.ListingData
{
    using System.Runtime.Serialization;

    /// <summary>
    /// A data packet containing Listings
    /// </summary>
    [DataContract]
    public class Listings
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
        public Listing[] Results { get; set; }

        /// <summary>
        /// Gets or sets the etsy query params
        /// </summary>
        [DataMember(Name = "params")]
        public QueryParams Params { get; set; }
    }
}
