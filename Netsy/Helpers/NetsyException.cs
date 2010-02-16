//-----------------------------------------------------------------------
// <copyright file="NetsyException.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 
namespace Netsy.Helpers
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Exception from the Etsy API
    /// </summary>
    #if (!SILVERLIGHT)
    [Serializable]
    #endif
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

        #if (!SILVERLIGHT)
        /// <summary>
        /// Initializes a new instance of the NetsyException class
        /// </summary>
        /// <param name="info">Serialization Info</param>
        /// <param name="context">the Streaming Context</param>
        protected NetsyException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
        #endif
    }
}
