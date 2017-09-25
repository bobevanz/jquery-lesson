$().ready(function() {
		$.getJSON(
			" http://md5.jsontest.com/?text=This is the md5",
			function(MD5){
				console.log("My MD5 is", MD5["md5"]);
				console.log("The original is", MD5["original"]);
			}

		);

	// $.getJSON(
	// 	 "http://headers.jsontest.com/",
	// 	 function(hdata){
	// 	 	console.log("My header is", hdata["Host"]);
	// 	 }
	// 	);
	// 	console.log("This is after the console log for my header");
	// 		$.getJSON(
	// 	"http://ip.jsontest.com",
	// 	function(ipdata){
	// 		console.log("My ip is", ipdata["ip"]);
	// 	}
	// 	);

	// console.log("This is after the console log for my ip");


});