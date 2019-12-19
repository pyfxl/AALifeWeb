
//重设大小
function resizeGrid() {
    try {
        //是否手机
        if ('ontouchstart' in document.documentElement) return;

        var grid = $("#grid");
        var contentArea = grid.find(".k-grid-content");

        var windowHeight = $(window).innerHeight();
        var gridHeight = grid.height();
        var contentHeight = contentArea.height();
        var offsetTop = grid.offset().top;

        var calculatedHeight = windowHeight - offsetTop - 15 - 2 - gridHeight;

        grid.height(gridHeight + calculatedHeight);
        contentArea.height(contentHeight + calculatedHeight);

    } catch (e) {
    }
}

//kendo datetime format
function kendoui_parameter_format(options) {
    if (options && options.filter && options.filter.filters && options.filter.filters.count != 0) {
        options.filter.filters.forEach(function (f) {
            if (f.operator && f.value != undefined) {
                if (typeof f.value.getMonth == 'function' && typeof f.value.getMonth() == 'number') {
                    f.value = kendo.toString(f.value, "yyyy/MM/dd HH:mm:ss");
                }
                //if (typeof f.value == 'boolean') {
                //    f.value = f.value ? "1" : "0";
                //}
                if (f.operator == 'in') {
                    //debugger;
                    var fits = [];
                    f.value.split(", ").forEach(function (m) {
                        var fit = {
                            field: f.field,
                            operator: "eq",
                            value: m
                        };
                        fits.push(fit);
                    });
                    options.filter.filters.push({ filters: fits, logic: "or" });
                }
            }
        });
    }
    return options;
}
