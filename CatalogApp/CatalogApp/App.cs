using System;
using CatalogApp.Helpers;
using MvvmCross.Platform.IoC;

namespace CatalogApp
{
	public class App : MvvmCross.Core.ViewModels.MvxApplication
	{
		public override void Initialize()
		{
			CreatableTypes()
				.EndingWith("Service")
				.AsInterfaces()
				.RegisterAsLazySingleton();

			if (Settings.FirstStartApplication)
				RegisterAppStart<ViewModels.LoadDataViewModel>();
			else
				RegisterAppStart<ViewModels.CategoriesViewModel>();
		}
	}
}