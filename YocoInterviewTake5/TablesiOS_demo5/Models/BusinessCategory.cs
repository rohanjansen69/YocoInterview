using System;
using SQLite;

namespace YocoInterviewTake5
{
	public class BusinessCategory 
	{
		string iD;
		[PrimaryKey]
		public string ID {
			get {
				return iD;
			}
			set {
				iD = value;
			}
		}

		string category;
		public string Category {
			get {
				return category;
			}
			set {
				category = value;
			}
		}

		string name;
		public string Name {
			get {
				return name;
			}
			set {
				name = value;
			}
		}
	}
}

