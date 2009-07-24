//-----------------------------------------------------------------------
// <copyright file="MainPage.xaml.cs" company="AFS">
// Copyright (c) AFS. All rights reserved.
// </copyright>
//----------------------------------------------------------------------- 
namespace Silverlight.Netsy.TestControl
{
    using System;
    using System.Windows.Controls;

    using global::Netsy.Core;
    using global::Netsy.DataModel;
    using global::Netsy.DataModel.UserData;
    using global::Netsy.Helpers;
    using global::Netsy.Interfaces;

    /// <summary>
    /// Main page class
    /// </summary>
    public partial class MainPage : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the MainPage class 
        /// </summary>
        /// <param name="userId">the user to look up</param>
        /// <param name="apiKey">the API key to use</param>
        public MainPage(int userId, string apiKey)
        {
            InitializeComponent();

            IUsersService users = new UsersService(apiKey);

            users.GetUserDetailsCompleted += this.GetUserDetailsCompleted;
            users.GetUserDetails(userId, DetailLevel.Low);
        }

        /// <summary>
        /// Event fired when the user details have arrived
        /// </summary>
        /// <param name="sender">the event sender</param>
        /// <param name="e">the event params</param>
        private void GetUserDetailsCompleted(object sender, ResultEventArgs<Users> e)
        {
            this.Dispatcher.BeginInvoke(new EventHandler<ResultEventArgs<Users>>(this.GetUserDetailsCompletedOnThread), sender, e);
        }

        /// <summary>
        /// Event fired when the user details have arrived
        /// </summary>
        /// <param name="sender">the event sender</param>
        /// <param name="e">the event params</param>
        private void GetUserDetailsCompletedOnThread(object sender, ResultEventArgs<Users> e)
        {
            if (e.ResultStatus.Success)
            {
                User user = e.ResultValue.Results[0];
                outputTextBlock.Text = string.Format("User {0} is {1}", user.UserId, user.UserName);
            }
            else
            {
                outputTextBlock.Text = string.Format("Error {0}: {1}", e.ResultStatus.ErrorMessage, e.ResultStatus.Exception);
            }
        }
    }
}
