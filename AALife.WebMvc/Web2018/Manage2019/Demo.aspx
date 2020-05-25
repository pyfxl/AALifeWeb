<%@ Page Title="" Language="C#" MasterPageFile="~/Web2018/Manage2019/MasterPage.master" AutoEventWireup="true" CodeBehind="Demo.aspx.cs" Inherits="AALife.WebMvc.Web2018.Manage2019.Demo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/theme/kendoui2019/js/kendo.aspnetmvc.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div id="example">
    <div id="Grid"></div>

    <script>
        jQuery(function() {
            jQuery("#Grid").kendoGrid({
                "columns": [{
                    "title": "UserID",
                    "field": "UserID",
                    "encoded": true,
                    //"aggregates": ["count"],
                    //"footerTemplate": "Total Count: #= count#",
                    //"groupFooterTemplate": "Count: #= count#"
                }, {
                    "title": "UserName",
                    "field": "UserName",
                    "encoded": true
                }, {
                    "title": "UserFrom",
                    "field": "UserFrom",
                    "encoded": true,
                    "aggregates": ["count"],
                    "groupHeaderTemplate": "UserFrom: #= value # (Count: #= count#)",
                    "footerTemplate": "Total Count: #= count#"
                }, {
                    "title": "UserTheme",
                    "field": "UserTheme",
                    "encoded": true,
                    "aggregates": ["count"],
                    "groupHeaderTemplate": "UserTheme: #= value # (Count: #= count#)"
                }],
                "pageable": {
                    "buttonCount": 10
                },
                "groupable": true,
                "sortable": true,
                "scrollable": false,
                "messages": {
                    "noRecords": "No records available."
                },
                "dataSource": {
                    "type": (function() {
                        if (kendo.data.transports['aspnetmvc-ajax']) {
                            return 'aspnetmvc-ajax';
                        } else {
                            throw new Error('The kendo.aspnetmvc.min.js script is not included.');
                        }
                    })(),
                    "transport": {
                        "read": {
                            "url": "/Demo/Read"
                        },
                        "prefix": ""
                    },
                    "pageSize": 10,
                    "page": 1,
                    "groupPaging": true,
                    "total": 0,
                    "serverPaging": true,
                    "serverSorting": true,
                    "serverFiltering": true,
                    "serverGrouping": true,
                    "serverAggregates": true,
                    "group": [{
                        "field": "UserFrom",
                        "dir": "asc",
                        "aggregates": [{
                            "field": "UserFrom",
                            "aggregate": "count"
                        },{
                            "field": "UserTheme",
                            "aggregate": "count"
                        }]
                    }],
                    "aggregate": [{
                        "field": "UserFrom",
                        "aggregate": "count"
                    },{
                        "field": "UserTheme",
                        "aggregate": "count"
                    }],
                    "filter": [],
                    "schema": {
                        "data": "Data",
                        "total": "Total",
                        "errors": "Errors",
                        "model": {
                            "fields": {
                                "UserID": {
                                    "type": "number"
                                },
                                "UserName": {
                                    "type": "string"
                                },
                                "UserFrom": {
                                    "type": "string"
                                },
                                "UserTheme": {
                                    "type": "string"
                                }
                            }
                        }
                    }
                }
            });
        });

    </script>
</div>
</asp:Content>
