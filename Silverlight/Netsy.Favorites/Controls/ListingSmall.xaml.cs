//-----------------------------------------------------------------------
// <copyright file="Constants.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.Listings.Controls
{
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media.Animation;

    /// <summary>
    /// Control to show a summery of a listing 
    /// </summary>
    public partial class ListingSmall : UserControl
    {
        /// <summary>
        /// Storyboard for control entry
        /// </summary>
        private Storyboard controlEnterStoryboard;

        /// <summary>
        /// Storyboard for control exit
        /// </summary>
        private Storyboard controlLeaveStoryboard;

        /// <summary>
        /// Storyboard for pulsing the bar visiblity 
        /// </summary>
        private Storyboard pulseStoryboard;

        /// <summary>
        /// True when the mouse is inside
        /// </summary>
        private bool mouseIn;

        /// <summary>
        /// Initializes a new instance of the ListingSmall class
        /// </summary>
        public ListingSmall()
        {
            InitializeComponent();
            this.InitializeStoryBoards();
        }

        /// <summary>
        /// set up the storyboards
        /// </summary>
        private void InitializeStoryBoards()
        {
            this.controlEnterStoryboard = (Storyboard)this.Resources["controlEnter"];

            this.controlLeaveStoryboard = (Storyboard)this.Resources["controlLeave"];
            this.controlLeaveStoryboard.Completed += (s, e) => this.HideTextWhenOut();

            this.pulseStoryboard = (Storyboard)this.Resources["pulse"];
            this.pulseStoryboard.Completed += (s, e) => this.HideTextWhenOut();
        }

        /// <summary>
        /// Action at the end of a fade-out animation - hide the panel 
        /// unless the mouse has already moved back in
        /// </summary>
        private void HideTextWhenOut()
        {
            if (!this.mouseIn)
            {
                this.TextOverlay.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

        /// <summary>
        /// Mouse focus enters the control
        /// </summary>
        /// <param name="sender">the event sender</param>
        /// <param name="e">the event params</param>
        private void OnMouseEnter(object sender, MouseEventArgs e)
        {
            this.mouseIn = true;
            this.TextOverlay.Visibility = System.Windows.Visibility.Visible;
            this.controlEnterStoryboard.Begin();
        }

        /// <summary>
        /// Mouse focus leaves the control
        /// </summary>
        /// <param name="sender">the event sender</param>
        /// <param name="e">the event params</param>
        private void OnMouseLeave(object sender, MouseEventArgs e)
        {
            this.mouseIn = false;
            this.controlLeaveStoryboard.Begin();
        }
    }
}
