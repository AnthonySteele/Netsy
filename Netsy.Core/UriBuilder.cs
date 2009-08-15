//-----------------------------------------------------------------------
// <copyright file="UriBuilder.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.Core
{
    using System;
    using System.Globalization;
    using System.Text;

    using Netsy.DataModel;
    using Netsy.Helpers;

    /// <summary>
    /// Class to build a Uri to the Etsy site
    /// </summary>
    internal class UriBuilder
    {
        /// <summary>
        /// the Etsy context data
        /// </summary>
        private readonly EtsyContext etsyContext;

        /// <summary>
        /// The Uri buing built
        /// </summary>
        private readonly StringBuilder result = new StringBuilder();

        /// <summary>
        /// does the uri have params yet
        /// </summary>
        private bool hasUriParams;

        /// <summary>
        /// Initializes a new instance of the UriBuilder class
        /// </summary>
        /// <param name="etsyContext">the etsy context to use</param>
        private UriBuilder(EtsyContext etsyContext)
        {
            this.etsyContext = etsyContext;
        }

        /// <summary>
        /// Start the Uri with the base uri
        /// </summary>
        /// <param name="etsyContext">the etsy context</param>
        /// <returns>the Uri builder</returns>
        public static UriBuilder Start(EtsyContext etsyContext)
        {
            UriBuilder instance = new UriBuilder(etsyContext);
            instance.Append(etsyContext.BaseUrl);

            return instance;
        }

        /// <summary>
        /// Append part of a url
        /// </summary>
        /// <param name="urlPart">the partial url text to append</param>
        /// <returns>the Uri builder</returns>
        public UriBuilder Append(string urlPart)
        {
            this.result.Append(urlPart);

            return this;
        }

        /// <summary>
        /// Append part of a url
        /// </summary>
        /// <param name="value">the value to append</param>
        /// <returns>the Uri builder</returns>
        public UriBuilder Append(int value)
        {
            this.result.Append(value.ToString(CultureInfo.InvariantCulture));

            return this;
        }

        /// <summary>
        /// Append a uri param to the uri
        /// </summary>
        /// <param name="paramName">the param name</param>
        /// <param name="paramValue">the param value</param>
        /// <returns>the uri builder</returns>
        public UriBuilder Param(string paramName, string paramValue)
        {
            if (this.hasUriParams)
            {
                this.result.Append("&");
            }
            else
            {
                // first param starts this way
                this.result.Append("?");
            }

            this.result.Append(paramName);
            this.result.Append("=");
            this.result.Append(paramValue);

            this.hasUriParams = true;
            return this;
        }

        /// <summary>
        /// Append a uri param to the uri
        /// </summary>
        /// <param name="paramName">the param name</param>
        /// <param name="paramValue">the param value</param>
        /// <returns>the uri builder</returns>
        public UriBuilder Param(string paramName, int paramValue)
        {
            return this.Param(paramName, paramValue.ToString(CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Append an optional uri param to the uri
        /// </summary>
        /// <param name="paramName">the param name</param>
        /// <param name="paramValue">the param value</param>
        /// <returns>the uri builder</returns>
        public UriBuilder OptionalParam(string paramName, int? paramValue)
        {
            if (paramValue.HasValue)
            {
                this.Param(paramName, paramValue.Value);
            }

            return this;
        }

        /// <summary>
        /// Append an offset to the uri
        /// </summary>
        /// <param name="offset">the offset</param>
        /// <returns>the uri builder</returns>
        public UriBuilder Offset(int offset)
        {
            return this.Param("offset", offset);
        }

        /// <summary>
        /// Append an limit to the uri
        /// </summary>
        /// <param name="limit">the results limit</param>
        /// <returns>the uri builder</returns>
        public UriBuilder Limit(int limit)
        {
            return this.Param("limit", limit);
        }

        /// <summary>
        /// Append a detail level to the uri
        /// </summary>
        /// <param name="detailLevel">the detail level</param>
        /// <returns>the uri builder</returns>
        public UriBuilder DetailLevel(DetailLevel detailLevel)
        {
            return this.Param("detail_level", detailLevel.ToStringLower());
        }

        /// <summary>
        /// Append a sort field to the uri
        /// </summary>
        /// <param name="sortField">the sort field</param>
        /// <returns>the uri builder</returns>
        public UriBuilder SortOn(SortField sortField)
        {
            return this.Param("sort_on", sortField.ToStringLower());
        }

        /// <summary>
        /// Append a sort order to the uri
        /// </summary>
        /// <param name="sortOrder">the sort order</param>
        /// <returns>the uri builder</returns>
        public UriBuilder SortOrder(SortOrder sortOrder)
        {
            return this.Param("sort_order", sortOrder.ToStringLower());
        }

        /// <summary>
        /// Append the Api key
        /// </summary>
        /// <returns>the uri builder</returns>
        public UriBuilder AppendApiKey()
        {
            return this.Param("api_key", this.etsyContext.ApiKey);             
        }

        /// <summary>
        /// Generate the resulting Uri
        /// </summary>
        /// <returns>the generated Uri</returns>
        public Uri Result()
        {
            return new Uri(this.result.ToString());
        }

        /// <summary>
        /// Display for debug
        /// </summary>
        /// <returns>the object state as a string</returns>
        public override string ToString()
        {
            return this.result.ToString();
        }
    }
}
