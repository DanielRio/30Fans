;(function () {
    var Fans;
    window.Fans = Fans = {};

    Fans.init = function(html){
        this.html = $(html);
        this.contactPopup = $("#contact");

        this.contact = new Fans.Contact(this.html.find('#navContact'));
//        this.contact.on("click", function(e){
//            e.preventDefault();
//            contactPopup.dialog();
//        });            
    };
})();