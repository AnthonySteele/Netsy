//-----------------------------------------------------------------------
// <copyright file="UserViewModel.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 
namespace Netsy.UI.ViewModels
{
    using System.Windows.Input;

    using Netsy.DataModel;
    using Netsy.Helpers;
    using Netsy.UI.Commands;

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
            this.WebLinkClickCommand = new HyperlinkNavigateCommand();
        }

        /// <summary>
        /// Gets the user data transfer object
        /// </summary>
        public User User
        {
            get { return this.user; }
        }

        /// <summary>
        /// Gets data data for display
        /// </summary>
        public string DateDisplay
        {
            get
            {
                return this.User.JoinEpochDate.ToShortDateString();
            }
        }

        /// <summary>
        /// Gets the image url for the largest image
        /// </summary>
        public string LargestImageUrl
        {
            get
            {
                if (User.ImageUrl75X75.HasContent())
                {
                    return User.ImageUrl75X75;
                }

                if (User.ImageUrl50X50.HasContent())
                {
                    return User.ImageUrl50X50;
                }

                if (User.ImageUrl30X30.HasContent())
                {
                    return User.ImageUrl30X30;
                }

                if (User.ImageUrl25X25.HasContent())
                {
                    return User.ImageUrl25X25;
                }

                return string.Empty;
            }
        }

        /// <summary>
        /// Gets the command to show the listing in a web browser
        /// </summary>
        public ICommand WebLinkClickCommand { get; private set; }
    }
}
