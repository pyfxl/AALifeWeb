<%@ Page Title="后台2019 | 消费分析" Language="C#" MasterPageFile="~/Web2018/Manage2019/MasterPage.master" AutoEventWireup="true" CodeFile="ItemPivotGrid.aspx.cs" Inherits="AALife.WebMvc.Web2018.Manage2019.Manage_ItemPivotGrid" %>
<%@ Register Src="~/Web2018/Manage2019/DevExpressUserControl.ascx" TagPrefix="uc1" TagName="DevExpressUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <uc1:DevExpressUserControl runat="server" ID="DevExpressUserControl" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="row">
        <div class="col-xs-12">
            <div id="chart"></div>
            <div class="hr hr16 hr-dotted"></div>
            <div id="grid"></div>
            <div id="popup"></div>
        </div><!-- /.col -->
    </div><!-- /.row -->
    
    <script>

        $(function () {

            var pivotGridChart = $("#chart").dxChart({
                commonSeriesSettings: {
                    type: "bar"
                },
                tooltip: {
                    enabled: true,
                    format: "decimal",
                    customizeTooltip: function(args) {
                        return {
                            html: args.seriesName + " | <div class='currency'>" + args.valueText + "</div>"
                        };
                    }
                },
                size: {
                    height: 250
                },
                adaptiveLayout: {
                    width: 450
                }
            }).dxChart("instance");

            var pivotGrid = $("#grid").dxPivotGrid({
                //height: 500,
                scrolling: { mode: "virtual" },
                fieldPanel: { visible: false },
                allowFiltering: true,
                headerFilter: {
                    allowSearch: true
                },
                showBorders: true,
                showRowTotals: true,
                showRowGrandTotals: false,
                showColumnTotals: true,
                showColumnGrandTotals: true,
                allowSorting: true,
                allowSortingBySummary: true,
                fieldChooser: {
                    enabled: true,
                    height: 450
                },
                onCellPrepared: function (e) {
                    var cText = e.cell.text;
                    //console.log(cText);
                    if(e.area == "data"){
                        var dataField = e.component.getDataSource().getAreaFields("data")[e.cell.dataIndex];
                        if (dataField.caption === COLUMN_ZB && e.cell.rowType == "D") {
                            //e.cellElement.css("background-color", "aliceblue");
                            e.cell.value < 0 ? e.cellElement.css("color", "red") : e.cell.value > 0 ? e.cellElement.css("color", "blue") : e.cellElement.css("color", "green");
                        }                        
                    }
                },
                onCellClick: function(e) {
                    if(e.area == "data") {
                        var drillDownDataSource = e.component.getDataSource().createDrillDownDataSource(e.cell);

                        $("#popup").dxPopup({
                            title: "明细列表",
                            onShown: function(e) {
                                if(!e.component.content().children().length) {
                                    e.component.content().append($("<div>"))
                                }
                                e.component.content().children().dxDataGrid({
                                    height: "100%",
                                    remoteOperations: true,
                                    columnAutoWidth: true,
                                    showBorders: true,
                                    dataSource: drillDownDataSource,
                                    columns: [
                                        { dataField: "ItemID", caption: "编号" },
                                        { dataField: "UserID", caption: "用户编号", visible: false },
                                        { dataField: "ItemTypeName", caption: "消费分类" },
                                        { dataField: "CategoryTypeName", caption: "商品类别" },
                                        { dataField: "ItemName", caption: "商品名称", width: 150 },
                                        { dataField: "ItemPrice", caption: "商品价格", dataType: "number", format: "decimal" },
                                        { dataField: "ItemBuyDate", caption: "购买日期", dataType: "date" },
                                        { dataField: "RegionTypeName", caption: "固定消费" },
                                        { dataField: "Recommend", caption: "推荐否" },
                                        { dataField: "CardName", caption: "钱包名称" },
                                        { dataField: "ZhuanTiName", caption: "专题名称" },
                                        { dataField: "ModifyDate", caption: "修改日期", dataType: "date" },
                                        { dataField: "Remark", caption: "备注", width: 150 },
                                        { dataField: "Synchronize", caption: "同步否", dataType: "number" }
                                    ]
                                });
                            }
                        }).dxPopup("show");
                    }
                },
                dataSource: {
                    fields: [
                        { dataField: "ItemID", visible: false },
                        //{ dataField: "ItemID", caption: "数量", summaryType: "count", area: "data" },
                        { dataField: "ItemName", caption: "商品名称", width: 150 },
                        { dataField: "Remark", caption: "备注", width: 150 },
                        { dataField: "RegionTypeName", caption: "固定消费" },
                        { dataField: "RegionType", visible: false },
                        { dataField: "Recommend", caption: "推荐否" },
                        { dataField: "ItemPrice", caption: "金额", dataType: "number", format: "decimal", summaryType: "sum", area: "data" },
                        //{ dataField: "ItemPrice", caption: "RunningTotal", dataType: "number", format: "decimal", summaryType: "sum", area: "data", runningTotal: "row", allowCrossGroupCalculation: true },
                        //{ dataField: "ItemPrice", caption: "平均", dataType: "number", format: { type: "fixedPoint", precision: 3 }, summaryType: "avg", area: "data" },
                        {
                            dataField: "ItemPrice", caption: COLUMN_ZB, dataType: "number", format: "decimal", summaryType: "sum", area: "data", calculateSummaryValue: function (cell) {
                                var grandTotal = cell.grandTotal("row"),
                                    value = cell.value(),
                                    grandTotalValue = grandTotal && grandTotal.value();

                                if (isDefined(grandTotalValue) && isDefined(value)) {
                                    value = (value / grandTotalValue * 100).toFixed();
                                } else if (isDefined(value)) {
                                    var parentCell = cell.parent("row"),
                                        children = parentCell && parentCell.children("row"),
                                        sum = 0;
                                    if (children) {
                                        for (var i = 0; i < children.length; i++) {
                                            sum += children[i].value() ? children[i].value() : 0;
                                        }
                                    }
                                    if (sum > 0) {
                                        value = (value / sum * 100).toFixed();
                                    } else {
                                        value = 0;
                                    }
                                } else {
                                    value = 0;
                                }

                                return value;
                            },                            
                            customizeText: function (cell) {
                                return cell.value + "%";
                            }
                        },
                        { dataField: "UserName", caption: "用户名", width: 100, expanded: true, area: "row", sortBySummaryField: "ItemPrice", sortOrder: "desc" },
                        { dataField: "UserID", caption: "用户编号", area: "filter", filterType: "include", filterValues: ["<%= UserID %>"] },
                        { dataField: "ItemTypeName", caption: "消费分类" },
                        { dataField: "ItemType", visible: false },
                        { dataField: "CategoryTypeName", caption: "商品类别", width: 100, area: "row", sortBySummaryField: "ItemPrice", sortOrder: "desc" },
                        { dataField: "CategoryTypeID", visible: false },
                        { dataField: "CardName", caption: "钱包名称" },
                        { dataField: "CardID", visible: false },
                        { dataField: "ZhuanTiName", caption: "专题名称" },
                        { dataField: "ZhuanTiID", visible: false },
                        { dataField: "ItemBuyDate", caption: "购买日期", dataType: "date", area: "column", sortByPath: [] },
                        { dataField: "ItemBuyDate", caption: "购买日期", dataType: "date", area: "filter", filterType: "include", filterValues: [[2018],[2019]] },
                        { dataField: "ModifyDate", caption: "修改日期", dataType: "date" },
                        { dataField: "Synchronize", caption: "同步否", dataType: "number" }
                    ],
                    paginate: true,
                    remoteOperations: true,
                    store: DevExpress.data.AspNet.createStore({
                        key: "ItemID",
                        loadUrl: "/api/v1/ItemPivotGrid"
                    })
                }
            }).dxPivotGrid("instance");
            
            pivotGrid.bindChart(pivotGridChart, {
                dataFieldsDisplayMode: "splitPanes",
                alternateDataFields: false,
                inverted: true
            });

            //function expand() {
            //    var dataSource = pivotGrid.getDataSource();
            //    dataSource.expandHeaderItem("row", ["我才天生"]);
            //}

            //setTimeout(expand, 0);
        });
    </script>
</asp:Content>

