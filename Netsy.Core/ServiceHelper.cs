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
