// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace YocoInterviewTake5
{
	[Register ("BusinessCell")]
	partial class BusinessCell
	{
		[Outlet]
		UIKit.UILabel BusinessName { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (BusinessName != null) {
				BusinessName.Dispose ();
				BusinessName = null;
			}
		}
	}
}
