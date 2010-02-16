//-----------------------------------------------------------------------
// <copyright file="MockFixedDataRequestGenerator.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.Requests.Mock
{
    using System;

    /// <summary>
    /// A mock RequestGenerator that returns the given data
    /// todo: move to a test project
    /// </summary>
    public class MockFixedDataRequestGenerator : IRequestGenerator
    {
        /// <summary>
        /// the data to return
        /// </summary>
        private readonly string resultData;

        /// <summary>
        /// Initializes a new instance of the MockFixedDataRequestGenerator class
        /// </summary>
        /// <param name="resultData">the data to return</param>
        public MockFixedDataRequestGenerator(string resultData)
        {
            this.resultData = resultData;
        }

        /// <summary>
        /// Start the request
        /// </summary>
        /// <param name="uri">the request uri</param>
        /// <param name="dataAction">the action to execute if data is returned</param>
        /// <param name="errorAction">the action to execute if an error is returned</param>
        /// <returns>the async state</returns>
        public IAsyncResult StartRequest(Uri uri, Action<string> dataAction, Action<Exception> errorAction)
        {
            if (dataAction == null)
            {
                throw new ArgumentNullException("dataAction");
            } 
            
            dataAction(this.resultData);
            return null;
        }
    }
}
