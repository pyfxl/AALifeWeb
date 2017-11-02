<%@ Page Title="后台管理 | 用户列表" Language="C#" MasterPageFile="~/Manage/MasterPage.master" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="Manage_Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="assets/kendo-custom-ui.js"></script>
    <script src="assets/kendo-main.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <span id="notification"></span>
    <div class="row">
        <div class="col-xs-12">
            <form class="form-inline">
                <div class="form-group">
                    <div class="km-widget km-buttongroup k-widget k-button-group k-buttondown" id="buttondown">
                    </div>
                </div>
                <div class="form-group">
                    <label>日期</label>
                    <input id="start" value="" style="width: 120px;" />
                    &minus;
                    <input id="end" value="" style="width: 120px;" />
                </div>
                <div class="form-group">
                    <label>关键字</label>
                    <input class="k-textbox" id="keysearch" value="" placeholder="用户名/密码/昵称" />
                </div>
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

            //查询对象
            var query = { startDate: "", endDate: "", keySearch: "", buttonDown: "" };

            //开始日期
            var start = $("#start").kendoDatePicker({
                change: changeDate,
                value: month_start(),
                format: "yyyy/MM/dd"
            }).data("kendoDatePicker");

            //结束日期
            var end = $("#end").kendoDatePicker({
                change: changeDate,
                value: month_end(),
                format: "yyyy/MM/dd"
            }).data("kendoDatePicker");

            //列表数据源
            var dataSource = new kendo.data.DataSource({
                transport: {
                    read: {
                        url: "/api/UserTable.asmx/GetUserTable",
                        dataType: "json",
                        contentType: "application/json; charset=utf-8",
                        type: "POST",
                        cache: false,
                        data: function () { return query },
                        beforeSend: function (xhr) {
                            xhr.setRequestHeader('Content-Encoding', "gzip");
                        }
                    },
                    create: {
                        url: "/api/UserTable.asmx/AddUserTable",
                        dataType: "json",
                        contentType: "application/json; charset=utf-8",
                        type: "POST"
                    },
                    update: {
                        url: "/api/UserTable.asmx/UpdateUserTable",
                        dataType: "json",
                        contentType: "application/json; charset=utf-8",
                        type: "POST"
                    },
                    destroy: {
                        url: "/api/UserTable.asmx/RemoveUserTable",
                        dataType: "json",
                        contentType: "application/json; charset=utf-8",
                        type: "POST"
                    },
                    parameterMap: function (options, operation) {
                        if (operation !== "read" && options.models) {
                            return kendo.stringify({ models: options.models[0] });
                        }
                        return kendo.stringify({ query: options });
                    }
                },
                pageSize: 30,
                serverPaging: true,
                serverSorting: true,
                batch: true,
                schema: {
                    data: "d.rows",
                    total: "d.total",
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
                    },
                    errors: "d.error"
                },
                requestEnd: function (e) {                    
                    if (e.type !== "read") {
                        if (e.response.d.error) {
                            return;
                        } else {
                            e.sender.read(query);
                        }
                    }
                },
                error: function (e) {
                    notification.show(e.errors, "error");
                    grid.one("dataBinding", function (x) {
                        x.preventDefault();
                    });
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
                autoBind: false,
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
                groupable: false,
                //selectable: "multiple",
                pageable: {
                    pageSizes: true,
                    buttonCount: 10,
                    messages: {
                        display: "共 {2} 条"
                    }
                },
                columnMenu: {
                    columns: false
                },
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
                                    e.sender.read(query);
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
                        field: "UserTheme",
                        title: "样式",
                        width: 90,
                        editor: function (container, options) {
                            var input = $("<input required validationmessage='样式为必选项' />");
                            input.attr("name", options.field);
                            input.appendTo(container);
                            input.kendoDropDownList({
                                dataTextField: "name",
                                dataValueField: "theme",
                                dataSource: dataTheme,
                                animation: false
                            });
                            $('<span class="k-invalid-msg" data-for="' + options.field + '"></span>').appendTo(container);
                        },
                        filterable: { multi: true }
                    },
                    {
                        field: "UserLevel",
                        title: "等级",
                        width: 80,
                        filterable: { multi: true }
                    },
                    {
                        template: "#= UserFromName #",
                        field: "UserFrom",
                        title: "来自",
                        width: 120,
                        editor: function (container, options) {
                            var input = $("<input required validationmessage='来自为必选项' />");
                            input.attr("name", options.field);
                            input.appendTo(container);
                            input.kendoDropDownList({
                                dataTextField: "UserFromName",
                                dataValueField: "UserFrom",
                                dataSource: dataUserFrom,
                                animation: false
                            });
                            $('<span class="k-invalid-msg" data-for="' + options.field + '"></span>').appendTo(container);
                        },
                        filterable: { multi: true }
                    },
                    {
                        field: "UserCity",
                        title: "城市",
                        width: 80,
                        filterable: { multi: true }
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
                        template: "#= UserWorkDayName #",
                        field: "UserWorkDay",
                        title: "工作日",
                        width: 110,
                        editor: function (container, options) {
                            var input = $("<input required validationmessage='工作日为必选项' />");
                            input.attr("name", options.field);
                            input.appendTo(container);
                            input.kendoDropDownList({
                                dataTextField: "WorkDayName",
                                dataValueField: "WorkDay",
                                dataSource: dataWorkDay,
                                animation: false
                            });
                            $('<span class="k-invalid-msg" data-for="' + options.field + '"></span>').appendTo(container);
                        },
                        filterable: { multi: true }
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
            }).data("kendoGrid");

            //自定义按钮组
            var buttonDown = $("#buttondown").kendoButtomDown({
                dataSource: [
                    { title: "全部", code: "b_all" },
                    { title: "本年", code: "b_year", sub: true },
                    { title: "本季", code: "b_quarter", sub: true },
                    { title: "本月", code: "b_month", sub: true },
                    { title: "本周", code: "b_week", sub: true },
                    { title: "本日", code: "b_day", sub: true }
                ],
                callback: function (data) {
                    setDate(data.startDate, data.endDate);
                    query.buttonDown = data.buttonDown;
                    queryList(true);
                }
            }).data("kendoButtomDown");

            //关键字组件
            var keySearch = $("#keysearch").kendoKeySearch({
                minLength: 1,
                done: function (key) {
                    setButtonDown("");
                    setDate("", "");
                    queryList();
                },
                focus: function (e) {
                    if (!e.key) {
                        setQuery();
                        e.data = { "startDate": start.value(), "endDate": end.value(), "buttonDown": query.buttonDown };
                    }
                },
                empty: function (data) {
                    setDate(data.startDate, data.endDate);
                    setButtonDown(data.buttonDown);
                    queryList();
                }
            }).data("kendoKeySearch");

            //提示
            var notification = $("#notification").kendoNotification({
                position: {
                    top: Math.floor($(window).height() / 2.5),
                    left: Math.floor($(window).width() / 2.2),
                    pinning: false
                }
            }).data("kendoNotification");

            //更改日期事件
            function changeDate() {
                setButtonDown("");
                queryList(true);
            }

            //设置日期值
            function setDate(startDate, endDate) {
                start.value(startDate);
                end.value(endDate);
            }

            //关键字清除
            function clearKey() {
                query.keySearch = "";
                keySearch.clear();
            }

            //设置按钮下拉
            function setButtonDown(code) {
                query.buttonDown = code;
                buttonDown.selected(code);
            }

            //设置查询条件
            function setQuery() {
                var _start = start.value();
                if (_start == null) _start = min_date();
                query.startDate = kendo.toString(_start, "yyyy/MM/dd 00:00:00");

                var _end = end.value();
                if (_end == null) _end = max_date();
                query.endDate = kendo.toString(_end, "yyyy/MM/dd 23:59:59");

                query.keySearch = keySearch.value();
            }

            //列表查询
            function queryList(flag) {
                if (flag) {
                    clearKey();
                }
                setQuery();
                dataSource.page(1);
                dataSource.skip(0);
            }

            //初始化
            function init() {
                setButtonDown("b_day");
                setDate(today_date(), today_date());
            }
            
            resizeGrid();
            init();
            queryList();
            navbarActive(0);

        });
    </script>
</asp:Content>

