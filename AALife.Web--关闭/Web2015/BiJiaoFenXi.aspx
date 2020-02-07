<%@ Page Title="消费比较分析" Language="C#" MasterPageFile="UserControl/MasterPage.master" AutoEventWireup="true" CodeFile="BiJiaoFenXi.aspx.cs" Inherits="BiJiaoFenXi" %>

<%@ Register Src="UserControl/FenXiMenu.ascx" TagName="FenXiMenu" TagPrefix="uc4" %>
<%@ Register Src="UserControl/DateTitle.ascx" TagName="DateTitle" TagPrefix="uc5" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script type="text/javascript" src="common/development-bundle/ui/monthpicker.js"></script>
<script type="text/javascript">
    var arrChart = null;

    $(function () {
        $("#datechoose").datepicker();        
        $("#datepicker").live("click", function () { datechoose($(this)); });
        $(".ui-state-default").click(function () { location.href = "BiJiaoFenXi.aspx?date=" + ($(".ui-datepicker-year").html() + "-" + this.getAttribute("data-month") + "-01"); });

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
        location.href = "BiJiaoMingXi.aspx?" + arrChart[index];
    }
</script>    
<style type="text/css">
.ui-datepicker td span, .ui-datepicker td a { height: 18px; line-height: 18px; }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<div id="datechoose" class="hasDatepicker"></div>
<div id="content">
    <!--内容开始-->
    <uc4:FenXiMenu ID="FenXiMenu1" runat="server" />
    <uc5:DateTitle ID="DateTitle1" runat="server" />
    <div class="maindiv">
        <div class="chartdiv">
            <script type="text/javascript" src="/ofcgwt/swfobject.js"></script>
            <script type="text/javascript">
                swfobject.embedSWF(
                "/ofcgwt/open-flash-chart-Wyrm.swf", "my_chart", "100%", "431",
                "9.0.0", "/ofcgwt/expressInstall.swf",
                { "data-file": "BiJiaoFXChartJson.aspx" }, { "wmode": "transparent" }
                );
            </script>
            <div id="my_chart"></div>
            <input type="hidden" id="hidChartData" runat="server" />
        </div>
        <table cellspacing="0" border="1" style="width:100%;" class="tablelist">
            <tr>
                <th style="width:14%;">编号</th>
                <th style="width:14%;">类别名称</th>
                <th style="width:14%;" class="cellprice bjcur">本月收入</th>
                <th style="width:15%;" class="cellprice bjcur">本月支出</th>
                <th style="width:14%;" class="cellprice bjprev">上月收入</th>
                <th style="width:15%;" class="cellprice bjprev">上月支出</th>
                <th style="width:14%;">操作</th>
            </tr>
            <asp:Repeater ID="List" runat="server">
            <ItemTemplate>
            <tr>
                <td><%# List.Items.Count + 1 %></td>
                <td><%# Eval("CategoryTypeName") %></td>
                <td class="cellprice bjcur">￥<%# Eval("ShouPriceCur", "{0:0.0##}") %></td>
                <td class="cellprice bjcur">￥<%# Eval("ZhiPriceCur", "{0:0.0##}") %></td>
                <td class="cellprice bjprev">￥<%# Eval("ShouPricePrev", "{0:0.0##}") %></td>
                <td class="cellprice bjprev">￥<%# Eval("ZhiPricePrev", "{0:0.0##}") %></td>
                <td><a href="BiJiaoMingXi.aspx?catTypeId=<%# Eval("CategoryTypeID") %>&catTypeName=<%# HttpUtility.UrlEncode(Eval("CategoryTypeName").ToString()) %>">详细</a></td>
            </tr>
            </ItemTemplate>
            <FooterTemplate>
            <tr>
                <td colspan="7" class="noitemcell"><asp:Label ID="Label1" runat="server" Text="没有消费记录。" Visible='<%# List.Items.Count == 0 %>'></asp:Label></td>
            </tr>
            </FooterTemplate>
            </asp:Repeater>
            <tr class="totalcell">
                <td>总计</td>
                <td>&nbsp;</td>
                <td class="cellprice bjcur">￥<asp:Label ID="Label2" runat="server"></asp:Label></td>
                <td class="cellprice bjcur">￥<asp:Label ID="Label4" runat="server"></asp:Label></td>
                <td class="cellprice bjprev">￥<asp:Label ID="Label3" runat="server"></asp:Label></td>
                <td class="cellprice bjprev">￥<asp:Label ID="Label5" runat="server"></asp:Label></td>
                <td>&nbsp;</td>
            </tr>
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