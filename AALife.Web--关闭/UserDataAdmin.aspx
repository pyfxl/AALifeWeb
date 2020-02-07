<%@ Page Title="数据管理" Language="C#" MasterPageFile="UserControl/MasterPage.master" AutoEventWireup="true" CodeFile="UserDataAdmin.aspx.cs" Inherits="UserDataAdmin" %>

<%@ Register Src="UserControl/UserMenu.ascx" TagName="UserMenu" TagPrefix="uc6" %>

<asp:Content ID="Content1" ContentPlaceHolderID="js" Runat="Server">
<script type="text/javascript">

    $(function () {

        $("#<%=ItemBuyDate1.ClientID %>").datepicker({
            changeMonth: true,
            changeYear: true
        });

        $("#<%=ItemBuyDate2.ClientID %>").datepicker({
            changeMonth: true,
            changeYear: true
        });

    });

</script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="content" Runat="Server">
<div id="r_content">
    <!--内容开始-->    
    <uc6:UserMenu ID="UserMenu1" runat="server" />
    <div id="r_tablemenu">
        <div class="r_login">
            <table border="0" style="width:90%;margin-left:30px;" class="tableform">
                <tr>
                    <td></td>
                </tr>
                <tr>
                    <td><img src="theme/images/pinber_01.gif" title="数据导入" /> 将外部Excel记账文件导入到AA生活记账：</td>
                </tr>
                <tr>
                    <td>1.&nbsp;&nbsp;<asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click"><img src="theme/images/icon_xls1.gif" title="下载模板" /> 下载模板。</asp:LinkButton></td>
                </tr>
                <tr>
                    <td>2.&nbsp;&nbsp;<asp:FileUpload ID="FileUpload1" runat="server" CssClass="fileinput" />&nbsp;&nbsp;<asp:Button ID="Button1" runat="server" Text="导入" CssClass="btninput" onclick="Button1_Click" /></td>
                </tr>
                <tr>
                    <td></td>
                </tr> 
                <tr>
                    <td><img src="theme/images/pinber_01.gif" title="数据导出" /> 把AA生活记账的数据导出到Excel：</td>
                </tr>
                <tr>
                    <td><asp:TextBox ID="ItemBuyDate1" runat="server" Width="98px"></asp:TextBox>&nbsp;-&nbsp;<asp:TextBox ID="ItemBuyDate2" runat="server" Width="98px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td><asp:Button ID="Button2" runat="server" Text="导出数据" CssClass="btninput" onclick="Button2_Click" /></td>
                </tr>
                <tr>
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
        $(".usermenu .u16").addClass("on");

        //顶部菜单
        var thisname = $(".usermenu .u16").children().text().trim();
        cur_menu(thisname, ".user_ul");
    });
</script>
</asp:Content>