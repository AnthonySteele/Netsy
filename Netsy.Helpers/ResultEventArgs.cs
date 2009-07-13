//-----------------------------------------------------------------------
// <copyright file="ResultEventArgs.cs" company="AFS">
// Copyright (c) AFS. All rights reserved.
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.Helpers
{
    using System;

    /// <summary>
    /// Delegate type for result event args
    /// </summary>
    /// <typeparam name="TResult">The result data type wrapped by these event args</typeparam>
    /// <typeparam name="TStatus">The result status type wrapped by these event args</typeparam>
    /// <param name="e">the event params</param>
    public delegate void ResultEventArgsHandler<TResult, TStatus>(ResultEventArgs<TResult, TStatus> e);

    /// <summary>
    /// Generic event arguments that wrap a single object
    /// </summary>
    /// <typeparam name="TResult">The result data type wrapped by these event args</typeparam>
    /// <typeparam name="TStatus">The result status type wrapped by these event args</typeparam>
    public class ResultEventArgs<TResult, TStatus> : EventArgs
    {
        /// <summary>
        /// stores the wrapped result value
        /// </summary>
        private readonly TResult resultValue;

        /// <summary>
        /// stores the wrapped result value
        /// </summary>
        private readonly TStatus resultStatus;

        /// <summary>
        /// Initializes a new instance of the ResultEventArgs class
        /// </summary>
        /// <param name="resultValue">the wrapped result value</param>
        /// <param name="resultStatus">the wrapped result status</param>
        public ResultEventArgs(TResult resultValue, TStatus resultStatus)
        {
            this.resultValue = resultValue;
            this.resultStatus = resultStatus;
        }

        /// <summary>
        /// Gets the wrapped result value
        /// </summary>
        public TResult ResultValue
        {
            get
            {
                return this.resultValue;
            }
        }

        /// <summary>
        /// Gets the wrapped result status
        /// </summary>
        public TStatus ResultStatus
        {
            get
            {
                return this.resultStatus;
            }
        }
    }
}
