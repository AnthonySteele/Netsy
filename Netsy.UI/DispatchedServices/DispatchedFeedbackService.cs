//-----------------------------------------------------------------------
// <copyright file="DispatchedFeedbackService.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.UI.DispatchedServices
{
    using System;
    using System.Windows.Threading;

    using Netsy.DataModel;
    using Netsy.Helpers;
    using Netsy.Interfaces;

    /// <summary>
    /// Feedback service wrapped to use a dispatcher 
    /// To put the results back on the Dispatcher's thread
    /// </summary>
    public class DispatchedFeedbackService : DispatchedService, IFeedbackService
    {
        /// <summary>
        /// The wrapped service
        /// </summary>
        private readonly IFeedbackService wrappedService;

        /// <summary>
        /// Initializes a new instance of the DispatchedFeedbackService class
        /// </summary>
        /// <param name="wrappedService">the wrapped service</param>
        /// <param name="dispatcher">the thread dispatcher</param>
        public DispatchedFeedbackService(IFeedbackService wrappedService, Dispatcher dispatcher) 
            : base(dispatcher)
        {
            this.wrappedService = wrappedService;

            this.wrappedService.GetFeedbackAsBuyerCompleted += (s, e) => this.DispatchEvent(this.GetFeedbackAsBuyerCompleted, s, e);
            this.wrappedService.GetFeedbackAsSellerCompleted += (s, e) => this.DispatchEvent(this.GetFeedbackAsSellerCompleted, s, e);
            this.wrappedService.GetFeedbackCompleted += (s, e) => this.DispatchEvent(this.GetFeedbackCompleted, s, e);
            this.wrappedService.GetFeedbackForOthersCompleted += (s, e) => this.DispatchEvent(this.GetFeedbackForOthersCompleted, s, e);
            this.wrappedService.GetFeedbackForUserCompleted += (s, e) => this.DispatchEvent(this.GetFeedbackForUserCompleted, s, e);
        }

        /// <summary>
        /// GetFeedback completed event
        /// </summary>
        public event EventHandler<ResultEventArgs<Feedbacks>> GetFeedbackCompleted;

        /// <summary>
        /// GetFeedbackForUser completed event
        /// </summary>
        public event EventHandler<ResultEventArgs<Feedbacks>> GetFeedbackForUserCompleted;

        /// <summary>
        /// GetFeedbackAsBuyer completed event
        /// </summary>
        public event EventHandler<ResultEventArgs<Feedbacks>> GetFeedbackAsBuyerCompleted;

        /// <summary>
        /// GetFeedbackForOther completed event
        /// </summary>
        public event EventHandler<ResultEventArgs<Feedbacks>> GetFeedbackForOthersCompleted;

        /// <summary>
        /// GetFeedbackAsSeller completed event
        /// </summary>
        public event EventHandler<ResultEventArgs<Feedbacks>> GetFeedbackAsSellerCompleted;
        
        /// <summary>
        /// Get a feedback record
        /// </summary>
        /// <param name="feedbackId">the id of the record</param>
        /// <returns>the Async state of the request</returns>
        public IAsyncResult GetFeedback(int feedbackId)
        {
            return this.wrappedService.GetFeedback(feedbackId);
        }

        /// <summary>
        /// Get a list of all feedback for a particular user.
        /// </summary>
        /// <param name="userId">the user's numeric ID</param>
        /// <param name="offset">To page through large result sets, set offset to a multiple of limit</param>
        /// <param name="limit">Specify the number of results to return</param>
        /// <returns>The Async state of the request</returns>
        public IAsyncResult GetFeedbackForUser(int userId, int offset, int limit)
        {
            return this.wrappedService.GetFeedbackForUser(userId, offset, limit);
        }

        /// <summary>
        /// Get a list of all feedback for a particular user.
        /// </summary>
        /// <param name="userName">the user's name</param>
        /// <param name="offset">To page through large result sets, set offset to a multiple of limit</param>
        /// <param name="limit">Specify the number of results to return</param>
        /// <returns>The Async state of the request</returns>
        public IAsyncResult GetFeedbackForUser(string userName, int offset, int limit)
        {
            return this.wrappedService.GetFeedbackForUser(userName, offset, limit);
        }

        /// <summary>
        /// Get a list of all feedback where the user was a buyer in the transaction.
        /// </summary>
        /// <param name="userId">the user's numeric ID</param>
        /// <param name="offset">To page through large result sets, set offset to a multiple of limit</param>
        /// <param name="limit">Specify the number of results to return</param>
        /// <returns>The Async state of the request</returns>
        public IAsyncResult GetFeedbackAsBuyer(int userId, int offset, int limit)
        {
            return this.wrappedService.GetFeedbackAsBuyer(userId, offset, limit);
        }

        /// <summary>
        /// Get a list of all feedback where the user was a buyer in the transaction.
        /// </summary>
        /// <param name="userName">the user's name</param>
        /// <param name="offset">To page through large result sets, set offset to a multiple of limit</param>
        /// <param name="limit">Specify the number of results to return</param>
        /// <returns>The Async state of the request</returns>
        public IAsyncResult GetFeedbackAsBuyer(string userName, int offset, int limit)
        {
            return this.wrappedService.GetFeedbackAsBuyer(userName, offset, limit);
        }

        /// <summary>
        /// Get a list of all feedback that the user left feedback for someone else.
        /// </summary>
        /// <param name="userId">the user's numeric ID</param>
        /// <param name="offset">To page through large result sets, set offset to a multiple of limit</param>
        /// <param name="limit">Specify the number of results to return</param>
        /// <returns>The Async state of the request</returns>
        public IAsyncResult GetFeedbackForOthers(int userId, int offset, int limit)
        {
            return this.wrappedService.GetFeedbackForOthers(userId, offset, limit);
        }

        /// <summary>
        /// Get a list of all feedback that the user left feedback for someone else.
        /// </summary>
        /// <param name="userName">the user's name</param>
        /// <param name="offset">To page through large result sets, set offset to a multiple of limit</param>
        /// <param name="limit">Specify the number of results to return</param>
        /// <returns>The Async state of the request</returns>
        public IAsyncResult GetFeedbackForOthers(string userName, int offset, int limit)
        {
            return this.wrappedService.GetFeedbackForOthers(userName, offset, limit);
        }

        /// <summary>
        /// Get a list of all feedback where the user was a seller in the transaction.
        /// </summary>
        /// <param name="userId">the user's numeric ID</param>
        /// <param name="offset">To page through large result sets, set offset to a multiple of limit</param>
        /// <param name="limit">Specify the number of results to return</param>
        /// <returns>The Async state of the request</returns>
        public IAsyncResult GetFeedbackAsSeller(int userId, int offset, int limit)
        {
            return this.wrappedService.GetFeedbackAsSeller(userId, offset, limit);
        }

        /// <summary>
        /// Get a list of all feedback where the user was a seller in the transaction.
        /// </summary>
        /// <param name="userName">the user's name</param>
        /// <param name="offset">To page through large result sets, set offset to a multiple of limit</param>
        /// <param name="limit">Specify the number of results to return</param>
        /// <returns>The Async state of the request</returns>
        public IAsyncResult GetFeedbackAsSeller(string userName, int offset, int limit)
        {
            return this.wrappedService.GetFeedbackAsSeller(userName, offset, limit);
        }
    }
}
