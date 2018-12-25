<%@ Control Language="C#" AutoEventWireup="true" Inherits="AALife.WebMvc.Web2018.UserControl_UserMenu" Codebehind="UserMenu.ascx.cs" %>
<table border="1" style="width:100%;" class="usermenu">
    <tr>
        <td style="width:12%;display:none;" class="u1"><a href="/UserAdmin.aspx"><img src="/theme/images/48_icon08.gif" title="用户资料" /><span>&nbsp;&nbsp;用户资料</span></a></td>
        <td style="width:13%;display:none;" class="u4"><a href="/UserBoundAdmin.aspx"><img src="/theme/images/COMMUNITY_LABEL.gif" title="用户绑定" /><span>&nbsp;&nbsp;用户绑定</span></a></td>
        <td style="width:12%;display:none;" class="u5"><a href="/UserDataAdmin.aspx"><img src="/theme/images/ico_software.gif" title="数据管理" /><span>&nbsp;&nbsp;数据管理</span></a></td>
        <td style="width:13%;display:none;" class="u2"><a href="/UserFunctionSetting.aspx"><img src="/theme/images/icon19.gif" title="菜单设置" /><span>&nbsp;&nbsp;菜单设置</span></a></td>
        <td style="width:12%;display:none;" class="u3"><a href="/UserCategoryAdmin.aspx"><img src="/theme/images/dot_12.gif" title="类别管理" /><span>&nbsp;&nbsp;类别管理</span></a></td>
        <td style="width:13%;display:none;" class="u6"><a href="/UserZhuanTi.aspx"><img src="/theme/images/i_hexa.gif" title="用户专题" /><span>&nbsp;&nbsp;用户专题</span></a></td>
        <td style="width:12%;display:none;" class="u7"><a href="/UserCardAdmin.aspx"><img src="/theme/images/icon_card2.gif" title="钱包管理" /><span>&nbsp;&nbsp;钱包管理</span></a></td>
        <td style="width:13%;display:none;" class="u8"><a href="/Helper.aspx"><img src="/theme/images/ku1.gif" title="用户声明" /><span>&nbsp;&nbsp;用户声明</span></a></td>
        <asp:Repeater ID="UserMenu" runat="server">
            <ItemTemplate>
            <td class='u<%# Eval("MenuID") %>'><a href='/<%# Eval("MenuURL") %>'><img src='/theme/images/<%# Eval("MenuImage") %>' title='<%# Eval("MenuName") %>' /><span>&nbsp;&nbsp;<%# Eval("MenuName") %></span></a></td>
            </ItemTemplate>
        </asp:Repeater>
    </tr>
</table>