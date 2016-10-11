using CatalogApp.ViewModels;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.iOS.Views;
using UIKit;

namespace CatalogApp.iOS.Views
{
	[Register("CategoriesView")]
	public class CategoriesView : MvxTableViewController
	{
		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			Title = "Catalog";

			UIApplication.SharedApplication.KeyWindow.RootViewController = this.NavigationController;

			var dataSource = new TableViewDataSource<Category>(this.TableView);
			this.TableView.RegisterClassForCellReuse(typeof(UITableViewCell), "UITableViewCell");
			this.TableView.Delegate = dataSource;
			this.TableView.DataSource = dataSource;

			var reloadDbButton = new UIBarButtonItem();
			reloadDbButton.Image = UIImage.FromBundle(Const.ImageResource.WebIconImage);
			this.NavigationItem.RightBarButtonItem = reloadDbButton;

			var set = this.CreateBindingSet<CategoriesView, CategoriesViewModel>();
			set.Bind(dataSource).For(ds => ds.ItemsSource).To(vm => vm.Categories);
			set.Bind(dataSource).For(ds => ds.ShowCategoryCommand).To(vm => vm.ShowSubjectCommand);
			set.Bind(reloadDbButton).For(bt => bt).To(vm => vm.ShowLoadDataCommand);
			set.Apply();
		}
	}
}