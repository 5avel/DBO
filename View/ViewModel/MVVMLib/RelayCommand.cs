﻿using System;
using System.Windows.Input;

namespace DBO.ViewModel.MVVMLib
{
    public class RelayCommand : ICommand
    {
        private readonly Action<object> execute;
        readonly Predicate<object> canExecute;

        public RelayCommand(Action<object> execute, Predicate<object> canExecute = null)
        {
            if (execute == null) throw new ArgumentNullException("handler");
            
            this.execute = execute;
            this.canExecute = canExecute;
        }


        public bool CanExecute(object parameter)
        {
            return canExecute == null ? true : canExecute.Invoke(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        public void Execute(object parameter)
        {
            execute.Invoke(parameter);
        }
    } 
}
