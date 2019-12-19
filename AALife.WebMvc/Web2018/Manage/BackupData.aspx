<%@ Page Title="后台管理 | 数据管理" Language="C#" MasterPageFile="~/Web2018/Manage/MasterPage.master" ValidateRequest="false" AutoEventWireup="true" Inherits="Manage_BackupData" Codebehind="BackupData.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="row">
    <div class="col-xs-12">
        <form id="form1" runat="server" class="form-horizontal">
            <div class="form-group col-xs-12">
                <div class="col-xs-12 col-md-2">
                    <asp:TextBox ID="UserIDBox" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-xs-12 col-md-2">
                    <asp:Button ID="Button1" runat="server" Text="导出到APP数据" CssClass="form-control btn btn-primary" OnClick="Button1_Click" UseSubmitBehavior="false" />
                </div>
                <div class="col-xs-12 col-md-2">
                    <asp:Button ID="Button3" runat="server" Text="(1)删除用户数据" CssClass="form-control btn btn-primary" OnClick="Button3_Click" UseSubmitBehavior="false" />
                </div>
                <div class="col-xs-12 col-md-2">
                    <asp:Button ID="Button5" runat="server" Text="(2)恢复APP数据" CssClass="form-control btn btn-primary" OnClick="Button5_Click" UseSubmitBehavior="false" />
                </div>
            </div>
        </form>
    </div>
</div>
<script>
    $(document).ready(function () {
        navbarActive(3);
    });
</script>
</asp:Content>