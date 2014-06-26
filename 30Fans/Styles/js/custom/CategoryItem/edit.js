;(function () {
    var CategoryItem;
    window.Fans.CategoryItem = CategoryItem = {};

    Fans.CategoryItem.showProducts = function(html){
        this.html = $(html);
        this.loaderImage = this.html.find('#productLoaderImage');
        this.resultContainer = this.html.find('#productsResult');

        this.resultContainer.load(this.resultContainer.data("url"));
        Fans.CategoryItem.hideLoader(this.loaderImage);
    };

    Fans.CategoryItem.hideLoader = function(loader){
        this.loader.hide();
    }

    Fans.CategoryItem.showLoader = function(loader){        
        this.loader.show();
    }

    var body = document.body;
    Fans.CategoryItem.showProducts(body);    
})();