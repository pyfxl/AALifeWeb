function insert_update_userrole(id, dataItem) {
    $.ajax({
        url: String.format($.const.webapi.usersrole_id, id),
        dataType: "json",
        type: "PUT",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(dataItem),
        error: function (e) {
            kendoui_ajax_error(e);
        }
    });
}

//选择角色删除用户
function delete_rolesuser(id, dataItems, callback) {
    $.ajax({
        url: String.format($.const.webapi.rolesuser_id, id),
        dataType: "json",
        type: "DELETE",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(dataItems),
        success: callback,
        error: function (e) {
            kendoui_ajax_error(e);
        }
    });
}

//选择角色插入用户
function insert_rolesuser(id, dataItems, callback) {
    $.ajax({
        url: String.format($.const.webapi.rolesuser_id, id),
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(dataItems),
        success: callback,
        error: function (e) {
            kendoui_ajax_error(e);
        }
    });
}

function update_permission_roles(pid, rid) {
    $.ajax({
        url: $.const.webapi.permissionsupdate,
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ "pid": pid, "rid": rid }),
        error: function (e) {
            kendoui_ajax_error(e);
        }
    });
}

function update_permission_deptments(pid, rid) {
    $.ajax({
        url: $.const.webapi.permissionsdeptmentupdate,
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ "pid": pid, "rid": rid }),
        error: function (e) {
            kendoui_ajax_error(e);
        }
    });
}