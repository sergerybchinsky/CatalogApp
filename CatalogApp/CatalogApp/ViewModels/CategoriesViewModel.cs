using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MvvmCross.Core.ViewModels;

namespace CatalogApp.ViewModels
{
	public class CategoriesViewModel : BaseViewModel, IDataServiceDelegate
	{
		public CategoriesViewModel(IPlatformDependency platformDependency, IDataService dataService) : base()
		{
			_platformDependency = platformDependency;
			_dataService = dataService;
			DataService.Delegate = this;
		}

		private ObservableCollection<Category> _categories;
		public ObservableCollection<Category> Categories
		{
			get { return _categories; }

			set
			{
				_categories = value;
				RaisePropertyChanged(() => Categories);
			}
		}

		private IMvxCommand _showSubjectCommand;
		public IMvxCommand ShowSubjectCommand
		{
			get
			{
				if (_showSubjectCommand == null)
					_showSubjectCommand = new MvxCommand<Category>((category) =>
					{
						var bundle = new MvxBundle();
						bundle.Data.Add("categoryID", category.ID.ToString());
						bundle.Data.Add("categoryName", category.Title);
						ShowViewModel<SubjectsViewModel>(bundle);
				});
				return _showSubjectCommand;
			}
		}

		private IMvxCommand _showLoadDataCommand;
		public IMvxCommand ShowLoadDataCommand
		{
			get
			{
				if (_showLoadDataCommand == null)
					_showLoadDataCommand = new MvxCommand(() =>
					{
						ShowViewModel<ReloadViewModel>();
					});
				return _showLoadDataCommand;
			}
		}

		protected override void ReloadFromBundle(IMvxBundle state)
		{
			base.ReloadFromBundle(state);
		}

		public async override void Start()
		{
			base.Start();
			Categories = new ObservableCollection<Category>(await _dataService.GetCategories());
		}

		public async Task DataBaseUdpated()
		{
			Categories = new ObservableCollection<Category>(await _dataService.GetCategories());
		}
	}
}