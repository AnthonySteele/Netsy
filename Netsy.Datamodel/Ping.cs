//-----------------------------------------------------------------------
// <copyright file="Ping.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.DataModel
{
    using System.Runtime.Serialization;

    /// <summary>
    ///  result data from Ping
    /// </summary>
    [DataContract]
    public class Ping
    {
        /// <summary>
        /// Gets or sets the result value from the ping
        /// </summary>
        [DataMember(Name = "results")]
        public string Results { get; set; }
    }
}
