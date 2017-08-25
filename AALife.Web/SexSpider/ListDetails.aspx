<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ListDetails.aspx.cs" Inherits="SexSpider_ListDetails" %>

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
    <title>列表明细</title>
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
    
    <form id="form1" runat="server">
        <asp:HiddenField ID="SiteIdHid" runat="server" />
    </form>

    <div id="main">
        <div id="window"></div>
        <div id="grid"></div>
    </div>
        
    <script>
        function onChange(arg) {
            var selected = $.map(this.select(), function (item) {
                return $(item).text();
            });

            alert("Selected: " + selected.length + " item(s), [" + selected.join(", ") + "]");
        }

        $(document).ready(function () {
            $("#grid").kendoGrid({
                dataSource: {
                    transport: {
                        read: {
                            url: "GetDetailJson4.aspx?siteId=" + $("#SiteIdHid").val(),
                            dataType: "json"
                        }
                    },
                    schema: {
                        model: {
                            fields: {
                                Title: { type: "string" },
                                Link: { type: "string" }
                            }
                        }
                    }
                },
                change: onChange,
                columns: [
                    {
                        template: "<a href='javascript:;' onclick='f_open(\"#:Link#\")'>#:Title#</a>",
                        field: "Title",
                        title: "标题（单击查看图片）"
                    },
                    {
                        template: "<a href='#:Link#' target='_blank'>#:Link#</a>",
                        field: "Link",
                        title: "链接"
                    }
                ]
            });

        });

    </script>
    
    <script>
        function f_open(url) {
            //debugger;
            //alert(url);

            //清除内容
            $("#window").html("");

            var _siteId = $("#SiteIdHid").val();
            var _url = encodeURIComponent(url);

            var window = $("#window");
                  
            window.kendoWindow({
                width: "70%",
                height: "80%",
                title: "图片查看",
                actions: ["Refresh", "Maximize", "Close"],
                content: "GetImageJson4.aspx?siteId=" + _siteId + "&url=" + _url
            }).data("kendoWindow").open().center();
        }

    </script> 

</body>
</html>
