;(function(Fans) {
    Fans.Contact = function(html){
        this.html = html;
        this.addEventListeners();
    };

    Fans.Contact.prototype.on = function(event,callback){
        this.html.on(event, callback);
    };

    Fans.Contact.prototype.addEventListeners = function(){
      this.html.on("click", $.proxy("onClick"));  
    };

    Fans.Contact.prototype.onClick = function(event){
		event.preventDefault();
        alert(event.target.value);
		this.html.trigger("click",event.target.value);
    };
})(Fans);