;(function () {
    var CategoryItem;
    window.CategoryItem = CategoryItem = {};

    Fans.showProducts = function(html){
        this.html = $(html);
        this.loaderImage = this.html.find('#productLoaderImage');
        this.resultContainer = this.html.find('#productsResult');

        //Fans.showLoader(this.loaderImage);
        this.resultContainer.load('/Product/List/2');
        Fans.hideLoader(this.loaderImage);
    };

    Fans.hideLoader = function(loader){
        this.loader.hide();
    }

    Fans.showLoader = function(loader){        
        this.loader.show();
    }

    var body = document.body;
    Fans.showProducts(body);    
})();