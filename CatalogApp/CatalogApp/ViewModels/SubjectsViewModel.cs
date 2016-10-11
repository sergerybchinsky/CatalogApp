using System;
using System.Collections;
using System.Collections.ObjectModel;
using MvvmCross.Core.ViewModels;

namespace CatalogApp.ViewModels
{
	public class SubjectsViewModel : BaseViewModel
	{
		public SubjectsViewModel(IPlatformDependency platformDependency, IDataService dataService) : base()
		{
			_platformDependency = platformDependency;
			_dataService = dataService;
		}

		private ObservableCollection<Subject> _subjects;
		public ObservableCollection<Subject> Subjects
		{
			get { return _subjects; }

			set
			{
				_subjects = value;
				RaisePropertyChanged(() => Subjects);
			}
		}

		private string _categoryName;
		public string CategoryName
		{
			get { return _categoryName; }

			set
			{
				_categoryName = value;
				RaisePropertyChanged(() => CategoryName);
			}
		}

		private IMvxCommand _showSubjectCommand;
		public IMvxCommand ShowSubjectCommand
		{
			get
			{
				if (_showSubjectCommand == null)
					_showSubjectCommand = new MvxCommand<Subject>((subject) =>
					{
						var bundle = new MvxBundle();
						bundle.Data.Add("subjectTitle", subject.Title);
						ShowViewModel<SubjectViewModel>(bundle);
					});
				return _showSubjectCommand;
			}
		}

		protected async override void InitFromBundle(IMvxBundle parameters)
		{
			base.InitFromBundle(parameters);
			int categoryId = Int32.Parse(parameters.Data["categoryID"]);
			CategoryName = parameters.Data["categoryName"];
			Subjects = new ObservableCollection<Subject>(await _dataService.GetSubjectsByCategoryId(categoryId));
		}

	}
}
