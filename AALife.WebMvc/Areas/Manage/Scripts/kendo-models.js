$.kendo = {
    deptment: {
        fields: {
            Id: { type: "string", editable: false, defaultValue: kendo.guid() },
            Name: { type: "string", validation: { required: true, validationMessage: "必填项！" } },
            Code: { type: "string", validation: { required: true, validationMessage: "必填项！" } },
            ParentId: { type: "string", nullable: true, defaultValue: null },
            Parent: { type: "object", nullable: true, defaultValue: null },
            Notes: { type: "string" }
        },
        columns: [
            {
                selectable: true,
                width: 100
            },
            {
                field: "Name",
                title: "组织名称",
                width: 200
            },
            {
                template: "#= data.Parent ? Parent.Name : '' #",
                field: "Parent",
                title: "上级组织",
                width: 200,
                editor: function (container, options) {
                    $('<input name="' + options.field + '"/>')
                        .appendTo(container)
                        .kendoDropDownTree({
                            dataTextField: "Name",
                            height: "200px",
                            loadOnDemand: true,
                            dataSource: {
                                transport: {
                                    read: {
                                        url: $.const.webapi.deptmentstree
                                    }
                                },
                                schema: {
                                    model: {
                                        id: "Id",
                                        parentId: "ParentId"
                                    }
                                }
                            }
                        });
                }
            },
            {
                field: "Code",
                title: "组织编码",
                width: 100
            },
            {
                field: "Notes",
                title: "描述",
                width: 100
            }
        ]
    },
    position: {
        fields: {
            Id: { type: "string", editable: false, defaultValue: kendo.guid() },
            ParentId: { type: "string", nullable: true, defaultValue: null },
            Parent: { type: "object", nullable: true, defaultValue: null },
            TitleId: { type: "string" },
            Title: { type: "object" },
            DeptmentId: { type: "string", nullable: true, defaultValue: null },
            Code: { type: "string", validation: { required: true, validationMessage: "必填项！" } },
            Name: { type: "string", validation: { required: true, validationMessage: "必填项！" } },
            Notes: { type: "string" }
        },
        columns: [
            {
                selectable: true,
                width: 100
            },
            {
                field: "Name",
                title: "岗位名称",
                width: 200
            },
            {
                template: "#= data.Parent ? Parent.Name : '' #",
                field: "Parent",
                title: "上级岗位",
                width: 200,
                editor: function (container, options) {
                    $('<input name="' + options.field + '"/>')
                        .appendTo(container)
                        .kendoDropDownTree({
                            template: "#= item.IsPosition ? '<i class=\"fa fa-sticky-note-o\"></i> ' : '' ##= item.Name #",
                            dataTextField: "Name",
                            height: "200px",
                            loadOnDemand: true,
                            dataSource: {
                                transport: {
                                    read: {
                                        url: $.const.webapi.deptmentspositiontree
                                    }
                                },
                                schema: {
                                    model: {
                                        id: "Id",
                                        parentId: "ParentId"
                                    }
                                }
                            },
                            select: function (e) {
                                var dataItem = e.sender.dataItem(e.node);
                                if (!dataItem.IsPosition) {
                                    e.preventDefault();
                                    alert("所选不是岗位！");
                                }
                            }
                        });
                }
            },
            {
                template: "#= data.Title ? Title.Name : '' #",
                field: "Title",
                title: "职位",
                width: 100,
                editor: function (container, options) {
                    $('<input name="' + options.field + '"/>')
                        .appendTo(container)
                        .kendoDropDownList({
                            dataTextField: "Name",
                            dataValueField: "Id",
                            dataSource: {
                                transport: {
                                    read: {
                                        url: "/api/v1/titlesapi"
                                    }
                                }
                            }
                        });
                }
            },
            {
                field: "Code",
                title: "岗位编码",
                width: 100
            },
            {
                field: "Notes",
                title: "描述",
                width: 100
            }
        ]
    },
    user: {
        fields: {
            Id: { type: "string" },
            UserName: { type: "string" },
            UserCode: { type: "string" },
            FirstName: { type: "string" },
            Position: { type: "object" },
            CreateDate: { type: "date" },
            Remark: { type: "string" }
        },
        columns: [
            {
                selectable: true,
                width: 100
            },
            {
                field: "Id",
                title: "编号",
                width: 100,
                hidden: true
            },
            {
                field: "FirstName",
                title: "姓名",
                width: 100
            },
            {
                field: "UserName",
                title: "用户名",
                width: 100
            },
            {
                field: "UserCode",
                title: "用户编码",
                width: 100
            },
            {
                template: "#= data.Position ? Position.Name : '' #",
                field: "Position",
                title: "所属岗位",
                width: 200
            },
            {
                field: "Remark",
                title: "备注",
                width: 100
            }
        ]
    }
}