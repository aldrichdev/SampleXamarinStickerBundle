using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Foundation;
using Messages;
using UIKit;

namespace Archerisms.iOS.Stickers {
	public partial class ArcherStickersViewController : UICollectionViewController, IUICollectionViewDataSource {
		enum CollectionViewItem {
            ArcherSticker
		}

		public static readonly string StoryboardIdentifier = "ArcherStickersViewController";

		public IArcherStickersViewControllerDelegate Builder { get; set; }

		readonly List<MSSticker> items;

		public ArcherStickersViewController (IntPtr handle) : base (handle)
		{
            items = GetArcherStickers();
        }

        private List<MSSticker> GetArcherStickers()
        {
            var list = new List<MSSticker>();

            // Season 10
            list.Add(GetArcherSticker("ARCHER_Archer-Steering-Spaceship", "gif"));
            list.Add(GetArcherSticker("ARCHER_Cheryl-Supervisor", "gif"));
            list.Add(GetArcherSticker("ARCHER_Kreiger_Sneaky-Alien", "gif"));
            list.Add(GetArcherSticker("ARCHER_Krieger-JazzHands", "gif"));
            list.Add(GetArcherSticker("ARCHER_Krieger-SmokeBomb", "gif"));
            list.Add(GetArcherSticker("ARCHER_Lana_Frustrated", "gif"));
            list.Add(GetArcherSticker("ARCHER_Lana_Nope", "gif"));

            // Older
            list.Add(GetArcherSticker("Archer2408x408", "png"));
            list.Add(GetArcherSticker("ArcherCobraWhiskeyR2", "png"));
            list.Add(GetArcherSticker("Archergasp1408x408", "png"));
            list.Add(GetArcherSticker("ArcherHandsHeadR2", "gif"));
            list.Add(GetArcherSticker("ArcherS9_Cheryl_Finger-Licking-Good_R3", "gif"));
            list.Add(GetArcherSticker("ArcherS9_Cyril-Frustrated_R3", "gif"));
            list.Add(GetArcherSticker("ArcherS9_Cyril_Pointing_R4", "gif"));
            list.Add(GetArcherSticker("ArcherS9_Krieger-Drinking_R1", "gif"));
            list.Add(GetArcherSticker("ArcherS9_Krieger_Dancing_R4", "gif"));
            list.Add(GetArcherSticker("ArcherS9_Lana_ShiftingEyes_R5", "gif"));
            list.Add(GetArcherSticker("ArcherS9_Malory-Dropping-Drink_R5", "gif"));
            list.Add(GetArcherSticker("ArcherS9_Pam_Show-Me-The-Money_R3", "gif"));
            list.Add(GetArcherSticker("ArcherS9_Ray_A-HA_R3", "gif"));
            list.Add(GetArcherSticker("ArcherS9_SecretDocuments_Open-Close_R5", "gif"));
            list.Add(GetArcherSticker("cheryl2Excited408x408", "png"));
            list.Add(GetArcherSticker("DangerZone408x408", "gif"));
            list.Add(GetArcherSticker("LanaSideEye408x408", "png"));
            list.Add(GetArcherSticker("Malory2408x408", "png"));
            list.Add(GetArcherSticker("Phrasing408x408", "gif"));
            list.Add(GetArcherSticker("Poovey3408x408", "png"));
            list.Add(GetArcherSticker("Sploosh408x408", "gif"));

            return list;
        }

        private MSSticker GetArcherSticker(string fileName, string fileType)
        {
            var bundle = NSBundle.MainBundle;
            var testUrl = bundle.GetUrlForResource(fileName, fileType);
            if (testUrl == null)
                throw new Exception("Unable to find archer sticker image");

            var description = "An Archer sticker";

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
            var cell = DequeueArcherStickerCell(item, indexPath);

            return cell;
		}

        UICollectionViewCell DequeueArcherStickerCell(MSSticker sticker, NSIndexPath indexPath)
        {
            var cell = CollectionView.DequeueReusableCell(ArcherStickerCell.ReuseIdentifier, indexPath) as ArcherStickerCell;
            if (cell == null)
                throw new Exception("Unable to dequeue an ArcherStickerCell");

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
