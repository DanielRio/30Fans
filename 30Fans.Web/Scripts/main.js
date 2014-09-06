
    
function resizeArticle() {
    var widthWindow = $(window).width();
    var heightWindow = $(window).height();

    $('.total-window-height').css({
        'min-height' : heightWindow-70
    });
}

function friendFilter() {
    $("#loading").show();
    $.ajax({
        url: "/Football/SelectedFriendFilter",
        data: {
            partName: $("#teamSearch").val()
        }
    }).done(function (data) {
        $("#search-result").show();
        $('#search-result').html(data);
        $("#loading").hide();
       // GOLEAK.desbloquearTela();
    }).fail(function (response) {
        //GOLEAK.desbloquearTela();
        $("#loading").hide();
        alert("Ooops, something wrong!");
        
    });
}

$(document).ready(function() {
    $('#teamSearch').focus();

    resizeArticle();


    // Open External Links in a New Window
    $("a[href^='http:']:not([href*='" + window.location.host + "']),a[href^='https:']:not([href*='" + window.location.host + "'])").each(function() {               
            $(this).attr("target", "_blank");
    });

    $('.in-reset').bind('click', function() {
        $('#teamSearch').focus();
    });
    
    
    
    $('.team-year').each(function() {
        $(this).bind('click', function() {
            $(this).addClass('open').find('li').slideDown();
        });
    });
    
    
    
    var hiddenInputs = $('.f-favoriteTeam .in-reset, .hero .primary');
    hiddenInputs.fadeOut();
    

    var xTriggered = 0;
    $('#teamSearch').keypress(function( e ) {
      if ( e.which == 13 ) {
         e.preventDefault();
      }
      if(xTriggered == 2) {
        hiddenInputs.fadeIn(800);
      } 
        
      xTriggered++;
    });

    /*
    $('html').click(function () {
        $("#search-result").hide();
    });
    */

    $("#teamSearch").keyup(function () {
        var texto = this.value;

        if (texto.length < 3) {
            $("#search-result").html("");
            return;
        }

        friendFilter();
    });

    $('.choose-lang a').bind('click', function() {
        $(this).parents('ul').find('.choose').slideToggle();
    });
});



$(window).resize(function() {
	resizeArticle();
});

$(window).load(function() {
    $('body').addClass('loaded');
});