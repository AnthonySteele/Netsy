//-----------------------------------------------------------------------
// <copyright file="ServiceHelper.cs" company="AFS">
// Copyright (c) AFS. All rights reserved.
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.Core
{
    using System;
    using System.IO;
    using System.Net;

    using Helpers;

    /// <summary>
    /// Helper methods for services
    /// </summary>
    public static class ServiceHelper
    {
        /// <summary>
        /// Generate a request, attatch the action on completion and start it
        /// </summary>
        /// <param name="uri">the uri to read</param>
        /// <param name="dataAction">the action to take on returned data</param>
        /// <param name="errorAction">the processing to do on error</param>
        /// <returns>the async state of the request</returns>
        public static IAsyncResult GenerateRequest(Uri uri, Action<string> dataAction, Action<Exception> errorAction)
        {
            WebRequest request = WebRequest.Create(uri);

            AsyncCallback completed = RequestCompletedCallback(dataAction, errorAction);
            return request.BeginGetResponse(completed, request);
        }

        /// <summary>
        /// Generate a callback for the request completion
        /// It's a template method, functional style
        /// </summary>
        /// <param name="dataAction">the processing to do on the returned data</param>
        /// <param name="errorAction">the processing to do on error</param>
        /// <returns>a callback method</returns>
        public static AsyncCallback RequestCompletedCallback(Action<string> dataAction, Action<Exception> errorAction)
        {
            return a =>
            {
                WebRequest request = (WebRequest)a.AsyncState;
                HttpWebResponse response = null;
                bool success = true;

                try
                {
                    response = (HttpWebResponse)request.EndGetResponse(a);
                }
                catch (WebException wex)
                {
                    success = false;
                    errorAction(wex);
                }

                if (success)
                {
                    Stream responseStream = response.GetResponseStream();
                    StreamReader streamReader = new StreamReader(responseStream);

                    string resultString = streamReader.ReadToEnd();
                    streamReader.Close();
                    response.Close();

                    // do the action on the result data
                    dataAction(resultString);
                }
            };
        }

        /// <summary>
        /// Send an event if any handler is attached
        /// </summary>
        /// <typeparam name="T">the type of data to send</typeparam>
        /// <param name="eventHandler">the event handler to fire</param>
        /// <param name="sender">the event sender</param>
        /// <param name="result">the event result</param>
        public static void TestSendEvent<T>(EventHandler<ResultEventArgs<T>> eventHandler, object sender, ResultEventArgs<T> result)
        {
            if (eventHandler != null)
            {
                eventHandler(sender, result);
            }
        }

        /// <summary>
        /// Send an error if any handler is attached
        /// </summary>
        /// <typeparam name="T">the type of data to send</typeparam>
        /// <param name="eventHandler">the event handler to fire</param>
        /// <param name="sender">the event sender</param>
        /// <param name="ex">the exception to send</param>
        public static void TestSendError<T>(EventHandler<ResultEventArgs<T>> eventHandler, object sender, Exception ex)
        {
            if (eventHandler != null)
            {
                var result = new ResultEventArgs<T>(default(T), new ResultStatus("Call failed", ex));
                eventHandler(sender, result);
            }
        }
    }
}
