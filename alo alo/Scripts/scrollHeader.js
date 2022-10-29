window.onscroll = function () { myFunction() };

var menuheader = document.getElementById("menu");

var sticky = menuheader.offsetTop;
function myFunction() {
    if (window.pageYOffset >= sticky) {
        menuheader.classList.add("sticky");
    } else {
        menuheader.classList.remove("sticky");
    }

}