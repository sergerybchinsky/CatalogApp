using System;
using System.Collections.ObjectModel;
using Foundation;
using MvvmCross.Binding.iOS.Views;
using MvvmCross.Core.ViewModels;
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
}