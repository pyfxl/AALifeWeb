<%@ Page Title="后台管理 | 消费列表" Language="C#" MasterPageFile="~/Manage/MasterPage.master" AutoEventWireup="true" CodeFile="ItemTable.aspx.cs" Inherits="Manage_ItemTable" %>

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
                        <input class="k-textbox" id="key" value="" placeholder="商品名称" />
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
            var query = { startDate:"", endDate:"", key: "", userId: 0 };

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
                btnGroup.select(9);
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
                        url: "/api/ItemTable.asmx/GetItemTable",
                        dataType: "json",
                        contentType: "application/json; charset=utf-8",
                        type: "POST",
                        cache: false,
                        data: function () { return query },
                        beforeSend: function (xhr) {
                            xhr.setRequestHeader('Content-Encoding', "gzip");
                        }
                    },
                    parameterMap: function (options, operation) {
                        if (operation !== "read" && options.models) {
                            return kendo.stringify({ models: options.models[0] });
                        }
                        return kendo.stringify({ pageModels: options });
                    }
                },
                pageSize: 30,
                serverPaging: true,
                schema: {
                    data: "d.rows",
                    total: "d.total",
                    model: {
                        id: "ItemID",
                        fields: {
                            ItemID: { type: "number" },
                            ItemName: { type: "string" },
                            CategoryTypeID: { type: "number" },
                            ItemPrice: { type: "number" },
                            ItemBuyDate: { type: "date" },
                            UserID: { type: "number" },
                            Recommend: { type: "number" },
                            ModifyDate: { type: "date" },
                            Synchronize: { type: "number" },
                            ItemAppID: { type: "number" },
                            RegionID: { type: "number" },
                            RegionType: { type: "string" },
                            ItemType: { type: "string" },
                            ZhuanTiID: { type: "number" },
                            CardID: { type: "number" }
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
            
            //列表
            var grid = $("#grid").kendoGrid({
                dataSource: dataSource,
                autoBind: false,
                height: 500,
                navigatable: true,
                resizable: true,
                filterable: true,
                sortable: true,
                groupable: false,
                //selectable: "multiple",
                pageable: {
                    pageSizes: true,
                    buttonCount: 10
                },
                columnMenu: {
                    columns: false
                },
                columns: [                    
                    {
                        field: "ItemID",
                        title: "编号",
                        width: 80
                    },
                    {
                        field: "ItemType",
                        title: "分类",
                        width: 80,
                        filterable: { multi: true }
                    },
                    {
                        field: "ItemName",
                        title: "商品名称",
                        width: 150
                    },
                    {
                        field: "CategoryTypeID",
                        title: "商品类别",
                        width: 80
                    },
                    {
                        field: "ItemPrice",
                        title: "价格",
                        format: "{0:#,0.###}",
                        width: 80
                    },
                    {
                        field: "ItemBuyDate",
                        title: "日期",
                        width: 150,
                        format: "{0:yyyy/MM/dd HH:mm:ss}"
                    },
                    {
                        template: "<a href='ItemTable.aspx?userId=#:UserID#'>#:UserID#</a>",
                        field: "UserID",
                        title: "用户ID",
                        width: 80
                    },
                    {
                        field: "Recommend",
                        title: "推荐否",
                        width: 80,
                        filterable: { multi: true }
                    },
                    {
                        field: "ModifyDate",
                        title: "修改日期",
                        width: 150,
                        format: "{0:yyyy/MM/dd HH:mm:ss}"
                    },
                    {
                        field: "Synchronize",
                        title: "同步否",
                        format: "0",
                        width: 80,
                        filterable: { multi: true }
                    },
                    {
                        field: "RegionID",
                        title: "固定ID",
                        width: 80
                    },
                    {
                        field: "RegionType",
                        title: "固定",
                        width: 80,
                        filterable: { multi: true }
                    },
                    {
                        field: "CardID",
                        title: "钱包ID",
                        width: 80
                    },
                    {
                        field: "ZhuanTiID",
                        title: "专题ID",
                        width: 80
                    }
                ]
            }).data("kendoGrid");

            //查询对象
            function setQuery() {
                query.startDate = kendo.toString(start.value(), "yyyy/MM/dd 00:00:00");
                query.endDate = kendo.toString(end.value(), "yyyy/MM/dd 23:59:59");
                query.key = $("#key").val();
                query.userId = getUrlParam("userId");
            }

            //列表查询
            function queryList() {
                setQuery();
                clearKey();
                dataSource.page(1);
                dataSource.skip(0);
                //dataSource.read();
            }

            //初始化
            function init() {
                var userId = getUrlParam("userId");
                if (!$.isEmptyObject(userId)) {
                    setBtnGroup(-1);
                    start.value(max_date());
                    end.value(today_date());
                }
            }

            //关键字定时器
            var keyTime;

            //关键字查询
            $("#key").keyup(function () {
                var str = $(this).val().trim();
                if (str.length < 2) return;

                setBtnGroup(-1);
                start.value(max_date());
                end.value(today_date());

                //query.key = str;
                if (keyTime != null) {
                    clearTimeout(keyTime);
                }
                keyTime = setTimeout(function () {
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
            navbarActive(1);

        });
    </script>
</asp:Content>

