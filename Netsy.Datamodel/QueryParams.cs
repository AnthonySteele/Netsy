//-----------------------------------------------------------------------
// <copyright file="QueryParams.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.DataModel
{
    using System.Runtime.Serialization;

    using Netsy.Helpers;

    /// <summary>
    /// Etsy query params data
    /// </summary>
    [DataContract]
    public class QueryParams
    {
        /// <summary>
        /// the detail level as a string
        /// </summary>
        private string detailLevelString;

        /// <summary>
        /// the detail level as an enum
        /// </summary>
        private DetailLevel detailLevelEnum;

        /// <summary>
        /// Gets or sets the user id
        /// </summary>
        [DataMember(Name = "user_id")]
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the detail level as a string
        /// </summary>
        [DataMember(Name = "detail_level")]
        public string DetailLevelString
        {
            get
            {
                return this.detailLevelString;
            }

            set
            {
                this.detailLevelString = value;
                this.detailLevelEnum = value.Parse<DetailLevel>();
            }
        }

        /// <summary>
        /// Gets the detail level as an enum
        /// </summary>
        public DetailLevel DetailLevelEnum
        {
            get
            {
                return this.detailLevelEnum;
            }
        }
    }
}
