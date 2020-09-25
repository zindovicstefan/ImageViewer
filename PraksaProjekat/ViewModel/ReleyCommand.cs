using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PraksaProjekat
{
	public class ReleyCommand : ICommand
	{
		private Action<object> execute;
		private Predicate<object> canExecute;

		public event EventHandler CanExecuteChanged;

		public ReleyCommand(Action<object> execute, Predicate<object> canExecute)
		{
			if (execute == null)
			{
				throw new NullReferenceException("Execute is null!");
			}

			this.execute = execute;
			this.canExecute = canExecute;
		}

		public ReleyCommand(Action<object> execute) : this(execute, null)
		{
		}

		public bool CanExecute(object parameter)
		{
			if (canExecute == null)
			{
				return true;
			}

			return canExecute.Invoke(parameter);
		}

		public void Execute(object parameter)
		{
			execute.Invoke(parameter);
		}
	}
}
