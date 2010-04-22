using System;
using System.Net;
using System.IO;
using System.Text;

namespace IronMimi {
	public class InvalidAuthorizationException : WebException {
		public InvalidAuthorizationException(string message, System.Exception e) : base(message, e) {}
	}
	
	public class AudienceOverageException : WebException {
		public AudienceOverageException(string message, System.Exception e) : base(message, e) {}
	}
	
	public class AccountException : WebException {
		public AccountException(string message, System.Exception e) : base(message, e) {}
	}
	
	public class PaymentException : WebException {
		public PaymentException(string message, System.Exception e) : base(message, e) {}
	}
	
	public class MailerApiNotEnabledException : WebException {
		public MailerApiNotEnabledException(string message, System.Exception e) : base(message, e) {}
	}
	
	public class Exception {
		public static System.Exception MakeException(WebException exception) {
			HttpWebResponse webResponse = (HttpWebResponse) exception.Response;
			StreamReader sr = new StreamReader(webResponse.GetResponseStream(), Encoding.UTF8);
			string mimiBodyMessage = sr.ReadToEnd();
			
			switch (webResponse.StatusCode) {
			case System.Net.HttpStatusCode.Unauthorized:
				return new InvalidAuthorizationException(mimiBodyMessage, exception);
			case HttpStatusCode.PaymentRequired:
				return new AudienceOverageException(mimiBodyMessage, exception);
			case HttpStatusCode.Forbidden:
				return new AccountException(mimiBodyMessage, exception);
			default:
				return exception;
			}
		}
	}
}
