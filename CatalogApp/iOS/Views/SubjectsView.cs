using CatalogApp.iOS.Views;
using CatalogApp.ViewModels;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.iOS.Views;
using UIKit;

namespace CatalogApp.iOS.View
{
	[Register("SubjectsView")]
	public class SubjectsView : MvxTableViewController
	{
		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			var dataSource = new TableViewDataSource<Subject>(this.TableView);
			this.TableView.RegisterClassForCellReuse(typeof(UITableViewCell), "UITableViewCell");
			this.TableView.Delegate = dataSource;
			this.TableView.DataSource = dataSource;

			var set = this.CreateBindingSet<SubjectsView, SubjectsViewModel>();
			set.Bind(this).For(t => t.Title).To(vm => vm.CategoryName);
			set.Bind(dataSource).For(ds => ds.ItemsSource).To(vm => vm.Subjects);
			set.Bind(dataSource).For(ds => ds.ShowSubjectCommand).To(vm => vm.ShowSubjectCommand);
			set.Apply();
		}
	}
}