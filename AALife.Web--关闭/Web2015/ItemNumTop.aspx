<%@ Page Title="消费次数排行" Language="C#" MasterPageFile="UserControl/MasterPage.master" AutoEventWireup="true" CodeFile="ItemNumTop.aspx.cs" Inherits="ItemNumTop" %>

<%@ Register Src="UserControl/RankMenu.ascx" TagName="RankMenu" TagPrefix="uc3" %>
<%@ Register Src="UserControl/DateTitle.ascx" TagName="DateTitle" TagPrefix="uc5" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script type="text/javascript" src="common/development-bundle/ui/monthpicker.js"></script>
<script type="text/javascript">
    var arrChart = null;

    $(function () {
        $("#datechoose").datepicker();        
        $("#datepicker").live("click", function () { datechoose($(this)); });
        $(".ui-state-default").click(function () { location.href = "ItemNumTop.aspx?date=" + ($(".ui-datepicker-year").html() + "-" + this.getAttribute("data-month") + "-01"); });

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

    function chart_open(index) {
        location.href = "ItemNumDetail.aspx?" + arrChart[index];
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
                "/ofcgwt/open-flash-chart-SimplifiedChinese.swf", "my_chart", "100%", "431",
                "9.0.0", "/ofcgwt/expressInstall.swf",
                { "data-file": "ItemNumChartJson.aspx" }, { "wmode": "transparent" }
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
            <asp:Repeater ID="NumTop" runat="server">
            <ItemTemplate>
            <tr>
                <td><%# Eval("ItemTypeName") %></td>
                <td><%# Eval("ItemName") %></td>
                <td><%# Eval("CountNum") %> 次</td>
                <td class="cellprice">￥<%# Eval("ItemPrice", "{0:0.0##}") %></td>
                <td><a href="ItemNumDetail.aspx?itemName=<%# HttpUtility.UrlEncode(Eval("ItemName").ToString()) %>&itemType=<%# Eval("ItemType") %>">查看详细</a></td>
            </tr>
            </ItemTemplate>
            <FooterTemplate>
            <tr>
                <td colspan="5" class="noitemcell"><asp:Label ID="Label1" runat="server" Text="没有消费记录。" Visible='<%# NumTop.Items.Count == 0 %>'></asp:Label></td>
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

        $("#content .tabletitle .r1").addClass("on");
    });
</script>
</asp:Content>