<%@ Page Title="智能添加消费" Language="C#" MasterPageFile="UserControl/MasterPage.master" AutoEventWireup="true" CodeFile="ItemAddSmart.aspx.cs" Inherits="ItemAddSmart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<style type="text/css">
#zhuantiedit {
    width: 400px;
    margin-left: -220px;
    height: 180px;
    overflow: auto;
    top: 240px;
}
#zhuantiedit table 
{
    border-collapse: collapse;
    border: 1px solid #FFF;
    margin-bottom: 5px;
}
#zhuantiedit table td, #zhuantiedit table th
{
    background: #F4F4F4;
    border: 1px solid #C4C4C4;
    height: 23px;
    text-align: center;
}
.userdiv {
    max-height: 507px;
}
</style>
<script type="text/javascript">
    $(function () {
        $(".smartitemtype p").click(function () {
            $("#<%=ItemType.ClientID %>").val($(this).text());
            $("#<%=ItemTypeHid.ClientID %>").val(this.getAttribute("ref"));
            $(".smartitemtype p").removeClass("on");
            $(this).addClass("on");
        });

        $(".smartcattypename p").click(function () {
            $("#<%=CategoryTypeID.ClientID %>").val($(this).attr("ref"));
            $("#<%=CategoryTypeDown.ClientID %>").val($(this).attr("ref"));
            $(".smartcattypename p").removeClass("on");
            $(this).addClass("on");
            
            $("#<%=ItemName.ClientID %>").val("");
            $("#<%=ItemPrice.ClientID %>").val("");
            $(".smartitemname").html("");
            $(".smartitemprice").html("");
            $(".smartitembuydate").html("");

            getitemname($(this).attr("ref"));
        });

        $("#<%=CategoryTypeDown.ClientID %>").change(function () {
            $(".smartcattypename p").removeClass("on");
            $(".smartitemname").html("");

            var i = $(this).get(0).selectedIndex;
            if (i > 0) {
                $(".smartcattypename p").removeClass("on");
                $(".smartcattypename p").eq(i - 1).addClass("on");

                getitemname($(this).val());
            }
        });

        function getitemname(catid) {
            $.ajax({
                type: "post",
                url: "AutoSmartItemNameJson.aspx?term=" + catid,
                dataType: "json",
                cache: false,
                beforeSend: function () {
                    loading(".smartitemname");
                }, success: function (response) {
                    log(response, ".smartitemname");
                }, complete: function () {

                }
            });
        }

        $("#<%=ItemName.ClientID %>").keyup(function () {
            $("#<%=ItemPrice.ClientID %>").val("");
            $(".smartitemprice").html("");
            $(".smartitembuydate").html("");
            
            $.ajax({
                type: "post",
                url: "AutoItemNameJson.aspx?term=" + encodeURIComponent($(this).val()),
                dataType: "json",
                cache: false,
                beforeSend: function () {
                    loading(".smartitemname");
                }, success: function (response) {
                    log(response, ".smartitemname");
                }, complete: function () {

                }, error: function () {
                    $(".smartitemname").html("");
                }
            });
        });
        
        $(".smartitemname p").live("click", function () {
            $("#<%=ItemName.ClientID %>").val($(this).text());
            $(".smartitemname p").removeClass("on");
            $(this).addClass("on");

            $("#<%=ItemPrice.ClientID %>").val("");
            $(".smartitemprice").html("");
            $(".smartitembuydate").html("");

            $.ajax({
                type: "post",
                url: "AutoSmartItemPriceJson.aspx?term=" + encodeURIComponent($(this).html()),
                dataType: "json",
                cache: false,
                beforeSend: function () {
                    loading(".smartitemprice");
                }, success: function (response) {
                    log(response, ".smartitemprice");
                }, complete: function () {

                }
            });
        });

        $(".smartitemprice p").live("click", function () {
            $("#<%=ItemPrice.ClientID %>").val($(this).text());
            $(".smartitemprice p").removeClass("on");
            $(this).addClass("on");
            
            newdate(now);
        });

        function log(data, obj) {
            $(obj).html("");
            for (var i = 0; i < data.length; i++) {
                $(obj).append("<p>" + data[i].label + "</p>");
            }
        }

        function loading(obj) {
            $(obj).html("<img src='/Images/Others/ui-anim_basic_16x16.gif' alt='' title='' />");
        }

        var now = new Date("<%=Session["TodayDate"].ToString().Replace("-", "/") %>");
        var cur = new Date("<%=Session["TodayDate"].ToString().Replace("-", "/") %>");

        $("#<%=RegionID.ClientID %>").click(function() {
            $(".region1").toggle();
            $(".region2").toggle();

            var ztDown = $("#<%=ZhuanTiDown.ClientID %>");
            if ($(this).attr("checked") == "checked") {
                ztDown.val(0);
                ztDown.attr("disabled", "disabled");
            } else {
                ztDown.removeAttr("disabled");
            }
        });

        if($("#<%=RegionID.ClientID %>").get(0).checked) {
            $(".region1").hide();
            $(".region2").show();
            var type = ".smartregion .r" + $("#<%=RegionTypeHid.ClientID %>").val();
            $(".smartregion p").removeClass("on");
            $(type).addClass("on");

            var ztDown = $("#<%=ZhuanTiDown.ClientID %>");
            ztDown.val(0);
            ztDown.attr("disabled", "disabled");
        }
        
        $(".smartregion p").live("click", function () {
            $(".smartregion p").removeClass("on");
            $(this).addClass("on");

            var type = $(this).attr("ref");
            $("#<%=RegionTypeHid.ClientID %>").val(type);
            var now2 = new Date(now);

            switch (type) {
                case "d":
                case "w":
                case "b":
                    now2.setMonth(now2.getMonth() + 1);
                    now2.setDate(now2.getDate() - 1);
                    break;
                case "m":
                    var now2 = new Date(now);
                    now2.setMonth(now2.getMonth() + 11);
                    break;
                case "j":
                case "y":
                    now2.setFullYear(now2.getFullYear() + 2);
                    break;

            }

            showdate2(now2);
        });

        <%--$(".rd").live("click", function () {
            $("#<%=RegionTypeHid.ClientID %>").val("d");
            var now2 = new Date(now);
            now2.setMonth(now2.getMonth() + 1);
            now2.setDate(now2.getDate() - 1);
            showdate2(now2);
        });

        $(".rw").live("click", function () {
            $("#<%=RegionTypeHid.ClientID %>").val("w");
            var now2 = new Date(now);
            now2.setMonth(now2.getMonth() + 1);
            now2.setDate(now2.getDate() - 1);
            showdate2(now2);
        });

        $(".rm").live("click", function () {
            $("#<%=RegionTypeHid.ClientID %>").val("m");
            var now2 = new Date(now);
            now2.setMonth(now2.getMonth() + 11);
            showdate2(now2);
        });

        $(".rj").live("click", function () {
            $("#<%=RegionTypeHid.ClientID %>").val("j");
            var now2 = new Date(now);
            now2.setFullYear(now2.getFullYear() + 2);
            showdate2(now2);
        });

        $(".ry").live("click", function () {
            $("#<%=RegionTypeHid.ClientID %>").val("y");
            var now2 = new Date(now);
            now2.setFullYear(now2.getFullYear() + 2);
            showdate2(now2);
        });
        
        $(".rb").live("click", function () {
            $("#<%=RegionTypeHid.ClientID %>").val("b");
            var now2 = new Date(now);
            now2.setMonth(now2.getMonth() + 1);
            now2.setDate(now2.getDate() - 1);
            showdate2(now2);
        });--%>

        var weekArr = new Array("周日", "周一", "周二", "周三", "周四", "周五", "周六");

        function showdate2(date) {
            var m = date.getMonth() + 1;
            var d = date.getDate();
            var v = date.getFullYear() + "-" + (m < 10 ? "0" + m : m) + "-" + (d < 10 ? "0" + d : d);
            $("#<%=ItemBuyDate2.ClientID %>").val(v);
        }

        function newdate(date, cur) {
            $(".smartitembuydate").html("<p class='pd'>后1天</p><p class='td'>当前</p><p class='nd'>前1天</p>");
            var nn = date.getFullYear() + "" + fullmonth(date.getMonth()+1) + "" + fullmonth(date.getDate());
            var cn = cur.getFullYear() + "" + fullmonth(cur.getMonth()+1) + "" + fullmonth(cur.getDate());
            if(nn == cn) $(".td").addClass("on");
            if(nn > cn) $(".nd").addClass("on");
            if(nn < cn) $(".pd").addClass("on");
            showdate(date);
        }

        function newdate(date) {
            $(".smartitembuydate").html("<p class='pd'>后1天</p><p class='td'>当前</p><p class='nd'>前1天</p>");
            $(".td").addClass("on");
            showdate(date);
        }

        $(".smartitembuydate p").live("click", function () {
            $(".smartitembuydate p").removeClass("on");
            $(this).addClass("on");
        });
        
        $(".pd").live("click", function () {
            now.setDate(now.getDate() - 1);
            showdate(now);
        });

        $(".nd").live("click", function () {
            now.setDate(now.getDate() + 1);
            showdate(now);
        });

        $(".td").live("click", function () {
            showdate(cur);
            var cd = cur.getFullYear() + "/" + fullmonth(cur.getMonth() + 1) + "/" + fullmonth(cur.getDate());
            now = new Date(cd);
        });

        function showdate(date) {
            var m = date.getMonth() + 1;
            var d = date.getDate();
            var v = date.getFullYear() + "-" + (m < 10 ? "0" + m : m) + "-" + (d < 10 ? "0" + d : d);
            $("#<%=ItemBuyDateHid.ClientID %>").val(v);
            $("#<%=ItemBuyDate.ClientID %>").val(v + " " + weekArr[date.getDay()]);
        }

        $("#<%=ItemBuyDate.ClientID %>").datepicker({
            showOtherMonths: true,
            selectOtherMonths: true,
            defaultDate: "<%=Session["TodayDate"].ToString() %>",
            onSelect: function (date, format) {
                now = new Date(date);
                cur = new Date(date);
                newdate(now);
            }
        });
        
        $("#<%=ItemBuyDate1.ClientID %>").datepicker({
            showOtherMonths: true,
            selectOtherMonths: true
        });

        $("#<%=ItemBuyDate2.ClientID %>").datepicker({
            showOtherMonths: true,
            selectOtherMonths: true
        });
    });

    //转账
    function showregion() {
        //alert(ztid);
        $("#zhuantiedit").show();
    }
    function regionclose() {
        $("#zhuantiedit").hide();
    }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<div id="zhuantiedit">
    <a href="javascript:regionclose();">×</a>
    <p>固定消费预览（<asp:Label ID="RegionCount" runat="server"></asp:Label>）</p>
    <asp:GridView ID="RegionList" runat="server" Width="100%" BorderWidth="1px"></asp:GridView>
    <asp:Button ID="Button2" runat="server" Text="确认提交" CssClass="btninput" Width="80px" OnClick="Buttom2_Click" />
