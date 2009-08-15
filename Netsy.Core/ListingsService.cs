//-----------------------------------------------------------------------
// <copyright file="ListingsService.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.Core
{
    using DataModel;

    /// <summary>
    /// Implementation of the listings service
    /// </summary>
    public class ListingsService
    {
        /// <summary>
        /// the Etsy context data
        /// </summary>
        private readonly EtsyContext etsyContext;

        /// <summary>
        /// Initializes a new instance of the ListingsService class
        /// </summary>
        /// <param name="etsyContext">the etsy context to use</param>
        public ListingsService(EtsyContext etsyContext)
        {
            this.etsyContext = etsyContext;
        }
    }
}
