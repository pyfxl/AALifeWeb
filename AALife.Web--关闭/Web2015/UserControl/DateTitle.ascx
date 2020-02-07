<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DateTitle.ascx.cs" Inherits="UserControl_DateTitle" %>
<table cellspacing="0" border="0" style="width:100%;" class="tabledate">
    <tr>
        <td style="width:25%;">&nbsp;</td>
        <td style="width:25%;"><a href="javascript:void(0)" id="datepicker"><strong>日期：</strong><%=Convert.ToDateTime(Session["TodayDate"]).ToString("yyyy年MM月") %>&nbsp;&nbsp;<img src="/Images/Others/calendar.png" alt="" title="" /></a></td>
        <td style="width:25%;"><strong>显示：</strong><input type="radio" name="view" id="radio1" value="列表" checked="checked" />列表&nbsp;&nbsp;<input type="radio" name="view" id="radio2" value="图表" />图表</td>
        <td style="width:25%;">&nbsp;</td>
    </tr>
</table>