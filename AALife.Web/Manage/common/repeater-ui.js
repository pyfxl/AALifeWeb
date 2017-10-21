(function () {
    var kendo = window.kendo,
        ui = kendo.ui,
        Widget = ui.Widget,

    CHANGE = "change";

    var Repeater = Widget.extend({
        init: function (element, options) {
            var that = this;

            kendo.ui.Widget.fn.init.call(that, element, options);

            //that.template = kendo.template(that.options.template || "<p><strong>#= data #</strong></p>");
            that.template = kendo.template(that._templates.main);
            
            that._dataSource();
        },
        options: {
            name: "Repeater",
            autoBind: true,
            template: ""
        },
        refresh: function () {
            var that = this,
                view = that.dataSource.view(),
                html = kendo.render(that.template, view);

            that.element.html(html);
        },
        _dataSource: function () {
            var that = this;

            // returns the datasource OR creates one if using array or configuration object
            that.dataSource = kendo.data.DataSource.create(that.options.dataSource);

            // bind to the change event to refresh the widget
            that.dataSource.bind(CHANGE, function () {
                that.refresh();
            });

            if (that.options.autoBind) {
                that.dataSource.fetch();
            }
        },
        _templates: {
            main: '<div class="btn-group k-button km-button">' +
                        '<button class="btn btn-link">#= data.main #</button>' +
                        '# if (data.sub) {#' +
                        '<button data-toggle="dropdown" class="btn btn-link dropdown-toggle" aria-expanded="false">' +
                            '<span class="ace-icon fa fa-caret-down icon-only"></span>' +
                        '</button>' +
                        '<ul class="dropdown-menu">' +
                            '# for (var i = 0; i < data.sub.length; i++) {#' +
                            '<li><button class="btn btn-link">#= data.sub[i] #</button></li>' +
                            '# } #' +
                        '</ul>' +
                        '# } #' +
                    '</div>'
        }
    });

    ui.plugin(Repeater);

})(jQuery);