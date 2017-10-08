<%@ Page Title="消费区间统计" Language="C#" MasterPageFile="UserControl/MasterPage.master" AutoEventWireup="true" CodeFile="QuJianTongJi.aspx.cs" Inherits="QuJianTongJi" %>

<%@ Register Src="UserControl/RankMenu.ascx" TagName="RankMenu" TagPrefix="uc3" %>
<%@ Register Src="UserControl/ViewTitle.ascx" TagName="ViewTitle" TagPrefix="uc8" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script type="text/javascript" src="common/development-bundle/ui/monthpicker.js"></script>
<script type="text/javascript">
    var arrChart = null;

    $(function () {
        $("#datechoose").datepicker();        
        $("#datepicker").live("click", function () { datechoose($(this)); });
        $(".ui-state-default").click(function () { location.href = "ItemPriceTop.aspx?date=" + ($(".ui-datepicker-year").html() + "-" + this.getAttribute("data-month") + "-01"); });
        
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
    <uc8:ViewTitle ID="ViewTitle1" runat="server" />
    <div class="maindiv">
        <div class="chartdiv">
            <script type="text/javascript" src="/ofcgwt/swfobject.js"></script>
            <script type="text/javascript">
                swfobject.embedSWF(
                "/ofcgwt/open-flash-chart-SimplifiedChinese.swf", "my_chart", "100%", "431",
                "9.0.0", "/ofcgwt/expressInstall.swf",
                { "data-file": "QuJianChartJson.aspx" }, { "wmode": "transparent" }
                );
            </script>
            <div id="my_chart"></div>
            <input type="hidden" id="hidChartData" runat="server" />
        </div>
        <table cellspacing="0" border="1" style="width:100%;" class="tablelist">
            <tr>
                <th style="width:16%;">分类</th>
                <th style="width:17%;">商品名称</th>
                <th style="width:12%;">区间</th>
                <th style="width:22%;">日期</th>
                <th style="width:17%;" class="cellprice">价格</th>
                <th style="width:16%;">操作</th>
            </tr>
            <asp:Repeater ID="List" runat="server">
            <ItemTemplate>
            <tr>
                <td><%# Eval("ItemTypeName") %></td>
                <td><%# Eval("ItemName") %></td>
                <td><%# Eval("RegionTypeName") %></td>
                <td><%# Eval("RegionDate") %></td>
                <td class="cellprice">￥<%# Eval("ItemPrice", "{0:0.0##}") %></td>
                <td><a href="ItemList.aspx?date=<%# Eval("ItemBuyDate", "{0:yyyy-MM-dd}") %>">查看详细</a>
                    &nbsp;&nbsp;<asp:LinkButton ID="Button2" runat="server" OnCommand="Button2_Command" CommandArgument='<%# Eval("RegionID") %>' Text="删除" OnClientClick="return confirm('确定要删除区间消费吗？');"></asp:LinkButton></td>                    
            </tr>
            </ItemTemplate>
            <FooterTemplate>
            <tr>
                <td colspan="6" class="noitemcell"><asp:Label ID="Label1" runat="server" Text="没有消费记录。" Visible='<%# List.Items.Count == 0 %>'></asp:Label></td>
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

        $("#content .tabletitle .r4").addClass("on");
    });
</script>
</asp:Content>