//-----------------------------------------------------------------------
// <copyright file="UserViewModel.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 
namespace Netsy.UI.ViewModels
{
    using Netsy.DataModel;

    /// <summary>
    /// View model for a user
    /// </summary>
    public class UserViewModel
    {
        /// <summary>
        /// the listing data transfer object
        /// </summary>
        private readonly User user;

        /// <summary>
        /// Initializes a new instance of the UserViewModel class
        /// </summary>
        /// <param name="user">the user Data transfer object</param>
        public UserViewModel(User user)
        {
            this.user = user;
        }

        /// <summary>
        /// Gets the shop data transfer object
        /// </summary>
        public User User
        {
            get { return this.user; }
        }
    }
}
