using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Foundation;
using Messages;
using UIKit;

namespace Xamarin.iOS.Stickers {
	public partial class XamarinStickersViewController : UICollectionViewController, IUICollectionViewDataSource {
		enum CollectionViewItem {
            XamarinSticker
		}

		public static readonly string StoryboardIdentifier = "XamarinStickersViewController";

		public IXamarinStickersViewControllerDelegate Builder { get; set; }

		readonly List<MSSticker> items;

		public XamarinStickersViewController (IntPtr handle) : base (handle)
		{
            items = GetXamarinStickers();
        }

        private List<MSSticker> GetXamarinStickers()
        {
            var list = new List<MSSticker>();

            list.Add(GetXamarinSticker("Xamarin_Xamarin-Steering-Spaceship", "gif"));
            list.Add(GetXamarinSticker("Xamarin_Cheryl-Supervisor", "gif"));
            list.Add(GetXamarinSticker("Xamarin_Kreiger_Sneaky-Alien", "gif"));
            list.Add(GetXamarinSticker("Xamarin_Krieger-JazzHands", "gif"));
            list.Add(GetXamarinSticker("Xamarin_Krieger-SmokeBomb", "gif"));
            list.Add(GetXamarinSticker("Xamarin_Lana_Frustrated", "gif"));
            list.Add(GetXamarinSticker("Xamarin_Lana_Nope", "gif"));

            list.Add(GetXamarinSticker("Xamarin2408x408", "png"));
            list.Add(GetXamarinSticker("XamarinCobraWhiskeyR2", "png"));
            list.Add(GetXamarinSticker("Xamaringasp1408x408", "png"));
            list.Add(GetXamarinSticker("XamarinHandsHeadR2", "gif"));
            list.Add(GetXamarinSticker("XamarinS9_Cheryl_Finger-Licking-Good_R3", "gif"));
            list.Add(GetXamarinSticker("XamarinS9_Cyril-Frustrated_R3", "gif"));
            list.Add(GetXamarinSticker("XamarinS9_Cyril_Pointing_R4", "gif"));
            list.Add(GetXamarinSticker("XamarinS9_Krieger-Drinking_R1", "gif"));
            list.Add(GetXamarinSticker("XamarinS9_Krieger_Dancing_R4", "gif"));
            list.Add(GetXamarinSticker("XamarinS9_Lana_ShiftingEyes_R5", "gif"));
            list.Add(GetXamarinSticker("XamarinS9_Malory-Dropping-Drink_R5", "gif"));
            list.Add(GetXamarinSticker("XamarinS9_Pam_Show-Me-The-Money_R3", "gif"));
            list.Add(GetXamarinSticker("XamarinS9_Ray_A-HA_R3", "gif"));
            list.Add(GetXamarinSticker("XamarinS9_SecretDocuments_Open-Close_R5", "gif"));
            list.Add(GetXamarinSticker("cheryl2Excited408x408", "png"));
            list.Add(GetXamarinSticker("DangerZone408x408", "gif"));
            list.Add(GetXamarinSticker("LanaSideEye408x408", "png"));
            list.Add(GetXamarinSticker("Malory2408x408", "png"));
            list.Add(GetXamarinSticker("Phrasing408x408", "gif"));
            list.Add(GetXamarinSticker("Poovey3408x408", "png"));
            list.Add(GetXamarinSticker("Sploosh408x408", "gif"));

            return list;
        }

        private MSSticker GetXamarinSticker(string fileName, string fileType)
        {
            var bundle = NSBundle.MainBundle;
            var testUrl = bundle.GetUrlForResource(fileName, fileType);
            if (testUrl == null)
                throw new Exception("Unable to find Xamarin sticker image");

            var description = "An Xamarin sticker";

            NSError error;
            var sticker = new MSSticker(testUrl, description, out error);

            if (error != null)
                throw new Exception($"Failed to create placeholder sticker: {error.LocalizedDescription}");

            return sticker;
        }

		[Export("collectionView:numberOfItemsInSection:")]
		public override nint GetItemsCount (UICollectionView collectionView, nint section)
		{
			return items.Count;
		}

		[Export("collectionView:cellForItemAtIndexPath:")]
		public override UICollectionViewCell GetCell (UICollectionView collectionView, NSIndexPath indexPath)
		{
            var item = items[indexPath.Row];
            var cell = DequeueXamarinStickerCell(item, indexPath);

            return cell;
		}

        UICollectionViewCell DequeueXamarinStickerCell(MSSticker sticker, NSIndexPath indexPath)
        {
            var cell = CollectionView.DequeueReusableCell(XamarinStickerCell.ReuseIdentifier, indexPath) as XamarinStickerCell;
            if (cell == null)
                throw new Exception("Unable to dequeue an XamarinStickerCell");

            cell.StickerView.Sticker = sticker;
            cell.StickerView.StartAnimating();

            return cell;
        }

		static KeyValuePair<K, V> KeyValue<K, V> (K key, V value)
		{
			return new KeyValuePair<K, V> (key, value);
		}
	}
}
