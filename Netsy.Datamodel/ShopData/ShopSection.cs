//-----------------------------------------------------------------------
// <copyright file="ShopSection.cs" company="AFS">
// Copyright (c) AFS. All rights reserved.
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.DataModel.ShopData
{
    using System.Runtime.Serialization;

    /// <summary>
    /// A shop section from the Etsy API 
    /// Fields copied from http://developer.etsy.com/docs#shop_sections
    /// </summary>
    [DataContract]
    public class ShopSection
    {
        /// <summary>
        /// Gets or sets the numeric ID of the shop section.
        /// </summary>
        [DataMember(Name = "section_id")]
        public int SectionId { get; set; }

        /// <summary>
        /// Gets or sets the title of the section.
        /// </summary>
        [DataMember(Name = "title")]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the number of active listings currently in the section.
        /// </summary>
        [DataMember(Name = "listing_count")]
        public int ListingCount { get; set; }
    }
}