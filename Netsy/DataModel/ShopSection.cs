//-----------------------------------------------------------------------
// <copyright file="ShopSection.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.DataModel
{
    using System.Runtime.Serialization;

    /// <summary>
    /// A shop section from the Etsy API 
    /// Fields copied from http://developer.etsy.com/docs#shop_sections
    /// Some sellers may choose to organize their listings into sections.  Each section is specific to a shop and has a numeric ID and a title.
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