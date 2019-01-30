
//查询对象
var query = { startDate: today_date_min(), endDate: today_date_max(), keyWords: "", userId: userInfo.Id };

var grid_selector = "#jqGrid";
var pager_selector = "#jqGridPager";

var parent_column = $(grid_selector).closest('[class*="col-"]');
//resize to fit page size
$(window).on('resize.jqGrid', function () {
    $(grid_selector).jqGrid('setGridWidth', parent_column.width());
});

$(document).ready(function () {
    $(grid_selector).jqGrid($.extend(true, {}, $.const.jgrid, {
        url: $.const.webapi.item,
        editurl: $.const.webapi.item,
        postData: query,
        colModel: [
            {
                label: "主键",
                name: 'Id',
                hidden: true,
                width: 70,
                editable: true,
                key: true
            },
            {
                label: "用户",
                name: 'UserId',
                hidden: true,
                width: 70,
                editable: true,
                editoptions: {
                    defaultValue: userInfo.Id
                }
            },
            {
                label: "分类",
                name: 'ItemType',
                jsonmap: 'ItemTypeName',
                align: 'center',
                width: 70,
                editable: true, // must set editable to true if you want to make the field editable
                edittype: "select",
                editoptions: {
                    value: $.const.data.itemtype
                }
            },
            {
                label: "固定",
                name: 'RegionType',
                jsonmap: 'RegionName',
                align: 'center',
                width: 70,
                editable: true,
                edittype: "select",
                editoptions: {
                    value: $.const.data.regiontype,
                    dataEvents: [
                        {
                            type: 'change', data: {}, fn: function (e) {
                                let curr = $(e.currentTarget);
                                let rowid = curr[0].id.split('_')[0];
                                let element = String.format("#{0}_ItemBuyDate", rowid);
                                let sel = curr.val();
                                if (sel) {
                                    buildRangeDate(element, rowid);
                                } else {
                                    $(element).data('daterangepicker').remove();
                                    $(element).datepicker($.extend(true, {}, $.const.datepicker));
                                }
                            }
                        }
                    ]
                }
            },
            {
                label: "固定Id",
                name: 'RegionId',
                hidden: true,
                width: 70,
                editable: true
            },
            {
                label: "商品名称",
                name: 'ItemName',
                width: 120,
                editable: true,
                editrules: {
                    required: true
                },
                editoptions: {
                    size: 20,
                    maxlength: 20,
                    dataInit: function (element) {
                        $(element).autocomplete({
                            minLength: 0,
                            source: String.format($.const.webapi.itemnames, userInfo.Id)
                        });
                        $(element).on("click", function () {
                            $(this).autocomplete('search', $(this).val());
                        });
                    }
                }
            },
            {
                label: "商品类别",
                name: 'CategoryTypeId',
                jsonmap: 'CategoryTypeName',
                align: 'center',
                width: 120,
                editable: true,
                edittype: 'select',
                editoptions: {
                    dataUrl: String.format($.const.webapi.categorytype, userInfo.Id),
                    buildSelect: function (data) {
                        let _data = JSON.parse(data);
                        let result = $("<select id='category' name='category' />");
                        $.each(_data.rows, function (i, d) {
                            result.append(String.format("<option value='{0}'>{1}</option>", d.CategoryTypeId, d.CategoryTypeName));
                        });
                        return result[0].outerHTML;
                    }
                }
            },
            {
                label: "价格",
                name: 'ItemPrice',
                align: 'right',
                formatter: 'currency',
                width: 100,
                summaryTpl: "<b>{0}</b>", // set the summary template to show the group summary
                summaryType: "sum", // set the formula to calculate the summary type
                editable: true,
                editrules: {
                    required: true,
                    number: true
                }
            },
            {
                label: "日期",
                name: 'ItemBuyDate',
                align: 'center',
                width: 100,
                formatter: 'date',
                datefmt: 'yyyy-mm-dd',
                editable: true,
                editrules: {
                    required: true,
                    date: true
                },
                editoptions: {
                    // dataInit is the client-side event that fires upon initializing the toolbar search field for a column
                    // use it to place a third party control to customize the toolbar
                    dataInit: function (element) {
                        $(element).datepicker($.extend(true, {}, $.const.datepicker));
                        $(element).datepicker('setDate', moment().format($.const.date.format));
                    }
                }
            },
            {
                label: "日期开始",
                name: 'ItemBuyDateStart',
                hidden: true,
                align: 'center',
                width: 100,
                formatter: 'date',
                datefmt: 'yyyy-mm-dd',
                editable: true
            },
            {
                label: "日期结束",
                name: 'ItemBuyDateEnd',
                hidden: true,
                align: 'center',
                width: 100,
                formatter: 'date',
                datefmt: 'yyyy-mm-dd',
                editable: true
            },
            {
                label: "专题",
                name: 'ZhuanTiId',
                jsonmap: 'ZhuanTiName',
                align: 'center',
                width: 100,
                editable: true,
                edittype: 'select',
                editoptions: {
                    dataUrl: String.format($.const.webapi.zhuanti, userInfo.Id),
                    buildSelect: function (data) {
                        let _data = JSON.parse(data);
                        let result = $("<select id='zhuanti' name='zhuanti' />");
                        result.append("<option value=''>---</option>");
                        $.each(_data.rows, function (i, d) {
                            result.append(String.format("<option value='{0}'>{1}</option>", d.ZhuanTiId, d.ZhuanTiName));
                        });
                        return result[0].outerHTML;
                    }
                }
            },
            {
                label: "钱包",
                name: 'CardId',
                jsonmap: 'CardName',
                align: 'center',
                width: 100,
                editable: true,
                edittype: 'select',
                editoptions: {
                    dataUrl: String.format($.const.webapi.card, userInfo.Id),
                    buildSelect: function (data) {
                        let _data = JSON.parse(data);
                        let result = $("<select id='card' name='card' />");
                        $.each(_data.rows, function (i, d) {
                            result.append(String.format("<option value='{0}'>{1}</option>", d.CardId, d.CardName));
                        });
                        return result[0].outerHTML;
                    }
                }
            },
            {
                label: "推荐",
                name: 'Recommend',
                align: 'center',
                width: 70,
                editable: true,
                edittype: 'checkbox',
                editoptions: {
                    value: "1:0"
                },
                formatter: "checkbox"
            },
            {
                label: "操作",
                name: "actions",
                align: 'center',
                width: 70,
                sortable: false,
                formatter: "actions",
                formatoptions: {
                    keys: true,
                    mtype: 'PUT', //更新用的
                    editOptions: {},
                    addOptions: {},
                    delOptions: {
                        mtype: 'DELETE',
                        onclickSubmit: function (options, rowid) {
                            options.url = String.format($.const.webapi.item_id, rowid);
                        }
                    },
                    onSuccess: function (response) {
                        jQuery(grid_selector).trigger("reloadGrid");
                    }
                }
            }
        ],
        sortname: 'ItemBuyDate',
        sortorder: 'desc',
        rownumbers: false,
        loadonce: false,
        scroll: 1,
        rowNum: $.const.UserSettings.PageNumber || $.const.SiteSettings.PageNumber,
        grouping: false,
        groupingView: {
            groupField: ["ItemBuyDate"],
            groupColumnShow: [true],
            groupText: ["<b>{0}</b>"],
            groupOrder: ["desc"],
            groupSummary: [true],
            groupCollapse: false,
            groupDataSorted: true 
        },
        onSelectRow: function (rowid, status) {
            $(String.format("#{0}_ItemPrice", rowid)).attr("autocomplete", "off");
            $(String.format("#{0}_ItemBuyDate", rowid)).attr("autocomplete", "off");

            let region = "RegionType".jtv(rowid);
            if (!region) return true;

            let element = String.format("#{0}_ItemBuyDate", rowid);
            buildRangeDate(element, rowid);
        },
        loadComplete: function (data) {
            let item = $(grid_selector).jqGrid('getGridParam', 'userData');
            setTotal(item);
        }
    }));

    var selfirst = true;
    $.extend(true, $.jgrid.inlineEdit, {
        //successfunc: function (response) {
        //    jQuery(grid_selector).trigger("reloadGrid");
        //},
        //beforeSaveRow: function (options, rowid) {            
        //},
        beforeSubmitRow: function (options, rowid) {
            //if (options.extraparam.oper != 'add') return true;

            let region = "RegionType".jtv(rowid);
            if (!region) return true;
            if (selfirst) {
                let item = {
                    ItemTypeName: "ItemType".jsv(rowid),
                    ItemName: "ItemName".jtv(rowid),
                    CategoryTypeName: "CategoryTypeId".jsv(rowid),
                    ItemPrice: "ItemPrice".jtv(rowid),
                    ItemBuyDate: "ItemBuyDate".jtv(rowid),
                    ZhuanTiName: "ZhuanTiId".jsv(rowid),
                    CardName: "CardId".jsv(rowid)
                };

                let date = "ItemBuyDate".jtv(rowid);
                let startd = "ItemBuyDateStart".jtv(rowid);
                let endd = "ItemBuyDateEnd".jtv(rowid);

                //验证区间是否选择
                if (!startd || !endd) {
                    $.jgrid.info_dialog("错误", "日期区间：此字段必需", "关闭", {
                        styleUI: $.const.jgrid.styleUI
                    });
                    return false;
                }

                //显示区间列表
                $("#region-modal").modal("show");

                $.fn.region.init(region, date, startd, endd, item, function (data) {
                    $("#region-tbody").empty();
                    $("#region-tbody-tmpl").tmpl(data).appendTo("#region-tbody");
                });

                $("#btn-region").on("click", function () {
                    selfirst = false;
                    $(grid_selector).jqGrid('saveRow', rowid, options);
                    $("#region-modal").modal("hide");
                });

                return false;
            }

            selfirst = true;
            return true; // return false break submiting
        }
    });

    $(window).triggerHandler('resize.jqGrid');//trigger window resize to make the grid get the correct size

    //$("#select-day").trigger("change");
    $("#start-date").datepicker("update", today_date());
    $("#end-date").datepicker("update", today_date());
});

