﻿/*按钮下拉控件*/
;(function () {
    var kendo = window.kendo,
        ui = kendo.ui,
        Widget = ui.Widget,
        proxy = $.proxy,

        startDate = endDate = today_date(),
        b_year_sub = ["2012年", "2013年", "2014年", "2015年", "2016年", "2017年", "2018年", "2019年", "2020年", "2021年", "2022年"],
        b_quarter_sub = ["第1季", "第2季", "第3季", "第4季"],
        b_month_sub = ["1月", "2月", "3月", "4月", "5月", "6月", "7月", "8月", "9月", "10月", "11月", "12月"],
        b_week_sub = ["上周", "下周"],
        b_day_sub = ["前天", "后天"],

        ACTIVE = 'k-state-active',
        CHANGE = 'change',
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

            //触发
            $('#' + that.options.defaultCode).trigger(CLICK);
        },
        options: {
            name: "ButtomDown",
            autoBind: true,
            template: "",
            defaultCode: "b_day",
            callback: null
        },
        refresh: function () {
            var that = this,
                view = that.dataSource.view(),
                html = kendo.render(that.template, view);

            that.element.html(html);
        },
        current: function () {
            return this.element.find('.k-state-active');
        },
        value: function () {
            var that = this;
            return that.options.defaultCode;
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

            //子类状态
            var idx = 0;

            var code = that.options.defaultCode = e.currentTarget.id;
            switch (code) {
                case "b_all":
                    that.startDate = "";
                    that.endDate = "";
                    break;
                case "b_year":
                    idx = getYear(startDate) - 2012;
                    that.startDate = year_start();
                    that.endDate = year_end();
                    break;
                case "b_quarter":
                    idx = getQuarter(startDate) - 1;
                    that.startDate = quarter_start();
                    that.endDate = quarter_end();
                    break;
                case "b_month":
                    idx = getMonth(startDate)
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

            that.current().find("li>span").removeClass("active");
            that.current().find("li>span").eq(idx).addClass("active");

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
            var code = that.options.defaultCode = currTarget.attr("ref");
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
            content: '<li class="k-button">' +
                        '<span class="k-link #if(data.sub){#f-link#}#" role="button" id="#= data.code #">#= data.title #</span>' +
                        '# if (data.sub) {#' +
                        '<span class="k-link s-link dropdown-toggle" data-toggle="dropdown" aria-expanded="false">' +
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

/*搜索栏插件*/
; (function ($) {
    // shorten references to variables. this is better for uglification
    var kendo = window.kendo,
        ui = kendo.ui,
        Widget = ui.Widget,
        proxy = $.proxy,
        grid = null

    var SearchBar = Widget.extend({
        // initialization code goes here
        init: function (element, options) {
            var that = this;

            // base call to initialize widget
            Widget.fn.init.call(that, element, options);

            // set class name
            that.element.addClass(that.options.name.toLowerCase());

            // 加载模板
            that._create();
            
            // 按钮事件
            that.element.on("click", "button", proxy(that._click, that));

            // 触发
            that.element.find("button").trigger("click");
        },
        options: {
            // the name is what it will appear as off the kendo namespace(i.e. kendo.ui.MyWidget).
            // The jQuery plugin would be jQuery.fn.kendoMyWidget.
            name: "SearchBar",
            // other options go here
            grid: null
        },
        _create: function () {
            var that = this;
            var hasSearchBar = false;

            // 指定grid
            that.grid = $(that.options.grid).data("kendoGrid");

            // 输出组件
            $.each(that.grid.columns, function (i, d) {
                if (d.searchbar) {
                    var searchType = $.isPlainObject(d.searchbar) ? d.searchbar.type : d.searchbar;
                    hasSearchBar = true;
                    switch (searchType) {
                        case "date":
                            that.element.append('<div class="form-group"><label for="' + d.field + '">' + d.title + '</label><input name="' + d.field + '"/></div>');
                            $("input[name=" + d.field + "]").kendoDatePicker();
                            break;
                        case "daterange":
                            var def = d.searchbar.default ? d.searchbar.default : "";
                            that.element.append('<div class="form-group"><label for="' + d.field + '">' + d.title + '</label><div class="input-group"><input name="' + d.field + '" value="' + def + '"/> - <input name="' + d.field + '" value="' + def + '"/></div></div>');
                            $("input[name=" + d.field + "]").kendoDatePicker();
                            break;
                        case "number":
                            that.element.append('<div class="form-group"><label for="' + d.field + '">' + d.title + '</label><input class="k-textbox" name="' + d.field + '"/></div>');
                            break;
                        case "numberrange":
                            that.element.append('<div class="form-group"><label for="' + d.field + '">' + d.title + '</label><div class="input-group"><input class="k-textbox" name="' + d.field + '"/> - <input class="k-textbox" name="' + d.field + '"/></div></div>');
                            break;
                        case "dropdown":
                            that.element.append('<div class="form-group"><label for="' + d.field + '">' + d.title + '</label><input name="' + d.field + '"/></div>');
                            $("input[name=" + d.field + "]").kendoDropDownList({
                                optionLabel: "请选择",
                                dataTextField: "text",
                                dataValueField: "value",
                                dataSource: d.values
                            });
                            break;
                        case "multeselect":
                            that.element.append('<div class="form-group"><label for="' + d.field + '">' + d.title + '</label><div class="input-group"><select name="' + d.field + '"></select></div></div>');
                            $("select[name=" + d.field + "]").kendoMultiSelect({
                                optionLabel: "请选择",
                                dataTextField: "text",
                                dataValueField: "value",
                                dataSource: d.values
                            });
                            break;
                        case "autocomplete":
                            that.element.append('<div class="form-group"><label for="' + d.field + '">' + d.title + '</label><input name="' + d.field + '"/></div>');
                            $("input[name=" + d.field + "]").kendoAutoComplete({
                                dataSource: {
                                    serverFiltering: true,
                                    transport: {
                                        read: {
                                            url: d.searchbar.url,
                                            dataType: "json"
                                        },
                                        parameterMap: function (options, operation) {
                                            return { term: options.filter.filters[0].value };
                                        },
                                        //parameterMap: function (options, operation) {
                                        //    return { term: options.filter.filters.length > 0 ? options.filter.filters[0].value : "" };
                                        //}
                                    },
                                    requestStart: function (e) {
                                        if (e.sender.filter() == undefined || e.sender.filter().filters.length == 0) {
                                            e.preventDefault();
                                        }
                                    }
                                },
                                dataTextField: "text",
                                filter: "startswith",
                                minLength: 2,
                                placeholder: "请输入"
                            });
                            break;
                        case "comboboxin":
                        case "combobox":
                            var filter0;
                            that.element.append('<div class="form-group"><label for="' + d.field + '">' + d.title + '</label><input name="' + d.field + '"/></div>');
                            $("input[name=" + d.field + "]").kendoComboBox({
                                dataSource: {
                                    serverFiltering: true,
                                    transport: {
                                        read: {
                                            url: d.searchbar.url,
                                            dataType: "json"
                                        },
                                        parameterMap: function (options, operation) {
                                            return { term: options.filter.filters[0].value };
                                        }
                                    },
                                    //requestStart: function (e) {
                                    //    if (!e.sender.filter() || !e.sender.filter().filters.length > 0)
                                    //        e.preventDefault();
                                    //}
                                },
                                filtering: function (e) {
                                    if (e.filter == undefined) {
                                        e.filter = filter0;
                                        e.sender.dataSource.filter(filter0);
                                        e.preventDefault();
                                    } else {
                                        filter0 = e.filter;
                                    }
                                    if ($.trim(e.filter.value) == "") {
                                        e.preventDefault();
                                    }
                                },
                                valuePrimitive: true,
                                autoBind: false,
                                minLength: 2,
                                filter: "contains",
                                placeholder: "请输入",
                                dataTextField: "text",
                                dataValueField: "value"
                            });
                            break;
                        default:
                            that.element.append('<div class="form-group"><label for="' + d.field + '">' + d.title + '</label><input class="k-textbox" name="' + d.field + '"/></div>');
                            break;
                    }
                }
            });

            // 输出搜索按钮
            if (hasSearchBar) {
                that.element.append('<button class="k-button k-primary">搜索</button>');
            }
        },
        _click: function (e) {
            var that = this;

            // 保存过滤配置
            var filters = [];

            // 按类型组装
            //that.element.find("input").each(function (i, d) {
            //    var element = $(d);
            //    var type = element.data("type");
            //    var value = element.val();
            //    var name = d.name;
            //    var operate = element.data("operate");
            //    switch (type) {
            //        case "date":
            //            if (value) {
            //                filters.push({ field: name, operator: "lte", value: value + " 23:59:59" });
            //                filters.push({ field: name, operator: "gte", value: value + " 00:00:00" });
            //            }
            //            break;
            //        case "daterange":
            //            if (value) {
            //                if (operate == "gte")
            //                    filters.push({ field: name, operator: operate, value: value + " 00:00:00" });
            //                else
            //                    filters.push({ field: name, operator: operate, value: value + " 23:59:59" });
            //            }
            //            break;
            //        case "number":
            //            if (value)
            //                filters.push({ field: name, operator: "eq", value: value });
            //            break;
            //        case "numberrange":
            //            if (value)
            //                filters.push({ field: name, operator: operate, value: value });
            //            break;
            //        case "dropdown":
            //            if (value)
            //                filters.push({ field: name, operator: "eq", value: value });
            //            break;
            //        case "multeselect":
            //            if (value)
            //                filters.push({ field: name, operator: "eq", value: value });
            //            break;
            //        default:
            //            if (value)
            //                filters.push({ field: name, operator: "startswith", value: value });
            //            break;
            //    }
            //});

            // 输出组件
            $.each(that.grid.columns, function (i, d) {
                if (d.searchbar) {
                    var searchType = $.isPlainObject(d.searchbar) ? d.searchbar.type : d.searchbar;
                    switch (searchType) {
                        case "date":
                            var field = $(that.element.find("input[name=" + d.field + "]"));
                            var v0 = $(field).val();
                            if (v0) {
                                filters.push({ field: d.field, operator: "gte", value: v0 + " 00:00:00" });
                                filters.push({ field: d.field, operator: "lte", value: v0 + " 23:59:59" });
                            }
                            break;
                        case "daterange":
                            var field = $(that.element.find("input[name=" + d.field + "]"));
                            var v0 = $(field[0]).val();
                            var v1 = $(field[1]).val();
                            if (v0)
                                filters.push({ field: d.field, operator: "gte", value: v0 + " 00:00:00" });
                            if (v1)
                                filters.push({ field: d.field, operator: "lte", value: v1 + " 23:59:59" });
                            break;
                        case "number":
                        case "dropdown":
                            var field = $(that.element.find("input[name=" + d.field + "]"));
                            var v0 = $(field).val();
                            if (v0)
                                filters.push({ field: d.field, operator: "eq", value: v0 });
                            break;
                        case "numberrange":
                            var field = $(that.element.find("input[name=" + d.field + "]"));
                            var v0 = $(field[0]).val();
                            var v1 = $(field[1]).val();
                            if (v0)
                                filters.push({ field: d.field, operator: "gte", value: v0 });
                            if (v1)
                                filters.push({ field: d.field, operator: "lte", value: v1 });
                            break;
                        case "multeselect":
                            var field = $(that.element.find("select[name=" + d.field + "]"));
                            var v0 = $(field).val();
                            if (v0.length > 0) {
                                var fit = [];
                                $.each(v0, function (i0, d0) {
                                    fit.push({ field: d.field, operator: "eq", value: d0 });
                                });
                                filters.push({ logic: "or", filters: fit });
                            }
                            break;
                        case "comboboxin":
                            var field = $(that.element.find("input[name=" + d.field + "]"));
                            var v0 = $(field).val();
                            if (v0)
                                filters.push({ field: d.field, operator: "in", value: v0 });
                            break;
                        case "combobox":
                            var field = $(that.element.find("input[name=" + d.field + "]"));
                            var v0 = $(field).val();
                            if (v0)
                                filters.push({ field: d.field, operator: "eq", value: v0 });
                            break;
                        default:
                            var field = $(that.element.find("input[name=" + d.field + "]"));
                            var v0 = $(field).val();
                            if (v0)
                                filters.push({ field: d.field, operator: "startswith", value: v0 });
                            break;
                    }
                }
            });

            // 触发
            that.grid.dataSource.filter(filters);
        }
    });

    ui.plugin(SearchBar);
})(jQuery);