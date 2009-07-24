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

    /// <summary>
    /// Helper methods for services
    /// </summary>
    public static class ServiceHelper
    {
        /// <summary>
        /// Generate a request 
        /// </summary>
        /// <param name="url">the url to read</param>
        /// <param name="dataAction">the action on returned data</param>
        /// <returns>the async state of the request</returns>
        public static IAsyncResult GenerateRequest(string url, Action<string> dataAction)
        {
            Uri uri = new Uri(url);
            WebRequest request = WebRequest.Create(uri);

            AsyncCallback completed = RequestCompletedCallback(dataAction);
            return request.BeginGetResponse(completed, request);
        }


        /// <summary>
        /// Generate a callback for the request conpleted
        /// </summary>
        /// <param name="dataAction">the processing to do on the returned data</param>
        /// <returns>a callback method</returns>
        public static AsyncCallback RequestCompletedCallback(Action<string> dataAction)
        {
            return a =>
            {
                WebRequest request = (WebRequest)a.AsyncState;

                HttpWebResponse response = (HttpWebResponse)request.EndGetResponse(a);
                Stream responseStream = response.GetResponseStream();
                StreamReader streamReader = new StreamReader(responseStream);

                string resultString = streamReader.ReadToEnd();
                streamReader.Close();
                response.Close();

                // do the action on the result data
                dataAction(resultString);
            };
        }
    }
}
