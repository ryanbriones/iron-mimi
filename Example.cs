using System;
using IronMimi;

class Example {
	public static void Main(String[] args) {
		MadMimiApi api = new MadMimiApi("username", "password");
		
		object parameters = new {
			promotion_name="Foo",
			recipient="foo@example.com"
		};
		api.SendMailing(parameters);
	}
}
