// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

(function( $ ) {
    // shorten references to variables. this is better for uglification 
    var kendo = window.kendo,
        ui = kendo.ui,
        Widget = ui.Widget;

    jQuery.fn.myNotification = function(options) {

        var notification = $("#gridNotification").kendoNotification($.extend({
            autoHideAfter: 0,
            hideOnClick: false,
            show: onShow,
            parentTarget: "body"
        }, options)).data("kendoNotification");

        return notification;
    };

    function onShow(e) {
        //debugger;
        var target = e.sender;
        if (target != null) {
            var parentTarget = $(target.options.parentTarget);
            if (target.getNotifications().length == 1) {
                var element = e.element.parent(),
                    eWidth = element.width(),
                    eHeight = element.height(),
                    wWidth = parentTarget.width(),
                    wHeight = parentTarget.height(),
                    wOffsetTop = parentTarget.offset().top,
                    wOffsetLeft = parentTarget.offset().left,
                    newTop, newLeft;
                
                newLeft = Math.floor(wWidth / 2 - eWidth / 2 + wOffsetLeft);
                newTop = Math.floor(wHeight / 2 - eHeight / 2);

                e.element.parent().css({top: newTop, left: newLeft});
            }
        }
    }

})(jQuery);

var sex = {
    api: {
        site: "/api/sexspider/siteapi",
        list: "/api/sexspider/listapi/{0}/{1}",
        image: "/api/sexspider/imageapi/{0}?url={1}"
    }
};