<%@ Page Title="后台管理 | 访问列表" Language="C#" MasterPageFile="~/Manage/MasterPage.master" AutoEventWireup="true" CodeFile="ViewTable.aspx.cs" Inherits="Manage_ViewTable" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="assets/kendo-custom-ui.js"></script>
    <script src="assets/kendo-main.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <span id="notification"></span>
    <div class="space-2"></div>
    <div class="row">
        <div class="col-xs-12">
            <div id="grid"></div>
        </div><!-- /.col -->
    </div><!-- /.row -->
    
    <script>

        $(document).ready(function () {

            //查询对象
            var query = { startDate: "", endDate: "", keySearch: "" };

            //列表数据源
            var dataSource = new kendo.data.DataSource({
                transport: {
                    read: {
                        url: "/api/ViewTable.asmx/GetViewTable",
                        dataType: "json",
                        contentType: "application/json; charset=utf-8",
                        type: "POST",
                        cache: false,
                        //data: function () { return query },
                        beforeSend: function (xhr) {
                            xhr.setRequestHeader('Content-Encoding', "gzip");
                        }
                    },
                    parameterMap: function (data, operation) {
                        if (data.models) {
                            return JSON.stringify({ products: data.models });
                        } else if (operation == "read") {
                            //Page methods always need values for their parameters
                            data = $.extend({ sort: null, filter: null, aggregates: null }, data);
                            return JSON.stringify(data);
                        }
                    }
                },
                pageSize: 30,
                serverPaging: true,
                serverSorting: true,
                serverFiltering: true,
                serverGrouping: false,
                schema: {
                    data: "d.Data",
                    total: "d.Total",
                    aggregates: "d.Aggregates",
                    model: {
                        id: "ViewID",
                        fields: {
                            ViewID: { type: "number" },
                            PageID: { type: "number" },
                            PageName: { type: "string" },
                            PageTitle: { type: "string" },
                            UserID: { type: "number" },
                            DateStart: { type: "date" },
                            DateEnd: { type: "date" },
                            ViewSeconds: { type: "number" },
                            Portal: { type: "string" },
                            Version: { type: "string" },
                            Browser: { type: "string" },
                            Width: { type: "number" },
                            Height: { type: "number" },
                            IP: { type: "string" },
                            Network: { type: "string" },
                            Synchronize: { type: "number" },
                            Remark: { type: "string" },
                            CreateDate: { type: "date" }
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
                columns: [                    
                    {
                        field: "ViewID",
                        title: "编号",
                        width: 80
                    },
                    {
                        field: "PageName",
                        title: "页面名称",
                        width: 200,
                        filterable: {
                            multi: true,
                            //when serverPaging of the Grid is enabled, dataSource should be provided for all the Filterable Multi Check widgets
                            dataSource: {
                                transport: {
                                    read: {
                                        url: "/api/ViewTable.asmx/GetViewPageTable",
                                        dataType: "json",
                                        contentType: "application/json; charset=utf-8",
                                        type: "POST",
                                        cache: false
                                    }
                                },
                                schema: {
                                    data: "d.rows",
                                    total: "d.total"
                                }
                            },
                            itemTemplate: function (e) {
                                if (e.field == "all") {
                                    //handle the check-all checkbox template
                                    return "<li class='k-item'><label class='k-label'><input type='checkbox' class='k-check-all'/>#=all#</label></li>";
                                } else {
                                    //handle the other checkboxes
                                    return "<li class='k-item'><label class='k-label'><input type='checkbox' value='#=PageName#'/>#=PageName#</label></li>"
                                }
                            }
                        },
                    },
                    {
                        field: "PageTitle",
                        title: "页面标题",
                        width: 80
                    },
                    {
                        field: "UserID",
                        title: "用户名",
                        width: 80
                    },
                    {
                        field: "DateStart",
                        title: "访问开始时间",
                        width: 150,
                        format: "{0:yyyy/MM/dd HH:mm:ss}"
                    },
                    {
                        field: "DateEnd",
                        title: "访问结束时间",
                        width: 150,
                        format: "{0:yyyy/MM/dd HH:mm:ss}"
                    },
                    {
                        field: "ViewSeconds",
                        title: "间隔秒",
                        width: 80
                    },
                    {
                        field: "Portal",
                        title: "平台",
                        width: 80
                    },
                    {
                        field: "Version",
                        title: "版本",
                        width: 80
                    },
                    {
                        field: "Browser",
                        title: "型号",
                        width: 120
                    },
                    {
                        field: "Width",
                        title: "屏幕宽",
                        width: 80
                    },
                    {
                        field: "Height",
                        title: "屏幕高",
                        width: 80
                    },
                    {
                        field: "Network",
                        title: "网络",
                        width: 80
                    },
                    {
                        field: "CreateDate",
                        title: "创建日期",
                        width: 150,
                        format: "{0:yyyy/MM/dd HH:mm:ss}"
                    },
                    {
                        field: "Remark",
                        title: "备注",
                        width: 300
                    }
                ]
            }).data("kendoGrid");

            //提示
            var notification = $("#notification").kendoNotification({
                position: {
                    top: Math.floor($(window).height() / 2.5),
                    left: Math.floor($(window).width() / 2.2),
                    pinning: false
                }
            }).data("kendoNotification");
            
            //列表查询
            function queryList(flag) {
                dataSource.page(1);
                dataSource.skip(0);
            }

            //初始化
            function init() {
                
            }

            resizeGrid();
            init();
            queryList();
            navbarActive(2);

        });
    </script>
</asp:Content>

