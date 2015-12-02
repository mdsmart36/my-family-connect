// code snippet borrowed from http://bootsnipp.com/snippets/featured/show-dropdown-hover-amp-caret-up
$(function () {
    $(".dropdown").hover(
            function () {
                $('.dropdown-menu', this).stop(true, true).fadeIn("fast");
                $(this).toggleClass('open');
                $('#caret', this).toggleClass("caret caret-up");
            },
            function () {
                $('.dropdown-menu', this).stop(true, true).fadeOut("fast");
                $(this).toggleClass('open');
                $('#caret', this).toggleClass("caret caret-up");
            });
});