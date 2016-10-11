using System;
using System.Diagnostics;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MvvmCross.Core.ViewModels;
using CatalogApp.Helpers;

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
						ShowViewModel<CategoriesViewModel>();                 
					});
				return _showCategoriesCommand;
			}
		}

		public async override void Start()
		{
			base.Start();
			Title = "Welcome";;

			IsBusy = true;
			BusyText = "Catalog is dowloading";
			await _dataService.FirstInit();
			DataProvider.SharedInstance.DownloadedSuccesDelegate += DownloadedSuccesDelegateHandler;
			DataProvider.SharedInstance.GetData();
		}

		protected async void DownloadedSuccesDelegateHandler(CatalogApp.Category[] categories)
		{
			DataProvider.SharedInstance.DownloadedSuccesDelegate -= DownloadedSuccesDelegateHandler;
			BusyText = "Catalog downloaded \nUpdating database";
			Debug.WriteLine($"[LoadDataViewModel.DownloadedSuccesDelegateHandler] : Updating database!");

			await _dataService.UpdateDB(categories).ContinueWith(
				(obj) =>
				{
					IsBusy = false;
					BusyText = "Catalog downloaded!";
					Settings.FirstStartApplication = false;
					Debug.WriteLine($"[LoadDataViewModel.DownloadedSuccesDelegateHandler] : Database udpated!");
				});
		}
	}
}
