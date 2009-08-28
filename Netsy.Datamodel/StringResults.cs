//-----------------------------------------------------------------------
// <copyright file="StringResults.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.DataModel
{
    using System.Runtime.Serialization;

    /// <summary>
    /// A data packet containing users
    /// </summary>
    [DataContract]
    public class StringResults
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
        public string[] Results { get; set; }

        /// <summary>
        /// Gets or sets the etsy query params
        /// </summary>
        [DataMember(Name = "params")]
        public QueryParams Params { get; set; }
    }
}
