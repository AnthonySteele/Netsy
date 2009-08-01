//-----------------------------------------------------------------------
// <copyright file="ListingImage.cs" company="AFS">
// Copyright (c) AFS. All rights reserved.
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.DataModel.ListingData
{
    using System.Runtime.Serialization;

    public class ListingImage
    {
        /// <summary>
        /// Gets or sets the full URL to a 25x25 thumbnail of the listing's image.
        /// </summary>
        [DataMember(Name = "image_url_25x25")]
        public string ImageUrl25x25 { get; set; }

        /// <summary>
        /// Gets or sets the full URL to a 50x50 thumbnail of the listing's image.
        /// </summary>
        [DataMember(Name = "image_url_50x50")]
        public string ImageUrl50x50 { get; set; }

        /// <summary>
        /// Gets or sets the full URL to a 75x75 thumbnail of the listing's image.
        /// </summary>
        [DataMember(Name = "image_url_75x75")]
        public string ImageUrl75x75 { get; set; }

        /// <summary>
        /// Gets or sets the full URL to a 155x125 thumbnail of the listing's image.
        /// </summary>
        [DataMember(Name = "image_url_155x125")]
        public string ImageUrl155x125 { get; set; }

        /// <summary>
        /// Gets or sets the full URL to a 200x200 thumbnail of the listing's image.
        /// </summary>
        [DataMember(Name = "image_url_200x200")]
        public string ImageUrl200x200 { get; set; }

        /// <summary>
        /// Gets or sets the full URL to the listing's image, always 430 pixels wide.
        /// </summary>
        [DataMember(Name = "image_url_430xN")]
        public string ImageUrl430xN { get; set; }


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
