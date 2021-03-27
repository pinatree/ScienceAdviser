/*
    Данный класс - полностью COPY-PASTE со StackOverflow
    https://stackoverflow.com/questions/34996198/the-name-commandmanager-does-not-exist-in-the-current-context-visual-studio-2
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Input;

namespace ScienceAdviser.IViewModel.Helpers
{
    //КОД БЫЛ СКОПИРОВАН ЦЕЛИКОМ И ПОЛНОСТЬЮ С https://sparethought.wordpress.com/2011/08/10/wpf-using-commands-with-commandparameter-without-commandmanager/
    //В СВЯЗИ С ТЕМ, ЧТО ПРИ ВЫНОСЕ В ОТДЕЛЬНУЮ БИБЛИОТЕКУ НЕТ ДОСТУПА К CommandManager ИЗ System.Windows.Input.
    //Если будут проблемы с привязками - вполне вероятно, что проблема находится тут. Еще были выпилины пара статичиских
    //методов и статический конструктор в виду непонятности их применения.
    //Возможно, обойти эту проблему позволяет Prism. Но пока лучше буду придерживаться канона.
    public class RelayCommand<T> : RelayCommand
    {
        public RelayCommand(Action<T> execute)
            : base(o => execute((T)o))
        {
        }

        public RelayCommand(Action<T> execute, Func<T, bool> canExecute)
            : base(o => execute((T)o), o => canExecute((T)o))
        {
        }
    }

    public class RelayCommand : ICommand
    {
        private readonly Func<object, bool> _canExecute;
        private readonly Action<object> _execute;

        public RelayCommand(Action<object> execute)
            : this(execute, null)
        {
        }

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");

            _execute = execute;
            _canExecute = canExecute;
        }

        [DebuggerStepThrough]
        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }

        public event EventHandler CanExecuteChanged = delegate { };

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged(this, EventArgs.Empty);
        }
    }
}
