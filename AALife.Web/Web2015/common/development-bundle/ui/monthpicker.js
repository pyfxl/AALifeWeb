$(function () {
    $("#datechoose").html(
        '<div class="ui-datepicker ui-widget-content" style="display: block;">' +
        '    <div class="ui-datepicker-header ui-widget-header">' +
        '        <a class="ui-datepicker-prev" title="<上年"><span class="ui-icon ui-icon-circle-triangle-w">&lt;上年</span></a>' +
        '        <a class="ui-datepicker-next" title="下年>"><span class="ui-icon ui-icon-circle-triangle-e">下年&gt;</span></a>' +
        '        <div class="ui-datepicker-title"><span class="ui-datepicker-year">2014</span>&nbsp;年</div>' +
        '    </div>' +
        '    <table class="ui-datepicker-calendar">' +
        '        <tbody>' +
        '            <tr>' +
        '                <td><a class="ui-state-default" data-month="01" href="#">1</a></td>' +
        '                <td><a class="ui-state-default" data-month="02" href="#">2</a></td>' +
        '                <td><a class="ui-state-default" data-month="03" href="#">3</a></td>' +
        '                <td><a class="ui-state-default" data-month="04" href="#">4</a></td>' +
        '            </tr>' +
        '            <tr>' +
        '                <td><a class="ui-state-default" data-month="05" href="#">5</a></td>' +
        '                <td><a class="ui-state-default" data-month="06" href="#">6</a></td>' +
        '                <td><a class="ui-state-default" data-month="07" href="#">7</a></td>' +
        '                <td><a class="ui-state-default" data-month="08" href="#">8</a></td>' +
        '            </tr>' +
        '            <tr>' +
        '                <td><a class="ui-state-default" data-month="09" href="#">9</a></td>' +
        '                <td><a class="ui-state-default" data-month="10" href="#">10</a></td>' +
        '                <td><a class="ui-state-default" data-month="11" href="#">11</a></td>' +
        '                <td><a class="ui-state-default" data-month="12" href="#">12</a></td>' +
        '            </tr>' +
        '        </tbody>' +
        '    </table>' +
        '</div>'
    )
    
    $(".ui-datepicker-prev").hover(function () { $(this).toggleClass("ui-state-hover ui-datepicker-prev-hover"); });
    $(".ui-datepicker-next").hover(function () { $(this).toggleClass("ui-state-hover ui-datepicker-next-hover"); });
    $(".ui-state-default").hover(function () { $(this).toggleClass("ui-state-hover"); });

    $(".ui-datepicker-prev").click(function () { $(".ui-datepicker-year").html(parseInt($(".ui-datepicker-year").html()) - 1); });
    $(".ui-datepicker-next").click(function () { $(".ui-datepicker-year").html(parseInt($(".ui-datepicker-year").html()) + 1); });
    //$(".ui-state-default").click(function () { location.href=(location.href + "?date=" + $(".ui-datepicker-year").html() + "-" + this.getAttribute("data-month") + "-01"); });

    var today = gettoday().split("-");

    $(".ui-state-default").each(function () {
        if (this.getAttribute("data-month") == today[1]) { $(this).addClass("ui-state-highlight ui-state-hover") };
    });

    $(".ui-datepicker-year").html(today[0]);
});