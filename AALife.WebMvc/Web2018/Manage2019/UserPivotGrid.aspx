<%@ Page Title="后台2019 | 用户分析" Language="C#" MasterPageFile="~/Web2018/Manage2019/MasterPage.master" AutoEventWireup="true" CodeFile="UserPivotGrid.aspx.cs" Inherits="AALife.WebMvc.Web2018.Manage2019.Manage_UserPivotGrid" %>
<%@ Register Src="~/Web2018/Manage2019/DevExpressUserControl.ascx" TagPrefix="uc1" TagName="DevExpressUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <uc1:DevExpressUserControl runat="server" id="DevExpressUserControl" />
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
                    format: { type: "fixedPoint", precision: 0 },
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
                showBorders: true,
                showRowTotals: false,
                showRowGrandTotals: true,
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
                        if (dataField.caption === COLUMN_ZZL/* && e.cell.rowType == "D"*/) {
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
                                        { dataField: "UserID", caption: "编号" },
                                        { dataField: "UserName", caption: "用户名" },
                                        { dataField: "UserNickName", caption: "用户昵称" },
                                        { dataField: "UserPhone", caption: "用户电话" },
                                        { dataField: "UserEmail", caption: "用户邮箱" },
                                        { dataField: "UserThemeName", caption: "用户主题" },
                                        { dataField: "UserFromName", caption: "用户来自", width: 150 },
                                        { dataField: "ModifyDate", caption: "修改日期", dataType: "date" },
                                        { dataField: "WorkDayName", caption: "工作日" },
                                        { dataField: "Synchronize", caption: "同步否", dataType: "number" },
                                        { dataField: "CreateDate", caption: "创建日期", dataType: "date" }
                                    ]
                                });
                            }
                        }).dxPopup("show");
                    }
                },
                dataSource: {
                    fields: [
                        { dataField: "UserID", caption: "用户ID" },
                        { dataField: "UserName", caption: "用户名" },
                        { dataField: "UserNickName", caption: "用户昵称" },
                        { dataField: "UserImage", caption: "用户头像" },
                        { dataField: "UserPhone", caption: "用户电话" },
                        { dataField: "UserEmail", caption: "用户邮箱" },
                        { dataField: "UserTheme", visible: false },
                        { dataField: "UserThemeName", caption: "用户主题" },
                        { dataField: "ModifyDate", caption: "修改日期", dataType: "date" },
                        { dataField: "UserWorkDay", visible: false },
                        { dataField: "WorkDayName", caption: "工作日" },
                        { dataField: "Synchronize", caption: "同步否", dataType: "number" },
                        { dataField: "CreateDate", caption: "创建日期", dataType: "date", area: "column", sortByPath: [] },
                        { dataField: "CreateDate", caption: "创建日期", dataType: "date", area: "filter", filterValues: [[2018],[2019]] },
                        { groupName: "CreateDate", caption: "创建日期", groupInterval: "quarter", visible: false },
                        { dataField: "UserFrom", visible: false },
                        { dataField: "UserFromName", caption: "用户来自", width: 150, area: "row", sortBySummaryField: "UserID", sortOrder: "desc" },
                        { caption: "数量", format: { type: "fixedPoint", precision: 0 }, summaryType: "count", area: "data" },
                        {
                            caption: COLUMN_ZZL, dataType: "number", area: "data", format: "decimal", summaryType: "count", calculateSummaryValue: function (cell) {
                                //Running total algorithm
                                var prevCell = cell.prev("column"),
                                    value = cell.value(),
                                    prevValue = prevCell && prevCell.value();
                                
                                if (isDefined(prevValue) && isDefined(value)) {
                                    value = ((value - prevValue) / prevValue * 100).toFixed();
                                } else {
                                    value = 0;
                                }
                                
                                return value;
                            },                            
                            customizeText: function (cell) {
                                return cell.value + "%";
                            }
                        },
                        {
                            caption: COLUMN_ZB, dataType: "number", format: "decimal", summaryType: "count", calculateSummaryValue: function (cell) {
                                var grandTotal = cell.grandTotal("row"),
                                    value = cell.value(),
                                    grandTotalValue = grandTotal && grandTotal.value();

                                if (isDefined(grandTotalValue) && isDefined(value)) {
                                    value = (value / grandTotalValue * 100).toFixed();
                                } else {
                                    value = 0;
                                }

                                return value;
                            },                            
                            customizeText: function (cell) {
                                return cell.value + "%";
                            }
                        },
                    ],
                    paginate: true,
                    remoteOperations: true,
                    store: DevExpress.data.AspNet.createStore({
                        key: "UserID",
                        loadUrl: "/api/v1/UserPivotGrid"
                    })
                }
            }).dxPivotGrid("instance");
            
            pivotGrid.bindChart(pivotGridChart, {
                dataFieldsDisplayMode: "splitPanes",
                alternateDataFields: false,
                inverted: true
            });

        });
    </script>
</asp:Content>

