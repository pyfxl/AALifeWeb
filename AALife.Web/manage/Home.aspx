<%@ Page Title="" Language="C#" MasterPageFile="~/manage/MasterPage.master" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="manage_Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>后台管理 | 用户列表</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="grid"></div>
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
                    pageSize: 20,
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
                                UserEmail: { type: "string" },
                                UserTheme: { type: "string" },
                                UserLevel: { type: "number" },
                                ModifyDate: { type: "date" },
                                CreateDate: { type: "date" },
                                UserCity: { type: "string" },
                                UserMoney: { type: "number" },
                                UserWorkDay: { type: "string" },
                                UserFunction: { type: "string" },
                                CategoryRate: { type: "number" },
                                Synchronize: { type: "number" },
                                MoneyStart: { type: "number" },
                                IsUpdate: { type: "number" },
                                UserFromName: { type: "string" }
                            }
                        }
                    }
                },
                height: 450,
                navigatable: true,
                resizable: true,
                filterable: true,
                sortable: true,
                pageable: {
                    refresh: true,
                    pageSizes: true,
                    buttonCount: 5
                },
                columnMenu: {
                    columns: false
                },
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
                        field: "UserImage",
                        title: "头像",
                        width: 90
                    },
                    {
                        field: "UserEmail",
                        title: "邮箱",
                        width: 150
                    },
                    {
                        field: "UserCity",
                        title: "城市",
                        width: 50
                    },
                    {
                        field: "UserFromName",
                        title: "来自",
                        width: 100
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
        });
    </script>
</asp:Content>

