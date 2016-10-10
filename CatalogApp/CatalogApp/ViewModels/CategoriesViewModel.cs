using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;

namespace CatalogApp.ViewModels
{
	public class CategoriesViewModel : BaseViewModel
	{
		public CategoriesViewModel(IPlatformDependency platformDependency, IDataService dataService) : base()
		{
			_platformDependency = platformDependency;
			_dataService = dataService;
		}

		ObservableCollection<Category> _categories;
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
					_showSubjectCommand = new MvxCommand(() =>
					{
						var bundle = new MvxBundle();
						ShowViewModel<SubjectViewModel>(bundle);
					});
				return _showSubjectCommand;
			}
		}

		public async override void Start()
		{
			base.Start();
		}
	}
}
