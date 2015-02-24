using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace YocoInterviewTake5
{
	public class BusinessCategoryService
	{
		private const string BusinessCategoryURL = "http://yoco-core-staging.herokuapp.com/api/common/v1/properties/businessCategories";

		public BusinessCategoryService()
		{

		}

		public async Task<string> GetBusinessCategoriesAsync()
		{
			using (var httpClient = new HttpClient())
			{   
				Task<HttpResponseMessage> getResponse = httpClient.GetAsync(BusinessCategoryURL);

				HttpResponseMessage response = await getResponse;

				var result = await response.Content.ReadAsStringAsync();

				return result;
			}
		}
	}
}

