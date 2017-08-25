<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Details.aspx.cs" Inherits="SexSpider_Details" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <title></title>
    <script src="../common/jquery-1.10.2.js"></script>
    <script>
        $(function () {

            $.get("GetListData4.aspx", function (data) {
                var obj = JSON.parse(data.replace("\r\n",""));
                site_list = obj.site_list;
                $.each(site_list, function (idx, value) {
                    //if(value.sitename.indexOf("失效")>0) return true;
                    //$("#site_list").append("<option value='" + value.siteid + "'>" + value.sitename + "</option>");
                    if (value.siteid == getUrlParam("siteId")) {
                        $("#siteLinkHid").val(value.sitelink);
                        $("#pageEncodeHid").val(value.pageencode);
                        $("#listDivHid").val(value.listdiv);
                        $("#listFilterHid").val(value.listfilter);
                        $("#imageDivHid").val(value.imagediv);
                        $("#imageFilterHid").val(value.imagefilter);
                        $("#pageDivHid").val(value.pagediv);
                        $("#pageFilterHid").val(value.pagefilter);
                        $("#domainHid").val(value.domain);
                        $("#pageLevelHid").val(value.pagelevel);

                        $(".title").html(value.sitename + " <a href='" + value.sitelink + "' target='_black'>" + value.sitelink + "</a>");
                    }

                });
            });
            
        });

        $(document).ajaxStop(function () {
            //__doPostBack("#Button1", "");
        });

        /*获取浏览器url的参数的值*/
        function getUrlParam(name) {
            var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)"); //构造一个含有目标参数的正则表达式对象
            var r = window.location.search.substr(1).match(reg);  //匹配目标参数
            if (r != null) return decodeURIComponent(r[2]); return null; //返回参数值
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>            
        <div>
            <asp:HiddenField ID="siteLinkHid" runat="server" />
            <asp:HiddenField ID="pageEncodeHid" runat="server" />
            <asp:HiddenField ID="listDivHid" runat="server" />
            <asp:HiddenField ID="listFilterHid" runat="server" />
            <asp:HiddenField ID="domainHid" runat="server" />
            <asp:HiddenField ID="imageDivHid" runat="server" />
            <asp:HiddenField ID="imageFilterHid" runat="server" />
            <asp:HiddenField ID="pageDivHid" runat="server" />
            <asp:HiddenField ID="pageFilterHid" runat="server" />
            <asp:HiddenField ID="pageLevelHid" runat="server" />
        </div>
        <div>
            <span class="title"></span><br />
            <asp:Button ID="Button1" runat="server" Text="加载" OnClick="Button1_Click" />
        </div>
        <div>
            <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                <ProgressTemplate>
                    <asp:Label ID="Label1" runat="server" Text="加载中..." ForeColor="Red"></asp:Label>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </div>
        <div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:Repeater ID="Repeater1" runat="server" OnItemCommand="Repeater1_ItemCommand">
                        <HeaderTemplate>
                            <ul class="list">
                        </HeaderTemplate>
                        <ItemTemplate>
                            <li><a href="<%# Eval("Link") %>" target="_blank"><%# Eval("Title") %></a> <asp:Button ID="Button2" runat="server" Text="下载" CommandArgument='<%# Eval("Link") %>' /></li>
                        </ItemTemplate>
                        <FooterTemplate>
                            </ul>
                        </FooterTemplate>
                    </asp:Repeater>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Button1" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
        <div style="position: absolute; right: 0; top: 0; bottom: 0; width: 50%; background: #ffd800; text-align: center; overflow: auto;">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Repeater ID="Repeater2" runat="server">
                        <ItemTemplate>
                            <p><img src="<%# Eval("ImageUrl") %>" /></p>
                        </ItemTemplate>
                    </asp:Repeater>
                </ContentTemplate>
            </asp:UpdatePanel>
            <div style="clear: both;"></div>
        </div>
    </form>
</body>
</html>
