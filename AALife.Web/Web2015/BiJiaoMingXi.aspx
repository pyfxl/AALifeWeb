<%@ Page Title="消费分类明细" Language="C#" MasterPageFile="UserControl/MasterPage.master" AutoEventWireup="true" CodeFile="BiJiaoMingXi.aspx.cs" Inherits="BiJiaoMingXi" %>

<%@ Register Src="UserControl/FenXiMenu.ascx" TagName="FenXiMenu" TagPrefix="uc4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script type="text/javascript">
    var arrChart = null;
    var arrChart2 = null;

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
        arrChart2 = $("#<%=hidChartData2.ClientID %>").val().split(",");
    });

    function chart_click(index) {
        location.href = "ItemNumDetail.aspx?url=<%=HttpUtility.UrlEncode(Request.Url.ToString()) %>&" + arrChart[index] + "&date=<%=Session["TodayDate"].ToString() %>";
    }
    function chart_click2(index) {
        location.href = "ItemNumDetail.aspx?url=<%=HttpUtility.UrlEncode(Request.Url.ToString()) %>&" + arrChart2[index] + "&date=<%=Convert.ToDateTime(Session["TodayDate"]).AddMonths(-1).ToString("yyyy-MM-dd") %>";
    }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<div id="datechoose"></div>
<div id="content">
    <!--内容开始-->
    <uc4:FenXiMenu ID="FenXiMenu1" runat="server" />
    <table cellspacing="0" border="0" style="width:100%;" class="tabledate">
        <tr>
            <td style="width:10%;">&nbsp;</td>
            <td style="width:20%;"><strong>日期：</strong><%=Convert.ToDateTime(Session["TodayDate"]).ToString("yyyy年MM月") %></td>
            <td style="width:20%;"><strong>类别：</strong><%=Request.QueryString["catTypeName"] %></td>
            <td style="width:20%;"><strong>显示：</strong><input type="radio" name="view" id="radio1" value="列表" checked="checked" />列表&nbsp;&nbsp;<input type="radio" id="radio2" value="图表" name="view" />图表</td>
            <td style="width:20%;"><a href="BiJiaoFenXi.aspx">&lt;&lt; 返回列表</a></td>
            <td style="width:10%;">&nbsp;</td>
        </tr>
    </table>
    <div class="maindiv">
        <div class="chartdiv" style="display:none;">
            <script type="text/javascript" src="/ofcgwt/swfobject.js"></script>
            <script type="text/javascript">
                swfobject.embedSWF(
                "/ofcgwt/open-flash-chart-SimplifiedChinese.swf", "my_chart", "100%", "431",
                "9.0.0", "/ofcgwt/expressInstall.swf",
                { "data-file": "BJMXChartJson.aspx?catTypeId=<%=Request.QueryString["catTypeId"] %>" }, { "wmode": "transparent" }
                );
            </script>
            <div id="my_chart"></div>
            <input type="hidden" id="hidChartData" runat="server" />
            <input type="hidden" id="hidChartData2" runat="server" />
        </div>
        <table cellspacing="0" border="1" style="width:100%;" class="tablelist">
            <tr>
                <th style="width:16%;">分类</th>
                <th style="width:17%;">商品名称</th>
                <th style="width:17%;" class="bjcur">本月次数 <img src="/Images/Others/zim.gif" border="0" alt="此列可点击" title="此列可点击" /></th>
                <th style="width:17%;" class="cellprice bjcur">本月总价</th>
                <th style="width:17%;" class="bjprev">上月次数 <img src="/Images/Others/zim.gif" border="0" alt="此列可点击" title="此列可点击" /></th>
                <th style="width:17%;" class="cellprice bjprev">上月总价</th>
            </tr>
            <asp:Repeater ID="CatTypeList" runat="server">
            <ItemTemplate>
            <tr>
                <td><%# Eval("ItemTypeName") %></td>
                <td><%# Eval("ItemName") %></td>
                <td class="bjcur"><a href="ItemNumDetail.aspx?url=<%=HttpUtility.UrlEncode(Request.Url.ToString()) %>&itemName=<%# HttpUtility.UrlEncode(Eval("ItemName").ToString()) %>&itemType=<%# Eval("ItemTypeCur") %>&catTypeId=<%# Eval("CatTypeIDCur") %>&date=<%# Session["TodayDate"].ToString() %>"><%# Eval("CountNumCur")%> 次 </a></td>
                <td class="cellprice bjcur">￥<%# Eval("ItemPriceCur", "{0:0.0##}")%></td>
                <td class="bjprev"><a href="ItemNumDetail.aspx?url=<%=HttpUtility.UrlEncode(Request.Url.ToString()) %>&itemName=<%# HttpUtility.UrlEncode(Eval("ItemName").ToString()) %>&itemType=<%# Eval("ItemTypePrev") %>&catTypeId=<%# Eval("CatTypeIDPrev") %>&date=<%# Convert.ToDateTime(Session["TodayDate"]).AddMonths(-1).ToString("yyyy-MM-dd") %>"><%# Eval("CountNumPrev")%> 次 </a></td>
                <td class="cellprice bjprev">￥<%# Eval("ItemPricePrev", "{0:0.0##}")%></td>
            </tr>
            </ItemTemplate>
            <FooterTemplate>
            <tr>
                <td colspan="6" class="noitemcell"><asp:Label ID="Label1" runat="server" Text="没有消费记录。" Visible="<%# CatTypeList.Items.Count == 0 %>"></asp:Label></td>
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
        $("#menu table tr td").eq(3).addClass("on");

        $("#content .tabletitle .f1").addClass("on");
    });
</script>
</asp:Content>