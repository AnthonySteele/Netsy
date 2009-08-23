//-----------------------------------------------------------------------
// <copyright file="GiftService.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 
namespace Netsy.Core
{
    using System;

    using Netsy.DataModel;
    using Netsy.Interfaces;

    /// <summary>
    /// Implementation of the Feedback service
    /// </summary>
    public class GiftService
    {
        /// <summary>
        /// the Etsy context data
        /// </summary>
        private readonly EtsyContext etsyContext;

        /// <summary>
        /// Initializes a new instance of the GiftService class
        /// </summary>
        /// <param name="etsyContext">the etsy context to use</param>
        public GiftService(EtsyContext etsyContext)
        {
            this.etsyContext = etsyContext;
        }
    }
}
