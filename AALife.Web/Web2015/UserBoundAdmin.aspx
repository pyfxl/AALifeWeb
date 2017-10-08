<%@ Page Title="用户绑定" Language="C#" MasterPageFile="UserControl/MasterPage.master" AutoEventWireup="true" CodeFile="UserBoundAdmin.aspx.cs" Inherits="UserBoundAdmin" %>

<%@ Register Src="UserControl/UserMenu.ascx" TagName="UserMenu" TagPrefix="uc6" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<div id="datechoose"></div>
<div id="content">
    <!--内容开始-->    
    <uc6:UserMenu ID="UserMenu1" runat="server" />
    <div class="maindiv userdiv">
        <% if (!IsBound) { %>
        <div class="groupdiv">
            <table cellspacing="0" border="1" style="width:100%;" class="tablelist">
                <tr>
                    <th colspan="2">新建帐号</th>
                </tr>
                <tr>
                    <td style="width:50%;">用户名</td>
                    <td><asp:TextBox ID="UserNameNew" runat="server" MaxLength="12" autocomplete="off"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>密　码</td>
                    <td><asp:TextBox ID="UserPasswordNew" runat="server" TextMode="Password" MaxLength="12" autocomplete="off"></asp:TextBox></td>
                </tr>
                <tr>
                    <td colspan="2"><asp:Button ID="Button9" runat="server" Text="提交" OnClick="NewButton_Click" CssClass="btninput" /></td>
                </tr>
            </table>
        </div>
        <div class="groupdiv">
            <table cellspacing="0" border="1" style="width:100%;" class="tablelist">
                <tr>
                    <th colspan="2">已有帐号</th>
                </tr>
                <tr>
                    <td style="width:50%;">用户名</td>
                    <td><asp:TextBox ID="UserNameBound" runat="server" MaxLength="12" autocomplete="off"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>密　码</td>
                    <td><asp:TextBox ID="UserPasswordBound" runat="server" TextMode="Password" MaxLength="12" autocomplete="off"></asp:TextBox></td>
                </tr>
                <tr>
                    <td colspan="2"><asp:Button ID="Button2" runat="server" Text="提交" OnClick="BoundButton_Click" CssClass="btninput" /></td>
                </tr>
            </table>
        </div>
        <% } else { %>        
        <div class="groupdiv">
            <table cellspacing="0" border="1" style="width:100%;" class="tablelist">
                <tr>
                    <th>帐号绑定</th>
                </tr>
                <tr>
                    <td>没有可绑定的帐号。</td>
                </tr>
            </table>
        </div>
        <% } %>
        <div class="h30"></div>
        <div class="title"><img src="/Images/Others/group_link.png" alt="" title="" />&nbsp;&nbsp;用户绑定列表</div>
        <table cellspacing="0" border="1" style="width:100%;" class="tablelist">
            <tr>
                <th style="width:25%;">编号</th>
                <th style="width:25%;">用户</th>
                <th style="width:25%;">来自</th>
                <th style="width:25%;">操作</th>
            </tr>
            <asp:Repeater ID="BoundList" runat="server">
                <ItemTemplate>
                <tr>
                    <td><%# BoundList.Items.Count + 1 %></td>
                    <td><%# Eval("UserNickName") %></td>
                    <td><%# Eval("OAuthFromName") %></td>
                    <td><asp:LinkButton ID="Button3" Visible='<%# Eval("OAuthFrom").ToString() == Session["UserFrom"].ToString() ? false : true %>' runat="server" Text="解除" OnCommand="Button3_Command" CommandArgument='<%# Eval("OAuthID") %>'></asp:LinkButton></td>
                </tr>
                </ItemTemplate>
                <FooterTemplate>
                <tr>
                    <td colspan="4" class="noitemcell"><asp:Label ID="Label6" runat="server" Text="没有绑定帐号。" Visible='<%# BoundList.Items.Count == 0 %>'></asp:Label></td>
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
        $("#content .tabletitle .u4").addClass("on");
    });
</script>
</asp:Content>