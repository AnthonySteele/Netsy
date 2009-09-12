//-----------------------------------------------------------------------
// <copyright file="ResultStatus.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 
namespace Netsy.Helpers
{
    using System;
    using System.Net;

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
        /// Gets a value indicating whether the request suceeded
        /// </summary>
        public bool Success
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets an error message on failure
        /// </summary>
        public string ErrorMessage
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the exception on failure
        /// </summary>
        public Exception Exception
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the status from a web execption
        /// </summary>
        public WebExceptionStatus WebStatus
        {
            get
            {
                WebException wex = Exception as WebException;
                if (wex == null)
                {
                    return WebExceptionStatus.Success;
                }
                    
                return wex.Status;
            }
        }
    }
}
