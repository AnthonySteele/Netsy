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
    public class DispatchedFeedbackService : IFeedbackService
    {
        /// <summary>
        /// The wrapped service
        /// </summary>
        private readonly IFeedbackService wrappedService;

        /// <summary>
        /// The thread dispatcher
        /// </summary>
        private readonly Dispatcher dispatcher;

        /// <summary>
        /// Initializes a new instance of the DispatchedFeedbackService class
        /// </summary>
        /// <param name="wrappedService">the wrapped service</param>
        /// <param name="dispatcher">the thread dispatcher</param>
        public DispatchedFeedbackService(IFeedbackService wrappedService, Dispatcher dispatcher)
        {
            this.wrappedService = wrappedService;
            this.wrappedService.GetFeedbackAsBuyerCompleted += this.WrappedServiceGetFeedbackAsBuyerCompleted;
            this.wrappedService.GetFeedbackAsSellerCompleted += this.WrappedServiceGetFeedbackAsSellerCompleted;
            this.wrappedService.GetFeedbackCompleted += this.WrappedServiceGetFeedbackCompleted;
            this.wrappedService.GetFeedbackForOthersCompleted += this.WrappedServiceGetFeedbackForOthersCompleted;
            this.wrappedService.GetFeedbackForUserCompleted += this.WrappedServiceGetFeedbackForUserCompleted;

            this.dispatcher = dispatcher;
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

        /// <summary>
        /// The wrapped service operation has completed
        /// </summary>
        /// <param name="sender">the event sender</param>
        /// <param name="e">the event args</param>
        private void WrappedServiceGetFeedbackForUserCompleted(object sender, ResultEventArgs<Feedbacks> e)
        {
            if (this.GetFeedbackForUserCompleted != null)
            {
                Action completedSynch = () => this.GetFeedbackForUserCompleted(sender, e);
                this.dispatcher.Invoke(completedSynch);
            }
        }

        /// <summary>
        /// The wrapped service operation has completed
        /// </summary>
        /// <param name="sender">the event sender</param>
        /// <param name="e">the event args</param>
        private void WrappedServiceGetFeedbackForOthersCompleted(object sender, ResultEventArgs<Feedbacks> e)
        {
            if (this.GetFeedbackForOthersCompleted != null)
            {
                Action completedSynch = () => this.GetFeedbackForOthersCompleted(sender, e);
                this.dispatcher.Invoke(completedSynch);
            }
        }

        /// <summary>
        /// The wrapped service operation has completed
        /// </summary>
        /// <param name="sender">the event sender</param>
        /// <param name="e">the event args</param>
        private void WrappedServiceGetFeedbackCompleted(object sender, ResultEventArgs<Feedbacks> e)
        {
            if (this.GetFeedbackCompleted != null)
            {
                Action completedSynch = () => this.GetFeedbackCompleted(sender, e);
                this.dispatcher.Invoke(completedSynch);
            }
        }

        /// <summary>
        /// The wrapped service operation has completed
        /// </summary>
        /// <param name="sender">the event sender</param>
        /// <param name="e">the event args</param>
        private void WrappedServiceGetFeedbackAsSellerCompleted(object sender, ResultEventArgs<Feedbacks> e)
        {
            if (this.GetFeedbackAsSellerCompleted != null)
            {
                Action completedSynch = () => this.GetFeedbackAsSellerCompleted(sender, e);
                this.dispatcher.Invoke(completedSynch);
            }
        }

        /// <summary>
        /// The wrapped service operation has completed
        /// </summary>
        /// <param name="sender">the event sender</param>
        /// <param name="e">the event args</param>
        private void WrappedServiceGetFeedbackAsBuyerCompleted(object sender, ResultEventArgs<Feedbacks> e)
        {
            if (this.GetFeedbackAsBuyerCompleted != null)
            {
                Action completedSynch = () => this.GetFeedbackAsBuyerCompleted(sender, e);
                this.dispatcher.Invoke(completedSynch);
            }
        }
    }
}
