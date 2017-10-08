var moment_format = "YYYY-MM-DD";

//今天日期
var today_date = function () {
    return moment().format(moment_format);
}

//前一天日期
var prev_date = function (d) {
    return moment(d).add(-1, "day").format(moment_format);
}

//后一天日期
var next_date = function (d) {
    return moment(d).add(1, "day").format(moment_format);
}

//最大日期
var max_date = function () {
    return moment("2012-06-01").format(moment_format);
}

//年的第一天
var year_start = function () {
    return moment().startOf("year").format(moment_format);
}

//年的最后一天
var year_end = function () {
    return moment().endOf("year").format(moment_format);
}

//季的第一天
var quarter_start = function () {
    return moment().startOf("quarter").format(moment_format);
}

//季的最后一天
var quarter_end = function () {
    return moment().endOf("quarter").format(moment_format);
}

//月的第一天
var month_start = function () {
    return moment().startOf("month").format(moment_format);
}

//月的最后一天
var month_end = function () {
    return moment().endOf("month").format(moment_format);
}

//周的第一天
var week_start = function () {
    return moment().startOf("week").format(moment_format);
}

//周的最后一天
var week_end = function () {
    return moment().endOf("week").format(moment_format);
}
