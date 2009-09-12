//-----------------------------------------------------------------------
// <copyright file="Method.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.DataModel
{
    using System.Runtime.Serialization;

    /// <summary>
    /// An Etsy API method's details
    /// </summary>
    [DataContract]
    public class Method
    {
        /// <summary>
        /// Gets or sets a descriptive name for the command.
        /// </summary>
        [DataMember(Name = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets brief text explaining the method.
        /// </summary>
        [DataMember(Name = "description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the method's relative URI pattern 
        /// with embedded parameters in curly braces ("{" and "}").
        /// </summary>
        [DataMember(Name = "uri")]
        public string Uri { get; set; }

        /// <summary>
        /// Gets or sets the return type of this method. 
        /// Valid return types are user, shop, listing, tag, gift-guide and method.
        /// </summary>
        [DataMember(Name = "type")]
        public string ResultType { get; set; }

        /// <summary>
        /// Gets or sets the HTTP method used in this call.  
        /// Currently all methods accept only GET. 
        /// Future version of the Etsy API will accept POST, PUT and DELETE.
        /// </summary>
        [DataMember(Name = "http_method")]
        public string HttpMethod { get; set; }
    }
}
