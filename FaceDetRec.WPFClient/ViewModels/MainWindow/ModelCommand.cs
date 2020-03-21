using System;
using System.Windows.Input;

namespace FaceDetRec.WPFClient.ViewModels.MainWindow
{
	public class ModelCommand : ICommand
	{
		private readonly Action<object> _execute = null;
		private readonly Predicate<object> _canExecute = null;

		public ModelCommand(Action<object> execute)
			: this(execute, null) { }

		public ModelCommand(Action execute)
			: this(execute, null) { }

		public ModelCommand(Action<object> execute,
			Predicate<object> canExecute)
		{
			_execute = execute;
			_canExecute = canExecute;
		}

		public ModelCommand(Action execute,
			Predicate<object> canExecute)
		{
			_execute = (ob) => { execute(); };
			_canExecute = canExecute;
		}

		public event EventHandler CanExecuteChanged;

		public bool CanExecute(object parameter)
		{
			return _canExecute?.Invoke(parameter) ?? true;
		}

		public void Execute(object parameter)
		{
			_execute?.Invoke(parameter);
		}

		public void OnCanExecuteChanged()
		{
			CanExecuteChanged?.Invoke(this, EventArgs.Empty);
		}
	}
}
	