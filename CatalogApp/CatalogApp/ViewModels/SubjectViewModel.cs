using MvvmCross.Core.ViewModels;

namespace CatalogApp.ViewModels
{
	public class SubjectViewModel : BaseViewModel
	{
		public SubjectViewModel(IPlatformDependency platformDependency, IDataService dataService) : base()
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

		protected async override void InitFromBundle(IMvxBundle parameters)
		{
			base.InitFromBundle(parameters);
			Title = parameters.Data["subjectTitle"];
		}
	}
}