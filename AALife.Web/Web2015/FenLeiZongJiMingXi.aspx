<%@ Page Title="消费类别明细" Language="C#" MasterPageFile="UserControl/MasterPage.master" AutoEventWireup="true" CodeFile="FenLeiZongJiMingXi.aspx.cs" Inherits="FenLeiZongJiMingXi" %>

<%@ Register Src="UserControl/RankMenu.ascx" TagName="RankMenu" TagPrefix="uc3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script type="text/javascript">
    var arrChart = null;

    $(function () {
        $(".tabledate input[type=radio]").click(function () {
            if ($(this).val() == "图表") {
                $.cookie("view", "1");
                getview();
            } else {
                $.cookie("view", "0");
                getview();
            }
        });

        fixhead();

        getview();

        arrChart = $("#<%=hidChartData.ClientID %>").val().split(",");
    });

    function chart_click(index) {
        location.href = "ItemNumDetail.aspx?url=<%=HttpUtility.UrlEncode(Request.Url.ToString()) %>&" + arrChart[index];
    }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<div id="datechoose"></div>
<div id="content">
    <!--内容开始-->
    <uc3:RankMenu ID="RankMenu1" runat="server" />
    <table cellspacing="0" border="0" style="width:100%;" class="tabledate">
        <tr>
            <td style="width:10%;">&nbsp;</td>
            <td style="width:20%;"><strong>日期：</strong><%=Convert.ToDateTime(Session["TodayDate"]).ToString("yyyy年MM月") %></td>
            <td style="width:20%;"><strong>类别：</strong><%=Request.QueryString["catTypeName"] %></td>
            <td style="width:20%;"><strong>显示：</strong><input type="radio" name="view" id="radio1" value="列表" checked="checked" />列表&nbsp;&nbsp;<input type="radio" id="radio2" value="图表" name="view" />图表</td>
            <td style="width:20%;"><a href="FenLeiZongJi.aspx">&lt;&lt; 返回列表</a></td>
            <td style="width:10%;">&nbsp;</td>
        </tr>
    </table>
    <div class="maindiv">
        <div class="chartdiv">
            <script type="text/javascript" src="/ofcgwt/swfobject.js"></script>
            <script type="text/javascript">
                swfobject.embedSWF(
                "/ofcgwt/open-flash-chart-SimplifiedChinese.swf", "my_chart", "100%", "431",
                "9.0.0", "/ofcgwt/expressInstall.swf",
                { "data-file": "FLZJMXChartJson.aspx?catTypeId=<%=catTypeId %>" }, { "wmode": "transparent" }
                );
            </script>
            <div id="my_chart"></div>
            <input type="hidden" id="hidChartData" runat="server" />
        </div>
        <table cellspacing="0" border="1" style="width:100%;" class="tablelist">
            <tr>
                <th style="width:20%;">分类</th>
                <th style="width:20%;">商品名称</th>
                <th style="width:20%;">次数</th>
                <th style="width:20%;" class="cellprice">总价格</th>
                <th style="width:20%;">操作</th>
            </tr>
            <asp:Repeater ID="List" runat="server">
            <ItemTemplate>
            <tr>
                <td><%# Eval("ItemTypeName") %></td>
                <td><%# Eval("ItemName") %></td>
                <td><%# Eval("CountNum") %> 次</td>
                <td class="cellprice">￥<%# Eval("ItemPrice", "{0:0.0##}") %></td>
                <td><a href="ItemNumDetail.aspx?url=<%=HttpUtility.UrlEncode(Request.Url.ToString()) %>&itemName=<%# HttpUtility.UrlEncode(Eval("ItemName").ToString()) %>&itemType=<%# Eval("ItemType") %>&catTypeId=<%# Eval("CategoryTypeID") %>">查看详细</a></td>
            </tr>
            </ItemTemplate>
            <FooterTemplate>
            <tr>
                <td colspan="5" class="noitemcell"><asp:Label ID="Label1" runat="server" Text="没有消费记录。" Visible="<%# List.Items.Count == 0 %>"></asp:Label></td>
            </tr>
            </FooterTemplate>
            </asp:Repeater>
        </table>
    </div>
    <div class="h10"></div>
    <!--内容结束-->
</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="js" Runat="Server">
<script type="text/javascript">
    $(function () {
        $("#menu table tr td").eq(2).addClass("on");

        $("#content .tabletitle .r5").addClass("on");
    });
</script>
</asp:Content>