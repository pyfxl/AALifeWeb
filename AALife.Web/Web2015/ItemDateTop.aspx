<%@ Page Title="消费日期排行" Language="C#" MasterPageFile="UserControl/MasterPage.master" AutoEventWireup="true" CodeFile="ItemDateTop.aspx.cs" Inherits="ItemDateTop" %>

<%@ Register Src="UserControl/RankMenu.ascx" TagName="RankMenu" TagPrefix="uc3" %>
<%@ Register Src="UserControl/DateTitle.ascx" TagName="DateTitle" TagPrefix="uc5" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script type="text/javascript" src="/Web2015/common/development-bundle/ui/monthpicker.js"></script>
<script type="text/javascript">
    var arrChart = null;

    $(function () {
        $("#datechoose").datepicker();        
        $("#datepicker").live("click", function () { datechoose($(this)); });
        $(".ui-state-default").click(function () { location.href = "ItemDateTop.aspx?date=" + ($(".ui-datepicker-year").html() + "-" + this.getAttribute("data-month") + "-01"); });

        $(".tabledate input[type=radio]").click(function(){
            if($(this).val() == "图表") {
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
        location.href = "ItemList.aspx?date=" + arrChart[index];
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
    <uc3:RankMenu ID="RankMenu1" runat="server" />
    <uc5:DateTitle ID="DateTitle1" runat="server" />    
    <div class="maindiv">
        <div class="chartdiv">
            <script type="text/javascript" src="/ofcgwt/swfobject.js"></script>
            <script type="text/javascript">
                swfobject.embedSWF(
                "/ofcgwt/open-flash-chart-Wyrm.swf", "my_chart", "100%", "431",
                "9.0.0", "/ofcgwt/expressInstall.swf",
                { "data-file": "ItemDateChartJson.aspx?orderBy=list" }, { "wmode": "transparent" }
                );
            </script>
            <div id="my_chart"></div>
            <input type="hidden" id="hidChartData" runat="server" />
        </div>
        <table cellspacing="0" border="1" style="width:100%;" class="tablelist">
            <tr>
                <th style="width:20%;">排名</th>
                <th style="width:20%;">日期</th>
                <th style="width:20%;" class="cellprice">总收入</th>
                <th style="width:20%;" class="cellprice">总支出</th>
                <th style="width:20%;">操作</th>
            </tr>
            <asp:Repeater ID="DateTop" runat="server">
            <ItemTemplate>
            <tr>
                <td><%# DateTop.Items.Count + 1 %></td>
                <td><%# Eval("ItemBuyDate", "{0:yyyy-MM-dd}") %></td>
                <td class="cellprice">￥<%# Eval("ShouRuPrice", "{0:0.0##}") %></td>
                <td class="cellprice">￥<%# Eval("ZhiChuPrice", "{0:0.0##}") %></td>
                <td><a href="ItemList.aspx?date=<%# Eval("ItemBuyDate", "{0:yyyy-MM-dd}") %>">查看详细</a></td>
            </tr>
            </ItemTemplate>
            <FooterTemplate>
            <tr>
                <td colspan="5" class="noitemcell"><asp:Label ID="Label1" runat="server" Text="没有消费记录。" Visible='<%# DateTop.Items.Count == 0 %>'></asp:Label></td>
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

        $("#content .tabletitle .r3").addClass("on");
    });
</script>
</asp:Content>