//-----------------------------------------------------------------------
// <copyright file="DataRetriever.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.Requests
{
    using System;
    using System.IO;
    using System.Net;

    using Netsy.Cache;
    using Netsy.DataModel;
    using Netsy.Helpers;

    /// <summary>
    /// Helper methods for services
    /// </summary>
    public class DataRetriever : IDataRetriever
    {
        private readonly IDataCache dataCache;

        private readonly IRequestGenerator RequestGenerator;

        public DataRetriever(IDataCache dataCache, IRequestGenerator RequestGenerator)
        {
            if (dataCache == null)
            {
                throw new ArgumentNullException("dataCache");
            }

            if (RequestGenerator == null)
            {
                throw new ArgumentNullException("RequestGenerator");
            }

            this.dataCache = dataCache;
            this.RequestGenerator = RequestGenerator;
        }

        /// <summary>
        /// Start the data retrieval using a request
        /// </summary>
        /// <typeparam name="T">The type to desrialise to</typeparam>
        /// <param name="uri">the Uri to poll</param>
        /// <param name="completedEvent">where to send completed data and errors</param>
        /// <returns>the async state of the request</returns>
        public IAsyncResult StartRetrieve<T>(Uri uri, EventHandler<ResultEventArgs<T>> completedEvent) where T : class
        {
            if (uri == null)
            {
                throw new ArgumentNullException("uri");
            }

            if (completedEvent == null)
            {
                throw new ArgumentNullException("completedEvent");
            }


            object cacheData = dataCache.Read(uri.ToString());
            if (cacheData != null)
            {
                SendSuccess((T)cacheData, completedEvent);
                return null;
            }

            Action<string> dataAction = s =>
            {
                try
                {
                    T data = s.Deserialize<T>();
                    dataCache.Write(uri.ToString(), data);
                    SendSuccess(data, completedEvent);
                }
                catch (Exception ex)
                {
                    TestSendError(completedEvent, "Error Deserializing data", ex);
                }
            };

            Action<Exception> errorAction = ex => TestSendError(completedEvent, "Web error", ex);

            return this.RequestGenerator.StartRequest(uri, dataAction, errorAction);
        }

        /// <summary>
        /// Send a success result
        /// </summary>
        /// <typeparam name="T">the type of data to send</typeparam>
        /// <param name="data">the event data</param>
        /// <param name="completedEvent">the completed event handler</param>
        private void SendSuccess<T>(T data, EventHandler<ResultEventArgs<T>> completedEvent)
        {
            ResultEventArgs<T> sucessResult = new ResultEventArgs<T>(data, new ResultStatus(true));
            RequestHelper.TestSendEvent(completedEvent, this, sucessResult);
        }

        /// <summary>
        /// Send an error if any handler is attached
        /// </summary>
        /// <typeparam name="T">the type of data to send</typeparam>
        /// <param name="eventHandler">the event handler to fire</param>
        /// <param name="errorMessage">the error message</param>
        /// <param name="ex">the exception to send</param>
        private void TestSendError<T>(EventHandler<ResultEventArgs<T>> eventHandler, string errorMessage, Exception ex)
        {
            if (eventHandler != null)
            {
                var result = new ResultEventArgs<T>(default(T), new ResultStatus(errorMessage, ex));
                eventHandler(this, result);
            }
        }
    }
}
