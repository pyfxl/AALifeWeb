<%@ Page Title="用户绑定" Language="C#" MasterPageFile="UserControl/MasterPage.master" AutoEventWireup="true" CodeFile="UserBoundAdmin.aspx.cs" Inherits="UserBoundAdmin" %>

<%@ Register Src="UserControl/UserMenu.ascx" TagName="UserMenu" TagPrefix="uc6" %>

<asp:Content ID="Content1" ContentPlaceHolderID="js" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="content" Runat="Server">
<div id="r_content">
    <!--内容开始-->    
    <uc6:UserMenu ID="UserMenu1" runat="server" />
    <div id="r_tablemenu">
        <div class="r_bound">
            <% if (!IsBound) { %>
            <div class="tips"><img src="theme/images/pinber_01.gif" title="绑定方式" /> 绑定方式</div>
            <table border="1" class="tablebound">
                <tr>
                    <th colspan="2">新建帐号</th>
                </tr>
                <tr>
                    <td style="width:100px;">用户名</td>
                    <td style="width:100px;"><asp:TextBox ID="UserNameNew" runat="server" MaxLength="12" autocomplete="off"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>密　码</td>
                    <td><asp:TextBox ID="UserPasswordNew" runat="server" TextMode="Password" MaxLength="12" autocomplete="off"></asp:TextBox></td>
                </tr>
                <tr>
                    <td colspan="2"><asp:Button ID="Button9" runat="server" Text="提交" OnClick="NewButton_Click" CssClass="btninput" /></td>
                </tr>
            </table>
            <table border="1" class="tablebound">
                <tr>
                    <th colspan="2">已有帐号</th>
                </tr>
                <tr>
                    <td style="width:100px;">用户名</td>
                    <td style="width:100px;"><asp:TextBox ID="UserNameBound" runat="server" MaxLength="12" autocomplete="off"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>密　码</td>
                    <td><asp:TextBox ID="UserPasswordBound" runat="server" TextMode="Password" MaxLength="12" autocomplete="off"></asp:TextBox></td>
                </tr>
                <tr>
                    <td colspan="2"><asp:Button ID="Button2" runat="server" Text="提交" OnClick="BoundButton_Click" CssClass="btninput" /></td>
                </tr>
            </table>
            <% } else { %>
            <div class="tips"><img src="theme/images/pinber_01.gif" title="绑定信息" /> 绑定信息</div>
            <table border="1" class="tablebound">
                <tr>
                    <th style="width:200px;">帐号绑定</th>
                </tr>
                <tr>
                    <td>没有可绑定的帐号。</td>
                </tr>
            </table>
            <% } %>
            <div class="clear"></div>
        </div>
        <div class="r_bound">
            <div class="tips"><img src="theme/images/pinber_01.gif" title="绑定列表" /> 绑定列表</div>
            <table border="0" style="width:100%;" class="tablelist">
                <tr>
                    <th style="width:25%;">编号</th>
                    <th style="width:25%;">用户</th>
                    <th style="width:25%;">来自</th>
                    <th style="width:25%;">操作</th>
                </tr>
                <asp:Repeater ID="BoundList" runat="server">
                <ItemTemplate>
                <tr class='<%# BoundList.Items.Count % 2 == 0 ? "trcolor1" : "trcolor2" %>'>
                    <td><%# BoundList.Items.Count + 1 %></td>
                    <td><%# Eval("UserNickName") %></td>
                    <td><%# Eval("OAuthFromName") %></td>
                    <td><asp:LinkButton ID="Button3" Visible='<%# Eval("OAuthFrom").ToString() == Session["UserFrom"].ToString() ? false : true %>' runat="server" Text="解除" OnCommand="Button3_Command" CommandArgument='<%# Eval("OAuthID") %>'></asp:LinkButton></td>
                </tr>
                </ItemTemplate>
                <FooterTemplate>
                <tr style='display:<%# BoundList.Items.Count == 0 ? "table-row" : "none"%>;'>
                    <td colspan="4"><asp:Label ID="Label6" runat="server" Text="没有绑定帐号。"></asp:Label></td>
                </tr>
                </FooterTemplate>
                </asp:Repeater>
            </table>
            <div class="clear"></div>
        </div>
    </div>
    <!--内容结束-->
</div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="end" Runat="Server">
<script type="text/javascript">
    $(function () {
        //设置当前菜单
        $(".usermenu .u14").addClass("on");

        //顶部菜单
        var thisname = $(".usermenu .u14").children().text().trim();
        cur_menu(thisname, ".user_ul");
    });
</script>
</asp:Content>