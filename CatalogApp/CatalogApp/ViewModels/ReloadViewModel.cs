using MvvmCross.Core.ViewModels;

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