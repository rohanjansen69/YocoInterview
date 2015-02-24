using System;
using System.IO;

namespace YocoInterviewTake5
{
	public static class DBHelper
	{
		public static string DBPath {
			get {
				var documents = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
				var path = Path.Combine(documents, "yocoInterview.db");
				return path;
			}
		}
	}
}

