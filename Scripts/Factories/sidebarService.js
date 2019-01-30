var sidebarService = function () {
    document.body.style.marginLeft = "0";
    return {
        toggleSidebar: function () {
            var div1 = document.getElementById('mySidenav');
            if (div1 !== null) {
                console.log('div1: ' + div1.style.width);
                if (div1.style.width !== '250px') {
                    document.getElementById("mySidenav").style.width = "250px";
                    document.getElementById("mySidenav").style.marginLeft = "-250px";
                    document.getElementById("mySidenav").style.padding = "0 0 0 5px";
                    document.body.style.marginLeft = "250px";
                    document.getElementById("mySidenav").style.position = "fixed";
                } else {
                    document.body.style.marginLeft = "0";
                    document.getElementById("mySidenav").style.marginLeft = "0";
                    document.getElementById("mySidenav").style.width = "0";
                    document.getElementById("mySidenav").style.padding = "0px";
                }
            } else if (div1 === null || div1 === '0') {
                document.body.style.marginLeft = "0";
            }
        }
    };
};
sidebarService.$inject = [];
