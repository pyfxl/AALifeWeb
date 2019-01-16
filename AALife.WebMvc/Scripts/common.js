//常量
$.const = {
    webapi: {
        item: "/api/v1/itemapi",
        item_id: "/api/v1/itemapi/{0}",
        items: "/api/v1/itemsapi",
        categorytype: "/api/v1/categorytypeapi/{0}",
        zhuanti: "/api/v1/zhuantiapi/{0}",
        card: "/api/v1/cardapi/{0}"
    },
    data: {
        itemtype: { "zc": "支出", "sr": "收入", "jc": "借出", "hr": "还入", "jr": "借入", "hc": "还出" },
        regiontype: { "": "", "d": "每日", "w": "每周", "m": "每月", "j": "每季", "y": "每年", "b": "工作日" }
    }
};

//region插件
(function ($) {
    //变量
    var $startDate, $endDate, $type, $item;

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
        var n = 0;
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
        init: function (startDate, endDate, type, date, startd, endd, item, callback) {
            $startDate = $(startDate);
            $endDate = $(endDate);
            $type = type;
            $item = item;
            startd = startd || date;
            endd = endd || _enddate($type, startd);
            let days = _days($type, startd, endd);
            $startDate.datepicker("update", startd);
            $endDate.datepicker("update", endd);
            callback(_getdata($type, startd, days, $item));
        },
        //重新加载，通过开始日期和结束日期
        reload: function (startd, endd, callback) {
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
