//用户界面插入岗位
function insert_usersposition(id, dataItems, callback) {
    $.ajax({
        url: String.format($.const.webapi.usersposition_id, id),
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(dataItems),
        success: callback
    });
}

//用户界面删除岗位
function delete_usersposition(id, dataItems, callback) {
    $.ajax({
        url: String.format($.const.webapi.usersposition_id, id),
        dataType: "json",
        type: "DELETE",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(dataItems),
        success: callback
    });
}

//更新用户主岗位
function update_usersmainposition(id, checkbox) {
    var pid = $(checkbox).data("id");
    $.ajax({
        url: "/api/v1/usersmainpositionapi",
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ "id": id, "pid": pid }),
        success: function () {
            kendoui_ajax_complete();
        },
        error: function (e) {
            kendoui_ajax_error(e);
        }
    });
}

//更新岗位负责人
function update_usersdeptmentleader(id, checkbox) {
    var pid = $(checkbox).data("id");
    $.ajax({
        url: "/api/v1/usersdeptmentleaderapi",
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ "id": id, "pid": pid }),
        success: function () {
            kendoui_ajax_complete();
        },
        error: function (e) {
            kendoui_ajax_error(e);
        }
    });
}
