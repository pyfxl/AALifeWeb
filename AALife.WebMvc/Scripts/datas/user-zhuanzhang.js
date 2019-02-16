
$(document).ready(function () {

    var zzGrid = "#zzGrid";

    //专题
    $(zzGrid).jqGrid($.extend(true, {}, $.const.jgrid, {
        url: String.format($.const.webapi.zhuanzhang, $.const.userInfo.Id),
        editurl: String.format($.const.webapi.zhuanzhang, $.const.userInfo.Id),
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
                label: "用户Id",
                name: 'UserId',
                hidden: true,
                width: 70,
                editable: true
            },
            {
                label: "转账Id",
                name: 'ZhuanZhangId',
                hidden: true,
                width: 70,
                editable: true
            },
            {
                label: "转出钱包",
                name: 'ZhuanZhangFrom',
                jsonmap: 'ZhuanZhangFromName',
                align: 'center',
                width: 70,
                editable: true,
                edittype: 'select',
                editoptions: {
                    dataUrl: String.format($.const.webapi.card, $.const.userInfo.Id),
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
                label: "转入钱包",
                name: 'ZhuanZhangTo',
                jsonmap: 'ZhuanZhangToName',
                align: 'center',
                width: 70,
                editable: true,
                edittype: 'select',
                editoptions: {
                    dataUrl: String.format($.const.webapi.card, $.const.userInfo.Id),
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
                label: "日期",
                name: 'ZhuanZhangDate',
                align: 'center',
                width: 70,
                formatter: 'date',
                datefmt: 'yyyy-mm-dd',
                editable: true, // must set editable to true if you want to make the field editable
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
                label: "金额",
                name: 'ZhuanZhangMoney',
                formatter: 'currency',
                align: 'right',
                width: 70,
                editable: true, // must set editable to true if you want to make the field editable
                editrules: {
                    required: true,
                    number: true
                }
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
                            options.url = String.format($.const.webapi.zhuanzhang, rowid);
                        }
                    },
                    onSuccess: function (response) {
                        jQuery(zzGrid).setGridParam({ datatype: 'json', page: 1 }).trigger("reloadGrid");
                        $.notify({
                            icon: 'fa fa-info-circle',
                            message: "保存成功。"
                        });
                    }
                }
            }
        ],
        onSelectRow: function (rowid, status) {
            $(String.format("#{0}_ZhuanZhangDate", rowid)).attr("autocomplete", "off");
        }
    }));

});
