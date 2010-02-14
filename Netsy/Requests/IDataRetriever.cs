namespace Netsy.Requests
{
    using System;

    using Netsy.Helpers;

    public interface IDataRetriever
    {
        /// <summary>
        /// Start the data retrieval using a request
        /// </summary>
        /// <typeparam name="T">The type to desrialise to</typeparam>
        /// <param name="uri">the Uri to poll</param>
        /// <param name="completedEvent">where to send completed data and errors</param>
        /// <returns>the async state of the request</returns>
        IAsyncResult StartRetrieve<T>(Uri uri, EventHandler<ResultEventArgs<T>> completedEvent) where T : class;
    }
}