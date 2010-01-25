//-----------------------------------------------------------------------
// <copyright file="FavoritesControl.xaml.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.Favorites
{
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media.Animation;

    /// <summary>
    /// Display Favourites
    /// </summary>
    public partial class FavoritesControl : UserControl
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
        /// Initializes a new instance of the FavoritesControl class
        /// </summary>
        public FavoritesControl()
        {
            InitializeComponent();
            this.InitializeStoryBoards();
        }

        /// <summary>
        /// Gets the Storyboard for pulsing the bar visiblity 
        /// </summary>
        public Storyboard PulseStoryboard
        {
            get { return this.pulseStoryboard; }
        }

        /// <summary>
        /// set up the storyboards
        /// </summary>
        private void InitializeStoryBoards()
        {
            this.controlEnterStoryboard = (Storyboard)this.Resources["controlEnter"];

            this.controlLeaveStoryboard = (Storyboard)this.Resources["controlLeave"];
            this.controlLeaveStoryboard.Completed += (s, e) => this.HidePanelWhenOut();

            this.pulseStoryboard = (Storyboard)this.Resources["pulse"];
            this.PulseStoryboard.Completed += (s, e) => this.HidePanelWhenOut();
        }

        /// <summary>
        /// Action at the end of a fade-out animation - hide the panel 
        /// unless the mouse has already moved back in
        /// </summary>
        private void HidePanelWhenOut()
        {
            if (!this.mouseIn)
            {
                this.NavigationPanel.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

        /// <summary>
        /// Move to previous page
        /// </summary>
        /// <param name="sender">the event page</param>
        /// <param name="e">the event params</param>
        private void PrevButtonClick(object sender, System.Windows.RoutedEventArgs e)
        {
            FavoritesControlViewModel viewModel = this.DataContext as FavoritesControlViewModel;
            if (viewModel == null)
            {
                return;
            }

            if (viewModel.PreviousPageCommand.CanExecute(viewModel))
            {
                viewModel.PreviousPageCommand.Execute(viewModel);
            }
        }

        /// <summary>
        /// Move to next page
        /// </summary>
        /// <param name="sender">the event page</param>
        /// <param name="e">the event params</param>
        private void NextButtonClick(object sender, System.Windows.RoutedEventArgs e)
        {
            FavoritesControlViewModel viewModel = this.DataContext as FavoritesControlViewModel;
            if (viewModel == null)
            {
                return;
            }

            if (viewModel.NextPageCommand.CanExecute(viewModel))
            {
                viewModel.NextPageCommand.Execute(viewModel);
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
            this.NavigationPanel.Visibility = System.Windows.Visibility.Visible;
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
