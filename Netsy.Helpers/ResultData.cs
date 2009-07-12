//-----------------------------------------------------------------------
// <copyright file="ResultData.cs" company="AFS">
// Copyright (c) AFS. All rights reserved.
// </copyright>
//----------------------------------------------------------------------- 
namespace Netsy.Helpers
{
    using System;

    /// <summary>
    /// Result Data class
    /// </summary>
    /// <typeparam name="T">the type of data returned</typeparam>
    public class ResultData<T>
    {
        /// <summary>
        /// Gets or sets a value indicating whether the request suceeded
        /// </summary>
        public bool Success
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets an error message on failure
        /// </summary>
        public string ErrorMessage
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the exception on failure
        /// </summary>
        public Exception Exception
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the data returned
        /// </summary>
        public T Data
        {
            get;
            set;
        }
    }
}
