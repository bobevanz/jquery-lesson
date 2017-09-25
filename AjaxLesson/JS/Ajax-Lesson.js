$().ready(function() { //This and the button, along with some html... this is how you normally select a specific vendor from list of vendors  
	function getVendor(id){
	$.getJSON(
		"http://prs.gregorydoud.net/Vendors/Get/" + id, // http://prsng.shows front end
		function(vendor) {
			console.log(vendor);
			}
		);
	} // end function

$("button").click(function() { // see first comment
	var vendorId = $("#inputId").val();
	getVendor(vendorId);

}); // end button function

	// $.getJSON(
	// 	"http://prs.gregorydoud.net/Vendors/List",
	// 	function(vendors){
	// 		console.log(vendors);
	// 		processVendors(vendors);
	// 	}
	// 	);

	// 	function processVendors(vendors){
	// 		for(var vendor of vendors){
	// 			console.log(vendor.Name, vendor.Email, vendor.Phone );
	// 		}
	// 	}


// 		$.getJSON(
// 			" http://md5.jsontest.com/?text=This_is_the_md5",
// 			function(MD5){
// 				console.log("My MD5 is", MD5["md5"]);
// 				console.log("The original is", MD5["original"]);
// 			}

// 		);

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

}); // end of ready function
