//常量
$.extend($.const, {    
    date: {
        format: "YYYY-MM-DD",
        typestart: { "d": today_date(), "w": week_start(), "m": month_start(), "j": quarter_start(), "y": year_start(), "a": "" },
        typeend: { "d": today_date(), "w": week_end(), "m": month_end(), "j": quarter_end(), "y": year_end(), "a": "" }
    },
    datepicker: {
        autoclose: true,
        todayHighlight: true,
        language: 'zh-CN',
        format: 'yyyy-mm-dd'
    },
    jgrid: {
        url: '',
        mtype: "GET",
        editurl: '',
        datatype: "json",
        styleUI: 'Bootstrap',
        responsive: true,
        altRows: true,
        rownumbers: true,
        width: 780,
        height: 340,
        rowNum: -1,
        loadonce: true,
        ajaxRowOptions: {
            contentType: "application/json"
        },
        serializeRowData: function (postdata) {
            return JSON.stringify(postdata);
        }
    },
    kendotree: {
        dataSource: {
            transport: {
                read: {
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    type: "GET",
                    cache: false,
                    beforeSend: function (xhr) {
                        xhr.setRequestHeader('Content-Encoding', "gzip");
                    }
                },
                update: {
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    type: "PUT",
                    complete: function (xhr, textStatus) {
                        display_kendoui_grid_complete(textStatus);
                    }
                },
                destroy: {
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    type: "DELETE",
                    complete: function (xhr, textStatus) {
                        display_kendoui_grid_complete(textStatus);
                    }
                },
                create: {
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    type: "POST",
                    complete: function (xhr, textStatus) {
                        display_kendoui_grid_complete(textStatus);
                    }
                }
            },
            batch: false
        },
        reorderable: true,
        navigatable: false,
        resizable: true,
        filterable: true,
        sortable: {
            mode: "multiple",
            allowUnsort: true,
            showIndexes: true
        },
        editable: true,
        scrollable: false,
        groupable: false,
        columnMenu: false
    },
    kendolite: {
        dataSource: {
            transport: {
                read: {
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    type: "GET",
                    cache: false,
                    beforeSend: function (xhr) {
                        xhr.setRequestHeader('Content-Encoding', "gzip");
                    }
                },
                update: {
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    type: "PUT",
                    complete: function (xhr, textStatus) {
                        display_kendoui_grid_complete(textStatus);
                    }
                },
                destroy: {
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    type: "DELETE",
                    complete: function (xhr, textStatus) {
                        display_kendoui_grid_complete(textStatus);
                    }
                },
                create: {
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    type: "POST",
                    complete: function (xhr, textStatus) {
                        display_kendoui_grid_complete(textStatus);
                    }
                }
            },
            schema: {
                data: "Data",
                total: "Total",
                errors: "Errors"
            }
        },
        reorderable: true,
        navigatable: false,
        resizable: true,
        filterable: true,
        sortable: {
            mode: "multiple",
            allowUnsort: true,
            showIndexes: true
        },
        scrollable: false,
        groupable: false,
        columnMenu: false
    },
    kendogrid: {
        dataSource: {
            transport: {
                read: {
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    type: "GET",
                    cache: false,
                    beforeSend: function (xhr) {
                        xhr.setRequestHeader('Content-Encoding', "gzip");
                    }
                },
                update: {
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    type: "PUT",
                    complete: function (xhr, textStatus) {
                        display_kendoui_grid_complete(textStatus);
                    }
                },
                destroy: {
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    type: "DELETE",
                    complete: function (xhr, textStatus) {
                        display_kendoui_grid_complete(textStatus);
                    }
                },
                create: {
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    type: "POST",
                    complete: function (xhr, textStatus) {
                        display_kendoui_grid_complete(textStatus);
                    }
                }
            },
            schema: {
                data: "Data",
                total: "Total",
                errors: "Errors",
                groups: "Groups",
                aggregates: "Aggregates"
            },
            batch: true,
            serverPaging: true,
            serverSorting: true,
            serverFiltering: true,
            serverGrouping: true,
            serverAggregates: false,
            pageSize: $.const.default.pagenumber
        },
        reorderable: true,
        navigatable: false,
        resizable: true,
        filterable: true,
        sortable: {
            mode: "multiple",
            allowUnsort: true,
            showIndexes: true
        },
        editable: "inline",
        pageable: { 
            buttonCount: $.const.default.pagebutton,
            pageSizes: $.const.default.pagenumbers,
            messages: {
                display: "共 {2} 条数据"
            }
        },
        groupable: true,
        columnMenu: false
    }
});

$.app = {
    modal: function (element, url) {
        let $ele = $(element);
        $ele.find(".modal-body").load(url);
        $ele.modal("show");
    }
};

