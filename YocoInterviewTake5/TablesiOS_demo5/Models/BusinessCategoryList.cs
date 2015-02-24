using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace YocoInterviewTake5
{
	public class BusinessCategoryList
	{
		[JsonProperty(PropertyName="status")]
		public int Status { get; set; }

		[JsonProperty(PropertyName="data")]
		public List<List<string>> Data { get; set; }
	}
}

