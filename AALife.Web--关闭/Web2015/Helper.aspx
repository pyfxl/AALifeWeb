<%@ Page Title="网站说明" Language="C#" MasterPageFile="UserControl/MasterPage.master" AutoEventWireup="true" CodeFile="Helper.aspx.cs" Inherits="Helper" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script type="text/javascript">    
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<div id="content">
    <!--内容开始-->  
    <div class="maindiv">
        <div class="title"><img src="/Images/Others/help.png" alt="" title="" />&nbsp;&nbsp;网站说明</div>
        <table cellspacing="0" border="0" style="width:100%;" class="tableuser tablelogin">
            <tr>
                <th></th>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <th>网站开始：</th>
                <td>AA生活记账成立于：2012年6月 ~ ？？</td>
                <td></td>
            </tr>
            <tr>
                <th style="width:24%;">网站描述：</th>
                <td><%=WebConfiguration.SiteDescription %></td>
                <td style="width:14%;"></td>
            </tr>
            <tr>
                <th></th>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <th>联系方式：</th>
                <td><p><a target="_blank" href="http://shang.qq.com/wpa/qunwpa?idkey=944c168235281af2bf07b97f297950d1502ac20a861f261fb0573c78c96abc9a"><img border="0" src="http://pub.idqqimg.com/wpa/images/group.png" alt="AA生活记账" title="AA生活记账"></a>&nbsp;&nbsp;&nbsp;&nbsp;
                    <a target="_blank" href="http://wpa.qq.com/msgrd?v=3&uin=67936108&site=qq&menu=yes"><img border="0" src="http://wpa.qq.com/pa?p=1:67936108:7" alt="点击这里给我发消息"></a></p>
                    <p>&nbsp;</p>
                    <p>fxlmail@gmail.com</p>
                </td>
                <td></td>
            </tr>
            <tr>
                <th></th>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <th>手机版本：</th>
                <td><p><a href="http://www.fxlweb.com/app/AALifeNew.apk">AA生活记账安卓版，官方下载。</a></p>
                    <p>&nbsp;</p>
                    <p><a target="_blank" href="http://zhushou.360.cn/detail/index/soft_id/194688">从360手机助手下载。</a>&nbsp;&nbsp;&nbsp;&nbsp;<a target="_blank" href="http://app.mi.com/detail/34899">从小米应用商店下载。</a></p>
                </td>
                <td></td>
            </tr>
            <tr>
                <th></th>
                <td></td>
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
</script>
</asp:Content>