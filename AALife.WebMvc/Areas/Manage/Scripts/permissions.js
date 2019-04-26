//角色权限
function update_permission_role(pid, id) {
    $.ajax({
        url: $.const.webapi.permissionsroleupdate,
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ "pid": pid, "id": id }),
        error: function (e) {
            kendoui_ajax_error(e);
        }
    });
}

//部门权限
function update_permission_deptment(pid, id) {
    $.ajax({
        url: $.const.webapi.permissionsdeptmentupdate,
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ "pid": pid, "id": id }),
        error: function (e) {
            kendoui_ajax_error(e);
        }
    });
}

//岗位权限
function update_permission_position(pid, id) {
    $.ajax({
        url: $.const.webapi.permissionspositionupdate,
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ "pid": pid, "id": id }),
        error: function (e) {
            kendoui_ajax_error(e);
        }
    });
}

