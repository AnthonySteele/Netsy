//-----------------------------------------------------------------------
// <copyright file="ShopWindowViewModel.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace NetsyGui.Shop
{
    using ViewModels;

    /// <summary>
    /// View model for the shop Window
    /// </summary>
    public class ShopWindowViewModel : BaseViewModel
    {
        /// <summary>
        /// the Id of the user/shop being shown
        /// </summary>
        private int userId;

        /// <summary>
        /// Gets or sets the Id of the user/shop being shown
        /// </summary>
        public int UserId
        {
            get
            {
                return this.userId;
            }

            set
            {
                if (this.userId != value)
                {
                    this.userId = value;
                    this.OnPropertyChanged("UserId");
                }
            }
        }
    }
}
