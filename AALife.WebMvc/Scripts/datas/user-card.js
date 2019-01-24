
$(document).ready(function () {

    var cardGrid = "#cardGrid";

    //钱包
    $(cardGrid).jqGrid($.extend(true, {}, $.const.jgrid, {
        url: String.format($.const.webapi.card, userInfo.Id),
        editurl: String.format($.const.webapi.card, userInfo.Id),
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
                label: "钱包Id",
                name: 'CardId',
                hidden: true,
                width: 70,
                editable: true
            },
            {
                label: "钱包名称",
                name: 'CardName',
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
