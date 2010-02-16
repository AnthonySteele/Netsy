//-----------------------------------------------------------------------
// <copyright file="MockFailingRequestGenerator.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.Requests.Mock
{
    using System;

    /// <summary>
    /// A mock RequestGenerator that always fails
    /// todo: move to a test project
    /// </summary>
    public class MockFailingRequestGenerator : IRequestGenerator
    {
        /// <summary>
        /// Start the request
        /// </summary>
        /// <param name="uri">the request uri</param>
        /// <param name="dataAction">the action to execute if data is returned</param>
        /// <param name="errorAction">the action to execute if an error is returned</param>
        /// <returns>the async state</returns>
        public IAsyncResult StartRequest(Uri uri, Action<string> dataAction, Action<Exception> errorAction)
        {
            if (errorAction == null)
            {
                throw new ArgumentNullException("errorAction");
            }

            errorAction(new ArgumentException("deliberate fail"));
            return null;
        }
    }
}


