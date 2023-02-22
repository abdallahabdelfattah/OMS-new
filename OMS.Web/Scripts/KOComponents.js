ko.components.register('paged-grid', {
    viewModel: function(params) {
        var self = this;
        self.GridCtrl = params.GridCtrl;
       
    },
    template: { element: 'grid-template' }
});

ko.components.register('rich-editor', {
    viewModel: function (params) {
        var self = this;
        self.Value = params.Value;

    },
    template: { element: 'richeditor-template' }
});

