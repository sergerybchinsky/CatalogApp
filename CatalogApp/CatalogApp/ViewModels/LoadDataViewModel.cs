using System;
using System.Diagnostics;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;

namespace CatalogApp.ViewModels
{
	public class LoadDataViewModel : BaseViewModel
	{
		public LoadDataViewModel(IPlatformDependency platformDependency, IDataService dataService) : base()
		{
			_platformDependency = platformDependency;
			_dataService = dataService;
		}

		private string _title;
		public string Title
		{
			get { return _title; }
			set
			{
				_title = value;
				RaisePropertyChanged(() => Title);
			}
		}

		private IMvxCommand _showCategoriesCommand;
		public IMvxCommand ShowCategoriesCommand
		{
			get
			{
				if (_showCategoriesCommand == null)
					_showCategoriesCommand = new MvxCommand(() =>
					{
						DataProvider.SharedInstance.DownloadedSuccesDelegate -= DownloadedSuccesDelegateHandler;
						ShowViewModel<CategoriesViewModel>();
					});
				return _showCategoriesCommand;
			}
		}

		public async override void Start()
		{
			base.Start();
			IsBusy = true;
			BusyText = "Catalog is dowloading";
			await _dataService.FirstInit();
			DataProvider.SharedInstance.DownloadedSuccesDelegate += DownloadedSuccesDelegateHandler;
			DataProvider.SharedInstance.GetData();
		}

		async void DownloadedSuccesDelegateHandler(CatalogApp.Category[] catalog)
		{
			BusyText = "Catalog downloaded \nUpdating database";
			Debug.WriteLine($"[LoadDataViewModel.DownloadedSuccesDelegateHandler] : Updating database!");

			await _dataService.UpdateDB(catalog).ContinueWith(
				(obj) =>
				{
					IsBusy = false;
					BusyText = "Catalog downloaded!";
					Debug.WriteLine($"[LoadDataViewModel.DownloadedSuccesDelegateHandler] : Database udpated!");
				});
		}
	}
}
