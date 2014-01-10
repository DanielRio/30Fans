; (function () { 
    var Products;
    window.Fans.Products = Products = {};

    Products = function(){
        // tentar fazer igual ao curso: criar um objeto e colocar os métodos dentro dele    
    }
    Fans.Products.init = function(html){
        this.html = $(html);
        this.ddCategories = this.html.find('#ddCategories');
        this.ddCategoriesItems = this.html.find('#ddCategoriesItems');
        this.ddProducts = this.html.find('#ddProducts');
        this.uploadFile = this.html.find('#uploadFile');
        this.submit = this.html.find('#submit');
        this.uploadPhotoFieldset = this.html.find('#uploadPhotoFieldset');
        
        this.ddCategories.on("change",function(){
            var selectedCategoryId = $(this).val();
            Fans.Products.loadCategoriesItems(html,selectedCategoryId);
        });

        this.ddCategoriesItems.on("change", function(){
            var selectedCategoryItemId = $(this).val();
            Fans.Products.loadProducts(ddProducts,selectedCategoryItemId);
        });

        this.ddProducts.on("change",function(){
            var selectedProductId = $(this).val();
            if (selectedProductId != "0")
                Fans.Products.showUploadFile(uploadPhotoFieldset,uploadFile,submit);
            else
                Fans.Products.hideUploadFile(uploadPhotoFieldset,uploadFile,submit);
        });
    };

    Fans.Products.loadCategoriesItems = function(html, categoryId){
        var ddCategoriesItems = this.html.find('#ddCategoriesItems');
        $.getJSON('/Admin/CategoryItem/' + categoryId , function(result){
            ddCategoriesItems.empty();
            $(result).each(function() {
                Fans.Products.populateDropDownList(ddCategoriesItems,this);
            });
        });
    };

    Fans.Products.loadProducts = function(dropdown, categoryItemId){        
        var _dropdown = $(dropdown);
        $.getJSON('/Admin/Products/' + categoryItemId , function(result){
            _dropdown.empty();
            $(result).each(function() {
                Fans.Products.populateDropDownList(_dropdown,this);
            });
        });
    };

    Fans.Products.populateDropDownList = function(dropdown,result){
        $(document.createElement('option'))
                    .attr('value', result.Id)
                    .text(result.Value)
                    .appendTo(dropdown);
    }

    Fans.Products.showUploadFile = function(uploadPhotoFieldset,upload, submit){
        $(upload).show();
        $(submit).show();
        console.log($(uploadPhotoFieldset));
        $(uploadPhotoFieldset).show();
    }

    Fans.Products.hideUploadFile = function(uploadPhotoFieldset, upload, submit){
        $(upload).hide();
        $(submit).hide();
        $(uploadPhotoFieldset).show();
    }

    var body = document.body;
    Fans.Products.init(body);    
})();