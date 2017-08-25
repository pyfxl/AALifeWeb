<%@ Page Title="转账明细" Language="C#" MasterPageFile="UserControl/MasterPage.master" AutoEventWireup="true" CodeFile="UserZhuanZhangDetail.aspx.cs" Inherits="UserZhuanZhangDetail" %>

<%@ Register Src="UserControl/UserMenu.ascx" TagName="UserMenu" TagPrefix="uc6" %>

<asp:Content ID="Content1" ContentPlaceHolderID="js" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" Runat="Server">
<div id="r_content">
    <!--内容开始-->
    <uc6:UserMenu ID="UserMenu1" runat="server" />
    <div id="r_tablemenu">
        <div class="r_usergrid">
            <p class="tips"><img src="theme/images/pinber_01.gif" title="转账明细" /> <strong>转账明细</strong>&nbsp;&nbsp;&nbsp;&nbsp;<a href="UserCardAdmin.aspx">&lt;&lt; 返回列表</a></p>
            <table border="0" style="width:100%;" class="tablelist">
                <tr>
                    <th style="width:154px;">编号</th>
                    <th style="width:154px;">转账来自</th>
                    <th style="width:154px;">转账给</th>
                    <th style="width:154px;" class="cellprice">转账金额</th>
                    <th style="width:154px;">转账日期</th>
                    <th></th>
                </tr>
            </table>
            <div style="height:430px;">
                <table border="0" style="width:100%;" class="tablelist">
                    <asp:Repeater ID="ItemList" runat="server" OnItemCommand="List_ItemCommand">
                    <ItemTemplate>
                    <tr class='<%# ItemList.Items.Count % 2 == 0 ? "trcolor1" : "trcolor2" %>'>
                        <td style="width:154px;"><%# ItemList.Items.Count + 1 %></td>
                        <td style="width:154px;"><%# Eval("ZhuanZhangFromName") %><asp:HiddenField ID="ZhangFromHid" runat="server" Value='<%# Eval("ZhuanZhangFrom") %>' /></td>
                        <td style="width:154px;"><%# Eval("ZhuanZhangToName") %><asp:HiddenField ID="ZhangToHid" runat="server" Value='<%# Eval("ZhuanZhangTo") %>' /></td>
                        <td style="width:154px;" class="cellprice"><%# Eval("ZhuanZhangMoney", "{0:0.0##}") %><asp:HiddenField ID="ZhangMoneyHid" runat="server" Value='<%# Eval("ZhuanZhangMoney") %>' /></td>
                        <td style="width:154px;"><%# Eval("ZhuanZhangDate", "{0:yyyy-MM-dd}") %></td>
                        <td><asp:LinkButton ID="Button" runat="server" CausesValidation="False" CommandName="Delete" CommandArgument='<%# Eval("ZZID") %>' Text="删除" OnClientClick="return confirm('确定要删除吗？');" CssClass="baselink"></asp:LinkButton></td>
                    </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                    <tr style='display:<%# ItemList.Items.Count == 0 ? "table-row" : "none" %>;'>
                        <td colspan="5"><asp:Label ID="Label1" runat="server" Text="没有转账记录。"></asp:Label></td>
                    </tr>
                    </FooterTemplate>
                    </asp:Repeater>
                </table>
            </div>
        </div>
    </div>
    <!--内容结束-->
</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="end" Runat="Server">
<script type="text/javascript">
    $(function () {
        //设置当前菜单
        $(".usermenu .u22").addClass("on");

        //顶部菜单
        var thisname = $(".usermenu .u22").children().text().trim();
        cur_menu(thisname, ".user_ul");
    });
</script>
</asp:Content>