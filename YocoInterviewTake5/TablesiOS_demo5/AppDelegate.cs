using System;
using UIKit;
using Foundation;
using System.IO;

namespace YocoInterviewTake5 {

	public class Application {
		public static void Main (string[] args)
		{
			try {
				UIApplication.Main (args, null, "AppDelegate");
			} catch (Exception e) {
				Console.WriteLine (e.ToString ());
			}
		}
	}

	[Register("AppDelegate")]
	public class AppDelegate : UIApplicationDelegate
	{
		UIWindow window;

		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
//			DeleteDatabaseIfItAlreadyExists ();

			window = new UIWindow (UIScreen.MainScreen.Bounds);
			window.MakeKeyAndVisible ();
			window.RootViewController = new BusinessCategoryViewController ();
			return true;
		}

		private void DeleteDatabaseIfItAlreadyExists()
		{
			var dbName = "yocoInterview.db";
			var documents = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
			var dbPath = Path.Combine(documents, dbName);

			if (File.Exists(dbPath))
			{
				File.Delete(dbPath);
			}
		}
	}
}