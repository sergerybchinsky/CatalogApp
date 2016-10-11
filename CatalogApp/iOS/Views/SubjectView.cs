using CatalogApp.ViewModels;
using CoreGraphics;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.iOS.Views;
using UIKit;

namespace CatalogApp.iOS
{
	[Register("SubjectView")]
	public class SubjectView : MvxViewController
	{
		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			this.View.BackgroundColor = UIColor.White;

			var fishContent = new UITextView();
			fishContent.Frame = new CGRect(20, 20, this.View.Frame.Width - 40, this.View.Frame.Height - 40);
			fishContent.Text = "that my free will had received a mortal wound; and that another's mistake or misfortune might plunge innocent me into unmerited disaster and death. Therefore, I saw that here was a sort of interregnum in Providence; for its even-handed equity never could have so gross an injustice. And yet still further pondering—while I jerked him now and then from between the whale and ship, which would threaten to jam him—still further pondering, I say, I saw that this situation of mine was the precise situation of every mortal that breathes; only, in most cases, he, one way or other, has this Siamese connexion with a plurality of other mortals. If your banker breaks, you snap; if your apothecary by mistake sends you poison in your pills, you die. True, you may say that, by exceeding caution, you may possibly escape these and the multitudinous other evil chances of life. But handle Queequeg's monkey-rope heedfully as I would, sometimes he jerked it so, that I came very near sliding overboard. Nor could I possibly forget that, do what I would, I only had the management of one end of it.\"";
			fishContent.BackgroundColor = UIColor.Clear;
			fishContent.Editable = false;
			this.View.AddSubview(fishContent);

			var set = this.CreateBindingSet<SubjectView, SubjectViewModel>();
			set.Bind(this).For(t => t.Title).To(vm => vm.Title);
			set.Apply();
		}
	}
}