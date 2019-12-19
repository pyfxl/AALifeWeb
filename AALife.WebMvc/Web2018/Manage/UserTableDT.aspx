<%@ Page Title="后台管理 | 用户列表DT" Language="C#" MasterPageFile="~/Web2018/Manage/MasterPage.master" AutoEventWireup="true" Inherits="Manage_UserTableDT" Codebehind="UserTableDT.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="row">
        <div class="col-xs-12">
            <div class="form-inline">
                <div class="form-group">
                    <label>来自</label>
                    <select id="UserFrom" class="multiselect" multiple="multiple" size="1">
                    </select>
                </div>
                <div class="form-group">
                    <label style="float: left;line-height: 29px;">注册日期</label>
                    <div class="col-xs-8">
                        <div class="input-daterange input-group">
                            <input type="text" class="form-control" name="start" />
                            <span class="input-group-addon">
                                <i class="fa fa-exchange"></i>
                            </span>
                            <input type="text" class="form-control" name="end" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="space-6"></div>

    <div class="row">
        <div class="col-xs-12">
            <table id="dynamic-table" class="table table-striped table-bordered table-hover">
                <thead>
                    <tr>
                        <th>编号</th>
                        <th>用户名</th>
                        <th>密码</th>
                        <th>昵称</th>
                        <th>邮箱</th>
                        <th>样式</th>
                        <th>来自</th>
                        <th>注册日期</th>
                        <th>修改日期</th>
                        <th>工作日</th>
                        <th>预警率</th>
                        <th>同步否</th>
                        <th>更新否</th>
                    </tr>
                </thead>
            </table>
        </div><!-- /.col -->
    </div><!-- /.row -->
    
    <!-- inline scripts related to this page -->
    <script type="text/javascript">
        jQuery(function ($) {

            //查询对象
            var query = { startDate: "", endDate: "", keySearch: "", filter: [], sort: [], skip: 0, take: 30 };

            var tablesHeight = 400;
            
            //初始化
            calcTableHeight();
            init();
            setDate();

            //initiate dataTables plugin
            var myTable =
            $('#dynamic-table')
            .wrap("<div class='dataTables_borderWrap' />")   //if you are applying horizontal scrolling (sScrollX)
            .DataTable({
                "autoWidth": true,
                "dom": "rt<'row'<'col-xs-6'i><'col-xs-6'p>>",
                "language": {
                    url: "//cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Chinese.json"
                },
                "processing": false,
                "serverSide": true,
                "ajax": {
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: '/Web2018/api/UserTable.asmx/GetUserTable',
                    data: function (d) {
                        query.skip = d.start;
                        query.take = d.length;
                        query.sort = getSort(d);

                        return JSON.stringify({ "query": query });
                    },
                    dataType: "json",
                    dataFilter: function (data) {
                        var json = jQuery.parseJSON(data);
                        json.recordsTotal = json.d.total;
                        json.recordsFiltered = json.d.total;
                        json.data = json.d.rows;

                        return JSON.stringify(json); // return JSON string
                    }
                },
                "columns": [
                    { data: 'UserID', width: '80px' },
                    { data: 'UserName', width: '100px' },
                    { data: 'UserPassword', width: '100px' },
                    { data: 'UserNickName', width: '100px' },
                    { data: 'UserEmail', width: '150px' },
                    { data: 'UserTheme', width: '90px' },
                    { data: 'UserFromName', width: '120px' },
                    { data: 'CreateDate', width: '150px', render: function (v) { return moment(v).format(moment_format_full) } },
                    { data: 'ModifyDate', width: '150px', render: function (v) { return moment(v).format(moment_format_full) } },
                    { data: 'UserWorkDayName', width: '110px' },
                    { data: 'CategoryRate', width: '80px' },
                    { data: 'Synchronize', width: '80px' },
                    { data: 'IsUpdate', width: '80px' }
                ],
                "displayLength": 30,
                "scrollY": tablesHeight,
                "scrollX": true,
            });

            //日期期间
            $('.input-daterange').datepicker({
                autoclose: true,
                language: 'zh-CN',
                format: 'yyyy-mm-dd'
            }).on("changeDate", function (e) {
                queryList();
            });

            //多选下拉
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                url: "/api/UserFrom.asmx/GetUserFrom", success: function (msg) {
                    $.each(msg.d.rows, function (i, v) {
                        $("#UserFrom").append($("<option/>").text(v.UserFromName).val(v.UserFrom));
                    });
                    
                    //js组件
                    $('#UserFrom').multiselect({
                        enableFiltering: false,
                        enableHTML: true,
                        includeSelectAllOption: false,
                        maxHeight: 300,
                        nonSelectedText: "请选择",
                        nSelectedText: "选择",
                        allSelectedText: "所有选择",
                        selectAllText: "全选",
                        buttonClass: 'btn btn-white btn-primary',
                        templates: {
                            button: '<button type="button" class="multiselect dropdown-toggle" data-toggle="dropdown"><span class="multiselect-selected-text"></span> &nbsp;<b class="fa fa-caret-down"></b></button>',
                            ul: '<ul class="multiselect-container dropdown-menu"></ul>',
                            filter: '<li class="multiselect-item filter"><div class="input-group"><span class="input-group-addon"><i class="fa fa-search"></i></span><input class="form-control multiselect-search" type="text"></div></li>',
                            filterClearBtn: '<span class="input-group-btn"><button class="btn btn-default btn-white btn-grey multiselect-clear-filter" type="button"><i class="fa fa-times-circle red2"></i></button></span>',
                            li: '<li><a tabindex="0"><label></label></a></li>',
                            divider: '<li class="multiselect-item divider"></li>',
                            liGroup: '<li class="multiselect-item multiselect-group"><label></label></li>'
                        },
                        onChange: function (option, checked, select) {
                            getSelected();
                            queryList();                            
                        }
                    });

                }
            });

            //取所有选中
            function getSelected() {
                var sel = [];
                $("#UserFrom").find("option:selected").each(function () {
                    sel.push('UserFrom = "' + $(this).val() + '"');
                });
                query.filter = sel;
            }

            //查询列表
            function queryList() {
                setDate();
                myTable.page(0);
                myTable.draw(false);
            }
            
            //设置日期
            function setDate() {
                var _start = $("input[name=start]").val();
                //if ($.isEmptyObject(_start)) _start = today_date();
                query.startDate = _start + " 00:00:00";

                var _end = $("input[name=end]").val();
                //if ($.isEmptyObject(_end)) _end = today_date();
                query.endDate = _end + " 23:59:59";
            }

            //取排序
            function getSort(d) {
                var col_index = d.order[0].column;
                var field = d.columns[col_index].data;
                var dir = d.order[0].dir;
                return [{ "field": field, "dir": dir }];
            }

            //初始化
            function init() {
                $("input[name=start]").val(today_date());
                $("input[name=end]").val(today_date());
            }

            //计算tables高度
            function calcTableHeight() {
                var tables = $("#dynamic-table");

                var windowHeight = $(window).innerHeight();
                var offsetTop = tables.offset().top;

                tablesHeight = windowHeight - offsetTop - 35 - 62 - 15 - 2;
            }

            navbarActive(0);
        });
    </script>
                
</asp:Content>

