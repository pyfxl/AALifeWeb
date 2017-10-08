<%@ Page Title="网上记账" Language="C#" MasterPageFile="UserControl/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script type="text/javascript">
    var arrChart = null;

    $(function () {
        $("#datechoosehome").datepicker({
            showOtherMonths: true,
            selectOtherMonths: true,
            defaultDate: "<%=Session["TodayDate"].ToString() %>",
            onSelect: function (date, format) {
                location.href = "Default.aspx?date=" + date;
            }
        });
        
        setlink(0, "ItemList.aspx");
        setlink(1, "JieHuanFenXi.aspx");

        arrChart = $("#<%=hidChartData.ClientID %>").val().split(",");

        //公告
        //alert($.cookie("message"));
        if ($.cookie("message") == "undefined" && "<%=WebConfiguration.SiteMessage%>" != "") {
            $.cookie("message", "1");
            $("#sitemsg").animate({ top: "180px", opacity: 0.95 }, 1000);
        }
    });
    
    function chart_click(index) {
        //alert(arrChart[index]);
        location.href = "ItemList.aspx?date=" + arrChart[index];
    }

    function setlink(idx, url) {
        var td_val = $(".tablehome tr td").eq(idx).html();
        $(".tablehome tr td").eq(idx).html("<a href='"+url+"'>" + td_val + "</a>");
    }
    
    //网站公告
    function msgclose() {
        $("#sitemsg").fadeOut();
    }
</script>    
<style type="text/css">
.ui-datepicker th { height: 27px; }
.ui-widget-content { width: 100%; border: none; }
.ui-widget-header { height: 24px; }
.ui-datepicker td span, .ui-datepicker td a { padding: 0; height: 22px; line-height: 22px; }
.ui-datepicker table { font-size: 11px; }
.ui-datepicker td, .ui-datepicker th { padding: 0; }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<div id="sitemsg">
    <a href="javascript:msgclose();" class="close">×</a>
    <h3>公&nbsp;&nbsp;&nbsp;&nbsp;告</h3>
    <p><%=Utility.UnReplaceString(WebConfiguration.SiteMessage)%></p>
</div>
<div id="content">
    <div class="left" style="width:69%;height:202px;">
        <div class="title"><img src="/Images/Others/coins.png" alt="" title="" />&nbsp;&nbsp;消费统计列表<span><%=Convert.ToDateTime(Session["TodayDate"]).ToString("yyyy年MM月dd日") %></span></div>
        <table border="0" cellspacing="0" style="width:100%;" class="tablehome">
            <tr>
                <td colspan="4" class="cellcenter cellright">收支统计</td>
                <td colspan="4" class="cellcenter">收支借还（全部）</td>
            </tr>
            <tr>
                <td style="width:12%;">收入</td>
                <td style="width:13%;" class="cellprice">￥<asp:Label ID="Label2" runat="server"></asp:Label></td>
                <td style="width:12%;">支出</td>
                <td style="width:13%;" class="cellprice cellright">￥<asp:Label ID="Label4" runat="server"></asp:Label></td>
                <td style="width:12%;" class="priceblue">借出</td>
                <td style="width:13%;" class="cellprice priceblue">￥<asp:Label ID="Label5" runat="server"></asp:Label></td>
                <td style="width:12%;" class="pricered">借入</td>
                <td style="width:13%;" class="cellprice pricered">￥<asp:Label ID="Label7" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td>月入</td>
                <td class="cellprice">￥<asp:Label ID="Label1" runat="server"></asp:Label></td>
                <td>月出</td>
                <td class="cellprice cellright">￥<asp:Label ID="Label3" runat="server"></asp:Label></td>
                <td class="pricered">还入</td>
                <td class="cellprice pricered">￥<asp:Label ID="Label6" runat="server"></asp:Label></td>
                <td class="priceblue">还出</td>
                <td class="cellprice priceblue">￥<asp:Label ID="Label8" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td>年入</td>
                <td class="cellprice">￥<asp:Label ID="Label11" runat="server"></asp:Label></td>
                <td>年出</td>
                <td class="cellprice cellright">￥<asp:Label ID="Label12" runat="server"></asp:Label></td>
                <td class="pricebold <%= ItemHelper.JieHuanColor(this.Label9.Text, 1) %>">未还</td>
                <td class="cellprice pricebold <%= ItemHelper.JieHuanColor(this.Label9.Text, 1) %>">￥<asp:Label ID="Label9" runat="server"></asp:Label></td>
                <td class="pricebold <%= ItemHelper.JieHuanColor(this.Label10.Text, 1) %>">欠还</td>
                <td class="cellprice pricebold <%= ItemHelper.JieHuanColor(this.Label10.Text, 1) %>">￥<asp:Label ID="Label10" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td colspan="4" class="userfunc cellright usermoney"><p><img src="/Images/Others/ico_service01_03.gif" border="0" alt="" /> <asp:DropDownList ID="CardList" runat="server" Width="80" AutoPostBack="true" OnSelectedIndexChanged="CardList_SelectionChanged"></asp:DropDownList></p><strong><asp:Label ID="Label14" runat="server"></asp:Label></strong><em><a href="UserCardAdmin.aspx" class="linkedit">管理</a></em></td>
                <td colspan="4" class="userfunc"><asp:Label ID="Label13" runat="server"></asp:Label></td>
            </tr>
        </table>        
    </div>
    <div class="right" style="width:30%;height:202px;">
        <div id="datechoosehome"></div>
        <div id="datehome" style="display:none;">
            <asp:Calendar ID="Calendar1" Width="100%" runat="server" 
                OnSelectionChanged="Calendar1_SelectionChanged" EnableViewState="False" CssClass="calcss" 
                OnVisibleMonthChanged="Calendar1_VisibleMonthChanged" NextMonthText="&raquo;" PrevMonthText="&laquo;">
                <DayHeaderStyle CssClass="calhead" />
                <TitleStyle CssClass="caltitle" />
                <TodayDayStyle CssClass="caltoday" />
                <NextPrevStyle ForeColor="White" />
                <OtherMonthDayStyle ForeColor="Gray" />
                <SelectedDayStyle ForeColor="Black" BackColor="#99CCFF" Font-Bold="true" />
            </asp:Calendar>
        </div>
    </div>
    <div class="h10"></div>
    <script type="text/javascript" src="/ofcgwt/swfobject.js"></script>
    <script type="text/javascript">
        swfobject.embedSWF(
        "/ofcgwt/open-flash-chart-Wyrm.swf", "my_chart", "100%", "231",
        "9.0.0", "/ofcgwt/expressInstall.swf",
        { "data-file": "ItemDateChartJson.aspx" }, { "wmode": "transparent" }
        );
    </script>
    <div id="my_chart"></div>
    <input type="hidden" id="hidChartData" runat="server" />
</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="js" Runat="Server">
</asp:Content>