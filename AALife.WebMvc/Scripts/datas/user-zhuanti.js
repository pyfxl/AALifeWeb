
$(document).ready(function () {

    var ztGrid = "#ztGrid";

    //专题
    $(ztGrid).jqGrid($.extend(true, {}, $.const.jgrid, {
        url: String.format($.const.webapi.zhuanti, userInfo.Id),
        editurl: String.format($.const.webapi.zhuanti, userInfo.Id),
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
                label: "专题Id",
                name: 'ZhuanTiId',
                hidden: true,
                width: 70,
                editable: true
            },
            {
                label: "专题名称",
                name: 'ZhuanTiName',
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
                        mtype: 'DELETE',
                        onclickSubmit: function (options, rowid) {
                            options.url = String.format($.const.webapi.item_id, rowid);
                        }
                    }
                }
            }
        ]
    }));

});
