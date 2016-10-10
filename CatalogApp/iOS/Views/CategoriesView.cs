using System;
using CatalogApp.ViewModels;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.iOS.Views;

namespace CatalogApp.iOS.Views
{
	[Register("CategoriesView")]
	public class CategoriesView : MvxTableViewController
	{
		public CategoriesView()
		{
		}

		public new CategoriesViewModel ViewModel
		{
			get { return base.ViewModel as CategoriesViewModel; }
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			Title = "Rubric";

			var set = this.CreateBindingSet<CategoriesView, CategoriesView>();
			//set.Bind(this.TableView).For(t => t.DataSource).To(vm => vm.Categories).Apply();
		}
	}
}
