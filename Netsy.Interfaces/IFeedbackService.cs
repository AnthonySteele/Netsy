//-----------------------------------------------------------------------
// <copyright file="IFeedbackService.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 
namespace Netsy.Interfaces
{
    using System;

    using Helpers;
    using Netsy.DataModel.FeedbackData;

    /// <summary>
    /// Interface to Feeback Commands on the etsy API
    /// </summary>
    public interface IFeedbackService
    {
        event EventHandler<ResultEventArgs<Feedbacks>> GetFeedbackCompleted;
        event EventHandler<ResultEventArgs<Feedbacks>> GetFeedbackForUserCompleted;
        event EventHandler<ResultEventArgs<Feedbacks>> GetFeedbackAsBuyerCompleted;
        event EventHandler<ResultEventArgs<Feedbacks>> GetFeedbackAsSellerCompleted;

        IAsyncResult GetFeedback(int feedbackId);

        IAsyncResult GetFeedbackForUser(int userId, int offset, int limit);
        IAsyncResult GetFeedbackForUser(string userName, int offset, int limit);

        IAsyncResult GetFeedbackAsBuyer(int userId, int offset, int limit);
        IAsyncResult GetFeedbackAsBuyer(string userName, int offset, int limit);

        IAsyncResult GetFeedbackForOthers(int userId, int offset, int limit);
        IAsyncResult GetFeedbackForOthers(string userName, int offset, int limit);

        IAsyncResult GetFeedbackAsSeller(int userId, int offset, int limit);
        IAsyncResult GetFeedbackAsSeller(string userName, int offset, int limit);
    }
}
