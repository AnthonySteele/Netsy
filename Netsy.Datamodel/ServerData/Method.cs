//-----------------------------------------------------------------------
// <copyright file="Method.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.DataModel.ServerData
{
    using System.Runtime.Serialization;

    /// <summary>
    /// An Etsy API method's details
    /// </summary>
    [DataContract]
    public class Method
    {
        /// <summary>
        /// Gets or sets the name of the method
        /// </summary>
        [DataMember(Name = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the name of the method
        /// </summary>
        [DataMember(Name = "description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the Uri format of the method
        /// </summary>
        [DataMember(Name = "uri")]
        public string Uri { get; set; }

        /// <summary>
        /// Gets or sets the result type of the method
        /// </summary>
        [DataMember(Name = "type")]
        public string ResultType { get; set; }

        /// <summary>
        /// Gets or sets the http method
        /// </summary>
        [DataMember(Name = "http_method")]
        public string HttpMethod { get; set; }
    }
}
