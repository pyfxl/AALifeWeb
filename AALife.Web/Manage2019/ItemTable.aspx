﻿<%@ Page Title="后台管理 | 消费列表" Language="C#" MasterPageFile="~/Manage2019/MasterPage.master" AutoEventWireup="true" CodeFile="ItemTable.aspx.cs" Inherits="Manage_ItemTable" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="assets/kendo-custom-ui.js"></script>
    <script src="assets/kendo-main.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <span id="notification"></span>
    <%--<div class="row">
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
                    <input class="k-textbox" id="keysearch" value="" placeholder="商品名称" />
                </div>
            </form>
        </div><!-- /.col -->
    </div><!-- /.row -->
    <div class="space-2"></div>--%>
    <div class="row">
        <div class="col-xs-12">
            <div id="grid"></div>
        </div><!-- /.col -->
    </div><!-- /.row -->
    
    <script>

        $(document).ready(function () {

            //查询对象
            var query = { startDate: "", endDate: "", keySearch: "", dateType: "1", buttonDown: "", userId: 0 };

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
                        url: "/api/ItemTableDapper.asmx/GetItemTable",
                        dataType: "json",
                        contentType: "application/json; charset=utf-8",
                        type: "POST",
                        cache: false,
                        //data: function () { return query },
                        beforeSend: function (xhr) {
                            xhr.setRequestHeader('Content-Encoding', "gzip");
                        }
                    },
                    parameterMap: function (options, operation) {
                        kendoui_parameter_format(options);
                        if (operation !== "read" && options.models) {
                            return kendo.stringify({ models: options.models[0] });
                        }
                        return kendo.stringify({ query: options });
                    }
                },
                pageSize: 30,
                serverPaging: true,
                serverSorting: true,
                serverFiltering: true,
                serverGrouping: true,
                serverAggregates: true,
                aggregate: [
                    { field: "ItemPrice", aggregate: "sum" },
                ],
                schema: {
                    data: "d.Data",
                    total: "d.Total",
                    errors: "d.Errors",
                    aggregates: "d.ExtraData",
                    groups: "d.Group",
                    model: {
                        id: "ItemID",
                        fields: {
                            ItemID: { type: "number" },
                            ItemName: { type: "string" },
                            CategoryTypeID: { type: "number" },
                            CategoryTypeName: { type: "string" },
                            ItemPrice: { type: "number" },
                            ItemBuyDate: { type: "date" },
                            UserID: { type: "number" },
                            UserName: { type: "string" },
                            Recommend: { type: "number" },
                            ModifyDate: { type: "date" },
                            Synchronize: { type: "number" },
                            ItemAppID: { type: "number" },
                            RegionID: { type: "number" },
                            RegionType: { type: "string" },
                            ItemType: { type: "string" },
                            ZhuanTiID: { type: "number" },
                            ZhuanTiName: { type: "string" },
                            CardID: { type: "number" },
                            CardName: { type: "string" },
                            Remark: { type: "string" }
                        }
                    }
                },
                //requestEnd: function (e) {                    
                //    if (e.type !== "read") {
                //        if (e.response.d.error) {
                //            return;
                //        } else {
                //            e.sender.read(query);
                //        }
                //    }
                //},
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
                autoBind: true,
                height: 500,
                navigatable: true,
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
                    pageSizes: [30, 50, 100, 'all'],
                    buttonCount: 10,
                    messages: {
                        display: "共 {2} 条"
                    }
                },
                columnMenu: false,
                columns: [                    
                    {
                        field: "ItemID",
                        title: "编号",
                        width: 80
                    },
                    {
                        values: <%= GetItemType() %>,
                        field: "ItemType",
                        title: "分类",
                        width: 80
                    },
                    {
                        field: "ItemName",
                        title: "商品名称",
                        width: 150
                    },
                    {
                        field: "CategoryTypeName",
                        title: "商品类别",
                        width: 150
                    },
                    {
                        field: "ItemPrice",
                        title: "价格",
                        format: "{0:#,0.###}",
                        attributes: { style: "text-align: right" },
                        width: 90,
                        aggregates: ["sum"],
                        groupHeaderColumnTemplate: "#= sum #",
                        footerTemplate: "#= sum #",
                    },
                    {
                        field: "ItemBuyDate",
                        title: "日期",
                        width: 100,
                        format: "{0:yyyy/MM/dd}"
                    },
                    {
                        field: "UserName",
                        title: "用户名",
                        width: 90
                    },
                    {
                        template: "#= Recommend == 1 ? '<i class=\"fa fa-heart\"></i>' : '' #",
                        field: "Recommend",
                        title: "推荐否",
                        width: 80
                    },
                    {
                        field: "ModifyDate",
                        title: "修改日期",
                        width: 150,
                        format: "{0:yyyy/MM/dd HH:mm:ss}"
                    },
                    {
                        template: "#= Synchronize == 1 ? '<i class=\"fa fa-check\"></i>' : '' #",
                        field: "Synchronize",
                        title: "同步否",
                        width: 80
                    },
                    {
                        values: <%= GetRegionType() %>,
                        field: "RegionType",
                        title: "固定",
                        width: 80
                    },
                    {
                        field: "CardName",
                        title: "钱包",
                        width: 80
                    },
                    {
                        field: "ZhuanTiName",
                        title: "专题",
                        width: 80
                    },
                    {
                        field: "Remark",
                        title: "备注",
                        width: 150
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
                query.userId = getUrlParam("userId");
            }

            //列表查询
            function queryList(flag) {
                if (flag) clearKey();
                setQuery();
                dataSource.page(1);
                dataSource.skip(0);
            }

            //初始化
            function init() {
                var userId = getUrlParam("userId");
                if (!$.isEmptyObject(userId)) {
                    setButtonDown("");
                    setDate("", "");
                    return;
                }

                var dateType = getUrlParam("dateType");
                if (!$.isEmptyObject(dateType)) {
                    query.dateType = dateType;
                }

                setButtonDown("b_day");
                setDate(today_date(), today_date());
            }

            resizeGrid();
            //init();
            //queryList();
            //navbarActive(1);

        });
    </script>
</asp:Content>

