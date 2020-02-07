<%@ Page Title="后台2019 | 用户列表" Language="C#" MasterPageFile="~/Web2018/Manage2019/MasterPage.master" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="AALife.WebMvc.Web2018.Manage2019.Manage_Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="row">
        <div class="col-xs-12">
            <div id="grid"></div>
        </div><!-- /.col -->
    </div><!-- /.row -->
    
    <script>

        $(document).ready(function () {

            //列表数据源
            var dataSource = new kendo.data.DataSource({
                transport: {
                    read: {
                        url: "/api/v1/UserTableDapper",
                        dataType: "json",
                        contentType: "application/json; charset=utf-8",
                        type: "GET",
                        cache: false,
                    },
                    parameterMap: function (options, operation) {
                        kendoui_parameter_format(options);
                        if (operation !== "read" && options.models) {
                            return kendo.stringify(options.models);
                        }
                        return options;
                    }
                },
                pageSize: 30,
                serverPaging: true,
                serverSorting: true,
                serverFiltering: true,
                serverGrouping: true,
                serverAggregates: true,
                aggregate: [
                ],
                batch: true,
                //sort: [{ field: "CreateDate", dir: "desc" }],
                schema: {
                    data: "Data",
                    total: "Total",
                    errors: "Errors",
                    aggregates: "ExtraData",
                    groups: "Group",
                    model: {
                        id: "UserID",
                        fields: {
                            UserID: { type: "number", editable: false, nullable: false, defaultValue: 0 },
                            UserName: {
                                type: "string",
                                validation: {
                                    required: true,
                                    validationMessage: "用户名为必填项",
                                    usernamevalidation: function (input) {
                                        if (input.is("[name='UserName']") && input.val() != "") {
                                            input.attr("data-usernamevalidation-msg", "用户名必须3-20位");
                                            return input.val().length >= 3 && input.val().length <= 20;
                                        }
                                        return true;
                                    }
                                }
                            },
                            UserPassword: { type: "string", validation: { required: true, validationMessage: "密码为必填项" } },
                            UserNickName: { type: "string" },
                            UserImage: { type: "string", defaultValue: "user.gif" },
                            UserPhone: {
                                type: "string",
                                validation: {
                                    userphonevalidation: function (input) {
                                        if (input.is("[name='UserPhone']") && input.val() != "") {
                                            input.attr("data-userphonevalidation-msg", "不是合法的手机号码");
                                            return /\d{11}/.test(input.val());
                                        }
                                        return true;
                                    }
                                }
                            },
                            UserEmail: { type: "string", validation: { email: true, validationMessage: "不是合法的邮件地址" } },
                            UserTheme: { type: "string", defaultValue: "main" },
                            UserLevel: { type: "number", defaultValue: 9, editable: false },
                            UserFrom: { type: "string", defaultValue: "web" },
                            UserFromName: { type: "string" },
                            ModifyDate: { type: "date", editable: false },
                            CreateDate: { type: "date", editable: false },
                            UserCity: { type: "string" },
                            UserMoney: { type: "number" },
                            UserWorkDay: { type: "string", defaultValue: "5" },
                            UserWorkDayName: { type: "string" },
                            UserFunction: { type: "string", editable: false },
                            CategoryRate: { type: "number", validation: { min: 0, max: 100 } },
                            Synchronize: { type: "number", validation: { min: 0, max: 1 } },
                            MoneyStart: { type: "number" },
                            IsUpdate: { type: "number", validation: { min: 0, max: 1 } }
                        }
                    }
                }
            });

            //用户来自数据源
            var dataUserFrom = new kendo.data.DataSource({
                transport: {
                    read: {
                        url: "/api/UserFrom.asmx/GetUserFrom",
                        dataType: "json",
                        contentType: "application/json; charset=utf-8",
                        type: "POST"
                    }
                },
                schema: {
                    data: "d.rows",
                    total: "d.total",
                    model: {
                        fields: {
                            UserFrom: { type: "string" },
                            UserFromName: { type: "string" },
                            Rank: { type: "number" }
                        }
                    }
                }
            });

            //工作日数据源
            var dataWorkDay = new kendo.data.DataSource({
                transport: {
                    read: {
                        url: "/api/WorkDay.asmx/GetWorkDay",
                        dataType: "json",
                        contentType: "application/json; charset=utf-8",
                        type: "POST"
                    }
                },
                schema: {
                    data: "d.rows",
                    total: "d.total",
                    model: {
                        fields: {
                            WorkDay: { type: "string" },
                            WorkDayName: { type: "string" },
                            Rank: { type: "number" }
                        }
                    }
                }
            });

            //列表
            var grid = $("#grid").kendoGrid({
                dataSource: dataSource,
                autoBind: true,
                height: 500,
                navigatable: true,
                editable: "inline",
                resizable: true,
                filterable: true,
                sortable: {
                    mode: "multiple",
                    allowUnsort: true,
                    showIndexes: true
                },
                groupable: true,
                //selectable: "multiple",
                pageable: {
                    pageSizes: [30, 50, 100],
                    buttonCount: 10,
                    messages: {
                        display: "共 {2} 条"
                    }
                },
                columnMenu: false,
                toolbar: ["create"],
                columns: [                    
                    {
                        command: [
                            { name: "edit", text: { edit: " ", cancel: " ", update: " " } },
                            { name: "destroy", text: " " }
                        ],
                        title: "&nbsp;",
                        locked: false,
                        width: 80
                    },
                    {
                        template: "<a href='ItemTable.aspx?userId=#:UserID#'>#:UserID#</a>",
                        field: "UserID",
                        title: "编号",
                        width: 80
                    },
                    {
                        field: "UserName",
                        title: "用户名",
                        width: 100
                    },
                    {
                        field: "UserPassword",
                        title: "密码",
                        width: 100
                    },
                    {
                        field: "UserNickName",
                        title: "昵称",
                        width: 100
                    },
                    {
                        template: "<div class='k-user'><img src='#if(UserImage.indexOf('http')){#/Images/Users/#:UserImage##}else{##:UserImage##}#'></div>",
                        field: "UserImage",
                        title: "头像",
                        width: 100,
                        attributes: { style: "text-align: center" },
                        editor: function (container, options) {
                            if (options.model.UserID == 0) {
                                $("<div class='user'><img src='/Images/Users/user.gif'></div>").appendTo(container);
                                return;
                            }
                            var input = $("<input type='file' />");
                            input.attr("name", options.field);
                            input.appendTo(container);
                            input.kendoUpload({
                                multiple: false,
                                showFileList: false,
                                async: {
                                    saveUrl: '/api/ImageUpload.ashx?userId=' + options.model.UserID,
                                    autoUpload: true
                                },
                                validation: {
                                    allowedExtensions: [".jpg", ".png", ".gif", ".bmp"]
                                },
                                success: function (e) {
                                    //kendo.alert("头像更新成功。");
                                    notification.show("头像更新成功。", "success");
                                    //e.sender.read(query);
                                },
                                error: function (e) {
                                    notification.show("头像更新失败！", "error");
                                }
                            });
                        }
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
                        values: <%= UserTheme %>,
                        field: "UserTheme",
                        title: "样式",
                        width: 90
                    },
                    {
                        field: "UserLevel",
                        title: "等级",
                        width: 80
                    },
                    {
                        values: <%= GetUserFrom() %>,
                        //template: "#= UserFromName #",
                        field: "UserFrom",
                        title: "来自",
                        width: 120
                    },
                    {
                        field: "UserCity",
                        title: "城市",
                        width: 80,
                        filterable: false
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
                        values: <%= GetUserWorkDay() %>,
                        //template: "#= UserWorkDayName #",
                        field: "UserWorkDay",
                        title: "工作日",
                        width: 110
                    },
                    {
                        field: "CategoryRate",
                        title: "预警率",
                        width: 80
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
                        width: 80,
                        filterable: { multi: true }
                    },
                    {
                        field: "IsUpdate",
                        title: "更新否",
                        width: 80,
                        filterable: { multi: true }
                    },
                    {
                        field: "UserFunction",
                        title: "功能",
                        width: 80
                    }
                ]
            });

            resizeGrid();

        });
    </script>
</asp:Content>

