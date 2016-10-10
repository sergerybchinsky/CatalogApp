using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;

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
