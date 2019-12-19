<%@ Page Title="后台2019 | 消费列表" Language="C#" MasterPageFile="~/Web2018/Manage2019/MasterPage.master" AutoEventWireup="true" CodeFile="ItemTable.aspx.cs" Inherits="AALife.WebMvc.Web2018.Manage2019.Manage_ItemTable" %>

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
                        url: "/api/ItemTableDapper",
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
                    { field: "ItemPrice", aggregate: "sum" },
                ],
                schema: {
                    data: "Data",
                    total: "Total",
                    errors: "Errors",
                    aggregates: "ExtraData",
                    groups: "Group",
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
                        groupHeaderColumnTemplate: "<span style='text-align: right;'>#= sum #</span>",
                        footerTemplate: "#= sum #",
                    },
                    {
                        field: "ItemBuyDate",
                        title: "日期",
                        width: 100,
                        format: "{0:yyyy/MM/dd}"
                    },
                    {
                        template: "<a href='ItemPivotGrid.aspx?userId=#= UserID #'>#= UserID #</a>",
                        field: "UserID",
                        title: "用户编号",
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
            });

            resizeGrid();

        });
    </script>
</asp:Content>

