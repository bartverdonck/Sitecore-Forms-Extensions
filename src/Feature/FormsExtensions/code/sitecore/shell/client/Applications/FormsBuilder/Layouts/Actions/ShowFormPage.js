(function (speak) {
    var parentApp = window.parent.Sitecore.Speak.app.findApplication('EditActionSubAppRenderer');
    var designBoardApp = window.parent.Sitecore.Speak.app.findComponent('FormDesignBoard');

    var getPages = function () {
        var fields = designBoardApp.getFieldsData();
        return _.reduce(fields,
            function (memo, item) {
                if (item && item.model && item.model.hasOwnProperty('templateId') && item.model.templateId === '{CFEE7B51-8505-45CE-B843-9358F827DF87}') {
                    memo.push({
                        itemId: item.itemId,
                        name: item.model.name
                    });
                }
                return memo;
            },
            [
                {
                    itemId: '',
                    name: ''
                }
            ],
            this);
    };

    speak.pageCode(["underscore"],
        function (_) {
            return {
                initialized: function () {
                    this.on({ "loaded": this.loadDone }, this);
                    this.Fields = getPages();
                    this.SelectPageForm.FormPageId.on("change:SelectedItem", this.validate, this);

                    if (parentApp) {
                        parentApp.loadDone(this, this.HeaderTitle.Text, this.HeaderSubTitle.Text);
                    }
                },
                validate: function () {
                    parentApp.setSelectability(this, false);
                    if (this.SelectPageForm.FormPageId.SelectedValue != "") {
                        parentApp.setSelectability(this, true);
                    }
                },
                setFormPageIdData: function (listComponent, data, currentValue) {
                    var items = data.slice(0);
                    if (currentValue && !_.findWhere(items, { itemId: currentValue })) {
                        var currentField = {
                            itemId: currentValue,
                            name: currentValue + " - value not in the selection list"
                        };
                        items.splice(1, 0, currentField);
                        listComponent.DynamicData = items;
                        $(listComponent.el).find('option').eq(1).css("font-style", "italic");
                    } else {
                        listComponent.DynamicData = items;
                        listComponent.SelectedValue = currentValue;
                    }
                },
                loadDone: function (parameters) {
                    this.Parameters = parameters || {};
                    this.setFormPageIdData(this.SelectPageForm.FormPageId, getPages(), this.Parameters["formPageId"]);
                    this.validate();
                },
                getData: function () {
                    this.Parameters["formPageId"] = this.SelectPageForm.FormPageId.SelectedValue;
                    return this.Parameters;
                }
            };
        });
})(Sitecore.Speak);
