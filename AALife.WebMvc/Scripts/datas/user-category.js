﻿
$(document).ready(function () {

    var catGrid = "#catGrid";

    //类别
    $(catGrid).jqGrid($.extend(true, {}, $.const.jgrid, {
        url: String.format($.const.webapi.categorytype, $.const.userInfo.Id),
        editurl: String.format($.const.webapi.categorytype, $.const.userInfo.Id),
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
                label: "类别Id",
                name: 'CategoryTypeId',
                hidden: true,
                width: 70,
                editable: true
            },
            {
                label: "类别名称",
                name: 'CategoryTypeName',
                align: 'center',
                width: 70,
                editable: true, // must set editable to true if you want to make the field editable
                editrules: {
                    required: true
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
                        mtype: 'DELETE', //删除用rowid
                        onclickSubmit: function (options, rowid) {
                            options.url = String.format($.const.webapi.categorytype, rowid);
                        }
                    },
                    onSuccess: function (response) {
                        jQuery(catGrid).setGridParam({ datatype: 'json', page: 1 }).trigger("reloadGrid");
                        $.notify({
                            icon: 'fa fa-info-circle',
                            message: "保存成功。"
                        });
                    }
                }
            }
        ],
        onSelectRow: function (rowid, status) {
            $(String.format("#{0}_CategoryTypeName", rowid)).attr("autocomplete", "off");
        }
    }));

    $.extend(true, $.jgrid.inlineEdit, {
        beforeSubmitRow: function (options, rowid) {
            return true;
        }
    });

});
