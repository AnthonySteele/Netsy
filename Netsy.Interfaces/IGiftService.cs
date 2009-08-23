//-----------------------------------------------------------------------
// <copyright file="IGiftService.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.Interfaces
{
    using System;

    using DataModel;
    using DataModel.ListingData;

    using Helpers;

    /// <summary>
    /// Interface to Gift Guide Commands on the etsy API
    /// </summary>
    public interface IGiftService
    {
        event EventHandler<ResultEventArgs<Listings>> GetGiftGuidesCompleted;
        event EventHandler<ResultEventArgs<Listings>> GGetGiftGuideListingsCompleted;

        IAsyncResult GetGiftGuides(int guideId, int offset, int limit, DetailLevel detailLevel);
        IAsyncResult GetGiftGuideListings(int guideId, int offset, int limit, DetailLevel detailLevel);
    }
}
