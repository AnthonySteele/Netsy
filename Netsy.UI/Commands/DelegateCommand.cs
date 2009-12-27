//-----------------------------------------------------------------------
// <copyright file="DelegateCommand.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.UI.Commands
{
    using System;
    using System.Windows.Input;

    /// <summary>
    /// A command that can take actions - delegates or lambdas
    /// </summary>
    /// <typeparam name="T">the type of value to act upon</typeparam>
    public class DelegateCommand<T> : ICommand where T : class 
    {        
        /// <summary>
        /// The function to call to check if the command can execute
        /// </summary>
        private readonly Func<T, bool> canExecuteFunction;

        /// <summary>
        /// the action to perform when executing the command
        /// </summary>
        private readonly Action<T> executeAction;

        /// <summary>
        /// Initializes a new instance of the DelegateCommand class
        /// </summary>
        /// <param name="executeAction">the action to take on execution</param>
        public DelegateCommand(Action<T> executeAction) : this(executeAction, null)
        {    
        }

        /// <summary>
        /// Initializes a new instance of the DelegateCommand class
        /// </summary>
        /// <param name="executeAction">the action to take on execution</param>
        /// <param name="canExecuteFunction">the fuction to test if the command can execute</param>
        public DelegateCommand(Action<T> executeAction, Func<T, bool> canExecuteFunction)
        {
            this.executeAction = executeAction;
            this.canExecuteFunction = canExecuteFunction;
        }

        /// <summary>
        /// the event for command executablity changed
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        /// <summary>
        /// Call CanExecute
        /// </summary>
        /// <param name="parameter">the parameter</param>
        /// <returns>true if the command can execute</returns>
        public bool CanExecute(object parameter)
        {
            if (this.canExecuteFunction != null)
            {
                return this.canExecuteFunction(parameter as T);
            }
                
            return true;
        }

        /// <summary>
        /// Execute the command
        /// </summary>
        /// <param name="parameter">the parameter</param>
        public void Execute(object parameter)
        {
            this.executeAction(parameter as T); 
        }
    }
}
