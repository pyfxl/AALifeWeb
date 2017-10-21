<%@ Page Title="后台管理 | 用户列表" Language="C#" MasterPageFile="~/Manage/MasterPage.master" AutoEventWireup="true" CodeFile="HomeButtonGroup.aspx.cs" Inherits="Manage_Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <span id="notification"></span>
    <div class="row">
        <div class="col-xs-12">
            <div class="overflow-y">
                <form class="form-inline" style="width: 875px;">
                    <div class="form-group">
                        <ul id="select-period">
                        </ul>
                    </div>
                    <div class="form-group">
                        <button type="button" id="prevBtn">前</button>
                        <button type="button" id="nextBtn">后</button>
                    </div>
                    <div class="form-group">
                        <label>日期</label>
                        <input id="start" value="" style="width: 120px;" />
                        &minus;
                        <input id="end" value="" style="width: 120px;" />
                    </div>
                    <div class="form-group">
                        <label>关键字</label>
                        <input class="k-textbox" id="key" value="" placeholder="用户名/密码/昵称" />
                    </div>
                </form>
            </div>
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
            var query = { startDate:"", endDate:"", key: "" };

            //开始日期
            var start = $("#start").kendoDatePicker({
                change: startChange,
                value: month_start(),
                format: "yyyy/MM/dd"
            }).data("kendoDatePicker");

            //结束日期
            var end = $("#end").kendoDatePicker({
                change: endChange,
                value: month_end(),
                format: "yyyy/MM/dd"
            }).data("kendoDatePicker");

            //开始日期change
            function startChange() {
                setBtnGroup(-1);
                queryList();
            }

            //结束日期change
            function endChange() {
                setBtnGroup(-1);
                queryList();
            }

            //前天button
            var prevBtn = $("#prevBtn").kendoButton({
                enable: false,
                click: function (e) {
                    var date = prev_date(start.value())
                    start.value(date);
                    end.value(date);
                    setBtnGroup(-1);
                    queryList();
                },
                icon: "arrow-w"
            }).data("kendoButton");

            //后天button
            var nextBtn = $("#nextBtn").kendoButton({
                enable: false,
                click: function (e) {
                    //alert(e.event.target.tagName);
                    var date = next_date(start.value());
                    start.value(date);
                    end.value(date);
                    setBtnGroup(-1);
                    queryList();
                },
                icon: "arrow-e"
            }).data("kendoButton");

            //设置按钮组toall
            function setBtnGroup(i) {
                if (i == -1) i = 9;
                btnGroup.select(i);
            }

            //设置按钮状态
            function setBtnStatus(flag) {
                prevBtn.enable(flag);
                nextBtn.enable(flag);
            }

            //按钮组init
            $.each(btnGroupData, function (i, v) {
                $("#select-period").append("<li>" + v + "</li>");
            });

            //按钮组kendo
            var btnGroup = $("#select-period").kendoMobileButtonGroup({
                select: function (e) {
                    setBtnStatus(false);
                    switch (e.index) {
                        case 0://全部
                            start.value(max_date());
                            end.value(today_date());
                            break;
                        case 1://今年
                            start.value(year_start());
                            end.value(year_end());
                            break;
                        case 2://本季
                            start.value(quarter_start());
                            end.value(quarter_end());
                            break;
                        case 3://本月
                            start.value(month_start());
                            end.value(month_end());
                            break;
                        case 4://本周
                            start.value(week_start());
                            end.value(week_end());
                            break;
                        case 5://今日
                            start.value(today_date());
                            end.value(today_date());
                            setBtnStatus(true);
                            break;
                    }
                    queryList();
                },
                index: 3
            }).data("kendoMobileButtonGroup");

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
                        return kendo.stringify(options);
                        //return '{"startDate":"2017-09-01","endDate":"2017-09-30"}';
                        //return '{"startDate":"' + kendo.toString(start.value(), "yyyy/MM/dd 00:00:00") + '","endDate":"' + kendo.toString(end.value(), "yyyy/MM/dd 23:59:59") + '"}';
                    }
                },
                pageSize: 30,
                serverPaging: false,
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
                                            input.attr("data-usernamevalidation-msg", "用户名必须3-10位");
                                            return input.val().length >= 3 && input.val().length < 10;
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
                            UserFrom: { type: "string", editable: false },
                            ModifyDate: { type: "date", editable: false },
                            CreateDate: { type: "date", editable: false },
                            UserCity: { type: "string" },
                            UserMoney: { type: "number" },
                            UserWorkDay: { type: "string", defaultValue: 5 },
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
                sortable: true,
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
                        template: "<div class='user'><img src='#if(UserImage.indexOf('http')){#/Images/Users/#:UserImage##}else{##:UserImage##}#'></div>",
                        field: "UserImage",
                        title: "头像",
                        width: 100,
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
                        field: "UserFrom",
                        title: "来自",
                        width: 120,
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
                        format: "0",
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
                        format: "0",
                        width: 80,
                        filterable: { multi: true }
                    },
                    {
                        field: "IsUpdate",
                        title: "更新否",
                        format: "0",
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

            //查询对象
            function setQuery() {
                query.startDate = kendo.toString(start.value(), "yyyy/MM/dd 00:00:00");
                query.endDate = kendo.toString(end.value(), "yyyy/MM/dd 23:59:59");
                query.key = $("#key").val();
            }

            //列表查询
            function queryList() {
                setQuery();
                clearKey();
                dataSource.read();
            }

            //初始化
            function init() {
                setBtnGroup(5);
                start.value(today_date());
                end.value(today_date());
                setBtnStatus(true);
            }

            //关键字定时器
            var keyTime;

            //关键字查询
            $("#key").keyup(function () {
                if (keyTime != null) {
                    clearTimeout(keyTime);
                }
                keyTime = setTimeout(function () {
                    var str = $("#key").val().trim();
                    if (str.length < 1) return;

                    setBtnGroup(-1);
                    start.value(max_date());
                    end.value(today_date());

                    setQuery();
                    dataSource.read();
                }, 1000);
            });

            //关键字清除
            function clearKey() {
                query.key = "";
                $("#key").val("");
            }

            //提示
            var notification = $("#notification").kendoNotification({
                position: {
                    top: Math.floor($(window).height() / 2.5),
                    left: Math.floor($(window).width() / 2.2),
                    pinning: false
                }
            }).data("kendoNotification");

            resizeGrid();
            init();
            queryList();
            navbarActive(0);

        });
    </script>
</asp:Content>

