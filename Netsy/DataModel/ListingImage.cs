//-----------------------------------------------------------------------
// <copyright file="ListingImage.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.DataModel
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Details on an etsy listing image
    /// Fields copied from http://developer.etsy.com/docs#images
    /// Images are a subtype of Listings and will appear when detail_level is high.
    /// </summary>
    public class ListingImage
    {
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
        /// Gets or sets the average color of the listing's primary image, in HSV format.
        /// </summary>
        [DataMember(Name = "hsv_color")]
        public int[] HsvColor { get; set; }

        /// <summary>
        /// Gets or sets the average color of the listing's primary image, in RGB hexadecimal ("web") format.
        /// </summary>
        [DataMember(Name = "rgb_color")]
        public string RgbColor { get; set; }
    }
}
