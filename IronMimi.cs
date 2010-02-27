using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Web;
using System.ComponentModel;

namespace IronMimi {
	public class MadMimiAuthentication {
		public string username { get; set; }
    public string api_key { get; set; }
	}
	
	public class MadMimiApi {
		MadMimiAuthentication _auth;
		
		public MadMimiApi(MadMimiAuthentication auth) {
			_auth = auth;
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
			requestParams.Add("username", _auth.username);
			requestParams.Add("api_key", _auth.api_key);
			
			String postData = MadMimiApi.ConvertDictionaryToQueryString(requestParams);
			
			using(WebClient client = new WebClient()) {
				client.UploadData(url, "POST", System.Text.Encoding.UTF8.GetBytes(postData));
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