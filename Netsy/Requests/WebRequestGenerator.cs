//-----------------------------------------------------------------------
// <copyright file="WebRequestGenerator.cs" company="AFS">
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

    /// <summary>
    /// Request data via a http web request
    /// </summary>
    public class WebRequestGenerator : IRequestGenerator
    {
        /// <summary>
        /// Start the request
        /// </summary>
        /// <param name="uri">the request uri</param>
        /// <param name="dataAction">the action to execute if data is returned</param>
        /// <param name="errorAction">the action to execute if an error is returned</param>
        /// <returns>the async state of the request</returns>
        public IAsyncResult StartRequest(Uri uri, Action<string> dataAction, Action<Exception> errorAction)
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
        private static AsyncCallback RequestCompletedCallback(Action<string> dataAction, Action<Exception> errorAction)
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
    }
}
