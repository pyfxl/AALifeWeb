<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ListManage.aspx.cs" Inherits="SexSpider_ListManage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <link href="../theme/kendoui/kendo.common.min.css" rel="stylesheet" />
    <link href="../theme/kendoui/kendo.default.min.css" rel="stylesheet" />
    <script src="../common/jquery-1.10.2.js"></script>
    <script src="../theme/kendoui/kendo.all.min.js"></script>
    <script src="../theme/kendoui/messages/kendo.messages.zh-CN.min.js"></script>
    <title>列表页面</title>
    <script>
        function resizeContainers() {
            var htmlHeight = window.innerHeight - 20;
            //alert(htmlHeight);
            //alert($('html').height());
            $("#grid").height(htmlHeight);
        }

        $(document).ready(resizeContainers);
        $(window).resize(resizeContainers);
    </script>
</head>
<body>
    <div id="main">
        <div id="grid"></div>
    </div>
    <script>
        $(document).ready(function () {
            $("#grid").kendoGrid({
                dataSource: {
                    transport: {
                        read: {
                            url: "GetListJson4.aspx",
                            dataType: "json"
                        },
                        update: {
                            url: "UpdateListJson4.aspx",
                            dataType: "json",
                            type: "POST"
                        },
                        destroy: {
                            url: "",
                            dataType: "json"
                        },
                        create: {
                            url: "",
                            dataType: "json"
                        },
                        parameterMap: function (options, operation) {
                            if (operation !== "read" && options.models) {
                                return { models: kendo.stringify(options.models) };
                            }
                        }
                    },
                    batch: true,
                    schema: {
                        model: {
                            id: "siteid",
                            fields: {
                                siteid: { type: "number", editable: false, nullable: true },
                                siterank: { type: "string" },
                                viplevel: { type: "number" },
                                ishided: { type: "number" },
                                sitename: { type: "string" },
                                listpage: { type: "string" },
                                pageencode: { type: "string" },
                                domain: { type: "string" },
                                sitelink: { type: "string" },
                                listdiv: { type: "string" },
                                imagediv: { type: "string" },
                                pagediv: { type: "string" },
                                pagelevel: { type: "number" },
                                listfilter: { type: "string" },
                                imagefilter: { type: "string" },
                                pagefilter: { type: "string" }
                            }
                        }
                    },
                    requestEnd: function (e) {
                        if (e.type !== "read") {
                            $("#grid").data("kendoGrid").dataSource.read();
                        }
                    }
                },
                navigatable: true,
                toolbar: ["create", "save", "cancel"],
                columns: [
                    {
                        field: "siteid",
                        title: "编号",
                        width: 60
                    },
                    {
                        field: "siterank",
                        title: "排序",
                        width: 60
                    },
                    {
                        field: "viplevel",
                        title: "VIP",
                        width: 60
                    },
                    {
                        field: "ishided",
                        title: "隐藏",
                        width: 60
                    },
                    {
                        template: "<a href='ListDetails.aspx?siteId=#:siteid#' target='_blank'>#:sitename#</a>",
                        field: "sitename",
                        title: "名称（单击查看列表）",
                        width: 200
                    },
                    {
                        field: "listpage",
                        title: "下页页面",
                        width: 200
                    },
                    {
                        field: "pageencode",
                        title: "编码",
                        width: 100
                    },
                    {
                        field: "domain",
                        title: "域名",
                        width: 200
                    },
                    {
                        template: "<a href='#:sitelink#' target='_blank'>#:sitelink#</a>",
                        field: "sitelink",
                        title: "链接",
                        width: 300
                    },
                    {
                        field: "listdiv",
                        title: "列表DIV",
                        width: 200
                    },
                    {
                        field: "imagediv",
                        title: "图片DIV",
                        width: 200
                    },
                    {
                        field: "pagediv",
                        title: "分页DIV",
                        width: 200
                    },
                    {
                        field: "pagelevel",
                        title: "分页",
                        width: 60
                    },
                    {
                        field: "listfilter",
                        title: "列表过滤",
                        width: 200
                    },
                    {
                        field: "imagefilter",
                        title: "图片过滤",
                        width: 200
                    },
                    {
                        field: "pagefilter",
                        title: "分页过滤",
                        width: 200
                    }
                ],
                editable: true
            });
        });
    </script>
</body>
</html>
