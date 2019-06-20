using System;

using Messages;
using Foundation;
using UIKit;
using System.Diagnostics;

namespace Archerisms.iOS.Stickers
{
	public partial class MessagesViewController : MSMessagesAppViewController, IArcherStickersViewControllerDelegate {
		public MessagesViewController (IntPtr handle) : base (handle) { }

		public override void WillBecomeActive (MSConversation conversation)
		{
			base.WillBecomeActive (conversation);

			// Present the view controller appropriate for the conversation and presentation style.
			PresentViewController (conversation, PresentationStyle);
		}

		public override void WillTransition (MSMessagesAppPresentationStyle presentationStyle)
		{
			var conversation = ActiveConversation;
			if (conversation == null)
				throw new Exception ("Expected an active converstation");

			// Present the view controller appropriate for the conversation and presentation style.
			PresentViewController (conversation, presentationStyle);
		}

		void PresentViewController (MSConversation conversation, MSMessagesAppPresentationStyle presentationStyle)
		{
            // For both Compact and Expanded presentation styles, show the list of stickers.
			UIViewController controller = InstantiateArcherStickersController();

			foreach (var child in ChildViewControllers) {
				child.WillMoveToParentViewController (null);
				child.View.RemoveFromSuperview ();
				child.RemoveFromParentViewController ();
			}

			AddChildViewController (controller);
			controller.View.Frame = View.Bounds;
			controller.View.TranslatesAutoresizingMaskIntoConstraints = false;
			View.AddSubview (controller.View);

			controller.View.LeftAnchor.ConstraintEqualTo (View.LeftAnchor).Active = true;
			controller.View.RightAnchor.ConstraintEqualTo (View.RightAnchor).Active = true;
			controller.View.TopAnchor.ConstraintEqualTo (View.TopAnchor).Active = true;
			controller.View.BottomAnchor.ConstraintEqualTo (View.BottomAnchor).Active = true;

			controller.DidMoveToParentViewController (this);
		}

		UIViewController InstantiateArcherStickersController ()
		{
			// Instantiate a `ArcherStickersViewController` and present it.
			var controller = Storyboard.InstantiateViewController (ArcherStickersViewController.StoryboardIdentifier) as ArcherStickersViewController;
			if (controller == null)
				throw new Exception ("Unable to instantiate an ArcherStickersViewController from the storyboard");

			controller.Builder = this;
			return controller;
		}

		public void DidSelectAdd (ArcherStickersViewController controller)
		{
			Request (MSMessagesAppPresentationStyle.Expanded);
		}
	}
}
