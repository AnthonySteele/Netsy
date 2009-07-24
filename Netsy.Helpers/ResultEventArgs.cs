//-----------------------------------------------------------------------
// <copyright file="ResultEventArgs.cs" company="AFS">
// Copyright (c) AFS. All rights reserved.
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.Helpers
{
    using System;

    /// <summary>
    /// Generic event arguments that wrap a pair of objects - result data, result status
    /// </summary>
    /// <typeparam name="T">The result data type wrapped by these event args</typeparam>
    public class ResultEventArgs<T> : EventArgs
    {
        /// <summary>
        /// stores the wrapped result value
        /// </summary>
        private readonly T resultValue;

        /// <summary>
        /// stores the wrapped result value
        /// </summary>
        private readonly ResultStatus resultStatus;

        /// <summary>
        /// Initializes a new instance of the ResultEventArgs class
        /// </summary>
        /// <param name="resultValue">the wrapped result value</param>
        /// <param name="resultStatus">the wrapped result status</param>
        public ResultEventArgs(T resultValue, ResultStatus resultStatus)
        {
            this.resultValue = resultValue;
            this.resultStatus = resultStatus;
        }

        /// <summary>
        /// Gets the wrapped result value
        /// </summary>
        public T ResultValue
        {
            get
            {
                return this.resultValue;
            }
        }

        /// <summary>
        /// Gets the wrapped result status
        /// </summary>
        public ResultStatus ResultStatus
        {
            get
            {
                return this.resultStatus;
            }
        }
    }
}
