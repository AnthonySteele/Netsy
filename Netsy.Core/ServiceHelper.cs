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

    using Netsy.DataModel;
    using Netsy.Helpers;

    /// <summary>
    /// Helper methods for services
    /// </summary>
    public static class ServiceHelper
    {
        /// <summary>
        /// Test that the api is a sate suitable to make calls
        /// </summary>
        /// <typeparam name="T">the type of data returned</typeparam>
        /// <param name="sender">the event sender</param>
        /// <param name="errorEvent">the error event to send if it's not in a good state</param>
        /// <param name="etsyContext">the context data to inspect</param>
        /// <returns>true if everyting is ok, false if there is an error</returns>
        public static bool TestCallPrerequisites<T>(object sender, EventHandler<ResultEventArgs<T>> errorEvent, EtsyContext etsyContext)
        {
            if (string.IsNullOrEmpty(etsyContext.ApiKey))
            {
                ResultEventArgs<T> errorResult = new ResultEventArgs<T>(default(T), new ResultStatus("No Api key", null));
                TestSendEvent(errorEvent, sender, errorResult);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Generate a web service request, attatch the action on completion and start it
        /// </summary>
        /// <typeparam name="T">The type to desrialise to</typeparam>
        /// <param name="sender">the sender</param>
        /// <param name="uri">the Uri to poll</param>
        /// <param name="completedEvent">where to send completed data and errors</param>
        /// <returns>the async state of the request</returns>
        public static IAsyncResult GenerateRequest<T>(object sender, Uri uri, EventHandler<ResultEventArgs<T>> completedEvent) where T : class
        {
            Action<string> dataAction = s =>
                {
                    try
                    {
                        T data = s.Deserialize<T>();
                        ResultEventArgs<T> sucessResult = new ResultEventArgs<T>(data, new ResultStatus(true));
                        TestSendEvent(completedEvent, sender, sucessResult);
                    }
                    catch (Exception ex)
                    {
                        TestSendError(completedEvent, sender, "Error Deserializing data", ex);
                    }
                };

            Action<Exception> errorAction = ex => TestSendError(completedEvent, sender, "Web error", ex);

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
        /// <param name="errorMessage">the error message</param>
        /// <param name="ex">the exception to send</param>
        public static void TestSendError<T>(EventHandler<ResultEventArgs<T>> eventHandler, object sender, string errorMessage, Exception ex)
        {
            if (eventHandler != null)
            {
                var result = new ResultEventArgs<T>(default(T), new ResultStatus(errorMessage, ex));
                eventHandler(sender, result);
            }
        }
    }
}
