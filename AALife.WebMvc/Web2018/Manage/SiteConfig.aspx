<%@ Page Title="后台管理 | 消费列表" Language="C#" MasterPageFile="~/Web2018/Manage/MasterPage.master" ValidateRequest="false" AutoEventWireup="true" Inherits="Manage_SiteConfig" Codebehind="SiteConfig.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="row">
    <div class="col-xs-12">
        <form id="form1" runat="server" class="form-horizontal">
            <div class="form-group col-xs-12">
                <label for="SiteNameBox" class="col-md-2 col-xs-4">网站标题</label>
                <div class="col-md-10 col-xs-8">
                    <asp:TextBox ID="SiteNameBox" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="form-group col-xs-12">
                <label for="SiteAuthorBox" class="col-md-2 col-xs-4">网站作者</label>
                <div class="col-md-4 col-xs-8">
                    <asp:TextBox ID="SiteAuthorBox" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="form-group col-xs-12">
                <label for="SiteKeywordsBox" class="col-md-2 col-xs-4">关键字</label>
                <div class="col-md-10 col-xs-8">
                    <asp:TextBox ID="SiteKeywordsBox" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="form-group col-xs-12">
                <label for="SiteDescriptionBox" class="col-md-2 col-xs-4">网站描述</label>
                <div class="col-md-10 col-xs-8">
                    <asp:TextBox ID="SiteDescriptionBox" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="form-group col-xs-12">
                <label for="PagePerNumberBox" class="col-md-2 col-xs-4">页记录数</label>
                <div class="col-md-4 col-xs-8">
                    <asp:TextBox ID="PagePerNumberBox" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="form-group col-xs-12">                
                <label for="UserWorkDayBox" class="col-md-2 col-xs-4">工作日</label>
                <div class="col-md-4 col-xs-8">
                    <asp:TextBox ID="UserWorkDayBox" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="form-group col-xs-12">
                <label for="CategoryRateBox" class="col-md-2 col-xs-4">预算率%</label>
                <div class="col-md-4 col-xs-8">
                    <asp:TextBox ID="CategoryRateBox" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="form-group col-xs-12">
                <label for="SiteTipsBox" class="col-md-2 col-xs-4">网站贴士</label>
                <div class="col-md-10 col-xs-8">
                    <asp:TextBox ID="SiteTipsBox" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="form-group col-xs-12">
                <label for="MessageCodeBox" class="col-md-2 col-xs-4">公告版本</label>
                <div class="col-md-10 col-xs-8">
                    <asp:TextBox ID="MessageCodeBox" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="form-group col-xs-12">
                <label for="SiteMessageBox" class="col-md-2 col-xs-4">网站公告</label>
                <div class="col-md-10 col-xs-8">
                    <asp:TextBox ID="SiteMessageBox" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="form-group col-xs-12">
                <label for="PhoneMessageBox" class="col-md-2 col-xs-4">手机公告</label>
                <div class="col-md-10 col-xs-8">
                    <asp:TextBox ID="PhoneMessageBox" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="form-group col-xs-12">
                <label for="Button1" class="col-md-2 col-xs-4">&nbsp;</label>
                <div class="col-md-10 col-xs-8">
                    <asp:Button ID="Button1" runat="server" Text="确认无误，修改表单" CssClass="form-control btn btn-primary" onclick="Button1_Click" />
                </div>
            </div> 
        </form>      
    </div><!-- /.col -->
</div><!-- /.row -->
<script>
    $(document).ready(function () {
        navbarActive(4);
    });
</script>
</asp:Content>
