//-----------------------------------------------------------------------
// <copyright file="ResultEventArgs.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.Helpers
{
    using System;

    /// <summary>
    ///  delegate type for handling results
    /// </summary>
    /// <typeparam name="T">The type of results</typeparam>
    /// <param name="resultData">the results data</param>
    public delegate void ResultsReceivedHandler<T>(ResultEventArgs<T> resultData);

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
