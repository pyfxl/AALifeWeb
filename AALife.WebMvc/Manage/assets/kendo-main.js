
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
