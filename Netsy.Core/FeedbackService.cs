//-----------------------------------------------------------------------
// <copyright file="FeedbackService.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 
namespace Netsy.Core
{
    using System;

    using Netsy.DataModel;
    using Netsy.DataModel.FeedbackData;
    using Netsy.Helpers;
    using Netsy.Interfaces;

    /// <summary>
    /// Implementation of the Feedback service
    /// </summary>
    public class FeedbackService : IFeedbackService
    {
        /// <summary>
        /// the Etsy context data
        /// </summary>
        private readonly EtsyContext etsyContext;

        /// <summary>
        /// Initializes a new instance of the FeedbackService class
        /// </summary>
        /// <param name="etsyContext">the etsy context to use</param>
        public FeedbackService(EtsyContext etsyContext)
        {
            this.etsyContext = etsyContext;
        }

        #region IFeedbackService Members

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
        public event EventHandler<ResultEventArgs<Feedbacks>> GetFeedbackForOtherCompleted;

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
            if (!ServiceHelper.TestCallPrerequisites(this, this.GetFeedbackCompleted, this.etsyContext))
            {
                return null;
            } 
            
            throw new NotImplementedException();
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
            if (!ServiceHelper.TestCallPrerequisites(this, this.GetFeedbackForUserCompleted, this.etsyContext))
            {
                return null;
            }
            
            throw new NotImplementedException();
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
            if (!ServiceHelper.TestCallPrerequisites(this, this.GetFeedbackForUserCompleted, this.etsyContext))
            {
                return null;
            }

            throw new NotImplementedException();
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
            if (!ServiceHelper.TestCallPrerequisites(this, this.GetFeedbackAsBuyerCompleted, this.etsyContext))
            {
                return null;
            }

            throw new NotImplementedException();
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
            if (!ServiceHelper.TestCallPrerequisites(this, this.GetFeedbackAsBuyerCompleted, this.etsyContext))
            {
                return null;
            }

            throw new NotImplementedException();
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
            if (!ServiceHelper.TestCallPrerequisites(this, this.GetFeedbackForOtherCompleted, this.etsyContext))
            {
                return null;
            } 
            
            throw new NotImplementedException();
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
            if (!ServiceHelper.TestCallPrerequisites(this, this.GetFeedbackForOtherCompleted, this.etsyContext))
            {
                return null;
            }
            
            throw new NotImplementedException();
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
            if (!ServiceHelper.TestCallPrerequisites(this, this.GetFeedbackAsSellerCompleted, this.etsyContext))
            {
                return null;
            }
                        
            throw new NotImplementedException();
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
            if (!ServiceHelper.TestCallPrerequisites(this, this.GetFeedbackAsSellerCompleted, this.etsyContext))
            {
                return null;
            }

            throw new NotImplementedException();
        }

        #endregion
    }
}
