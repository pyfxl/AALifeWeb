<%@ Page Title="" Language="C#" MasterPageFile="~/manage/MasterPage.master" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="manage_Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>后台管理 | 用户列表</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="row">
        <div class="col-xs-12">
            <form class="form-inline">
                <label>日期</label>
                <div class="form-group">
                    <input id="start" value="" />
                    -
                    <input id="end" value="" />
                </div>
                <button class="k-button" id="get">查询</button>
            </form>
        </div><!-- /.col -->
    </div><!-- /.row -->
    <div class="space-2"></div>
    <div class="row">
        <div class="col-xs-12">
            <div id="grid"></div>
        </div><!-- /.col -->
    </div><!-- /.row -->
    
    <script>
        $(document).ready(function () {            
            var grid = $("#grid").kendoGrid({
                dataSource: {
                    transport: {
                        read: {
                            url: "/api/UserTable/GetUserTable.aspx",
                            dataType: "json"
                        }
                    },
                    pageSize: 60,
                    serverPaging: false,
                    schema: {
                        model: {
                            id: "UserID",
                            fields: {
                                UserID: { type: "number" },
                                UserName: { type: "string" },
                                UserPassword: { type: "string" },
                                UserNickName: { type: "string" },
                                UserImage: { type: "string" },
                                UserPhone: { type: "string" },
                                UserEmail: { type: "string" },
                                UserTheme: { type: "string" },
                                UserLevel: { type: "number" },
                                UserFrom: { type: "string" },
                                ModifyDate: { type: "date" },
                                CreateDate: { type: "date" },
                                UserCity: { type: "string" },
                                UserMoney: { type: "number" },
                                UserWorkDay: { type: "string" },
                                UserFunction: { type: "string" },
                                CategoryRate: { type: "number" },
                                Synchronize: { type: "number" },
                                MoneyStart: { type: "number" },
                                IsUpdate: { type: "number" }
                            }
                        }
                    }
                },
                height: 500,
                navigatable: true,
                resizable: true,
                filterable: true,
                sortable: true,
                groupable: false,
                selectable: "multiple",
                pageable: {
                    refresh: true,
                    pageSizes: true,
                    buttonCount: 10
                },
                columnMenu: {
                    columns: false
                },
                toolbar: ["create", "save", "cancel"],
                columns: [
                    {
                        field: "UserID",
                        title: "编号",
                        width: 70
                    },
                    {
                        field: "UserName",
                        title: "用户名",
                        width: 90
                    },
                    {
                        field: "UserPassword",
                        title: "密码",
                        width: 90
                    },
                    {
                        field: "UserNickName",
                        title: "昵称",
                        width: 90
                    },
                    {
                        template: "<div class='user'><img src='#if(UserImage.indexOf('http')){#/Images/Users/#:UserImage##}else{##:UserImage##}#'></div>",
                        field: "UserImage",
                        title: "头像",
                        width: 90
                    },
                    {
                        field: "UserPhone",
                        title: "手机",
                        width: 100
                    },
                    {
                        field: "UserEmail",
                        title: "邮箱",
                        width: 150
                    },
                    {
                        field: "UserTheme",
                        title: "样式",
                        width: 70
                    },
                    {
                        field: "UserLevel",
                        title: "等级",
                        width: 50
                    },
                    {
                        field: "UserFrom",
                        title: "来自",
                        width: 100
                    },
                    {
                        field: "UserCity",
                        title: "城市",
                        width: 50
                    },
                    {
                        field: "CreateDate",
                        title: "注册日期",
                        width: 150,
                        format: "{0:yyyy/MM/dd HH:mm:ss}"
                    },
                    {
                        field: "ModifyDate",
                        title: "修改日期",
                        width: 150,
                        format: "{0:yyyy/MM/dd HH:mm:ss}"
                    },
                    {
                        field: "UserWorkDay",
                        title: "工作日",
                        width: 50
                    },
                    {
                        field: "CategoryRate",
                        title: "预警率",
                        width: 50
                    },
                    {
                        field: "UserMoney",
                        title: "钱包",
                        width: 100
                    },
                    {
                        field: "MoneyStart",
                        title: "开始金额",
                        width: 100
                    },
                    {
                        field: "Synchronize",
                        title: "同步否",
                        width: 50
                    },
                    {
                        field: "IsUpdate",
                        title: "更新否",
                        width: 50
                    },
                    {
                        field: "UserFunction",
                        title: "功能",
                        width: 100
                    }
                ]
            });

            var getUserImage = function (url) {
                console.log(url);
            }

            function startChange() {
                var startDate = start.value(),
                endDate = end.value();

                if (startDate) {
                    startDate = new Date(startDate);
                    startDate.setDate(startDate.getDate());
                    end.min(startDate);
                } else if (endDate) {
                    start.max(new Date(endDate));
                } else {
                    endDate = new Date();
                    start.max(endDate);
                    end.min(endDate);
                }
            }

            function endChange() {
                var endDate = end.value(),
                startDate = start.value();

                if (endDate) {
                    endDate = new Date(endDate);
                    endDate.setDate(endDate.getDate());
                    start.max(endDate);
                } else if (startDate) {
                    end.min(new Date(startDate));
                } else {
                    endDate = new Date();
                    start.max(endDate);
                    end.min(endDate);
                }
            }

            var start = $("#start").kendoDatePicker({
                change: startChange,
                value: moment().format("YYYY-MM-DD"),
                format: "yyyy/MM/dd"
            }).data("kendoDatePicker");

            var end = $("#end").kendoDatePicker({
                change: endChange,
                value: moment().add(1, "months").format("YYYY-MM-DD"),
                format: "yyyy/MM/dd"
            }).data("kendoDatePicker");

        });
    </script>
</asp:Content>

