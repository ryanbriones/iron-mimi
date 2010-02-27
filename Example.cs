using System;
using IronMimi;

class Example {
	public static void Main(String[] args) {
		MadMimiAuthentication auth = new MadMimiAuthentication() {
																 	 username = "me@example.com",
																 	 api_key = "*******"
																 };
		MadMimiApi api = new MadMimiApi(auth);
		
		object parameters = new {
			promotion_name="Foo",
			recipient="egunderson+foo@obtiva.com"
		};
		api.SendMailing(parameters);
	}
}