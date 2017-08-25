<%@ Page Title="菜单设置" Language="C#" MasterPageFile="UserControl/MasterPage.master" AutoEventWireup="true" CodeFile="UserFunctionSetting.aspx.cs" Inherits="UserFunctionSetting" %>

<%@ Register Src="UserControl/UserMenu.ascx" TagName="UserMenu" TagPrefix="uc6" %>

<asp:Content ID="Content1" ContentPlaceHolderID="js" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="content" Runat="Server">
<div id="r_content">
    <!--内容开始-->
    <uc6:UserMenu ID="UserMenu1" runat="server" />
    <div id="r_tablemenu">
        <div class="r_login">
            <table border="0" style="width:100%;" class="tableform">
                <tr>
                    <th style="width:24%;"></th>
                    <td></td>
                    <td style="width:22%;"></td>
                </tr>
                <tr>
                    <th>系统菜单</th>
                    <td class="radioinput">
                        <asp:Repeater ID="SystemMenu" runat="server">
                            <ItemTemplate>
                            <asp:CheckBox ID="MenuBox" runat="server" AutoPostBack="true" OnCheckedChanged="Button1_Click" Enabled='<%# Eval("MenuLive") %>'></asp:CheckBox><asp:HiddenField ID="MenuIDHid" runat="server" Value='<%# Eval("MenuID") %>' />&nbsp;&nbsp;<%# Eval("MenuName") %>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </ItemTemplate>
                        </asp:Repeater>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <th></th>
                    <td></td>
                    <td></td>
                </tr>
                <tr style='display:<%=this.QueryList.Items.Count==0 ? "none" : "table-row" %>;'>
                    <th>用户查询</th>
                    <td class="radioinput">
                         <asp:Repeater ID="QueryList" runat="server">
                            <ItemTemplate>
                            <asp:CheckBox ID="MenuBox" runat="server" AutoPostBack="true" OnCheckedChanged="Button1_Click"></asp:CheckBox><asp:HiddenField ID="MenuIDHid" runat="server" Value='<%# Eval("MenuID") %>' />&nbsp;&nbsp;<%# Eval("MenuName") %>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </ItemTemplate>
                        </asp:Repeater>
                    </td>
                    <td></td>
                </tr>
                <tr style='display:<%=this.QueryList.Items.Count==0 ? "none" : "table-row" %>;'>
                    <th></th>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <th>用户中心</th>
                    <td class="radioinput">
                        <asp:Repeater ID="UserMenu" runat="server">
                            <ItemTemplate>
                            <asp:CheckBox ID="MenuBox" runat="server" AutoPostBack="true" OnCheckedChanged="Button1_Click"></asp:CheckBox><asp:HiddenField ID="MenuIDHid" runat="server" Value='<%# Eval("MenuID") %>' />&nbsp;&nbsp;<%# Eval("MenuName") %>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </ItemTemplate>
                        </asp:Repeater>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <th></th>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <th></th>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <th></th>
                    <td></td>
                    <td></td>
                </tr>
            </table>
        </div>
    </div>
    <!--内容结束-->
</div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="end" Runat="Server">
<script type="text/javascript">
    $(function () {
        //设置当前菜单
        $(".usermenu .u17").addClass("on");

        //顶部菜单
        var thisname = $(".usermenu .u17").children().text().trim();
        cur_menu(thisname, ".user_ul");
    });
</script>
</asp:Content>