//设置底部总计
function setTotal(item) {
    $("#shourucount").html(item.ShouRuCount);
    $("#shouruamount").html(item.ShouRuAmount);
    $("#zhichucount").html(item.ZhiChuCount);
    $("#zhichuamount").html(item.ZhiChuAmount);
    $("#jiecunamount").html(item.JieCunAmount);
    $("#qianhuanamount").html(item.QianHuanAmount);
    $("#weihuanamount").html(item.WeiHuanAmount);
}

//创建日期多选
function buildRangeDate(element, rowid) {
    //var element = String.format("#{0}_ItemBuyDate", rowid);
    let startele = $(String.format("#{0}_ItemBuyDateStart", rowid));
    let endele = $(String.format("#{0}_ItemBuyDateEnd", rowid));
    $(element).datepicker("destroy");
    $(element).daterangepicker({
        startDate: startele.val() || moment(),
        endDate: endele.val() || moment(),
        locale: $.daterangepicker.locale,
        opens: 'center',
        autoUpdateInput: false
    }, function (start, end, label) {
        $(element).focus();
        startele.val(moment(start).format($.const.date.format));
        endele.val(moment(end).format($.const.date.format));
    });
    $(element).on('apply.daterangepicker', function (ev, picker) {
        $(this).val(picker.startDate.format($.const.date.format));
    });
}
