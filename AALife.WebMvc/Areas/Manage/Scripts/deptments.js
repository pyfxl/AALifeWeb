//选择部门插入用户
function insert_deptmentsuser(id, dataItems, callback) {
    $.ajax({
        url: String.format($.const.webapi.deptmentsuser_id, id),
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(dataItems),
        success: callback
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
        success: callback
    });
}

//更新用户部门
function update_userdeptments(uid, checkedNodes, callback) {
    $.ajax({
        url: String.format($.const.webapi.userdeptments, uid),
        dataType: "json",
        type: "PUT",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(checkedNodes)
    });
}
