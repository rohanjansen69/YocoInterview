
using System;
using System.Drawing;

using Foundation;
using UIKit;

namespace YocoInterviewTake5
{
	public partial class BusinessCell : UITableViewCell
	{
		public static readonly UINib Nib = UINib.FromName ("BusinessCell", NSBundle.MainBundle);
		public static readonly NSString Key = new NSString ("BusinessCell");

		public BusinessCell (IntPtr handle) : base (handle)
		{
		}

		public static BusinessCell Create ()
		{
			return (BusinessCell)Nib.Instantiate (null, null) [0];
		}
	}
}

