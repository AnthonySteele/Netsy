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

    using Netsy.DataModel;
    using Netsy.Helpers;

    /// <summary>
    /// Interface to Feeback Commands on the etsy API
    /// </summary>
    public interface IFeedbackService
    {
        /// <summary>
        /// GetFeedback completed event
        /// </summary>
        event EventHandler<ResultEventArgs<Feedbacks>> GetFeedbackCompleted;

        /// <summary>
        /// GetFeedbackForUser completed event
        /// </summary>
        event EventHandler<ResultEventArgs<Feedbacks>> GetFeedbackForUserCompleted;

        /// <summary>
        /// GetFeedbackAsBuyer completed event
        /// </summary>
        event EventHandler<ResultEventArgs<Feedbacks>> GetFeedbackAsBuyerCompleted;

        /// <summary>
        /// GetFeedbackForOther completed event
        /// </summary>
        event EventHandler<ResultEventArgs<Feedbacks>> GetFeedbackForOthersCompleted;

        /// <summary>
        /// GetFeedbackAsSeller completed event
        /// </summary>
        event EventHandler<ResultEventArgs<Feedbacks>> GetFeedbackAsSellerCompleted;

        /// <summary>
        /// Get a feedback record
        /// </summary>
        /// <param name="feedbackId">the id of the record</param>
        /// <returns>the Async state of the request</returns>
        IAsyncResult GetFeedback(int feedbackId);

        /// <summary>
        /// Get a list of all feedback for a particular user.
        /// </summary>
        /// <param name="userId">the user's numeric ID</param>
        /// <param name="offset">To page through large result sets, set offset to a multiple of limit</param>
        /// <param name="limit">Specify the number of results to return</param>
        /// <returns>The Async state of the request</returns>
        IAsyncResult GetFeedbackForUser(int userId, int offset, int limit);

        /// <summary>
        /// Get a list of all feedback for a particular user.
        /// </summary>
        /// <param name="userName">the user's name</param>
        /// <param name="offset">To page through large result sets, set offset to a multiple of limit</param>
        /// <param name="limit">Specify the number of results to return</param>
        /// <returns>The Async state of the request</returns>
        IAsyncResult GetFeedbackForUser(string userName, int offset, int limit);

        /// <summary>
        /// Get a list of all feedback where the user was a buyer in the transaction.
        /// </summary>
        /// <param name="userId">the user's numeric ID</param>
        /// <param name="offset">To page through large result sets, set offset to a multiple of limit</param>
        /// <param name="limit">Specify the number of results to return</param>
        /// <returns>The Async state of the request</returns>
        IAsyncResult GetFeedbackAsBuyer(int userId, int offset, int limit);

        /// <summary>
        /// Get a list of all feedback where the user was a buyer in the transaction.
        /// </summary>
        /// <param name="userName">the user's name</param>
        /// <param name="offset">To page through large result sets, set offset to a multiple of limit</param>
        /// <param name="limit">Specify the number of results to return</param>
        /// <returns>The Async state of the request</returns>
        IAsyncResult GetFeedbackAsBuyer(string userName, int offset, int limit);

        /// <summary>
        /// Get a list of all feedback that the user left feedback for someone else.
        /// </summary>
        /// <param name="userId">the user's numeric ID</param>
        /// <param name="offset">To page through large result sets, set offset to a multiple of limit</param>
        /// <param name="limit">Specify the number of results to return</param>
        /// <returns>The Async state of the request</returns>
        IAsyncResult GetFeedbackForOthers(int userId, int offset, int limit);

        /// <summary>
        /// Get a list of all feedback that the user left feedback for someone else.
        /// </summary>
        /// <param name="userName">the user's name</param>
        /// <param name="offset">To page through large result sets, set offset to a multiple of limit</param>
        /// <param name="limit">Specify the number of results to return</param>
        /// <returns>The Async state of the request</returns>
        IAsyncResult GetFeedbackForOthers(string userName, int offset, int limit);

        /// <summary>
        /// Get a list of all feedback where the user was a seller in the transaction.
        /// </summary>
        /// <param name="userId">the user's numeric ID</param>
        /// <param name="offset">To page through large result sets, set offset to a multiple of limit</param>
        /// <param name="limit">Specify the number of results to return</param>
        /// <returns>The Async state of the request</returns>
        IAsyncResult GetFeedbackAsSeller(int userId, int offset, int limit);

        /// <summary>
        /// Get a list of all feedback where the user was a seller in the transaction.
        /// </summary>
        /// <param name="userName">the user's name</param>
        /// <param name="offset">To page through large result sets, set offset to a multiple of limit</param>
        /// <param name="limit">Specify the number of results to return</param>
        /// <returns>The Async state of the request</returns>
        IAsyncResult GetFeedbackAsSeller(string userName, int offset, int limit);
    }
}
