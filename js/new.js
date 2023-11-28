$(function () {
    $(".font-minus").bind("click", function () {
        var size = parseInt($('p').css("font-size"));
        size = size - 1;
        if (size <= 12) {
            size = 12;
        }

        $('p').css("font-size", size);
    });
});

$(function () {
    $(".font-plus").bind("click", function () {
        var size = parseInt($('p').css("font-size"));

        size = size + 1;
        if (size >= 16) {
            size = 16;
        }

        $('p').css("font-size", size);
    });
});
$(function () {
    $(".font-normal").bind("click", function () {
        var size = parseInt($('p').css("font-size"));
        size = 14;
        $('p').css("font-size", size);
    });
});

$(function () {
    $(window).load(function () { // On load
        $('.wrapper').css({ 'min-height': (($(window).height()) - ($('.header').height()) - ($('.footer').height()) - ($('.menu').height()) - ($('.topbar').height())) + 'px' });
    });
    $(window).resize(function () { // On resize
        $('.wrapper').css({ 'min-height': (($(window).height()) - ($('.header').height()) - ($('.footer').height()) - ($('.menu').height()) - ($('.topbar').height())) + 'px' });
    });
  
});