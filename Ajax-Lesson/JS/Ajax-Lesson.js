$().ready(function() {

	$.getJSON(
		"http://ip.jsontest.com",
		function(ipdata){
			console.log("My ip is", ipdata["ip"]);
		}
		);

	console.log("This is after the console log for my ip");


});