</div>
<div id="content">
    <!--内容开始-->  
    <div class="maindiv userdiv">
        <div class="title"><img src="/Images/Others/add.png" alt="" title="" />&nbsp;&nbsp;智能添加消费</div>
        <table cellspacing="0" border="0" style="width:100%;" class="tableuser">
            <tr>
                <th></th>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <th style="width:14%;">消费分类</th>
                <td style="width:110px;">
                <asp:TextBox ID="ItemType" runat="server" MaxLength="20" ForeColor="#CCCCCC">支出</asp:TextBox><asp:HiddenField ID="ItemTypeHid" runat="server" Value="zc" />
                </td>
                <td>
                <div class="smartitem smartitemtype">
                    <p ref="zc" class="on">支出</p>
                    <p ref="sr">收入</p>
                    <p ref="jc">借出</p>
                    <p ref="hr">还入</p>
                    <p ref="jr">借入</p>
                    <p ref="hc">还出</p>
                </div>
                </td>
            </tr>
            <tr>
                <th>商品类别</th>
                <td class="typeselect">
                <asp:DropDownList ID="CategoryTypeDown" runat="server"></asp:DropDownList><asp:HiddenField ID="CategoryTypeID" runat="server" />
                </td>
                <td>
                <div class="smartitem smartcattypename">
                <asp:Repeater ID="CatTypeList" runat="server">
                    <ItemTemplate>
                        <p ref="<%# Eval("CategoryTypeID") %>"><%# Eval("CategoryTypeName") %></p>
                    </ItemTemplate>
                </asp:Repeater>
                </div>
                </td>
            </tr>
            <tr>
                <th>商品名称</th>
                <td><asp:TextBox ID="ItemName" runat="server" MaxLength="20" autocomplete="off"></asp:TextBox></td>
                <td><div class="smartitem smartitemname"></div></td>
            </tr>                        
            <tr>
                <th>商品价格</th>
                <td><asp:TextBox ID="ItemPrice" runat="server" onkeyup="getprice(this);" MaxLength="10"></asp:TextBox></td>
                <td><div class="smartitem smartitemprice"></div></td>
            </tr>
            <tr>
                <th>固定消费</th>
                <td colspan="2"><asp:CheckBox ID="RegionID" runat="server" CssClass="radioinput" /></td>
            </tr>
            <tr class="region1">
                <th>购买日期</th>
                <td><asp:TextBox ID="ItemBuyDate" runat="server" MaxLength="10"></asp:TextBox><asp:HiddenField ID="ItemBuyDateHid" runat="server" /></td>
                <td><div class="smartitem smartitembuydate" onselectstart="return false;"></div></td>
            </tr>
            <tr class="region2">
                <th></th>
                <td colspan="2"><div class="smartitem smartregion" style="padding-left:0;"><p class="rd" ref="d">每天</p><p class="rw" ref="w">每周</p><p class="rm on" ref="m">每月</p><p class="rj" ref="j">每季</p><p class="ry" ref="y">每年</p><p class="rb" ref="b">工作日</p></div><asp:HiddenField ID="RegionTypeHid" runat="server" Value="m" /></td>
            </tr>
            <tr class="region2">
                <th>购买日期</th>
                <td colspan="2"><asp:TextBox ID="ItemBuyDate1" runat="server" MaxLength="10"></asp:TextBox>&nbsp;-&nbsp;<asp:TextBox ID="ItemBuyDate2" runat="server" MaxLength="10"></asp:TextBox></td>
            </tr>
            <tr>
                <th>用户专题</th>
                <td class="typeselect"><asp:DropDownList ID="ZhuanTiDown" runat="server"></asp:DropDownList></td>
                <td></td>
            </tr>
            <tr>
                <th>支付钱包</th>
                <td><asp:DropDownList ID="CardDown" runat="server"></asp:DropDownList></td>
                <td></td>
            </tr>
            <tr>
                <th></th>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <th></th>
                <td><asp:Button ID="SubmitButtom" runat="server" Text="提交" OnClick="SubmitButtom_Click" CssClass="btninput" /></td>
                <td>&nbsp;&nbsp;&nbsp;<asp:Button ID="Button1" runat="server" Text="X2" OnClick="Buttom1_Click" CssClass="btninput" UseSubmitBehavior="false" Width="50px" /></td>
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
    $(function () {
        $("#menu table tr td").eq(4).addClass("on");
    });
</script>
</asp:Content>