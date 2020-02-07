<%@ Page Title="趣味统计分析" Language="C#" MasterPageFile="UserControl/MasterPage.master" AutoEventWireup="true" CodeFile="QuWeiTongJi.aspx.cs" Inherits="QuWeiTongJi" %>

<%@ Register Src="UserControl/FenXiMenu.ascx" TagName="FenXiMenu" TagPrefix="uc4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script type="text/javascript">    
</script>    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<div id="content">
    <!--内容开始-->
    <uc4:FenXiMenu ID="FenXiMenu1" runat="server" />
    <table cellspacing="0" border="0" style="width:100%;" class="tabledate">
        <tr>
            <td>数据来源于本网站2012年6月开始以来，所有用户添加的消费记录。</td>
        </tr>
    </table>
    <div class="maindiv">
        <div class="left">
            <div class="title"><img src="/Images/Others/page_white_h.png" alt="" title="" />&nbsp;&nbsp;消费商品 Top 10</div>
            <table cellspacing="0" border="1" style="width:100%;" class="tablelist">
                <tr>
                    <th style="width:34%;">商品名称</th>
                    <th style="width:33%;">最后日期</th>
                    <th style="width:33%;">总次数</th>
                </tr>
                <asp:Repeater ID="ItemNameCountList" runat="server">
                <ItemTemplate>
                <tr>
                    <td><%# Eval("ItemName")%></td>
                    <td><%# Eval("ItemBuyDate", "{0:yy-MM-dd}")%></td>
                    <td><%# Eval("CountNum")%></td>
                </tr>
                </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
        <div class="right">
            <div class="title"><img src="/Images/Others/page_white_h.png" alt="" title="" />&nbsp;&nbsp;最多消费用户 Top 10</div>
            <table cellspacing="0" border="1" style="width:100%;" class="tablelist">
                <tr>
                    <th style="width:34%;">用户名</th>
                    <th style="width:33%;">来自</th>
                    <th style="width:33%;">总数量</th>
                </tr>
                <asp:Repeater ID="UserItemCountList" runat="server">
                <ItemTemplate>
                <tr>
                    <td><%# ItemHelper.HideUserName(Eval("UserName").ToString())%></td>
                    <td><%# Eval("UserFromName")%></td>
                    <td><%# Eval("CountNum")%></td>
                </tr>
                </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
        <div class="h10"></div>
        <div class="left">
            <div class="title"><img src="/Images/Others/page_white_h.png" alt="" title="" />&nbsp;&nbsp;消费价格 Top 10</div>
            <table cellspacing="0" border="1" style="width:100%;" class="tablelist">
                <tr>
                    <th style="width:34%;">商品名称</th>
                    <th style="width:33%;" class="cellprice">商品价格</th>
                    <th style="width:33%;">购买日期</th>
                </tr>
                <asp:Repeater ID="ItemPriceMaxList" runat="server">
                <ItemTemplate>
                <tr>
                    <td><%# Eval("ItemName")%></td>
                    <td class="cellprice">￥<%# Eval("ItemPrice", "{0:0.0##}")%></td>
                    <td><%# Eval("ItemBuyDate", "{0:yy-MM-dd}")%></td>
                </tr>
                </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
        <div class="right">
            <div class="title"><img src="/Images/Others/page_white_h.png" alt="" title="" />&nbsp;&nbsp;最后消费用户 Top 10</div>
            <table cellspacing="0" border="1" style="width:100%;" class="tablelist">
                <tr>
                    <th style="width:34%;">用户名</th>
                    <th style="width:33%;">来自</th>
                    <th style="width:33%;">注册日期</th>
                </tr>
                <asp:Repeater ID="UserItemLastList" runat="server">
                <ItemTemplate>
                <tr>
                    <td><%# ItemHelper.HideUserName(Eval("UserName").ToString())%></td>
                    <td><%# Eval("UserFromName")%></td>
                    <td><%# Eval("CreateDate", "{0:yy-MM-dd}")%></td>
                </tr>
                </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
        <div class="h10"></div>
        <div class="left">
            <div class="title"><img src="/Images/Others/page_white_h.png" alt="" title="" />&nbsp;&nbsp;最后商品 Top 10</div>
            <table cellspacing="0" border="1" style="width:100%;" class="tablelist">
                <tr>
                    <th style="width:34%;">商品名称</th>
                    <th style="width:33%;" class="cellprice">商品价格</th>
                    <th style="width:33%;">购买日期</th>
                </tr>
                <asp:Repeater ID="ItemAddLastList" runat="server">
                <ItemTemplate>
                <tr>
                    <td><%# Eval("ItemName")%></td>
                    <td class="cellprice">￥<%# Eval("ItemPrice", "{0:0.0##}")%></td>
                    <td><%# Eval("ItemBuyDate", "{0:yy-MM-dd}")%></td>
                </tr>
                </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
        <div class="right">
            <div class="title"><img src="/Images/Others/page_white_h.png" alt="" title="" />&nbsp;&nbsp;最后注册用户 Top 10</div>
            <table cellspacing="0" border="1" style="width:100%;" class="tablelist">
                <tr>
                    <th style="width:34%;">用户名</th>
                    <th style="width:33%;">来自</th>
                    <th style="width:33%;">注册日期</th>
                </tr>
                <asp:Repeater ID="UserAddLastList" runat="server">
                <ItemTemplate>
                <tr>
                    <td><%# ItemHelper.HideUserName(Eval("UserName").ToString())%></td>
                    <td><%# Eval("UserFromName")%></td>
                    <td><%# Eval("CreateDate", "{0:yy-MM-dd}")%></td>
                </tr>
                </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
    </div>
    <!--内容结束-->
</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="js" Runat="Server">
<script type="text/javascript">
    $(function () {
        $("#menu table tr td").eq(3).addClass("on");

        $("#content .tabletitle .f7").addClass("on");
    });
</script>
</asp:Content>