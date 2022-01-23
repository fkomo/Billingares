using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Billingares.App.ViewModels
{
	public class ViewModelBase<TViewModel> : INotifyPropertyChanged
	{
		public ViewModelBase()
		{

		}

		#region Implementation of INotifyPropertyChanged

		/// <inheritdoc />
		public event PropertyChangedEventHandler PropertyChanged;

		/// <summary>
		/// Called when <see cref="INotifyPropertyChanged.PropertyChanged"/> occurs
		/// to invoke attached handlers.
		/// </summary>
		/// <param name="propertyName"></param>
		protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		#endregion Implementation of INotifyPropertyChanged
	}
}
