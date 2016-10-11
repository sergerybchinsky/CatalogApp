using System;
using System.Diagnostics;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using CatalogApp.Helpers;

namespace CatalogApp.ViewModels
{
	public class ReloadViewModel : LoadDataViewModel
	{ 
		public ReloadViewModel(IPlatformDependency platformDependency, IDataService dataService) : base(platformDependency, dataService)
		{
		}

		private IMvxCommand _reloadDBCommand;
		public IMvxCommand ReloadDBCommand
		{
			get
			{
				if (_reloadDBCommand == null)
					_reloadDBCommand = new MvxCommand(() =>
					{
						IsBusy = true;
						BusyText = "Catalog is dowloading";
						DataProvider.SharedInstance.DownloadedSuccesDelegate += DownloadedSuccesDelegateHandler;
						DataProvider.SharedInstance.GetData();
					});
				return _reloadDBCommand;
			}
		}

		public async override void Start()
		{
			Title = "Reload local db";
			IsBusy = false;
		}
	}
	
}
