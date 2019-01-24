var fn_date = (function (window) {

	var self = this;

	self.moment_format = "YYYY-MM-DD";

	self.moment_format_min = "YYYY-MM-DD 00:00:00";

	self.moment_format_max = "YYYY-MM-DD 23:59:59";

	self.moment_format_full = "YYYY-MM-DD HH:mm:ss";

	self.min_date = "1900-01-01";

	self.max_date = "2099-12-31";

	//今天日期
	self.today_date = function () {
		return moment().format(self.moment_format);
	}

	//今天日期
	self.today_date_min = function () {
		return moment().format(self.moment_format_min);
	}

	//今天日期
	self.today_date_max = function () {
		return moment().format(self.moment_format_max);
	}

	//设置日期
	self.set_date_number = function (d, n) {
		return moment(d).add(n, "day").format(self.moment_format);
	}

	//最小日期
	self.get_min_date = function () {
		return moment(self.min_date).format(self.moment_format);
	}

	//最大日期
	self.get_max_date = function () {
		return moment(self.max_date).format(self.moment_format);
	}

	//年的第一天
	self.year_start = function () {
		return moment().startOf("year").format(self.moment_format);
	}

	//年的最后一天
	self.year_end = function () {
		return moment().endOf("year").format(self.moment_format);
	}

	//几年的第一天
	self.year_start_number = function (n) {
		return moment(n, "YYYY").startOf("year").format(self.moment_format);
	}

	//几年的最后一天
	self.year_end_number = function (n) {
		return moment(n, "YYYY").endOf("year").format(self.moment_format);
	}

	//季的第一天
	self.quarter_start = function () {
		return moment().startOf("quarter").format(self.moment_format);
	}

	//季的最后一天
	self.quarter_end = function () {
		return moment().endOf("quarter").format(self.moment_format);
	}

	//几季的第一天
	self.quarter_start_number = function (n) {
		return moment(n, "Q").startOf("quarter").format(self.moment_format);
	}

	//几季的最后一天
	self.quarter_end_number = function (n) {
		return moment(n, "Q").endOf("quarter").format(self.moment_format);
	}

	//月的第一天
	self.month_start = function () {
		return moment().startOf("month").format(self.moment_format);
	}

	//月的最后一天
	self.month_end = function () {
		return moment().endOf("month").format(self.moment_format);
	}

	//几月的第一天
	self.month_start_number = function (n) {
		return moment(n, "MM").startOf("month").format(self.moment_format);
	}

	//几月的最后一天
	self.month_end_number = function (n) {
		return moment(n, "MM").endOf("month").format(self.moment_format);
	}

	//周的第一天
	self.week_start = function () {
		return moment().startOf("week").format(self.moment_format);
	}

	//周的最后一天
	self.week_end = function () {
		return moment().endOf("week").format(self.moment_format);
	}

	//取年份
	self.getYear = function (d) {
		return moment(d || moment()).year();
	}

	//取季度
	self.getQuarter = function (d) {
		return moment(d || moment()).quarter();
	}

	//取月份
	self.getMonth = function (d) {
		return moment(d || moment()).month();
	}

})(window);
