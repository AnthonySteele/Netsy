//-----------------------------------------------------------------------
// <copyright file="ResultEventArgs.cs" company="AFS">
// Copyright (c) AFS. All rights reserved.
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.Helpers
{
    using System;

    /// <summary>
    /// Generic event arguments that wrap a single object
    /// </summary>
    /// <typeparam name="T">The result data type wrapped by these event args</typeparam>
    /// <typeparam name="U">The result status type wrapped by these event args</typeparam>
    public class EventArgs<T, U> : EventArgs
    {
        /// <summary>
        /// stores the wrapped result value
        /// </summary>
        private readonly T resultValue;

        /// <summary>
        /// stores the wrapped result value
        /// </summary>
        private readonly U resultStatus;

        /// <summary>
        /// Initializes a new instance of the EventArgs class
        /// </summary>
        /// <param name="resultValue">the wrapped result value</param>
        /// <param name="resultStatus">the wrapped result status</param>
        public EventArgs(T resultValue, U resultStatus)
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
        public U ResultStatus
        {
            get
            {
                return this.resultStatus;
            }
        }
    }
}
