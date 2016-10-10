using System;
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

			RegisterAppStart<ViewModels.RubricViewModel>();
        }
    }

}
