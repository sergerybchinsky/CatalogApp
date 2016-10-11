using System;
using System.Collections.ObjectModel;
using CatalogApp.ViewModels;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.iOS.Views;
using MvvmCross.Core.ViewModels;
using MvvmCross.iOS.Views;
using UIKit;

namespace CatalogApp.iOS.Views
{
	public class TableViewDataSource<T> : MvxActionBasedTableViewSource, IUITableViewDelegate, IUITableViewDataSource
	{
		public TableViewDataSource(UITableView tableView) : base(tableView)
		{ }

		public override nint NumberOfSections(UITableView tableView)
		{
			return 1;
		}

		public override nint RowsInSection(UITableView tableview, nint section)
		{
			return this.ItemsSource == null ? 0 : ((ObservableCollection<T>)this.ItemsSource).Count;
		}

		protected override UITableViewCell GetOrCreateCellFor(UITableView tableView, Foundation.NSIndexPath indexPath, object item)
		{
			var cell = tableView.DequeueReusableCell("UITableViewCell", indexPath);
			if(item is Category)
				cell.TextLabel.Text = (item as Category).Title;
			else if(item is Subject)
				cell.TextLabel.Text = (item as Subject).Title;

			return cell;
		}


		public IMvxCommand ShowCategoryCommand { get; set; }
		public IMvxCommand ShowSubjectCommand { get; set; }

		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
			var item = GetItemAt(indexPath);

			if (item is Category)
			{
				if (ShowCategoryCommand != null)
					ShowCategoryCommand.Execute(item);
			}
			else if (item is Subject)
			{
				if (ShowSubjectCommand != null)
					ShowSubjectCommand.Execute(item);
			}	
		}

		protected override object GetItemAt(NSIndexPath indexPath)
		{
			return ((ObservableCollection<T>)this.ItemsSource)[indexPath.Row];
		}
	}

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
