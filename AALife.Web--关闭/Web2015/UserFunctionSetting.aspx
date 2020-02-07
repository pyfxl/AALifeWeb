<%@ Page Title="功能设置" Language="C#" MasterPageFile="UserControl/MasterPage.master" AutoEventWireup="true" CodeFile="UserFunctionSetting.aspx.cs" Inherits="UserFunctionSetting" %>

<%@ Register Src="UserControl/UserMenu.ascx" TagName="UserMenu" TagPrefix="uc6" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<div id="datechoose"></div>
<div id="content">
    <!--内容开始-->
    <uc6:UserMenu ID="UserMenu1" runat="server" />
    <div class="maindiv userdiv">
        <table cellspacing="0" border="0" style="width:100%;" class="tableuser tablelogin">
            <tr>
                <th style="width:24%;"></th>
                <td></td>
            </tr>
            <tr>
                <th rowspan="2">消费排行</th>
                <td class="radioinput">
                    <asp:CheckBox ID="FenLeiZongJi" runat="server"></asp:CheckBox>&nbsp;&nbsp;消费分类排行&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:CheckBox ID="ItemNumTop" runat="server"></asp:CheckBox>&nbsp;&nbsp;消费次数排行&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:CheckBox ID="ItemPriceTop" runat="server"></asp:CheckBox>&nbsp;&nbsp;消费单价排行
                </td>
            </tr>
            <tr>
                <td class="radioinput">
                    <asp:CheckBox ID="ItemDateTop" runat="server"></asp:CheckBox>&nbsp;&nbsp;消费日期排行&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:CheckBox ID="QuJianTongJi" runat="server"></asp:CheckBox>&nbsp;&nbsp;消费区间统计&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:CheckBox ID="TuiJianFenXi" runat="server"></asp:CheckBox>&nbsp;&nbsp;消费推荐统计
                </td>
            </tr>
            <tr>
                <th></th>
                <td></td>
            </tr>
            <tr>
                <th rowspan="2">消费分析</th>
                <td class="radioinput">
                    <asp:CheckBox ID="BiJiaoFenXi" runat="server"></asp:CheckBox>&nbsp;&nbsp;消费分析比较&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:CheckBox ID="JianGeFenXi" runat="server"></asp:CheckBox>&nbsp;&nbsp;消费间隔分析&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:CheckBox ID="TianShuFenXi" runat="server"></asp:CheckBox>&nbsp;&nbsp;消费天数分析
                </td>
            </tr>
            <tr>
                <td class="radioinput">
                    <asp:CheckBox ID="JiaGeFenXi" runat="server"></asp:CheckBox>&nbsp;&nbsp;消费价格分析&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:CheckBox ID="JieHuanFenXi" runat="server"></asp:CheckBox>&nbsp;&nbsp;收支借还分析&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:CheckBox ID="QuWeiTongJi" runat="server"></asp:CheckBox>&nbsp;&nbsp;趣味统计分析
                </td>
            </tr>
            <tr>
                <th></th>
                <td></td>
            </tr>
            <tr>
                <th rowspan="2">用户设置</th>
                <td class="radioinput">
                    <asp:CheckBox ID="UserAdmin" runat="server"></asp:CheckBox>&nbsp;&nbsp;用户资料&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:CheckBox ID="UserBoundAdmin" runat="server"></asp:CheckBox>&nbsp;&nbsp;用户绑定&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:CheckBox ID="UserDataAdmin" runat="server"></asp:CheckBox>&nbsp;&nbsp;数据管理&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:CheckBox ID="UserFunction" runat="server"></asp:CheckBox>&nbsp;&nbsp;功能设置&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:CheckBox ID="UserZhuanTi" runat="server"></asp:CheckBox>&nbsp;&nbsp;用户专题
                </td>                    
            </tr>
            <tr>
                <td class="radioinput">                    
                    <asp:CheckBox ID="UserCardAdmin" runat="server"></asp:CheckBox>&nbsp;&nbsp;钱包管理
                </td>
            </tr>
            <tr>
                <th>其它功能</th>
                <td class="radioinput">
                    <asp:CheckBox ID="UserCategoryAdmin" runat="server"></asp:CheckBox>&nbsp;&nbsp;类别管理&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:CheckBox ID="SearchItem" runat="server"></asp:CheckBox>&nbsp;&nbsp;消费搜索&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:CheckBox ID="UserLogout" runat="server"></asp:CheckBox>&nbsp;&nbsp;用户退出&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:CheckBox ID="AboutUs" runat="server"></asp:CheckBox>&nbsp;&nbsp;网站说明
                </td>
            </tr>
            <tr>
                <th></th>
                <td></td>
            </tr>
            <tr>
                <th></th>
                <td><asp:Button ID="Button1" runat="server" Text="保存设置" OnClick="Button1_Click" CssClass="btninput" /></td>
            </tr>
            <tr>
                <th></th>
                <td></td>
            </tr>
        </table>
    </div>
    <div class="h10"></div>
    <!--内容结束-->
</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="js" Runat="Server">
<script type="text/javascript">
    $(function () {
        $("#content .tabletitle .u2").addClass("on");
    });
</script>
</asp:Content>