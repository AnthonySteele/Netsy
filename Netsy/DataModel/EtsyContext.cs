//-----------------------------------------------------------------------
// <copyright file="EtsyContext.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 
namespace Netsy.DataModel
{
    /// <summary>
    /// Context data for etsy services
    /// Wraps all settings needed, suitable for DI as singleton
    /// </summary>
    public class EtsyContext
    {
        /// <summary>
        /// The defaultUrl to use for all Etsy API access
        /// </summary>
        private const string DefaultBaseUrl = "http://beta-api.etsy.com/v1/";

        /// <summary>
        /// Creates a new instance of the Etsy context with a base URl and APi key
        /// </summary>
        /// <param name="baseUrl">the base Url to use</param>
        /// <param name="apiKey">the API key to use</param>
        public EtsyContext(string baseUrl, string apiKey)
        {
            this.BaseUrl = baseUrl;
            this.ApiKey = apiKey;
        }

        /// <summary>
        /// Creates a new instance of the Etsy context with a base URl and APi key
        /// </summary>
        /// <param name="apiKey">the API key to use</param>
        public EtsyContext(string apiKey)
        {
            this.BaseUrl = DefaultBaseUrl;
            this.ApiKey = apiKey;
        }

        /// <summary>
        /// Gets the Url prefix for the Etsy API
        /// </summary>
        public string BaseUrl { get; private set; }

        /// <summary>
        /// Gets the Applicaiton's Etsy API Key
        /// </summary>
        public string ApiKey { get; private set; }
    }
}
