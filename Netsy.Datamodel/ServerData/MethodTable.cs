//-----------------------------------------------------------------------
// <copyright file="MethodTable.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.DataModel.ServerData
{
    using System.Runtime.Serialization;

    /// <summary>
    /// result data from GetMethodTable
    /// </summary>
    [DataContract]
    public class MethodTable
    {
        /// <summary>
        /// Gets or sets how many methods were returned
        /// </summary>
        [DataMember(Name = "count")]
        public int Count { get; set; }

        /// <summary>
        /// Gets or sets the result methods
        /// </summary>
        [DataMember(Name = "results")]
        public Method[] Results { get; set; }

        /// <summary>
        /// Gets or sets the etsy query params
        /// </summary>
        [DataMember(Name = "params")]
        public QueryParams Params { get; set; }
    }
}
