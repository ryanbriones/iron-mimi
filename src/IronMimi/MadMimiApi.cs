using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Web;
using System.ComponentModel;

namespace IronMimi {
	public class MadMimiApi {
		private string _username;
		private string _apikey;
		
		public MadMimiApi(string username, string apikey) {
			_username = username;
			_apikey = apikey;
		}
		
		public int SendMailing(object parameters) {
			var requestParams = new Dictionary<string, string>();
			
			if (parameters != null) {
				foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(parameters)) {
					var value = descriptor.GetValue(parameters).ToString();
					requestParams.Add(descriptor.Name, value);
				}
			}
			
			MakePostRequest("https://madmimi.com/mailer", requestParams);
			return 1;
		}
		
		private string MakePostRequest(String url, IDictionary<string, string> requestParams) {
			requestParams.Add("username", _username);
			requestParams.Add("api_key", _apikey);
			
			String postData = MadMimiApi.ConvertDictionaryToQueryString(requestParams);
			
			using(WebClient client = new WebClient()) {
				try {
					client.UploadData(url, "POST", System.Text.Encoding.UTF8.GetBytes(postData));
				} catch(WebException e) {
					throw IronMimi.Exception.MakeException(e);
				}
			}
			
			return "string";
		}
		
		private static String ConvertDictionaryToQueryString(IDictionary<string, string> dict) {
			var pairs = new List<string>();

			foreach (var key in dict.Keys) {
				pairs.Add(String.Format("{0}={1}", key, HttpUtility.UrlEncode(dict[key])));
			}

			return String.Join("&", pairs.ToArray());
		}
	}
}