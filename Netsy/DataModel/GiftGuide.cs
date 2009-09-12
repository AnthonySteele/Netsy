//-----------------------------------------------------------------------
// <copyright file="GiftGuide.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.DataModel
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Etsy gift guides data. Fields from http://developer.etsy.com/docs#gift_guides
    /// Gift guides display summary information about each gift guide on the Etsy website.  
    /// Gift Guides don't support the detail_level parameter; all fields are available at all times
    /// </summary>
    [DataContract]
    public class GiftGuide
    {
        /// <summary>
        /// Gets or sets the numeric ID for this guide.
        /// </summary>
        [DataMember(Name = "guide_id")]
        public int GuideId { get; set; }

        /// <summary>
        /// Gets or sets the date and time the date and time the gift guide was created, in epoch seconds.
        /// </summary>
        [DataMember(Name = "creation_tsz_epoch")]
        public double CreationEpoch { get; set; }

        /// <summary>
        /// Gets or sets a short description of the guide
        /// </summary>
        [DataMember(Name = "description")]
        public string Description { get; set; }

                /// <summary>
        /// Gets or sets the guide's main title.
        /// </summary>
        [DataMember(Name = "title")]
        public string Title { get; set; }
            
        /// <summary>
        /// Gets or sets a field on which the guides can be sorted.
        /// </summary>
        [DataMember(Name = "display_order")]
        public int DisplayOrder { get; set; }

        /// <summary>
        /// Gets or sets the numeric ID of the guide's parent section.
        /// </summary>
        [DataMember(Name = "guide_section_id")]
        public int GuideSectionId { get; set; }
        
        /// <summary>
        /// Gets or sets the title of the guide's parent section.
        /// </summary>
        [DataMember(Name = "guide_section_title")]
        public string GuideSectionTitle { get; set; }
    }
}
