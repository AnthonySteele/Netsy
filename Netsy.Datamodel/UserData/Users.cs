//-----------------------------------------------------------------------
// <copyright file="Users.cs" company="AFS">
// Copyright (c) AFS. All rights reserved.
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.DataModel.UserData
{
    using System.Runtime.Serialization;

    /// <summary>
    /// A data packet containing users
    /// </summary>
    [DataContract]
    public class Users
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
        public User[] Results { get; set; }

        /// <summary>
        /// Gets or sets the etsy query params
        /// </summary>
        [DataMember(Name = "params")]
        public QueryParams Params { get; set; }
    }
}
