using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;

namespace YocoInterviewTake5
{
	public class BusinessDataManager
	{
		public BusinessDataManager()
		{

		}

		public void InsertItem(BusinessCategory category)
		{
			using (var db = new SQLite.SQLiteConnection(DBHelper.DBPath ))
			{
				db.Insert(category);
			}
		}

		public List<BusinessCategory> GetAllItems()
		{
			using (var db = new SQLite.SQLiteConnection(DBHelper.DBPath ))
			{
				var result = db.Table<BusinessCategory> ().ToList ();
				return result;
			}
		}

		public int ItemCount()
		{
			return GetAllItems().Count;
		}
	}

	public class BusinessCategoryManager
	{
		private readonly BusinessCategoryService _service;
		private readonly BusinessDataManager _dataManager;

		public BusinessCategoryManager ()
		{
			_service = new BusinessCategoryService ();
			_dataManager = new BusinessDataManager ();
		}

		public async Task LoadBusinessCategoriesAsync()
		{
			LoadCachedBusinessCategoriesIfAny ();
			var result = await _service.GetBusinessCategoriesAsync ().ConfigureAwait (false);
			HandleSuccess (Deserialize(result));
		}

		private void LoadCachedBusinessCategoriesIfAny()
		{
			if(_dataManager.ItemCount () > 0) {
				BusinessCategories = _dataManager.GetAllItems ();
			}
		}

		private BusinessCategoryList Deserialize(string responseBody)
		{
			var toReturn = JsonConvert.DeserializeObject<BusinessCategoryList>(responseBody);
			return toReturn;
		}

		void HandleSuccess (BusinessCategoryList obj)
		{
			var tempList = new List<BusinessCategory> ();
			try
			{
				foreach(var listItem in obj.Data) 
				{
					var item = new BusinessCategory() {
						ID = listItem[0],
						Name = listItem[1],
						Category = listItem[2]
					};

					tempList.Add (item);
					_dataManager.InsertItem (item);
				}


				var test = _dataManager.GetAllItems ();
				BusinessCategories = tempList;
			}
			catch(Exception ex)
			{
				var msg = ex.Message;
				throw;
			}
		}

		List<BusinessCategory> businessCategories;
		public List<BusinessCategory> BusinessCategories {
			get {
				return businessCategories;
			}
			set {
				businessCategories = value;
			}
		}
	}
}

