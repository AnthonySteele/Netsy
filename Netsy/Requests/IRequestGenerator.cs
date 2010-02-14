//-----------------------------------------------------------------------
// <copyright file="IRequestGenerator.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.Requests
{
    using System;

    /// <summary>
    /// Interface to an objeect that can request raw string data asynchronously
    /// </summary>
    public interface IRequestGenerator
    {
        /// <summary>
        /// Start the request
        /// </summary>
        /// <param name="uri">the request uri</param>
        /// <param name="dataAction">the action to execute if data is returned</param>
        /// <param name="errorAction">the action to execute if an error is returned</param>
        /// <returns>the async state of the request</returns>
        IAsyncResult StartRequest(Uri uri, Action<string> dataAction, Action<Exception> errorAction);
    }
}
