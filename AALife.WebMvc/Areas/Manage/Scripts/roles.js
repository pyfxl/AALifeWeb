function insert_update_userrole(id, dataItem) {
    $.ajax({
        url: String.format($.const.webapi.userroles, id),
        dataType: "json",
        type: "PUT",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(dataItem)
    });
}

function delete_userrole(id, dataItems, callback) {
    $.ajax({
        url: String.format($.const.webapi.userroles, id),
        dataType: "json",
        type: "DELETE",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(dataItems),
        success: callback
    });
}

function insert_userrole(id, dataItems, callback) {
    $.ajax({
        url: String.format($.const.webapi.userroles, id),
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(dataItems),
        success: callback
    });
}

function update_permission(pid, rid) {
    $.ajax({
        url: $.const.webapi.permissionsupdate,
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ "pid": pid, "rid": rid })
    });
}

function update_permission_deptment(pid, rid) {
    $.ajax({
        url: $.const.webapi.permissionsdeptmentupdate,
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ "pid": pid, "rid": rid })
    });
}