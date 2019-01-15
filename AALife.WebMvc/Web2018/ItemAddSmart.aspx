<%@ Page Title="添加消费" Language="C#" MasterPageFile="UserControl/MasterPage.master" AutoEventWireup="true" Inherits="AALife.WebMvc.Web2018.ItemAddSmart" Codebehind="ItemAddSmart.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="js" Runat="Server">
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
                url: "/Web2015/AutoSmartItemNameJson.aspx?term=" + catid,
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
                url: "/Web2015/AutoItemNameJson.aspx?term=" + encodeURIComponent($(this).val()),
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
        
        $("body").on("click", ".smartitemname p", function () {
            $("#<%=ItemName.ClientID %>").val($(this).text());
            $(".smartitemname p").removeClass("on");
            $(this).addClass("on");

            $("#<%=ItemPrice.ClientID %>").val("");
            $(".smartitemprice").html("");
            $(".smartitembuydate").html("");

            $.ajax({
                type: "post",
                url: "/Web2015/AutoSmartItemPriceJson.aspx?term=" + encodeURIComponent($(this).html()),
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

        $("body").on("click", ".smartitemprice p", function () {
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
            $(obj).html("<img src='/theme/images/ui-anim_basic_16x16.gif' alt='' title='' />");
        }

        var now = new Date("<%=Session["TodayDate"].ToString().Replace("-", "/") %>");
        var cur = new Date("<%=Session["TodayDate"].ToString().Replace("-", "/") %>");

        $("#<%=RegionID.ClientID %>").click(function() {
            $(".region1").toggle();
            $(".region2").toggle();

            <%--var ztDown = $("#<%=ZhuanTiDown.ClientID %>");
            if ($(this).prop("checked")) {
                ztDown.val(0);
                ztDown.attr("disabled", "disabled");
            } else {
                ztDown.removeAttr("disabled");
            }--%>
        });

        if($("#<%=RegionID.ClientID %>").get(0).checked) {
            $(".region1").hide();
            $(".region2").show();
            var type = ".smartregion .r" + $("#<%=RegionTypeHid.ClientID %>").val();
            $(".smartregion p").removeClass("on");
            $(type).addClass("on");

            <%--var ztDown = $("#<%=ZhuanTiDown.ClientID %>");
            ztDown.val(0);
            ztDown.attr("disabled", "disabled");--%>
        }
        
        $("body").on("click", ".smartregion p", function () {
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

        $("body").on("click", ".smartitembuydate p", function () {
            $(".smartitembuydate p").removeClass("on");
            $(this).addClass("on");
        });
        
        $("body").on("click", ".pd", function () {
            now.setDate(now.getDate() - 1);
            showdate(now);
        });

        $("body").on("click", ".nd", function () {
            now.setDate(now.getDate() + 1);
            showdate(now);
        });

        $("body").on("click", ".td", function () {
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
            changeMonth: true,
            changeYear: true,
            defaultDate: "<%=Session["TodayDate"].ToString() %>",
            onSelect: function (date, format) {
                now = new Date(date);
                cur = new Date(date);
                newdate(now);
            }
        });
        
        $("#<%=ItemBuyDate1.ClientID %>").datepicker({
            changeMonth: true,
            changeYear: true
        });

        $("#<%=ItemBuyDate2.ClientID %>").datepicker({
            changeMonth: true,
            changeYear: true
        });

        //固定消费预览
        var dialog = $("#dialog").dialog({
            autoOpen: false,
            width: 450,
            height: 300
        });
        dialog.parent().appendTo($("form:first"));

    });


</script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="content" Runat="Server">
<div class="r_title">
    <h1>添加消费</h1>
    <ul>
        <li><a href="javascript:history.go(-1);"><img src="/theme/images/dot_19.gif" title="返回" /> 返回</a></li>
    </ul>
</div>
<div id="dialog" title="固定消费(<%=RegionCount %>)">
    <asp:GridView ID="RegionList" runat="server" Width="100%" BorderWidth="1px"></asp:GridView>
    <asp:Button ID="Button2" runat="server" Text="确认提交" CssClass="btninput" Width="80px" OnClick="Buttom2_Click" />
</div>
<div id="r_content">
    <!--内容开始-->
    <div class="r_add">
        <table border="0" style="width:926px;" class="tableform">
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
                <td colspan="2"><asp:TextBox ID="ItemBuyDate1" runat="server" MaxLength="10"></asp:TextBox>&nbsp;-&nbsp;<asp:TextBox ID="ItemBuyDate2" runat="server"></asp:TextBox></td>
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
                <th>商品备注</th>
                <td><asp:TextBox ID="Remark" runat="server" MaxLength="100" autocomplete="off"></asp:TextBox></td>
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
                <td>&nbsp;&nbsp;&nbsp;<asp:Button ID="Button1" runat="server" Text="X2" OnClick="Buttom1_Click" CssClass="btninput btninput2" UseSubmitBehavior="false" /></td>
            </tr>
        </table>
    </div>
    <!--内容结束-->
</div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="end" Runat="Server">
<script type="text/javascript">
    $(function () {
        //设置当前菜单
        $(".menu_nav li").eq(1).addClass("cur");
        $("#menu_div .system_ul li").eq(1).addClass("cur");
    });
</script>
</asp:Content>