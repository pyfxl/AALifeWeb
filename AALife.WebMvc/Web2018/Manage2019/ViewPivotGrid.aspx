<%@ Page Title="后台2019 | 访问分析" Language="C#" MasterPageFile="~/Web2018/Manage2019/MasterPage.master" AutoEventWireup="true" CodeFile="ViewPivotGrid.aspx.cs" Inherits="AALife.WebMvc.Web2018.Manage2019.Manage_ViewPivotGrid" %>
<%@ Register Src="~/Web2018/Manage2019/DevExpressUserControl.ascx" TagPrefix="uc1" TagName="DevExpressUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <uc1:DevExpressUserControl runat="server" ID="DevExpressUserControl" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="row">
        <div class="col-xs-12">
            <div class="options text-center">
                <div class="option">
                    <div id="change-inverted"></div>
                </div>
            </div>
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
                    customizeTooltip: function (args) {
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
                showRowGrandTotals: true,
                showColumnTotals: true,
                showColumnGrandTotals: true,
                allowSorting: true,
                allowSortingBySummary: true,
                fieldChooser: {
                    enabled: true,
                    height: 450
                },
                onCellClick: function (e) {
                    if (e.area == "data") {
                        var drillDownDataSource = e.component.getDataSource().createDrillDownDataSource(e.cell);

                        $("#popup").dxPopup({
                            title: "明细列表",
                            onShown: function (e) {
                                if (!e.component.content().children().length) {
                                    e.component.content().append($("<div>"))
                                }
                                e.component.content().children().dxDataGrid({
                                    height: "100%",
                                    remoteOperations: true,
                                    columnAutoWidth: true,
                                    showBorders: true,
                                    dataSource: drillDownDataSource,
                                    columns: [
                                        { dataField: "ViewID", caption: "访问ID", visible: false },
                                        { dataField: "PageID", caption: "页面ID", visible: false },
                                        { dataField: "UserID", caption: "用户ID", visible: false },
                                        { dataField: "DateStart", caption: "开始日期", dataType: "date" },
                                        { dataField: "DateEnd", caption: "结束日期", dataType: "date" },
                                        { dataField: "Portal", caption: "平台" },
                                        { dataField: "Version", caption: "版本" },
                                        { dataField: "Browser", caption: "浏览器" },
                                        { dataField: "Width", caption: "宽度" },
                                        { dataField: "Height", caption: "高度" },
                                        { dataField: "IP", caption: "IP地址" },
                                        { dataField: "CreateDate", caption: "创建日期", dataType: "date" },
                                        { dataField: "Network", caption: "网络" },
                                        { caption: "数量", summaryType: "count" },
                                        { dataField: "Synchronize", visible: false },
                                        { dataField: "Remark", visible: false },
                                        { dataField: "ViewPageTable", visible: false },
                                        { dataField: "UserTable", visible: false }
                                    ]
                                });
                            }
                        }).dxPopup("show");
                    }
                },
                dataSource: {
                    fields: [
                        { dataField: "ViewID", caption: "访问ID", visible: false },
                        { dataField: "PageID", caption: "页面ID", visible: false },
                        { dataField: "UserID", caption: "用户ID", visible: false },
                        { dataField: "DateStart", caption: "开始日期", dataType: "date" },
                        { dataField: "DateEnd", caption: "结束日期", dataType: "date" },
                        { dataField: "Portal", caption: "平台", area: "row" },
                        { dataField: "Version", caption: "版本", area: "column" },
                        { dataField: "Browser", caption: "浏览器" },
                        { dataField: "Width", caption: "宽度" },
                        { dataField: "Height", caption: "高度" },
                        { dataField: "IP", caption: "IP地址" },
                        { dataField: "CreateDate", caption: "创建日期", dataType: "date" },
                        { dataField: "Network", caption: "网络" },
                        { caption: "数量", summaryType: "count", area: "data" },
                        { dataField: "Synchronize", visible: false },
                        { dataField: "Remark", visible: false },
                        { dataField: "ViewPageTable", visible: false },
                        { dataField: "UserTable", visible: false }
                    ],
                    paginate: true,
                    remoteOperations: true,
                    store: DevExpress.data.AspNet.createStore({
                        key: "ViewID",
                        loadUrl: "/api/v1/ViewPivotGrid"
                    })
                }
            }).dxPivotGrid("instance");

            pivotGrid.bindChart(pivotGridChart, {
                dataFieldsDisplayMode: "splitPanes",
                alternateDataFields: false,
                //inverted: true //x轴数值切换
            });

            $("#change-inverted").dxCheckBox({
                text: "切换数值",
                value: false,
                onValueChanged: function (e) {
                    pivotGrid.bindChart(pivotGridChart, {
                        dataFieldsDisplayMode: "splitPanes",
                        alternateDataFields: false,
                        inverted: e.value
                    });
                }
            });

            //function expand() {
            //    var dataSource = pivotGrid.getDataSource();
            //    dataSource.expandHeaderItem("row", ["我才天生"]);
            //}

            //setTimeout(expand, 0);

            //createChart()();

        });

        //以下是创建饼图

        function createPivot() {
            $("#divPivot").dxPivotGrid({
                fieldChooser: {
                    enabled: true
                },
                export: {
                    enabled: true
                },
                loadPanel: true,
                allowSortingBySummary: true,
                allowSorting: true,
                allowFiltering: true,
                allowExpandAll: true,
                dataSource: {
                    fields: columns,
                    store: dataSource,
                    filter: null
                },

                showRowTotals: true,
                showRowGrandTotals: true,
                showColumnGrandTotals: true,
                showColumnTotals: true,
                onCellClick: function (e) {
                    if (e.area != "data") {
                        var kF;
                        var clmArea = e.area;
                        var pivotId = e.component._$element[0].id;
                        var pivot = $("#" + pivotId).dxPivotGrid("instance");
                        var clmsF = pivot.getDataSource().fields();
                        var clmIndex = e.cell.path.length - 1;
                        var clm;
                        var clmValue;
                        if (clmArea == 'column') {
                            clm = e.columnFields[clmIndex].dataField;
                        }
                        else {
                            clm = e.rowFields[clmIndex].dataField;
                        }
                        clmValue = e.cell.path[clmIndex];
                        for (kF = 0; kF <= clmsF.length - 1 ; kF++) {
                            if (clmsF[kF].dataField == clm) {
                                clmsF[kF].area = 'filter';
                                break;
                            }
                        }
                        pivot.option('dataSource.fields', clmsF);
                        var filterStr = pivot.option('dataSource.filter');
                        if (filterStr == null) {
                            filterStr = [];
                        }
                        if (filterStr.length > 0) {
                            filterStr.push("&&");
                        }
                        filterStr.push([clm, '=', clmValue]);
                        pivot.option('dataSource.filter', filterStr);
                    }
                }
            });
        }

        function createChart() {
            var pivotGrid = $("#grid").dxPivotGrid("instance");

            pivotGrid.on("contentReady", function () {
                //alert('change');
                $("#chart").dxPieChart("option", "dataSource", createChartDataSource(pivotGrid.getDataSource()));
            });

            var dataSourceChart = createChartDataSource(pivotGrid.getDataSource());

            $("#chart").dxPieChart({
                dataSource: dataSourceChart,
                type: 'Pie',
                animation: {
                    enabled: true
                },
                scrollBar: {
                    visible: true
                },
                scrollingMode: "all",
                zoomingMode: "all",
                legend: {
                    visible: true,
                    verticalAlignment: "bottom",
                    horizontalAlignment: "center",
                    border: {
                        visible: true
                    }
                },
                commonSeriesSettings: {
                    type: 'Pie',
                    argumentField: "arg",
                    valueField: "val"
                },
                loadingIndicator: {
                    show: true
                },
                seriesTemplate: {
                    nameField:"name"
                }
            });
        }

        var foreachTree = function (items, func, members) {
            members = members || [];
            for (var i = 0; i < items.length; i++) {
                members.unshift(items[i]);
                func(members);
                if (items[i].children) {
                    foreachTree(items[i].children, func, members);
                }
                members.shift();
            }
        };

        var createChartDataSource = function (pivotGridDataSource) {
            try {
                var data = pivotGridDataSource.getData(),
                     rowLevel = 1,
                     columnLevel = 1,
                     dataSource = [],
                     measureIndex = 0;
                var dataFields = pivotGridDataSource.getAreaFields("data");
                foreachTree(data.rows, function (members) {
                    rowLevel = Math.max(rowLevel, members.length);
                });
                foreachTree(data.columns, function (members) {
                    columnLevel = Math.max(columnLevel, members.length);
                });
                var rows = data.rows.length ? data.rows : [{ index: 0, value: "Grand Total" }];
                var columns = data.columns.length ? data.columns : [{ index: 0, value: "Grand Total" }];
                foreachTree(rows, function (rowMembers) {
                    if (rowLevel === rowMembers.length) {
                        var names = $.map(rowMembers, function (member) { return member.value; }).reverse();
                        foreachTree(columns, function (columnMembers) {
                            if (columnLevel === columnMembers.length) {
                                var args = $.map(columnMembers, function (member) { return member.value; }).reverse();
                                $.each(dataFields, function (measureIndex, dataField) {
                                    var value = ((data.values[rowMembers[0].index] || [])[columnMembers[0].index] || [])[measureIndex];
                                    dataSource.push({
                                        name: names.join(" - "),
                                        arg: args.join("/") + "/" + dataField.caption,
                                        val: value === undefined ? null : value
                                    });
                                })
                            }
                        });
                    }
                });
                return dataSource;
            }
            catch (err) {
                return "0"
            }

        };
    </script>
</asp:Content>