//region插件
(function ($) {
    //变量
    var $type = null, $item = null;

    //常量
    var _d = 3,
        _w = 12,
        _m = 12 * 3,
        _y = 12 * 5,
        _format = "YYYY-MM-DD";
    var _types = { "d": "days", "b": "days", "w": "weeks", "m": "months", "j": "quarters", "y": "years" };

    //取两个日期间的天数
    var _days = function (type, startd, endd) {
        return moment(endd).diff(moment(startd), _types[type]);
    };

    //根据类型取最后日期
    var _enddate = function (type, startd) {
        let n = 0;
        switch (type) {
            case "d":
            case "b":
                n = _d;
                break;
            case "w":
                n = _w;
                break;
            case "m":
            case "j":
                n = _m;
            case "y":
                n = _y;
                break;
        }
        return moment(startd).add(n, "months").add(-1, "days").format(_format);
    };

    //生成连续天数
    var _getdata = function (type, date, days, item) {
        let arr = [];
        for (let i = 0; i <= days; i++) {
            let d = moment(date).add(i, _types[type]).format(_format);
            if (!_isworkday(d, item.UserWorkDay)) continue;
            let obj = {
                ItemRow: i + 1,
                ItemTypeName: item.ItemTypeName,
                CategoryTypeName: item.CategoryTypeName,
                ItemName: item.ItemName,
                ItemPrice: item.ItemPrice,
                ItemBuyDate: d,
                ZhuanTiName: item.ZhuanTiName,
                CardName: item.CardName
            };
            arr.push(obj);
        }
        return arr;
    };

    //是否工作日
    var _isworkday = function (date, day) {
        let week = moment(date).format('d');
        switch (day) {
            case 1:
                if (week != 1) return false;
                break;
            case 2:
                if (week > 2 || week == 0) return false;
                break;
            case 3:
                if (week > 3 || week == 0) return false;
                break;
            case 4:
                if (week > 4 || week == 0) return false;
                break;
            case 5:
                if (week > 5 || week == 0) return false;
                break;
            case 6:
                if (week == 0) return false;
                break;
        }

        return true;
    }

    $.fn.region = {
        //初始化
        init: function (type, date, startd, endd, item, callback) {
            $type = type;
            $item = item;
            startd = startd || date;
            endd = endd || _enddate($type, startd);
            let days = _days($type, startd, endd);
            callback(_getdata($type, startd, days, $item));
        }
    }
})(jQuery);

//返回jquery对象
$.jqo = function (field, id) {
    return $("#" + id + "_" + field);
}

//返回jqgrid行文本字段值
String.prototype.jtv = function (id) {
    return $.jqo(this, id).val();
}

//返回jqgrid行下拉字段值
String.prototype.jsv = function (id) {
    return $.jqo(this, id).find("option:selected").text();
}

//kendo grid error
function display_kendoui_grid_error(e) {
    let errors,
        text = e.xhr.responseText;
    if (text) {
        let json = JSON.parse(text);
        errors = json.Errors || json.Message;        
    } else {
        errors = e.errorThrown;
    }
    kendoui_notification(errors, "error");
}

//kendo grid complete
function display_kendoui_grid_complete(textStatus, grid) {
    if (textStatus == "success") {
        kendoui_notification("成功。", "success");
        //grid.dataSource.read();
    }
}

//kendo grid complete
function kendoui_notification(message, type) {    
    if (notification.show) {
        notification.show(message, type);
    } else {
        alert(errors);
    }
}

//kendo datetime format
function parameter_format(options) {
    if (options && options.filter && options.filter.filters && options.filter.filters.count != 0) {
        options.filter.filters.forEach(function (f) {
            if (f.operator && f.value && typeof f.value.getMonth == 'function' && typeof f.value.getMonth() == 'number') {
                f.value = moment(f.value).format("YYYY/MM/DD");
            }
        });
    }
    return options;
}

//调整grid高度
function resizeMain() {
    var div = $("#main");
    var windowHeight = $(window).innerHeight();
    var offsetTop = div.offset().top;
    var paddingBottom = 7;
    var fixHeight = 2;

    var calculatedHeight = windowHeight - offsetTop - paddingBottom - fixHeight;
    div.height(calculatedHeight);
}

//设置头部选中
function navActive(i) {
    var navbar = $(".top-zone .nav > li");
    navbar.removeClass("active");
    navbar.eq(i).addClass("active");
}

jQuery(function ($) {
    // kendo ui
    kendo.culture("zh-CN");
});

//解决kendo日期时区问题
Date.prototype.toISOString = function () {
    return moment(this).format("YYYY-MM-DDTHH:mm:ss");
};

