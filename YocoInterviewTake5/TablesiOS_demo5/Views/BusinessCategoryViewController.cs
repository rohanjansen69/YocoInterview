using System;
using CoreGraphics;
using System.Collections.Generic;
using System.Linq;
using UIKit;
using Foundation;
using System.Threading.Tasks;
using System.IO;

namespace YocoInterviewTake5 {


	public class BusinessCategoryViewController : UITableViewController {

		List<BusinessCategory> _businessCategories;
		List<BusinessCategory> BusinessCategories {
			get {
				return _businessCategories;
			}
			set {
				_businessCategories = value;
				if(_businessCategories.Count > 0) {
					BeginInvokeOnMainThread (() => {
						TableView.Source = new BusinessCategoryTableSource (BusinessCategories);
						TableView.ReloadData ();
					});
				}
			}
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			CreateDatabase ();

			TableView = new UITableView (CGRect.Empty, UITableViewStyle.Plain); // Grouped or Plain
			TableView.BackgroundView = new UIImageView (UIImage.FromBundle ("images/Background"));
			TableView.RegisterNibForCellReuse (BusinessCell.Nib, BusinessCell.Key);

			Task.Run (async () => {
				await PopulateTableView ().ConfigureAwait (false);
			});
		}

		void CreateDatabase()
		{
			using (var conn= new SQLite.SQLiteConnection(DBHelper.DBPath))
			{
				conn.CreateTable<BusinessCategory>();
			}
		}

		// Adjust for overlap of first row by status bar for for iOS 7
		public override void ViewDidLayoutSubviews ()
		{
			base.ViewDidLayoutSubviews ();

			TableView.ContentInset = new UIEdgeInsets (this.TopLayoutGuide.Length, 0, 0, 0);
		}

		protected async Task PopulateTableView()
		{
			var manager = new BusinessCategoryManager ();

			await manager.LoadBusinessCategoriesAsync ().ContinueWith (x => {
				BusinessCategories = manager.BusinessCategories;
			}).ConfigureAwait (false);
		}
	}
}