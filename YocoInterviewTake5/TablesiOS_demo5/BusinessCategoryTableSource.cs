using System;
using CoreGraphics;
using System.Collections.Generic;
using System.Linq;
using UIKit;
using Foundation;

namespace YocoInterviewTake5
{
	public class BusinessCategoryTableSource : UITableViewSource
	{		
		static readonly string BusinessCategoryCellID = "BusinessCell";
		List<BusinessCategory> Data;
		IGrouping<string, BusinessCategory>[] Grouping;
		
		public BusinessCategoryTableSource (List<BusinessCategory> sessions)
		{
			Data = sessions;
			Grouping = GetBusinessCategoriesGroupedByCategoryName();
		}
		
		public override nint RowsInSection (UITableView tableview, nint section)
		{
			return Grouping[section].Count ();
		}
		
		public override nint NumberOfSections (UITableView tableView)
		{
			return Grouping.Count ();
		}
		
		public override string TitleForHeader (UITableView tableView, nint section)
		{
			return Grouping [section].ElementAt (0).Category;
		}
		
		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			var categoryGroup = Grouping [indexPath.Section];
			var business = categoryGroup.ElementAt (indexPath.Row);
			
			new UIAlertView ("Business Selected", business.Name, null, "OK", null).Show ();
			
			tableView.DeselectRow (indexPath, true);
		}
		
		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			var categoryGroup = Grouping [indexPath.Section];
			var business = categoryGroup.ElementAt (indexPath.Row);

			var cell = (BusinessCell)tableView.DequeueReusableCell (BusinessCategoryCellID);	

			if(cell == null) {
				cell = new BusinessCell (IntPtr.Zero);
			}

			cell.TextLabel.Text = business.Name;
			
			return cell;
		}
		
		
		// helper method
		protected internal IGrouping<string, BusinessCategory>[] GetBusinessCategoriesGroupedByCategoryName ()
		{
			var sessionsGrouped = (from s in Data
				group s by s.Category into g
			                       select g).ToArray ();
			
			return (IGrouping<string, BusinessCategory>[])sessionsGrouped;
		}
	} 
}

