<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DevExpressUserControl.ascx.cs" Inherits="AALife.WebMvc.Web2018.Manage2019.DevExpressUserControl" %>
<link href="../../Content/dx.common.css" rel="stylesheet" />
<link href="../../Content/dx.light.css" rel="stylesheet" />
<script src="../../Scripts/dx.all.js"></script>
<script src="../../Scripts/devextreme-localization/dx.messages.zh.js"></script>
<script src="../../Scripts/dx.aspnet.data.js"></script>
<script>
    //常量
    const COLUMN_ZZL = "增长率(%)";
    const COLUMN_ZB = "占比(%)";

    //dev中文化
    DevExpress.localization.locale("zh");

    //判断是否空
    var isDefined = function(object) {
        return (object !== null) && (object !== undefined);
    };

</script>