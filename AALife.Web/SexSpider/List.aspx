<%@ Page Language="C#" AutoEventWireup="true" CodeFile="List.aspx.cs" Inherits="SexSpider_List" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <title></title>
    <link href="../theme/kendoui/kendo.common.min.css" rel="stylesheet" />
    <link href="../theme/kendoui/kendo.bootstrap.min.css" rel="stylesheet" />
    <script src="../common/jquery-1.10.2.js"></script>
    <script src="../theme/kendoui/kendo.all.min.js"></script>
    <script>
        $(function () {
            var site_list;

            $.get("GetListData4.aspx", function (data) {
                var obj = JSON.parse(data.replace("\r\n",""));
                site_list = obj.site_list;
                $.each(site_list, function (idx, value) {
                    //if(value.sitename.indexOf("失效")>0) return true;
                    $(".list_table").append("<tr>" +
                        "<td>" + value.siteid + "</td>" +
                        "<td>" + value.sitename + "</td>" +
                        "<td><a href='Details.aspx?siteId=" + value.siteid + "' target='_blank'>列表</a></td>" +
                        "<td><a href='" + value.sitelink + "' target='_blank'>" + value.sitelink + "</a></td>" +
                        "<td>" + value.pageencode + "</td>" +
                        "<td>未加载</td>" +
                        "</tr>");
                });
            });

            $("#Button1").click(function () {
                $(".list_table tr:not(:first)").each(function (i, v) {

                    var sitelink = $(v).find("td").eq(3).text();
                    var pageencode = $(v).find("td").eq(4).text();

                    setTimeout(function () {
                        $.ajax({
                            type: "POST",
                            //async: false,
                            url: "List.aspx/CheckSiteLink",
                            data: JSON.stringify({ "siteLink": sitelink, "encoding": pageencode }),
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (data) {
                                $(v).find("td:last").html("成功");
                            },
                            error: function (e) {
                                $(v).find("td:last").html(JSON.parse(e.responseText).Message);
                            },
                            beforeSend: function () {
                                $(v).find("td:last").html("加载中");
                            }
                        });
                    }, 500 * i);

                });
            });

        });

        $(document).ajaxStart(function(){  
            $(".loading").html("正在运行");
        }).ajaxStop(function(){  
            $(".loading").html("");
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager EnablePageMethods="true" EnablePartialRendering="true" ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <p><input type="button" id="Button1" value="刷新" /> <span class="loading"></span></p>
        <table class="list_table" border="1" style="border-collapse: collapse;" id="grid">
            <colgroup>
                <col style="width: 10%;" />
                <col style="width: 20%;" />
                <col style="width: 10%;" />
                <col />
                <col style="width: 10%;" />
                <col style="width: 10%;" />
            </colgroup>
            <tr>
                <th>编号</th>
                <th>名称</th>
                <th>查看</th>
                <th>地址</th>
                <th>编码</th>
                <th>状态</th>
            </tr>
        </table>
    </form>
    <script>
        $(document).ready(function() {
            $("#grid").kendoGrid({
                height: 550
            });
        });
    </script> 
</body>
</html>
