//added for the topmenu
function topMenu() {
    var x = document.getElementById("Topnav");
    if (x.className === "topnav") {
        x.className += " responsive";
    } else {
        x.className = "topnav";
    }
}

var slideout = new Slideout({
	'panel': document.getElementById('panelx'),
	'menu': document.getElementById('menu'),
	// 'padding': 256,
	'padding': 256,
	'tolerance': 70,
	'side':'right'
});

if(slideout.isOpen()){
	angular.element(document.querySelector('#menu')).css('display', 'block');
}

if(!slideout.isOpen()){
	angular.element(document.querySelector('#menu')).css('display', 'none');
}

// Toggle button
function toggleSlide() {
	slideout.toggle();
	if(slideout.isOpen()){
		angular.element(document.querySelector('#menu')).css('display', 'block');
	}
	if(!slideout.isOpen()){
		angular.element(document.querySelector('#menu')).css('display', 'none');
	}
}
		
function openSlide() {	
	angular.element(document.querySelector('.full-overlay')).removeClass("hidden");
	angular.element(document.querySelector('#menu')).css('display', 'block');
	slideout.open();
}

document.querySelector('.close-slide').addEventListener('click', function() {
	angular.element(document.querySelector('.close-slide')).addClass("hidden");
	angular.element(document.querySelector('.slidecart')).addClass("hidden");
	angular.element(document.querySelector('.full-overlay')).toggleClass("hidden");
	angular.element(document.querySelector('#menu')).css('display', 'none');
	slideout.close();
});
		
slideout.on('open', function() {
	angular.element(document.querySelector('.full-overlay')).removeClass("hidden");
	angular.element(document.querySelector('.close-slide')).removeClass("hidden");
	angular.element(document.querySelector('.slidecart')).removeClass("hidden");
});