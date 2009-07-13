//-----------------------------------------------------------------------
// <copyright file="ResultStatus.cs" company="AFS">
// Copyright (c) AFS. All rights reserved.
// </copyright>
//----------------------------------------------------------------------- 
namespace Netsy.Helpers
{
    using System;

    /// <summary>
    /// Result status class
    /// </summary>
    public class ResultStatus
    {
        /// <summary>
        /// Initializes a new instance of the ResultStatus class
        /// </summary>
        /// <param name="success">was the call sucessfull</param>
        public ResultStatus(bool success)
        {
            this.Success = success;
        }

        /// <summary>
        /// Initializes a new instance of the ResultStatus class
        /// </summary>
        /// <param name="errorMessage">the error message</param>
        /// <param name="ex">the exception</param>
        public ResultStatus(string errorMessage, Exception ex)
        {
            this.Success = false;
            this.ErrorMessage = errorMessage;
            this.Exception = ex;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the request suceeded
        /// </summary>
        public bool Success
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets an error message on failure
        /// </summary>
        public string ErrorMessage
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the exception on failure
        /// </summary>
        public Exception Exception
        {
            get;
            private set;
        }
    }
}
