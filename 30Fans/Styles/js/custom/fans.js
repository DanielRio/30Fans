;(function () {
    var Fans;
    window.Fans = Fans = {};

    Fans.init = function(html){
        this.html = $(html);

        this.contact = new Fans.Contact(this.html.find('#navContact'));
        this.contact.on("click", function(e){
            Fans.showDialog(e,this);
        });

        this.condition = new Fans.Condition(this.html.find('#navConditions'));
        this.condition.on("click", function(e){
            Fans.showDialog(e,this);
        });
    };

    Fans.showDialog = function(event, button){
        event.preventDefault();
        var dialogName = $(button).data("dialog");
        $('#' + dialogName).dialog({    
            width: 800,
            height: 600, 
            modal: true 
        });
    }
})();