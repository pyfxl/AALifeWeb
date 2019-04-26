//选择部门插入用户
function insert_deptmentsuser(id, dataItems, callback) {
    $.ajax({
        url: String.format($.const.webapi.deptmentsuser_id, id),
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

//选择部门删除用户
function delete_deptmentsuser(id, dataItems, callback) {
    $.ajax({
        url: String.format($.const.webapi.deptmentsuser_id, id),
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

//更新用户部门
function update_userdeptments(uid, checkedNodes, callback) {
    $.ajax({
        url: String.format($.const.webapi.usersdeptment, uid),
        dataType: "json",
        type: "PUT",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(checkedNodes),
        error: function (e) {
            kendoui_ajax_error(e);
        }
    });
}

//部门岗位删除
function delete_deptmentsposition(id, dataItems, callback) {
    $.ajax({
        url: String.format($.const.webapi.deptmentsposition_id, id),
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

//部门删除
function delete_deptments(dataItems, callback) {
    $.ajax({
        url: $.const.webapi.deptments,
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

//部门树路径
function get_deptmentstreepath(id, callback) {
    $.ajax({
        url: String.format("/api/v1/deptmenttreepathapi?id={0}", id),
        dataType: "json",
        type: "GET",
        contentType: "application/json; charset=utf-8",
        success: callback,
        error: function (e) {
            kendoui_ajax_error(e);
        }
    });
}

//组织授权
function insert_deptmentspermission(id, dataItems, callback) {
    $.ajax({
        url: String.format("/api/v1/deptmentspermissionapi?id={0}", id),
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