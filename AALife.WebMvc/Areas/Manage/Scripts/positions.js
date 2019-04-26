//选择岗位插入用户
function insert_positionsuser(id, dataItems, callback) {
    $.ajax({
        url: String.format($.const.webapi.positionsuser_id, id),
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

//选择岗位删除用户
function delete_positionsuser(id, dataItems, callback) {
    $.ajax({
        url: String.format($.const.webapi.positionsuser_id, id),
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

//选择部门插入岗位
function insert_deptmentsposition(id, dataItems, callback) {
    $.ajax({
        url: String.format($.const.webapi.deptmentsposition_id, id),
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

//选择部门删除岗位
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

//删除岗位
function delete_positions(id, dataItems, callback) {
    $.ajax({
        url: String.format($.const.webapi.positions_id, id),
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

//岗位授权
function insert_positionspermission(id, dataItems, callback) {
    $.ajax({
        url: String.format("/api/v1/positionspermissionapi?id={0}", id),
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