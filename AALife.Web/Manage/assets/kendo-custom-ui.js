/*按钮下拉控件*/
; (function () {
    var kendo = window.kendo,
        ui = kendo.ui,
        Widget = ui.Widget,
        proxy = $.proxy,

        startDate = endDate = today_date(),
        b_year_sub = ["2012年", "2013年", "2014年", "2015年", "2016年", "2017年", "2018年", "2019年", "2020年"],
        b_quarter_sub = ["第1季", "第2季", "第3季", "第4季"],
        b_month_sub = ["1月", "2月", "3月", "4月", "5月", "6月", "7月", "8月", "9月", "10月", "11月", "12月"],
        b_week_sub = ["上周", "下周"],
        b_day_sub = ["前天", "后天"],

        ACTIVE = 'k-state-active km-state-active',
        CHANGE = "change",
        CLICK = 'click';

    var ButtomDown = Widget.extend({
        init: function (element, options) {
            var that = this;

            //初始化
            kendo.ui.Widget.fn.init.call(that, element, options);

            //加载模板
            that.template = kendo.template(that._templates.content);

            //数据源
            that._dataSource();

            //按钮事件
            that.element.on(CLICK, ".k-link[role='button']", proxy(that._click, that));
            that.element.on(CLICK, ".btn-link", proxy(that._subclick, that));            
        },
        options: {
            name: "ButtomDown",
            autoBind: true,
            template: "",
            callback: null
        },
        refresh: function () {
            var that = this,
                view = that.dataSource.view(),
                html = kendo.render(that.template, view);

            that.element.html(html);
        },
        current: function () {
            return this.element.find('.k-state-active, .km-state-active');
        },
        selected: function (code) {
            this.current().removeClass(ACTIVE);
            if (code != "") {
                var currTarget = $(this.element.find("#" + code).parent());
                currTarget.addClass(ACTIVE);
            }
        },
        _click: function (e) {
            var that = this;
            var currTarget = $(e.currentTarget);

            //设置状态
            that.current().removeClass(ACTIVE);
            currTarget.parent().addClass(ACTIVE);

            var code = e.currentTarget.id;
            switch (code) {
                case "b_all":
                    that.startDate = min_date();
                    that.endDate = max_date();
                    break;
                case "b_year":
                    that.startDate = year_start();
                    that.endDate = year_end();
                    break;
                case "b_quarter":
                    that.startDate = quarter_start();
                    that.endDate = quarter_end();
                    break;
                case "b_month":
                    that.startDate = month_start();
                    that.endDate = month_end();
                    break;
                case "b_week":
                    that.startDate = week_start();
                    that.endDate = week_end();
                    break;
                case "b_day":
                    that.startDate = today_date();
                    that.endDate = today_date();
                    break;
            }

            //回调
            that.options.callback({ "startDate": that.startDate, "endDate": that.endDate, "buttonDown": code });
        },
        _subclick: function (e) {
            var that = this;
            var currTarget = $(e.currentTarget);

            //设置状态
            that.current().removeClass(ACTIVE);
            currTarget.parent().parent().parent().addClass(ACTIVE);

            this.element.find(".active").removeClass("active");
            currTarget.addClass("active");

            var num = currTarget.parent().index() + 1;
            var code = currTarget.attr("ref");
            switch (code) {
                case "b_year":
                    var year = b_year_sub[num - 1].replace(/[^\d]/g, "");
                    that.startDate = year_start_number(year);
                    that.endDate = year_end_number(year);
                    break;
                case "b_quarter":
                    that.startDate = quarter_start_number(num);
                    that.endDate = quarter_end_number(num);
                    break;
                case "b_month":
                    that.startDate = month_start_number(num);
                    that.endDate = month_end_number(num);
                    break;
                case "b_week":
                    if (num == 1) {
                        that.startDate = set_date_number(that.startDate, -7);
                        that.endDate = set_date_number(that.endDate, -7);
                    } else {
                        that.startDate = set_date_number(that.startDate, 7);
                        that.endDate = set_date_number(that.endDate, 7);
                    }
                    break;
                case "b_day":
                    if (num == 1) {
                        that.startDate = set_date_number(that.startDate, -1);
                        that.endDate = that.startDate;
                    } else {
                        that.startDate = set_date_number(that.startDate, 1);
                        that.endDate = that.startDate;
                    }
                    break;
            }

            //回调
            that.options.callback({ "startDate": that.startDate, "endDate": that.endDate, "buttonDown": code });
        },
        _dataSource: function () {
            var that = this;

            //加载数据源
            that.dataSource = kendo.data.DataSource.create(that.options.dataSource);

            //处理数据源
            $.each(that.dataSource.options.data, function (i, item) {
                switch (item.code) {
                    case "b_year":
                        item.subdata = b_year_sub;
                        break;
                    case "b_quarter":
                        item.subdata = b_quarter_sub;
                        break;
                    case "b_month":
                        item.subdata = b_month_sub;
                        break;
                    case "b_week":
                        item.subdata = b_week_sub;
                        break;
                    case "b_day":
                        item.subdata = b_day_sub;
                        break;
                }
            });
            
            //修改事件
            that.dataSource.bind(CHANGE, function () {
                that.refresh();
            });

            //自动绑定
            if (that.options.autoBind) {
                that.dataSource.fetch();
            }
        },
        _templates: {
            //模板
            content: '<li class="k-button km-button">' +
                        '<span class="k-link" role="button" id="#= data.code #">#= data.title #</span>' +
                        '# if (data.sub) {#' +
                        '<span class="k-link dropdown-toggle" data-toggle="dropdown" aria-expanded="false">' +
                            '<span class="k-icon k-i-arrow-60-down"></span>' +
                        '</span>' +
                        '<ul class="dropdown-menu">' +
                            '# for (var i=0,len=data.subdata.length; i<len; i++) {#' +
                            '<li><span class="btn btn-link" role="button" ref="#= data.code #">#= data.subdata[i] #</span></li>' +
                            '# } #' +
                        '</ul>' +
                        '# } #' +
                    '</li>'
        }
    });

    ui.plugin(ButtomDown);

})(jQuery);

/*关键字搜索插件*/
;(function () {
    var kendo = window.kendo,
        ui = kendo.ui,
        Widget = ui.Widget,
        proxy = $.proxy,

        FOCUS = "focus",
        KEYUP = "keyup",
        DONE = "done",
        EMPTY = "empty";
    
    var KeySearch = Widget.extend({
        init: function (element, options) {
            var that = this;

            //初始化
            kendo.ui.Widget.fn.init.call(that, element, options);

            that.element.on(FOCUS, proxy(that._focus, that));
            that.element.on(KEYUP, proxy(that._keyup, that));
        },
        events: [FOCUS, DONE, EMPTY],
        options: {
            name: "KeySearch",
            minLength: 2,
            delay: 1000
        },
        value: function () {
            return this.element.val().trim();
        },
        clear: function () {
            this.element.val("");
        },
        _focus: function () {
            this.trigger(FOCUS, this);
        },
        _keyup: function (e) {
            var that = this;
            clearTimeout(that._typingTimeout);
            that._typingTimeout = setTimeout(function () {
                that.key = $(e.currentTarget).val();
                if (that.key.length < that.options.minLength) {
                    that.trigger(EMPTY, that.data);
                    return;
                }

                that.trigger(DONE, that.key);
            }, that.options.delay);
        }
    });

    ui.plugin(KeySearch);

})(jQuery);
