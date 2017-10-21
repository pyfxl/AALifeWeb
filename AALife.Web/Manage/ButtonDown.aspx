<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ButtonDown.aspx.cs" Inherits="Manage_ButtonDown" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>ButtonDown</title>
    
    <!-- bootstrap & fontawesome -->
    <link rel="stylesheet" href="../ace/assets/css/bootstrap.min.css" />
    <link rel="stylesheet" href="../ace/assets/font-awesome/4.5.0/css/font-awesome.min.css" />

    <script src="../ace/assets/js/jquery-2.1.4.min.js"></script>

    <!-- kendo ui 以ace为主kendo放前面 -->
    <link href="../theme/kendoui/kendo.common.min.css" rel="stylesheet" />
    <link href="../theme/kendoui/kendo.default.min.css" rel="stylesheet" />
    <link href="../theme/kendoui/kendo.default.mobile.min.css" rel="stylesheet" />
    <script src="../theme/kendoui/kendo.all.min.js"></script>
    <script src="../theme/kendoui/cultures/kendo.culture.zh-CN.min.js"></script>
    <script src="../theme/kendoui/messages/kendo.messages.zh-CN.min.js"></script>
    
    <script src="../ace/assets/js/bootstrap.min.js"></script>

    <!-- js -->
    <script src="../common/moment.js"></script>
    <script src="../common/fn-date.js"></script>

    <script src="common/buttondown-ui.js"></script>
</head>
<body>
    <div class="container">
        <div class="row">
            <div class="col-xs-12">
                <div class="overflow-y">
                    <form class="form-inline" style="width: 875px;">
                        <div class="form-group">
                            <ul class="km-widget km-buttongroup k-widget k-button-group" id="buttondown">
                            </ul>
                        </div>
                    </form>
                </div>
            </div><!-- /.col -->
        </div><!-- /.row -->
    </div>
    
    <script>

        $(document).ready(function () {

            //自定义按钮组
            $("#buttondown").kendoButtomDown({
                dataSource: [
                    { title: "全部", code: "b_all" },
                    { title: "本年", code: "b_year", sub: true },
                    { title: "本季", code: "b_quarter", sub: true },
                    { title: "本月", code: "b_month", sub: true },
                    { title: "本周", code: "b_week", sub: true },
                    { title: "本日", code: "b_day", sub: true }
                ],
                callback: function (startDate, endDate) {
                    //alert(start + "," + end);
                    //start.value(startDate);
                    //end.value(endDate);
                    //queryList();
                }
            });

        });
    </script>

</body>
</html>
