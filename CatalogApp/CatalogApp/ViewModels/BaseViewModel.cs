using MvvmCross.Core.ViewModels;

namespace CatalogApp.ViewModels
{
	public class BaseViewModel : MvxViewModel
	{
		protected IDataService _dataService;
		protected IPlatformDependency _platformDependency;

		private bool _isBusy;
		public bool IsBusy
		{
			get { return _isBusy; }
			set
			{
				_isBusy = value;
				RaisePropertyChanged(() => IsBusy);
			}
		}

		private string _busyText;
		public string BusyText
		{
			get { return _busyText; }
			set
			{
				_busyText = value;
				RaisePropertyChanged(() => BusyText);
			}
		}
	}
}