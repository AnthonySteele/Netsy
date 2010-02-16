namespace Netsy.Requests
{
    using System;

    using Netsy.Helpers;

    /// <summary>
    /// Interface to an object tha6 can retrieve string data from a Uri
    /// </summary>
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