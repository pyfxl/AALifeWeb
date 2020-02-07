<%@ Page Title="趣味统计" Language="C#" AutoEventWireup="true" CodeFile="QuWeiTongJiNew.aspx.cs" Inherits="QuWeiTongJiNew" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title><%=Page.Header.Title %><%=WebConfiguration.SiteName%></title>
<meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no" />
<meta name="format-detection" content="telephone=no" />
<meta name="apple-mobile-web-app-capable" content="yes" />
<link href="/Web2015/common/master-sj.css" rel="stylesheet" type="text/css" />
<link href="/Web2015/common/main.css" rel="stylesheet" type="text/css" />
<% if(Request.QueryString["flag"]=="1") { %>
<style type="text/css">
body { background: #FAFAFA; font-size: 14px; }
table.tablelist th { background: #EAEAEA; }
table.tablelist td { background: #FAFAFA; }
table.tablelist td, table.tablelist th { height: 39px; }
#content .title { height: 38px; line-height: 38px; font-size: 14px; }
.h10 { height: 1px; background: #DADADA; }
</style>
<% } %>   
<% if(Request.QueryString["flag"]=="2") { %>
<style type="text/css">
body { background: #FAFAFA; font-size: 20px; }
table.tablelist th { background: #EAEAEA; }
table.tablelist td { background: #FAFAFA; }
table.tablelist td, table.tablelist th { height: 60px; border-width: 2px; }
#content .title { height: 59px; line-height: 59px; font-size: 20px; }
.h10 { height: 2px; background: #DADADA; }
</style>
<% } %> 
</head>
<body>
    <form id="form1" runat="server">
    <!--总框架-->
    <div id="box">
        <!--顶部-->
	    <!--菜单-->
        <!--内容-->
        <div id="content">
            <!--内容开始-->
            <table cellspacing="0" border="0" style="width:100%;" class="tablelist">
                <tr>
                    <td>数据来自官网2012年6月开始的所有记录。</td>
                </tr>
            </table>
            <div class="h10"></div>
            <div class="tongjidiv">
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
                            <td><%# Eval("ItemBuyDate", "{0:MM-dd}")%></td>
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
                            <td><%# Eval("ItemBuyDate", "{0:MM-dd}")%></td>
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
                            <td><%# Eval("ItemBuyDate", "{0:MM-dd}")%></td>
                        </tr>
                        </ItemTemplate>
                        </asp:Repeater>
                    </table>
                </div>
                <div class="h10"></div>
                <div class="left">
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
                            <td><%# Eval("CreateDate", "{0:MM-dd}")%></td>
                        </tr>
                        </ItemTemplate>
                        </asp:Repeater>
                    </table>
                </div>
                <div class="h10"></div>
                <div class="left">
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
                            <td><%# Eval("CreateDate", "{0:MM-dd}")%></td>
                        </tr>
                        </ItemTemplate>
                        </asp:Repeater>
                    </table>
                </div>
                <div class="h10"></div>
            </div>
            <!--内容结束-->
        </div>
    </div>
    </form>
</body>
</html>