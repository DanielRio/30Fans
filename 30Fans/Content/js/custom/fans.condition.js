;(function(Fans) {
    Fans.Condition = function(html){
        this.html = html;
        this.addEventListeners();
    };

    Fans.Condition.prototype.on = function(event,callback){
        this.html.on(event, callback);
    };

    Fans.Condition.prototype.addEventListeners = function(){
      this.html.on("click", $.proxy("onClick"));  
    };

    Fans.Condition.prototype.onClick = function(event){
		event.preventDefault();
        alert(event.target.value);
		this.html.trigger("click",event.target.value);
    };
})(Fans);