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
