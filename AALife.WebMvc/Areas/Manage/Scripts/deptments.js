function insert_update_userrole(id, dataItem) {
    $.ajax({
        url: String.format($.const.webapi.userroles, id),
        dataType: "json",
        type: "PUT",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(dataItem)
    });
}

function delete_userdeptment(id, dataItems, callback) {
    $.ajax({
        url: String.format($.const.webapi.userdeptments_id, id),
        dataType: "json",
        type: "DELETE",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(dataItems),
        success: callback
    });
}

function insert_userdeptment(id, dataItems, callback) {
    $.ajax({
        url: String.format($.const.webapi.userdeptments_id, id),
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(dataItems),
        success: callback
    });
}

function userdeptments_update(uid, checkedNodes, callback) {
    $.ajax({
        url: String.format("/api/v1/userdeptmentsupdateapi/{0}", uid),
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(checkedNodes)
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