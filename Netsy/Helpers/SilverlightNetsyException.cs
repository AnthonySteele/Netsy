//-----------------------------------------------------------------------
// <copyright file="SilverlightNetsyException.cs" company="AFS">
// Copyright (c) AFS. All rights reserved.
// </copyright>
//----------------------------------------------------------------------- 
namespace Netsy.Helpers
{
    using System;

    /// <summary>
    /// Exception from the Etsy API
    /// Silverlight version, without Serialization 
    /// </summary>
    public class NetsyException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the NetsyException class
        /// </summary>
        public NetsyException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the NetsyException class
        /// </summary>
        /// <param name="message">the message</param>
        public NetsyException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the NetsyException class
        /// </summary>
        /// <param name="message">the message</param>
        /// <param name="innerException">the exception to wrap</param>
        public NetsyException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
